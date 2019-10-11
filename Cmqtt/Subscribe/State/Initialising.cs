using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    // Create the client and transition to connected
    public class Initialising : IState
    {
        private readonly Options _options;

        public Initialising(Options options)
        {
            _options = options;
        }

        private Task<IMqttClient> CreateClient()
        {
            var config = new MqttConfiguration
            {
                Port = _options.Port,
                MaximumQualityOfService = MqttQualityOfService.ExactlyOnce,
                AllowWildcardsInTopicFilters = true
            };

            return MqttClient.CreateAsync(_options.Broker, config);
        }

        public IObservable<ITransition> Enter()
        {
            return Observable
                .StartAsync(CreateClient)
                .Select(client => new Transition.ToConnecting(client));
        }
    }
}
