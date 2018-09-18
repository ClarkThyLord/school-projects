using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC___converter.scripts
{
    class base_converter
    {
        public static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public static Dictionary<string, int> types = new Dictionary<string, int>
        {
            {
                "Decimal",
                -1
            },
            {
                "Binary",
                2
            },
            {
                "Hexadecimal",
                16
            },
            {
                "Base32",
                32
            },
            {
                "Base64",
                64
            }
        };

        public static string run (string content, string from, string to)
        {
            return "";
        }
    }
}
