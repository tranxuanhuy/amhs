using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace amhs
{
    public class NetworkHeartbeat
    {
        private static object lockObj = new object();

        public bool Running { get; private set; }
        public int PingTimeout { get; private set; }
        public int HeartbeatDelay { get; private set; }
        public IPAddress[] EndPoints { get; private set; }
        public string[] NodeNames { get; private set; }

        private int[] NodeTimeout;

        public int Count => EndPoints.Length;
        public PingReply[] PingResults { get; private set; }
        private Ping[] Pings { get; set; }

        public NetworkHeartbeat(IEnumerable<IPAddress> hosts, int pingTimeout, int heartbeatDelay, IEnumerable<Node> listNode)
        {
            PingTimeout = pingTimeout;
            HeartbeatDelay = heartbeatDelay;

            EndPoints = hosts.ToArray();
            NodeNames = listNode.Select(o => o.Name).ToArray();
            NodeTimeout = listNode.Select(o => o.Timeout).ToArray();
            PingResults = new PingReply[EndPoints.Length];
            Pings = EndPoints.Select(h => new Ping()).ToArray();
        }

        public async void Start()
        {
            if (!Running)
            {
                try
                {
                    Debug.WriteLine("Heartbeat : starting ...");

                    int[] pingRetryDownCount = new int[Count];
                    // set up the tasks
                    var chrono = new Stopwatch();
                    var tasks = new Task<PingReply>[Count];

                    Running = true;

                    while (Running)
                    {
                        // set up and run async ping tasks                 
                        OnPulseStarted(DateTime.Now, chrono.Elapsed);
                        chrono.Restart();
                        for (int i = 0; i < Count; i++)
                        {
                            tasks[i] = PingAndUpdateAsync(Pings[i], EndPoints[i], i);
                        }
                        await Task.WhenAll(tasks);

                        for (int i = 0; i < tasks.Length; i++)
                        {
                            var pingResult = tasks[i].Result;

                            if (pingResult != null)
                            {
                                if (PingResults[i] == null)
                                {
                                    if (pingResult.Status == IPStatus.Success)
                                        OnPingUp(i);
                                }
                                else if (pingResult.Status != PingResults[i].Status)
                                {
                                    if (pingResult.Status == IPStatus.Success && pingRetryDownCount[i]> Properties.Settings.Default.PingRetryBeforeDown)
                                        OnPingUp(i);
                                    else if (PingResults[i].Status == IPStatus.Success)
                                        pingRetryDownCount[i] = 0;
                                }
                                else if ((pingResult.Status == PingResults[i].Status)&& pingResult.Status!= IPStatus.Success)
                                {
                                    pingRetryDownCount[i]++;
                                    if (pingRetryDownCount[i]== Properties.Settings.Default.PingRetryBeforeDown)
                                    {
                                        OnPingDown(i);
                                    }
                                }
                            }
                            else
                            {
                                if (PingResults[i] != null && PingResults[i].Status == IPStatus.Success)
                                    OnPingUp(i);
                            }

                            PingResults[i] = tasks[i].Result;
                            Debug.WriteLine("> Ping [" + PingResults[i].Status.ToString().ToUpper() + "] at " + EndPoints[i] + " in " + PingResults[i].RoundtripTime + " ms");
                        }

                        OnPulseEnded(DateTime.Now, chrono.Elapsed);

                        // heartbeat delay
                        var delay = Math.Max(0, HeartbeatDelay - (int)chrono.ElapsedMilliseconds);
                        await Task.Delay(delay);
                    }
                    Debug.Write("Heartbeat : stopped");
                }
                catch (Exception)
                {
                    Debug.Write("Heartbeat : stopped after error");
                    Running = false;
                    throw;
                }
            }
            else
            {
                Debug.WriteLine("Heartbeat : already started ...");
            }
        }

        public void Stop()
        {
            Debug.WriteLine("Heartbeat : stopping ...");
            Running = false;
        }

        private async Task<PingReply> PingAndUpdateAsync(Ping ping, IPAddress epIP, int epIndex)
        {
            try
            {
                //return await ping.SendPingAsync(epIP, PingTimeout);
                //neu pingtimeout cua node ko set (=0) thi lay value cua system ping timeout
                return await ping.SendPingAsync(epIP, NodeTimeout[epIndex]==0?PingTimeout: NodeTimeout[epIndex]);
            }
            catch (Exception ex)
            {
                Debug.Write("-[" + epIP + "] : error in SendPing()");
                OnPingError(epIndex, ex);
                return null;
            }
        }

        // Event on ping errors
        public event EventHandler<PingErrorEventArgs> PingError;
        public class PingErrorEventArgs : EventArgs
        {
            public int EndPointIndex { get; private set; }
            public Exception InnerException { get; private set; }

            public PingErrorEventArgs(int epIndex, Exception ex)
            {
                EndPointIndex = epIndex;
                InnerException = ex;
            }
        }
        private void OnPingError(int epIndex, Exception ex) => PingError?.Invoke(this, new PingErrorEventArgs(epIndex, ex));

        // Event on ping Down
        public event EventHandler<int> PingDown;
        private void OnPingDown(int epIndex)
        {
            //Debug.WriteLine("# Ping [DOWN] at " + EndPoints[epIndex]);
            //            String data = String.Format("{0,-10} {1,-20} {2,-20} {3}",
            //DateTime.Now.ToString("HH:mm:ss"), "DOWN", EndPoints[epIndex], NodeNames[epIndex]);

            String data = DateTime.Now.ToString("HH:mm:ss") + "\t" + "DOWN" + "\t" + EndPoints[epIndex] + "\t" + NodeNames[epIndex];
            Debug.WriteLine(data);

            //When Date change create new file, append log
            var sFileName = Path.Combine(Directory.GetCurrentDirectory(), "log", DateTime.Today.ToString("yyyy_MM_dd") + ".log");
            using (StreamWriter w = File.AppendText(sFileName))
            {
                
                w.WriteLine(data);
            }

            PingDown?.Invoke(this, epIndex);
        }

        // Event on ping Up
        public event EventHandler<int> PingUp;
        private void OnPingUp(int epIndex)
        {
            //Debug.WriteLine("# Ping [UP] at " + EndPoints[epIndex]);
            //            String data = String.Format("{0,-10} {1,-20} {2,-20} {3}",
            //DateTime.Now.ToString("HH:mm:ss"), "UP", EndPoints[epIndex], NodeNames[epIndex]);
            String data = DateTime.Now.ToString("HH:mm:ss") + "\t" + "UP" + "\t" + EndPoints[epIndex] + "\t" + NodeNames[epIndex];
            Debug.WriteLine(data);

            //When Date change create new file, append log
            var sFileName = Path.Combine(Directory.GetCurrentDirectory(), "log", DateTime.Today.ToString("yyyy_MM_dd") + ".log")  ;
            using (StreamWriter w = File.AppendText(sFileName))
            {
                w.WriteLine(data);
            }

            PingUp?.Invoke(this, epIndex);
        }

        // Event on pulse started
        public event EventHandler<PulseEventArgs> PulseStarted;
        public class PulseEventArgs : EventArgs
        {
            public DateTime TimeStamp { get; private set; }
            public TimeSpan Delay { get; private set; }

            public PulseEventArgs(DateTime date, TimeSpan delay)
            {
                TimeStamp = date;
                Delay = delay;
            }
        }
        private void OnPulseStarted(DateTime date, TimeSpan delay)
        {
            Debug.WriteLine("# Heartbeat [PULSE START] after " + (int)delay.TotalMilliseconds + " ms");
            PulseStarted?.Invoke(this, new PulseEventArgs(date, delay));
        }

        // Event on pulse ended
        public event EventHandler<PulseEventArgs> PulseEnded;
        private void OnPulseEnded(DateTime date, TimeSpan delay)
        {
            PulseEnded?.Invoke(this, new PulseEventArgs(date, delay));
            Debug.WriteLine("# Heartbeat [PULSE END] after " + (int)delay.TotalMilliseconds + " ms");
        }
    }
}
