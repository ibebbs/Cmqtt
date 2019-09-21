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
    }
}
