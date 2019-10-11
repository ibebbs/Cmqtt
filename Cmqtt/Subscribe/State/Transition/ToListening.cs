using System.Net.Mqtt;

namespace Cmqtt.Subscribe.State.Transition
{
    public class ToListening : ITransition
    {
        public ToListening(IMqttClient client)
        {
            Client = client;
        }

        public IMqttClient Client { get; }
    }
}
