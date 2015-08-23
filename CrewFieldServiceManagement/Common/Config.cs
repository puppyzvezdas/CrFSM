using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class Config
    {
        public const string GeneralPath = "C:\\CFSM\\";
        public const string DataPath = GeneralPath + "data\\";
        public const string LogPath = GeneralPath + "log\\";
        public static Regex EmailAddressRegex = new Regex("[a-zA-Z0-9]@[a-z].[a-z]");
        public const string PublisherEndpointAddres = "net.tcp://localhost:7001/Pub";
        public const string SubscriberEndpointAddres = "net.tcp://localhost:7002/Sub";
    }
}
