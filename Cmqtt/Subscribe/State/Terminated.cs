using System;
using System.Reactive.Linq;

namespace Cmqtt.Subscribe.State
{
    // Stop state
    public class Terminated : IState
    {
        public IObservable<ITransition> Enter()
        {
            return Observable.Never<ITransition>();
        }
    }
}
