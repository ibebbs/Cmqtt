using System.Net.Mqtt;

namespace Cmqtt.Subscribe.State.Transition
{
    public class ToDisconnecting : ITransition
    {
        public ToDisconnecting(IMqttClient client, bool reconnect = false)
        {
            Client = client;
            Reconnect = reconnect;
        }

        public IMqttClient Client { get; }
        public bool Reconnect { get; }
    }
}
