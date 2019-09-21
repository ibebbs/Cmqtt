using System;
using System.Net.Mqtt;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe
{
    public static class Action
    {
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

                    using (client.MessageStream.ObserveOn(TaskPoolScheduler.Default).Subscribe(message => Console.WriteLine(options.Encoding.AsSystemEncoding().GetString(message.Payload))))
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
