using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_DiceGame
{
    public static class Parser
    {
        public static List<List<int>> ParseFromString(string[] args)
        {
            var diceList = new List<List<int>>();

            foreach (string diceStr in args)
            {
                List<int> dice = diceStr.Split(',')
                                      .Select(int.Parse)
                                      .ToList();
                diceList.Add(dice);
            }

            return diceList;
        }
        
    }
}
