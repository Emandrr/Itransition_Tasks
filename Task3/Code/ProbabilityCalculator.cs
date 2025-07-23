using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_DiceGame
{
    public static class ProbabilityCalculator
    {
        public static int BestOption(List<List<int>> ListOfDices)
        {
            int index = 0;
            double probability = 0;
            double temp = 0;
            for(int i=0;i<ListOfDices.Count;++i)
            {
                temp = CalculateTotalProbability(ListOfDices, i);
                if ( temp> probability)
                {
                    index = i;
                    probability = temp;
                }
            }
            return index;
        }
        public static int WorseOption(List<List<int>> ListOfDices)
        {
            int index = 0;
            double probability = 1;
            double temp = 0;
            for (int i = 0; i < ListOfDices.Count; ++i)
            {
                temp = CalculateTotalProbability(ListOfDices, i);
                if (temp < probability)
                {
                    index = i;
                    probability = temp;
                }
            }
            return index;
        }
        public static double CalculateTotalProbability(List<List<int>> ListOfDices,int index) 
        {
            double probability = 1.0;
            foreach (var dice in ListOfDices)
                probability *= SelfProbability(ListOfDices[index], dice);
            return probability;
        }
        private static double SelfProbability(List<int> SelectedDice,List<int>OneOfDices)
        {
            double wins = 0;
            foreach (int a in SelectedDice)
                foreach (int b in OneOfDices)
                    if (a > b) wins++;
            return (double)wins / (SelectedDice.Count * OneOfDices.Count);
        }
    }
}
