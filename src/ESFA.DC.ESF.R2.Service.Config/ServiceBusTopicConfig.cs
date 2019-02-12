﻿using System;
using ESFA.DC.Queueing;

namespace ESFA.DC.ESF.R2.Service.Config
{
    public class ServiceBusTopicConfig : TopicConfiguration
    {
        public ServiceBusTopicConfig(string connectionString, string topicName, string subscriptionName, int maxConcurrentCalls, TimeSpan ts, int minimumBackoffSeconds = 5, int maximumBackoffSeconds = 50, int maximumRetryCount = 10)
            : base(connectionString, topicName, subscriptionName, maxConcurrentCalls, minimumBackoffSeconds, maximumBackoffSeconds, maximumRetryCount, ts)
        {
        }
    }
}
