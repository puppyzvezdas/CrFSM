using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using DataRepositoryServiceLib;
using System.Threading;

namespace DataRepositoryServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1 of the address configuration procedure: Create a URI to serve as the base address.
            Uri baseAddressDRS = new Uri("http://localhost:8088/CrewFieldServiceManagement/");

            // Step 2 of the hosting procedure: Create ServiceHost
            ServiceHost dataRepositoryService = new ServiceHost(typeof(DataRepositoryServiceLib.DataRepositoryService), baseAddressDRS);

            try
            {
                // Step 3 of the hosting procedure: Add a service endpoint.
                dataRepositoryService.AddServiceEndpoint(typeof(IDataRepositoryService), new WSHttpBinding(), "DataRepositoryService");

                // Step 4 of the hosting procedure: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                dataRepositoryService.Description.Behaviors.Add(smb);

                // Step 5 of the hosting procedure: Start (and then stop) the service.
                dataRepositoryService.Open();

                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                CrFSMLogger.CrFSMLogger.Instance.WriteToLog("Data repository service is up");
                Console.ReadLine();
                // Close the ServiceHostBase to shutdown the service.
                dataRepositoryService.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                dataRepositoryService.Abort();
            }
        }
    }
}
