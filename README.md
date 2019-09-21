# Cmqtt
Cross-platform .net Core tool for interacting with MQTT. Inspire by Curl.

# Usage
Usage information available by using the '--help' option. Example help text shown below:

## Basic Help
```
dotnet .\Cmqtt.dll --help

Cmqtt 1.0.0

  publish      Publish a message to a broker on a specific topic

  subscribe    Subscribe to messages from a broker on a specific topic

  help         Display more information on a specific command.

  version      Display version information.
```

## Publish Help
```
dotnet .\Cmqtt.dll publish --help

Cmqtt 1.0.0

  -t, --topic       Required. Topic on which to publish a message.

  -m, --message     The message to publish to the broker.

  -e, --encoding    (Default: Utf8) The encoding to use for the message

  -b, --broker      Required. Address of the broker to publish to.

  -p, --port        (Default: 1883) Port on which to connect of the broker.

  -c, --client      (Default: cmqtt) Client id to use when connecting to the broker.

  --User            The username to use to authenticate with the broker

  --Password        The password to use to authenticate with the broker

  --help            Display this help screen.

  --version         Display version information.
```

## Subscribe Help
```
dotnet .\Cmqtt.dll subscribe --help

Cmqtt 1.0.0

  -t, --topic       Required. Topic on which to subscribe for messages.

  -e, --encoding    (Default: Utf8) The encoding to use for the message

  -b, --broker      Required. Address of the broker to publish to.

  -p, --port        (Default: 1883) Port on which to connect of the broker.

  -c, --client      (Default: cmqtt) Client id to use when connecting to the broker.

  --User            The username to use to authenticate with the broker

  --Password        The password to use to authenticate with the broker

  --help            Display this help screen.

  --version         Display version information.
```
