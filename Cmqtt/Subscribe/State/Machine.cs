using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe.State
{
    public class Machine
    {
        private readonly Options _options;
        private readonly Subject<IState> _state;

        public Machine(Options options)
        {
            _options = options;

            _state = new Subject<IState>();
        }

        private static void CompleteObserver(IObserver<Unit> observer)
        {
            observer.OnNext(Unit.Default);
            observer.OnCompleted();
        }

        public IObservable<Unit> Run()
        {
            return Observable.Create<Unit>(
                observer =>
                {
                    // First create a stream of transitions by ...
                    IObservable<ITransition> transitions = _state
                        // ... starting from the initializing state ...
                        .StartWith(new Initialising(_options))
                        // ... enter the current state ...
                        .Select(state => state.Enter())
                        // ... subscribing to the transition observable ...
                        .Switch()
                        // ... and ensure only a single shared subscription is made to the transitions observable ...
                        .Publish()
                        // ... held until there are no more subscribers
                        .RefCount();

                    // Then, for each transition type, select the new state...
                    IObservable<IState> states = Observable.Merge<IState>(
                        transitions.OfType<Transition.ToConnecting>().Select(transition => new Connecting(_options, transition.Client)),
                        transitions.OfType<Transition.ToDisconnecting>().Select(transition => new Disconnecting(_options, transition.Client, transition.Reconnect)),
                        transitions.OfType<Transition.ToListening>().Select(transition => new Listening(_options, transition.Client)),
                        transitions.OfType<Transition.ToSubscribing>().Select(transition => new Subscribing(_options, transition.Client)),
                        transitions.OfType<Transition.ToTerminated>().Select(transition => new Terminated()),
                        transitions.OfType<Transition.ToTerminating>().Select(transition => new Terminating(_options, transition.Client)),
                        transitions.OfType<Transition.ToUnsubscribing>().Select(transition => new Unsubscribing(_options, transition.Client, transition.Reconnect))
                    );

                    // Finally, subscribe to the state observable ...
                    return states
                        // ... ensuring all transitions are serialized ...
                        .ObserveOn(Scheduler.CurrentThread)
                        // ... and we're not terminated ...
                        .TakeWhile(state => !(state is Terminated))
                        // ... back onto the source state observable
                        .Subscribe(_state.OnNext, observer.OnError, () => CompleteObserver(observer));
                }
            );
        }
    }
}
