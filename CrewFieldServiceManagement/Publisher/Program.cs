using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common.PubSubCommon;

namespace Publisher
{
    class Program
    {
        static IPublish _proxy;
        static void Main(string[] args)
        {
            CreateProxy();
            Message alertData = PrepareEvent();
            Console.WriteLine("Press enter to send even");
            _proxy.Publish(alertData, "TestTopic");
            Console.WriteLine("event sent");
            Console.ReadLine();
        }

        static private void CreateProxy()
        {
            string endpointAddressInString = Common.Config.PublisherEndpointAddres;
            EndpointAddress endpointAddress = new EndpointAddress(endpointAddressInString);
            NetTcpBinding netTcpBinding = new NetTcpBinding();
            _proxy = ChannelFactory<IPublish>.CreateChannel(netTcpBinding, endpointAddress);
        }
        static private Message PrepareEvent()
        {
            Message e = new Message();
            e.TopicName = "TestTopic";
            e.EventData = "Blabla";
            return e;
        }

    }
}
