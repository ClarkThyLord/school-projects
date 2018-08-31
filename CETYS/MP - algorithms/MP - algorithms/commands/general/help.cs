using System;
using System.Collections.Generic;
using System.Text;

namespace MP___algorithms.commands.general
{
    class help
    {
        // Command name
        static public Dictionary<string, string> name = new Dictionary<string, string> {
            {
                "either",
                "?"
            },
            {
                "english",
                "help"
            },
            {
                "spanish",
                "ayuda"
            }
        };

        // Command description
        static public Dictionary<string, string> description = new Dictionary<string, string> {
            {
                "english",
                "list of commands"
            },
            {
                "spanish",
                "lista de comandos"
            }
        };

        // Execute command
        static public void run (Dictionary<string, Type> commands)
        {
            return;
        }
    }
}
