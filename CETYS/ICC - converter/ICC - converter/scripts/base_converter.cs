using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ICC___converter.scripts
{
    class base_converter
    {
        public static Dictionary<string, Dictionary<string, Func<string, string>>> converters = new Dictionary<string, Dictionary<string, Func<string, string>>>
        {
            {
                "Decimal",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        (string content) => content
                    },
                    {
                        "to",
                        (string content) => content
                    }
                }
            },
            {
                "Binary",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        binary_to_decimal
                    },
                    {
                        "to",
                        decimal_to_binary
                    }
                }
            },
            {
                "Hexadecimal",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        hexadecimal_to_decimal
                    },
                    {
                        "to",
                        decimal_to_hexadecimal
                    }
                }
            },
            {
                "Base32",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        base32_to_decimal
                    },
                    {
                        "to",
                        decimal_to_base32
                    }
                }
            },
            {
                "Base64",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        base64_to_decimal
                    },
                    {
                        "to",
                        decimal_to_base64
                    }
                }
            }
        };

        public static string run (string content, string from, string to)
        {
            Console.WriteLine("{0} FROM {1} TO {2}", content, from, to);

            if (from == "Binary" && to == "Decimal")
            {
                return converters[from]["from"](content);
            }
            else if (from == "Decimal" && to == "Binary")
            {
                return converters[to]["to"](content);
            }
            else
            {
                return converters[to]["to"](converters[from]["from"](content));
            }
        }

        public static string string_reverse(string content)
        {
            char[] charArray = content.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string binary_to_decimal(string content)
        {
            try
            {
                double result = 0;

                content = string_reverse(content);

                double pow = 0;
                foreach (char bit in content)
                {
                    result += int.Parse(bit.ToString()) * Math.Pow(2, pow);
                    pow += 1;
                }

                return result.ToString();
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string decimal_to_binary(string content)
        {
            try
            {
                string result = "";

                System.Numerics.BigInteger content_int = System.Numerics.BigInteger.Parse(content);
                while (true)
                {
                    result += (content_int % 2).ToString();
                    content_int = content_int / 2;

                    if (content_int == 0)
                    {
                        break;
                    }
                }
                
                return string_reverse(result);
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string decimal_to_hexadecimal(string content)
        {
            try
            {
                string alphabet = "0123456789ABCDEF";

                string result = "";

                System.Numerics.BigInteger content_int = System.Numerics.BigInteger.Parse(content);
                while (true)
                {
                    result += alphabet[(int)content_int % 16].ToString();
                    content_int = content_int / 16;

                    if (content_int < 16)
                    {
                        result += alphabet[(int)content_int].ToString();
                        break;
                    }
                }

                return string_reverse(result);
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string hexadecimal_to_decimal(string content)
        {
            try
            {
                string alphabet = "0123456789ABCDEF";

                double result = 0;

                content = string_reverse(content);

                double pow = 0;
                foreach (char character in content)
                {
                    result += alphabet.IndexOf(character) * Math.Pow(16, pow);
                    pow += 1;
                }

                return result.ToString();
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string decimal_to_base32(string content)
        {
            try
            {
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

                string result = "";

                return result;
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string base32_to_decimal(string content)
        {
            try
            {
                string result = "";

                return result;
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string decimal_to_base64(string content)
        {
            try
            {
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

                string result = "";

                return result;
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string base64_to_decimal(string content)
        {
            try
            {
                string result = "";

                return result;
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }
    }
}
