using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.PubSubCommon;
using System.Net;

namespace PunSubServiceHost
{
    public class Filter
    {
        static Dictionary<string, List<IPublish>> _subscribersList = new Dictionary<string, List<IPublish>>();

        static public Dictionary<string, List<IPublish>> SubscribersList
        {
            get
            {
                lock (typeof(Filter))
                {
                    return _subscribersList;
                }
            }

        }

        static public List<IPublish> GetSubscribers(String topicName)
        {
            lock (typeof(Filter))
            {
                if (SubscribersList.ContainsKey(topicName))
                {
                    return SubscribersList[topicName];
                }
                else
                    return null;
            }
        }

        static public void AddSubscriber(String topicName, IPublish subscriberCallbackReference)
        {
            lock (typeof(Filter))
            {
                if (SubscribersList.ContainsKey(topicName))
                {
                    if (!SubscribersList[topicName].Contains(subscriberCallbackReference))
                    {
                        SubscribersList[topicName].Add(subscriberCallbackReference);
                    }
                }
                else
                {
                    List<IPublish> newSubscribersList = new List<IPublish>();
                    newSubscribersList.Add(subscriberCallbackReference);
                    SubscribersList.Add(topicName, newSubscribersList);
                }
            }

        }

        static public void RemoveSubscriber(String topicName, IPublish subscriberCallbackReference)
        {
            lock (typeof(Filter))
            {
                if (SubscribersList.ContainsKey(topicName))
                {
                    if (SubscribersList[topicName].Contains(subscriberCallbackReference))
                    {
                        SubscribersList[topicName].Remove(subscriberCallbackReference);
                    }
                }
            }
        }

    }
}
