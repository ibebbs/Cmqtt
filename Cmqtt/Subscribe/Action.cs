using System;
using System.Net.Mqtt;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Cmqtt.Subscribe
{
    public static class Action
    {
        public static async Task<int> Runner(Options options)
        {
            var machine = new State.Machine(options);

            await machine.Run();

            return 0;
        }
    }
}
