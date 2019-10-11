using System.Net.Mqtt;

namespace Cmqtt.Subscribe.State.Transition
{
    public class ToConnecting : ITransition
    {
        public ToConnecting(IMqttClient client)
        {
            Client = client;
        }

        public IMqttClient Client { get; }
    }
}
