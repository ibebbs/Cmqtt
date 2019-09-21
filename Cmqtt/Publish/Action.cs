using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace Cmqtt.Publish
{
    public static class Action
    {
        public static async Task<int> Runner(Options options)
        {
            var config = new MqttConfiguration
            {
                Port = options.Port,
                AllowWildcardsInTopicFilters = true
            };

            try
            {
                var client = await MqttClient.CreateAsync(options.Broker, config);
                var session = await client.ConnectAsync(new MqttClientCredentials(options.Client, options.Username, options.Password), cleanSession: true);

                var payload = string.IsNullOrEmpty(options.Message)
                    ? new byte[0]
                    : options.Encoding.AsSystemEncoding().GetBytes(options.Message);

                var message = new MqttApplicationMessage(options.Topic, payload);

                await client.PublishAsync(message, MqttQualityOfService.AtMostOnce);

                await client.DisconnectAsync();

                Console.WriteLine("Message published successfully.");

                return 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
                return -1;
            }
        }
    }
}
