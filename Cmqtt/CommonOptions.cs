using CommandLine;

namespace Cmqtt
{
    public class CommonOptions
    {
        [Option('b', "broker", Required = true, HelpText = "Address of the broker to publish to.")]
        public string Broker { get; set; }

        [Option('p', "port", Required = false, Default = 1883, HelpText = "Port on which to connect of the broker.")]
        public int Port { get; set; }

        [Option('c', "client", Required = false, Default = "cmqtt", HelpText = "Client id to use when connecting to the broker.")]
        public string Client { get; set; }

        [Option("User", Required = false, Default = null, HelpText = "The username to use to authenticate with the broker")]
        public string Username { get; set; }

        [Option("Password", Required = false, Default = null, HelpText = "The password to use to authenticate with the broker")]
        public string Password { get; set; }
    }
}
