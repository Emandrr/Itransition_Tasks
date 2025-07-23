
namespace Task3_DiceGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            var answer = InputValidator.ValidateCommandLineParams(args);
            if (answer.Item1=="false")
            {
                Console.WriteLine(answer.Item2);
                return;
            }
            Game game = new Game(args);
            
            game.Start();
        }
    }
}
