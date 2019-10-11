using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    // Disconnect the client then
    // - if reconnect = true transition to connecting
    // - if reconnect = false transition to terminating
    public class Disconnecting : IState
    {
        private readonly Options _options;
        private readonly IMqttClient _client;
        private readonly bool _reconnect;

        public Disconnecting(Options options, IMqttClient client, bool reconnect)
        {
            _options = options;
            _client = client;
            _reconnect = reconnect;
        }

        private async Task<ITransition> Disconnect()
        {
            Console.WriteLine($"Disconnecting from {_options.Broker} on {_options.Port}");

            try
            {
                await _client.DisconnectAsync();
            }
            catch
            {
                // We might be here due to a forced disconnection
                // in which case disconnecting is likely to throw
                // but we don't care so simply swallow the exception
            }

            return _reconnect
                ? (ITransition)new Transition.ToConnecting(_client)
                : (ITransition)new Transition.ToTerminating(_client);
        }

        public IObservable<ITransition> Enter()
        {
            return Observable.StartAsync(Disconnect);
        }
    }
}
