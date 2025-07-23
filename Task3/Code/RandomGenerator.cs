using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Task3_DiceGame
{
    public static class RandomGenerator
    {
        public static int ChoiseOfComputer(int length)
        {
            //Random random = new Random();
            int computerMove = RandomNumberGenerator.GetInt32(0, length);
            return computerMove;
        }
    }
}
