using CommandLine;

namespace Cmqtt.Subscribe
{
    [Verb("subscribe", HelpText = "Subscribe to messages from a broker on a specific topic")]
    public class Options : CommonOptions
    {
        [Option('t', "topic", Required = true, HelpText = "Topic on which to subscribe for messages.")]
        public string Topic { get; set; }

        [Option('e', "encoding", Required = false, Default = Encoding.Utf8, HelpText = "The encoding to use for the message")]
        public Encoding Encoding { get; set; }

        [Option('v', "verbose", Required = false, Default = false, HelpText = "If specified, this flag causes message payloads to be published with additional metadata including the topic they were published to and the time they were received")]
        public bool Verbose { get; set; }
    }
}
