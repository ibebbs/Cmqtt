using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    // Dispose of the client then transition to terminated
    public class Terminating : IState
    {
        private readonly Options _options;
        private readonly IMqttClient _client;

        public Terminating(Options options, IMqttClient client)
        {
            _options = options;
            _client = client;
        }

        private Task<ITransition> DisposeClient()
        {
            try
            {
                _client.Dispose();
            }
            catch
            {
                // We're terminating so don't really
                // care if an exception is thrown here
            }

            return Task.FromResult<ITransition>(new Transition.ToTerminated());
        }

        public IObservable<ITransition> Enter()
        {
            return Observable.StartAsync(DisposeClient);
        }
    }
}
