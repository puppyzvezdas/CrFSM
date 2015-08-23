using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Common.PubSubCommon
{
    [ServiceContract]
    public interface IPublish
    {
        [OperationContract(IsOneWay = true)]
        void Publish(Message e, string topicName);
    }
}
