﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace amhs
{
    class PingHelper
    {
        public static string[] addresses = { "microsoft.com", "yahoo.com", "google.com" };
        static void PingListAsync(string[] args)
        {
            List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();
            foreach (var address in addresses)
            {
                pingTasks.Add(PingAsync(address));
            }

            //Wait for all the tasks to complete
            Task.WaitAll(pingTasks.ToArray());

            //Now you can iterate over your list of pingTasks
            foreach (var pingTask in pingTasks)
            {
                //pingTask.Result is whatever type T was declared in PingAsync
                Console.WriteLine(pingTask.Result.RoundtripTime);
            }
            Console.ReadLine();
        }

        static Task<PingReply> PingAsync(string address)
        {
            var tcs = new TaskCompletionSource<PingReply>();
            Ping ping = new Ping();
            ping.PingCompleted += (obj, sender) =>
            {
                tcs.SetResult(sender.Reply);
            };
            ping.SendAsync(address, new object());
            return tcs.Task;
        }
    }
}
