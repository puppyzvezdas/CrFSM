using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.PubSubCommon;
using System.ServiceModel;
using System.Reflection;

namespace PunSubServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PublishingService : IPublish
    {

        public void Publish(Message e, string topicName)
        {
            List<IPublish> subscribers = Filter.GetSubscribers(topicName);

            if (subscribers == null)
                return;

            Type type = typeof(IPublish);

            MethodInfo publishMethodInfo = type.GetMethod("Publish");

            foreach (IPublish subcriber in subscribers)
            {
                try
                {
                    publishMethodInfo.Invoke(subcriber, new object[] { e, topicName });
                }
                catch(Exception ex)
                {
                    CrFSMLogger.CrFSMLogger.Instance.WriteToLog(ex);
                }
            }
        }
    }
}
