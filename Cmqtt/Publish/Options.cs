using CommandLine;

namespace Cmqtt.Publish
{
    [Verb("publish", HelpText = "Publish a message to a broker on a specific topic")]
    public class Options : CommonOptions
    {

        [Option('t', "topic", Required = true, HelpText = "Topic on which to publish a message.")]
        public string Topic { get; set; }

        [Option('m', "message", HelpText = "The message to publish to the broker.", SetName = "Source")]
        public string Message { get; set; }

        [Option('f', "file", HelpText = "The file containing the message to publish to the broker.", SetName = "Source")]
        public string File { get; set; }

        [Option('e', "encoding", Required = false, Default = Encoding.Utf8, HelpText = "The encoding to use for the message")]
        public Encoding Encoding { get; set; }
    }
}
