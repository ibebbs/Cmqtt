using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace Cmqtt.Publish
{
    public static class Action
    {
        private static async Task<byte[]> GetContent(Options options)
        {
            if (!string.IsNullOrWhiteSpace(options.Message))
            {
                return options.Encoding.AsSystemEncoding().GetBytes(options.Message);
            }
            else if (!string.IsNullOrWhiteSpace(options.File))
            {
                return await File.ReadAllBytesAsync(options.File);
            }
            else
            {
                return new byte[0];
            }
        }

        public static async Task<int> Runner(Options options)
        {
            var config = new MqttConfiguration
            {
                Port = options.Port,
                AllowWildcardsInTopicFilters = true
            };

            try
            {
                using (var client = await MqttClient.CreateAsync(options.Broker, config))
                {
                    var session = await client.ConnectAsync(new MqttClientCredentials(Client.Id.Provider.GetClientId(options), options.Username, options.Password), cleanSession: true);

                    var payload = await GetContent(options);

                    var message = new MqttApplicationMessage(options.Topic, payload);

                    await client.PublishAsync(message, MqttQualityOfService.AtMostOnce);

                    await client.DisconnectAsync();
                }

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
