using System.Net.Mqtt;

namespace Cmqtt.Subscribe.State.Transition
{
    public class ToUnsubscribing : ITransition
    {
        public ToUnsubscribing(IMqttClient client, bool reconnect = false)
        {
            Client = client;
            Reconnect = reconnect;
        }

        public IMqttClient Client { get; }
        public bool Reconnect { get; }
    }
}
