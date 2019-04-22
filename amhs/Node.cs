using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace amhs
{
   public class Node
    {
        private int ID;
        private   string      name;
  private   string      remark;
  private   Point       location;
  private   string   iPAddress;
  private   bool        alarmEnable;
  private   int       timeout;
        private string  addressName;
        private string  soundFilePath;
        private string  remoteConnectUser;
        private string  remoteConnectPass;

        public string Name { get => name; set => name = value; }
        public string Remark { get => remark; set => remark = value; }
        public Point Location { get => location; set => location = value; }
        public string IPAddress { get => iPAddress; set => iPAddress = value; }
        public bool AlarmEnable { get => alarmEnable; set => alarmEnable = value; }
        public int Timeout { get => timeout; set => timeout = value; }
        public string AlarmFilename { get => soundFilePath; set => soundFilePath = value; }
        public string RemoteConnectUser { get => remoteConnectUser; set => remoteConnectUser = value; }
        public string RemoteConnectPass { get => remoteConnectPass; set => remoteConnectPass = value; }
        public string AddressName { get => addressName; set => addressName = value; }
        public int ID1 { get => ID; set => ID = value; }

        public static Node FromTxt(string txtLine)
        {
            string[] values = txtLine.Split('\t');
            Node dailyValues = new Node();

            dailyValues.ID= int.Parse(values[0]);
            dailyValues.name = values[1];
            dailyValues.remark = values[2];
            dailyValues.iPAddress = values[4];
            dailyValues.addressName = values[7];
            dailyValues.soundFilePath = values[8];
            dailyValues.RemoteConnectUser = values[9];
            dailyValues.RemoteConnectPass = values[10];
            dailyValues.alarmEnable = true;

            return dailyValues;
        }
    }
}
