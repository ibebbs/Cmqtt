using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    // Connect the client and transition to subscribing
    public class Connecting : IState
    {
        private readonly Options _options;
        private readonly IMqttClient _client;

        public Connecting(Options options, IMqttClient client)
        {
            _options = options;
            _client = client;
        }

        private async Task<ITransition> ConnectClient()
        {
            try
            {
                Console.WriteLine($"Connecting to {_options.Broker} on {_options.Port}");

                _ = await _client.ConnectAsync(new MqttClientCredentials(_options.Client, _options.Username, _options.Password), cleanSession: true);

                return new Transition.ToSubscribing(_client);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: '{exception.Message}'. Exiting.");

                return new Transition.ToTerminating(_client);
            }
        }

        public IObservable<ITransition> Enter()
        {
            return Observable.StartAsync(ConnectClient);
        }
    }
}
