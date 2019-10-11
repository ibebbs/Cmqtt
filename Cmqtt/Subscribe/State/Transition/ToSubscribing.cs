using System.Net.Mqtt;

namespace Cmqtt.Subscribe.State.Transition
{
    public class ToSubscribing : ITransition
    {
        public ToSubscribing(IMqttClient client)
        {
            Client = client;
        }

        public IMqttClient Client { get; }
    }
}
