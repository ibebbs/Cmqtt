using System;

namespace Cmqtt
{
    public static class Extensions
    {
        public static System.Text.Encoding AsSystemEncoding(this Encoding encoding)
        {
            switch (encoding)
            {
                case Encoding.Ascii: return System.Text.Encoding.ASCII;
                case Encoding.Utf8: return System.Text.Encoding.UTF8;
                default: throw new ArgumentException($"Unknown encoding type '{encoding}'", "encoding");
            }
        }
    }
}
