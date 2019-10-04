using System;
using System.Net.Mqtt;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Cmqtt.Subscribe
{
    internal class Container
    {
        public static Container FromMessage(MqttApplicationMessage message, Options options, Func<DateTimeOffset> dateTimeProvider = null)
        {
            dateTimeProvider = dateTimeProvider ?? new Func<DateTimeOffset>(() => DateTimeOffset.UtcNow);

            return new Container
            {
                Topic = message.Topic,
                Received = dateTimeProvider(),
                Payload = options.Encoding.AsSystemEncoding().GetString(message.Payload)
            };
        }

        public string Topic { get; set; }

        public DateTimeOffset Received { get; set; }

        public string Payload { get; set; }
    }

    internal static class ContainerHelpers
    {
        public static string Serialize(this Container container)
        {
            return JsonSerializer.Serialize(container, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
        }
    }
}
