using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.PubSubCommon;
using System.ServiceModel;

namespace Subscriber
{
    class Program : IPublish
    {
        ISubscribe _proxy;
        string _endpoint = string.Empty;

        void Main(string[] args)
        {
            _endpoint = Common.Config.SubscriberEndpointAddres;
            MakeProxy(_endpoint, this);
            _proxy.Subscribe("TestTopic");
            Console.ReadLine();
        }

        public void MakeProxy(string EndpoindAddress, object callbackinstance)
        {
            NetTcpBinding netTcpbinding = new NetTcpBinding(SecurityMode.None);
            EndpointAddress endpointAddress = new EndpointAddress(EndpoindAddress);
            InstanceContext context = new InstanceContext(callbackinstance);

            DuplexChannelFactory<ISubscribe> channelFactory = new DuplexChannelFactory<ISubscribe>(new InstanceContext(this), netTcpbinding, endpointAddress);
            _proxy = channelFactory.CreateChannel();
        }

        public void Publish(Message e, String topicName)
        {
            if (e != null)
            {
                Console.WriteLine("Event received");
                Console.WriteLine("topic name {0} event data {1}",e.TopicName, e.EventData);
            }
        }
    }
}
