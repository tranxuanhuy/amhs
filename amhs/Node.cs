﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace amhs
{
    class Node
    {
  private   string      name;
  private   string      remark;
  private   Point       position;
  private   string   iPAddress;
  private   bool        alarmEnable;
  private   int       timeout;
        private string  addressName;
        private string  soundFilePath;
        private string  remoteConnectUser;
        private string  remoteConnectPass;

        public string Name { get => name; set => name = value; }
        public string Remark { get => remark; set => remark = value; }
        public Point Location { get => position; set => position = value; }
        public string IPAddress { get => iPAddress; set => iPAddress = value; }
        public bool AlarmEnable { get => alarmEnable; set => alarmEnable = value; }
        public int Timeout { get => timeout; set => timeout = value; }
        public string SoundFilePath { get => soundFilePath; set => soundFilePath = value; }
        public string RemoteConnectUser { get => remoteConnectUser; set => remoteConnectUser = value; }
        public string RemoteConnectPass { get => remoteConnectPass; set => remoteConnectPass = value; }
        public string AddressName { get => addressName; set => addressName = value; }

        public static Node FromCsv(string csvLine)
        {
            string[] values = csvLine.Split('\t');
            Node dailyValues = new Node();
            dailyValues.name = values[0];
            dailyValues.iPAddress = values[3];
            dailyValues.addressName = values[6];
              return dailyValues;
        }
    }
}