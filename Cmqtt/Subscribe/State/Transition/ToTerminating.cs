using System.Net.Mqtt;

namespace Cmqtt.Subscribe.State.Transition
{
    public class ToTerminating : ITransition
    {
        public ToTerminating(IMqttClient client)
        {
            Client = client;
        }

        public IMqttClient Client { get; }
    }
}
