using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICC___converter.scripts
{
    class base_guesser
    {
        public static string run (string content)
        {
            Dictionary<string, Dictionary<string, dynamic>> bases = new Dictionary<string, Dictionary<string, dynamic>>
            {
                {
                    "Binary",
                    new Dictionary<string, dynamic>
                    {
                        {
                            "alphabet",
                            "01"
                        },
                        {
                            "excluded",
                            false
                        },
                        {
                            "probability",
                            0
                        }
                    }
                },
                {
                    "Decimal",
                    new Dictionary<string, dynamic>
                    {
                        {
                            "alphabet",
                            "0123456789"
                        },
                        {
                            "excluded",
                            false
                        },
                        {
                            "probability",
                            0
                        }
                    }
                },
                {
                    "Hexadecimal",
                    new Dictionary<string, dynamic>
                    {
                        {
                            "alphabet",
                            "0123456789ABCDEF"
                        },
                        {
                            "excluded",
                            false
                        },
                        {
                            "probability",
                            0
                        }
                    }
                },
                {
                    "Base32",
                    new Dictionary<string, dynamic>
                    {
                        {
                            "alphabet",
                            "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"
                        },
                        {
                            "excluded",
                            true
                        },
                        {
                            "probability",
                            0
                        }
                    }
                },
                {
                    "Base64",
                    new Dictionary<string, dynamic>
                    {
                        {
                            "alphabet",
                            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
                        },
                        {
                            "excluded",
                            true
                        },
                        {
                            "probability",
                            0
                        }
                    }
                }
            };

            for (int i = 0; i < (content.Length >= 10 ? 10 : content.Length); i++)
            {
                foreach (KeyValuePair<string, Dictionary<string, dynamic>> @base in bases) {
                    if (content[i].ToString().IndexOfAny(@base.Value["alphabet"].ToCharArray()) != -1)
                    {
                        @base.Value["probability"] += 1;
                    }
                    else
                    {
                        @base.Value["excluded"] = true;
                        @base.Value["probability"] -= 0.5;
                    }
                }
            }

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>
            {
                {
                    "base",
                    ""
                },
                {
                    "probability",
                    0
                }
            };
            
            foreach (KeyValuePair<string, Dictionary<string, dynamic>> @base in bases)
            {
                if ((int)@base.Value["probability"] > (int)result["probability"] && !@base.Value["excluded"])
                {
                    result["base"] = @base.Key;
                    result["probability"] = @base.Value["probability"];
                }
            }

            return (string)result["base"];
        }
    }
}
