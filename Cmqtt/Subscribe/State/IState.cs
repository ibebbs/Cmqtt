using System;

namespace Cmqtt.Subscribe.State
{
    public interface IState
    {
        IObservable<ITransition> Enter();
    }
}
