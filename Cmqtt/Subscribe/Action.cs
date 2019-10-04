using System;
using System.Net.Mqtt;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe
{
    public static class Action
    {
        private static string FormatOutput(Options options, MqttApplicationMessage message)
        {
            if (options.Verbose)
            {
                return Container.FromMessage(message, options).Serialize();
            }
            else
            {
                return options.Encoding.AsSystemEncoding().GetString(message.Payload);
            }
        }

        public static async Task<int> Runner(Options options)
        {
            var config = new MqttConfiguration
            {
                Port = options.Port,
                MaximumQualityOfService = MqttQualityOfService.ExactlyOnce,
                AllowWildcardsInTopicFilters = true
            };

            try
            {
                using (var client = await MqttClient.CreateAsync(options.Broker, config))
                {
                    var session = await client.ConnectAsync(new MqttClientCredentials(options.Client, options.Username, options.Password), cleanSession: true);

                    await client.SubscribeAsync(options.Topic, MqttQualityOfService.ExactlyOnce);

                using (client.MessageStream.ObserveOn(TaskPoolScheduler.Default).Select(message => FormatOutput(options, message)).Subscribe(Console.WriteLine))
                {
                    Console.WriteLine("Subscribed to messages");
                    Console.WriteLine("Hit <Enter> to exit");
                    Console.ReadLine();
                }

                    await client.UnsubscribeAsync(options.Topic);
                    await client.DisconnectAsync();
                }

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
