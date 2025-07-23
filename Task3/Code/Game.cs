using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task3_DiceGame
{
    public class Game
    {
        private string[] args;
        private List<List<int>>? Dices;
        
        public Game(string[] args)
        {
            this.args = args;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the game!\n"+
                              "First of all,let's determine who will make first move\n"+
                              "I selected a random value from 0 to 1\n"+
                              "If you guess my choise, you will make first move,otherwise I start");
            Dices = Parser.ParseFromString(args);
            
            MoveSelect();


        }
        private void MoveSelect()
        {
            int choise;
            int res;
            byte[] key;
            while (true)
            {
                choise  = RandomGenerator.ChoiseOfComputer(2);
                key = HmacGenerator.RandomHMACKey();
                byte[] myHMAC = HmacGenerator.CreationOfHMACSHA3_256(key, choise);
                Console.WriteLine("(HMAC: " + BitConverter.ToString(myHMAC).Replace("-", "") + ").");
                Console.WriteLine("Try to guess my selection");
                Console.WriteLine("0 - 0\r\n1 - 1\r\nX - exit\r\n? - help");

                res = CheckConsole(1);
                if (res == 100)
                {
                    return;
                }
                else if (res == 101)
                {
                    HelpInfo.PrintRules();
                }
                else break;

            }
            
                ClearStringInConsole();
                Console.WriteLine("Your selection: " + res);
                Console.WriteLine("My Selection: " + choise);
                Console.WriteLine("(KEY: " + BitConverter.ToString(key).Replace("-", "") + ").");
                if (choise == res)
                {
                    DiceSelection(true);
                }
                else
                {
                    DiceSelection(false);
                }
            
        }
        private void ClearStringInConsole()
        {
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        public int CheckConsole(int range)
        {
            int res;
            string output;
            while (true)
            {
                output = Console.ReadLine();
                res = InputValidator.ValidateFromRange(output, range);
                if (res == -1)
                {
                    ClearStringInConsole();
                    continue;
                }
                else break;
            }
            return res;
        }
        private int MoveOnDice(List<int> ChoosedDice)
        {
            int res,choise;
            byte[] key;
            while (true)
            {
                Console.WriteLine("I selected a random value in the range 0..5");
                choise = RandomGenerator.ChoiseOfComputer(6);
                key  = HmacGenerator.RandomHMACKey();
                byte[] myHMAC = HmacGenerator.CreationOfHMACSHA3_256(key, choise);
                Console.WriteLine("(HMAC: " + BitConverter.ToString(myHMAC).Replace("-", "") + ").");
                Console.WriteLine("Add your number modulo 6");
                Console.WriteLine("0 - 0\r\n1 - 1\r\n2 - 2\r\n3 - 3\r\n4 - 4\r\n5 - 5\r\nX - exit\r\n? - help");
                res = CheckConsole(5);
                if (res == 100)
                {
                    return -1;
                }
                else if (res == 101)
                {
                    HelpInfo.PrintRules();
                }
                else break;
                
            }
            int result = (res + choise) % 6;
            ClearStringInConsole();
            Console.WriteLine("Your selection: " + res);
            Console.WriteLine("My selection: " + choise.ToString());
            Console.WriteLine("(KEY: " + BitConverter.ToString(key).Replace("-", "") + ").");
            Console.WriteLine($"The fair generation result is {res} + {choise} = {result} (mod 6).");
            return ChoosedDice[result];
        }
        
        private void PrintDices()
        {
            for (int i = 0; i < Dices.Count(); ++i)
            {
                Console.Write($"{i} - ");
                for (int j = 0; j < Dices[i].Count; ++j)
                {
                    Console.Write(Dices[i][j]);
                    if (j != Dices[i].Count - 1) Console.Write(',');
                }
                Console.Write('\n');
            }
            Console.WriteLine("X - exit\r\n? - help");
        }
        private void DiceSelection(bool UserStartsFirst)
        {
            List<int> UserDice= new List<int>();
            List<int> ComputerDice = new List<int>();
            int res;
            bool breakout = false;
                if (UserStartsFirst)
                {
                while (true)
                {
                    Console.WriteLine("You make the first move\nChoose your dice:");
                    PrintDices();
                    res = CheckConsole(Dices.Count - 1);
                    if (res == 100)
                    {
                        breakout = true;
                        break;
                    }
                    else if (res == 101)
                    {
                        HelpInfo.PrintTable(Dices);
                    }
                    else break;
                }
                if (!breakout)
                {
                    ClearStringInConsole();
                    Console.WriteLine($"Your selection: {res}");
                    UserDice = Dices[res];
                    Dices.RemoveAt(res);
                    int index = ProbabilityCalculator.BestOption(Dices);
                    Console.WriteLine("I choose " + "[" + args[index] + "] dice.");
                    ComputerDice = Dices[index];
                    Dices.RemoveAt(index);
                }
                
                }
                else
                {
                
                    Console.Write("I make the first move and choose the ");
                    int index = ProbabilityCalculator.BestOption(Dices);
                    ComputerDice = Dices[index];
                    Dices.RemoveAt(index);
                    Console.WriteLine("[" + args[index] + "] dice.");
                while (true)
                {
                    Console.WriteLine("Choose your dice:");
                    PrintDices();
                    res = CheckConsole(Dices.Count - 1);
                    if (res == 100)
                    {
                        breakout = true;
                        break;
                    }
                    else if (res == 101)
                    {
                        HelpInfo.PrintTable(Dices);
                    }
                    else break;
                }
                if (!breakout)
                {
                    ClearStringInConsole();
                    Console.WriteLine("Your selection: " + res);
                    UserDice = Dices[res];
                    Dices.RemoveAt(index);
                }
                }
            
            if(!breakout)Continue(UserStartsFirst, UserDice, ComputerDice);
        }
        private void Continue(bool UserStartsFirst,List<int>UserDice,List<int>ComputerDice)
        {
            int UserRes = 0;
            int CompRes = 0;
            if (UserStartsFirst)
            {
                Console.WriteLine("It's time for your roll.");
                UserRes = MoveOnDice(UserDice);
                if (UserRes == -1) return;
                Console.WriteLine($"Your roll result: {UserRes}");
                CompRes = MoveOnDice(ComputerDice);
                if (CompRes == -1) return;
                Console.WriteLine($"My roll result: {CompRes}");
            }
            else
            {
                CompRes = MoveOnDice(ComputerDice);
                if (CompRes == -1) return;
                Console.WriteLine($"My roll result: {CompRes}");
                UserRes = MoveOnDice(UserDice);
                if (UserRes == -1) return;
                Console.WriteLine($"Your roll result: {UserRes}");
            }
            if(UserRes==CompRes)
            {
                Console.WriteLine($"Draw! ({UserRes} = {CompRes})!");
            }
            else if(UserRes>CompRes)
            {
                Console.WriteLine($"You win! ({UserRes} > {CompRes})!");
            }
            else
            {
                Console.WriteLine($"I win! ({UserRes} < {CompRes})!");
            }
        }
    }
}
