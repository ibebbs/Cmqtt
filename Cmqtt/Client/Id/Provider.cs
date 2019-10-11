using System.Diagnostics;

namespace Cmqtt.Client.Id
{
    public static class Provider
    {
        public static string GetClientId(CommonOptions options)
        {
            return string.IsNullOrWhiteSpace(options.Client)
                ? $"cmqtt{Process.GetCurrentProcess().Id}"
                : options.Client;            
        }
    }
}
