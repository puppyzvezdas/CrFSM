using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.PubSubCommon;
using System.ServiceModel;

namespace PunSubServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SubsrciptionService : ISubscribe
    {
        public void Subscribe(string topicName)
        {
            IPublish subscriber = OperationContext.Current.GetCallbackChannel<IPublish>();
            Filter.AddSubscriber(topicName, subscriber);
        }

        public void UnSubscribe(string topicName)
        {
            IPublish subscriber = OperationContext.Current.GetCallbackChannel<IPublish>();
            Filter.RemoveSubscriber(topicName, subscriber);
        }
    }
}
