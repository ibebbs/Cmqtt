using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    // Subscribe to topic and transition to listening
    public class Subscribing : IState
    {
        private readonly Options _options;
        private readonly IMqttClient _client;

        public Subscribing(Options options, IMqttClient client)
        {
            _options = options;
            _client = client;
        }

        private async Task<ITransition> SubscribeToTopic()
        {
            try
            {
                Console.WriteLine($"Subscribing to topic '{_options.Topic}'");

                await _client.SubscribeAsync(_options.Topic, MqttQualityOfService.ExactlyOnce);

                return new Transition.ToListening(_client);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: '{exception.Message}'.");

                return new Transition.ToDisconnecting(_client, false);
            }
        }

        public IObservable<ITransition> Enter()
        {
            return Observable.StartAsync(SubscribeToTopic);
        }
    }
}
