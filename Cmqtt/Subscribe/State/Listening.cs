using System;
using System.Net.Mqtt;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    // Listen for messages until:
    // - user quits then transition to unsubscribing with "reconnect = false"
    // - disconnected then transition to unsubscribing with "reconnect = false"
    public class Listening : IState
    {
        private readonly Options _options;
        private readonly IMqttClient _client;

        public Listening(Options options, IMqttClient client)
        {
            _options = options;
            _client = client;
        }
        private static string FormatOutput(Options options, MqttApplicationMessage message)
        {
            if (options.Verbose)
            {
                return Container.FromMessage(message, options).Serialize();
            }
            else
            {
                return options.Encoding.AsSystemEncoding().GetString(message.Payload);
            }
        }

        private IObservable<ITransition> ListenToMessagesAndTransitionToUnsubscribingWhenUserExits()
        {
            return Observable.StartAsync(
                async () =>
                {
                    using (_client.MessageStream.ObserveOn(TaskPoolScheduler.Default).Select(message => FormatOutput(_options, message)).Subscribe(Console.WriteLine))
                    {
                        Console.WriteLine("Subscribed to messages");
                        Console.WriteLine("Hit <Enter> to exit");
                        await Task.Run(() => Console.ReadLine());

                        return new Transition.ToUnsubscribing(_client, false);
                    }
                }
            );
        }

        private IObservable<ITransition> TransitionToUnsubscribingWhenClientDisconnects()
        {
            return Observable
                .FromEventPattern<EventHandler<MqttEndpointDisconnected>, MqttEndpointDisconnected>(handler => _client.Disconnected += handler, handler => _client.Disconnected -= handler)
                .Do(args => Console.WriteLine($"Client disconnected due to '{args.EventArgs.Reason}'. Preparing to reconnect..."))
                .Select(args => new Transition.ToUnsubscribing(_client, true));
        }

        public IObservable<ITransition> Enter()
        {
            return Observable.Amb(
                ListenToMessagesAndTransitionToUnsubscribingWhenUserExits(),
                TransitionToUnsubscribingWhenClientDisconnects()
            );
        }
    }
}
