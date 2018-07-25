using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TurningEdge.Networking.Helpers
{
    public static class AddressHelper
    {
        public static IPAddress Parse(string ipAddress)
        {
            IPAddress parsedIpAddress;

            if (!IPAddress.TryParse(ipAddress, out parsedIpAddress))
            {
                IPHostEntry hostEntry;
                hostEntry = Dns.GetHostEntry(ipAddress);
                if (hostEntry.AddressList.Length > 0)
                    parsedIpAddress = hostEntry.AddressList[0];
                else
                    parsedIpAddress = IPAddress.Parse("127.0.0.1");
            }

            return parsedIpAddress;
        }

        public static string GetLocalAddress()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    return addr.ToString();
                }
            }

            return "127.0.0.1";
        }

        public static string GetPublicAddress()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }

        public static bool CheckIfSamePublic(string hostname)
        {
            return AddressHelper.Parse("127.0.0.1").ToString() 
                == AddressHelper.GetPublicAddress();
        }
    }
}
