using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common.PubSubCommon;
using System.ServiceModel.Description;

namespace PunSubServiceHost
{
    class Program
    {
        private static ServiceHost publishServiceHost = null;
        private static ServiceHost subscribeServiceHost = null;   

        static void Main(string[] args)
        {
            CrFSMLogger.CrFSMLogger.Instance.WriteToLog("Starting PubSub service");
            Console.WriteLine("Starting services...");
            try
            {
                Console.WriteLine("Starting publishing service");
                HostPublishService();
                Console.WriteLine("Publishing service succesfully started");

                Console.WriteLine("Starting subscription service");
                HostSubsrciberService();
                Console.WriteLine("Subscription service succesfully started");

                Console.WriteLine("The services are ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();

                CrFSMLogger.CrFSMLogger.Instance.WriteToLog("PubSub Started");

                Console.ReadLine();

                

                publishServiceHost.Close();
                subscribeServiceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                publishServiceHost.Abort();
                subscribeServiceHost.Abort();
            }
        }


        private static void HostPublishService()
        {
            publishServiceHost = new ServiceHost(typeof(PublishingService));
            NetTcpBinding binding = new NetTcpBinding();
            TimeSpan ts = new TimeSpan(1, 0, 0);
            binding.CloseTimeout = ts;
            binding.OpenTimeout = ts;
            binding.SendTimeout = ts;
            binding.ReceiveTimeout = ts;
            publishServiceHost.AddServiceEndpoint(typeof(IPublish), binding, "net.tcp://localhost:7001/Pub");
            publishServiceHost.Open();
        }

        private static void HostSubsrciberService()
        {
            subscribeServiceHost = new ServiceHost(typeof(SubsrciptionService));
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            TimeSpan ts = new TimeSpan(1, 0, 0);
            binding.CloseTimeout = ts;
            binding.OpenTimeout = ts;
            binding.SendTimeout = ts;
            binding.ReceiveTimeout = ts;
            subscribeServiceHost.AddServiceEndpoint(typeof(ISubscribe), binding, "net.tcp://localhost:7002/Sub");
            subscribeServiceHost.Open();
        }
    }
}
