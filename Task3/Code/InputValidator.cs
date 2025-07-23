using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task3_DiceGame
{
    public static class InputValidator
    {
        public static (string,string) ValidateCommandLineParams(string[] args)
        {
            if (args == null)
            {
                return ("false","no args found");
                
            }
            if(args.Length <= 2)
            {
                return ("false", "a few args found");
            }
            foreach (string item in args)
            {
                if (string.IsNullOrWhiteSpace(item) ||
                    item.Any(c => !char.IsDigit(c) && c != ','))
                {
                    return ("false", "invalid argument"); 
                }
                string[] numbers = item.Split(',');
                if (numbers.Length != 6)
                {
                    return ("false", "invalid argument");
                }
                foreach (string numStr in numbers)
                {
                    if (!int.TryParse(numStr, out int number) || number <= 0)
                    {
                        return ("false", "invalid argument");
                    }
                }
            }

            return ("true","true");

        }
        public static int ValidateFromRange(string output,int range)
        {
            int res = 0;
            if (output == "X") return 100;
            else if (output == "?") return 101;
            else if(!Int32.TryParse(output,out res))
            {
                return -1;
            }
            else
            {
                if (res < 0 || res > range) return -1;
                else return res;
            }
        }
    }
}
