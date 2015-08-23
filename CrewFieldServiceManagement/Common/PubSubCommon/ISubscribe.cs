using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Common.PubSubCommon
{
    [ServiceContract(CallbackContract = typeof(IPublish))]
    public interface ISubscribe
    {
        [OperationContract]
        void Subscribe(string topicName);

        [OperationContract]
        void UnSubscribe(string topicName);
    }
}
