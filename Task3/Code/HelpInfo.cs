using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
namespace Task3_DiceGame
{
    public static class HelpInfo
    {
        public static void PrintRules()
        {
            Console.WriteLine("The main goal of the game to win again compiter on DiceGame\n" +
                "To prove fairness of computer playstyle, game will show hash result(sha2563) " +
                "and then key, so user can calculate HMAC and prove fairness of computer");
        }
        public static void PrintTable(List<List<int>> Dices)
        {
            const string GreenColor = "\x1b[32m";
            const string RedColor = "\x1b[31m";
            const string ResetColor = "\x1b[0m";
            ConsoleTable table = new ConsoleTable();
            int IndexBest = ProbabilityCalculator.BestOption(Dices);
            int IndexWorst = ProbabilityCalculator.WorseOption(Dices);
            var headers = new List<string> { "User dice v" };
            headers.AddRange(Dices.Select(c => string.Join(",", c)));
            table.AddColumn(headers);
            List<string> temp = new List<string>();
            temp.Capacity = Dices.Count;
            for (int i = 0; i < Dices.Count; i++)
            {
                var row = new List<string> { string.Join(",", Dices[i]) };

                for (int j = 0; j < Dices.Count; j++)
                {
                    if (i == j)
                    {
                        row.Add("-");
                        continue;
                    }

                    double prob = ProbabilityCalculator.CalculateTotalProbability(Dices,j);
                    string probStr = prob.ToString();
                    row.Add(probStr);
                }

                table.AddRow(row.ToArray());
            }

            table.Configure(o =>
            {
                o.EnableCount = false;
                o.NumberAlignment = Alignment.Right;
            });

            Console.WriteLine("Probability of the win for the user:");
            table.Write(Format.Default);
        }
    }
}
