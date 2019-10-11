using System;
using System.Net.Mqtt;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    // Remove the subscription (ignoring any exceptions) then transition to Disconnecting
    public class Unsubscribing : IState
    {
        private readonly Options _options;
        private readonly IMqttClient _client;
        private readonly bool _reconnect;

        public Unsubscribing(Options options, IMqttClient client, bool reconnect)
        {
            _options = options;
            _client = client;
            _reconnect = reconnect;
        }

        private async Task<ITransition> Unsubscribe()
        {
            Console.WriteLine($"Unsubscribing from topic '{_options.Topic}'");

            try
            {
                await _client.UnsubscribeAsync(_options.Topic);
            }
            catch
            {
                // We might be here due to a forced disconnection
                // in which case unsubscribing is likely to throw
                // but we don't care so simply swallow the exception
            }

            return new Transition.ToDisconnecting(_client, _reconnect);
        }

        public IObservable<ITransition> Enter()
        {
            return Observable.StartAsync(Unsubscribe);
        }
    }
}
