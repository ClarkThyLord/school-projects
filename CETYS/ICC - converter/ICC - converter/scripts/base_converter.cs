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

        public static Dictionary<string, Dictionary<string, Func<string, string>>> types = new Dictionary<string, Dictionary<string, Func<string, string>>>
        {
            {
                "Decimal",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        decimal_to_binary
                    },
                    {
                        "to",
                        binary_to_decimal
                    }
                }
            },
            {
                "Binary",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        (string content) =>  content
                    },
                    {
                        "to",
                        binary_to_decimal
                    }
                }
            },
            {
                "Hexadecimal",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        hexadecimal_to_binary
                    },
                    {
                        "to",
                        binary_to_hexadecimal
                    }
                }
            },
            {
                "Base32",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        base32_to_binary
                    },
                    {
                        "to",
                        binary_to_base32
                    }
                }
            },
            {
                "Base64",
                new Dictionary<string, Func<string, string>>
                {
                    {
                        "from",
                        base64_to_binary
                    },
                    {
                        "to",
                        binary_to_base64
                    }
                }
            }
        };

        public static string run (string content, string from, string to)
        {
            if (from == "Binary")
            {
                return types[from]["from"](content);
            }
            else if (to == "Binary")
            {
                return types[to]["to"](content);
            } else
            {
                return types[to]["to"](types[from]["from"](content));
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
                foreach (char number in content)
                {
                    result += number * Math.Pow(2, pow);
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
                List<int> result = new List<int>();

                foreach (char number in content)
                {
                    int temp = (int)char.GetNumericValue(number);

                    while (true)
                    {
                        result.Add((int)temp % 2);
                        temp = temp / 2;

                        if (temp == 0)
                        {
                            break;
                        }
                    }
                }

                result.Reverse();
                return string.Join("", result);
            }
            catch (InvalidCastException e)
            {
                return "";
            }
        }

        public static string binary_to_hexadecimal(string content)
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

        public static string hexadecimal_to_binary(string content)
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

        public static string binary_to_base32(string content)
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

        public static string base32_to_binary(string content)
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

        public static string binary_to_base64(string content)
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

        public static string base64_to_binary(string content)
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
