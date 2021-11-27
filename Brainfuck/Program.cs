using System;
using Brainfuck;

namespace Brainfuck
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (args.Length >= 2)
                    if (int.TryParse(args[1], out int delay))
                        BrainFuckInterpreter.Delay = delay;

                BrainFuckInterpreter.Execute(args[0]);
                return;
            }

            Console.WriteLine("Enter brainfuck::");
            
            string userinput = Console.ReadLine();

            if (userinput.ToLower().StartsWith("delay="))
            {
                //When the user enters a delay, you have to ask for the brainfuck again

                if (int.TryParse(userinput.Split('=')[1], out int delay))
                    BrainFuckInterpreter.Delay = delay;
                else
                    Console.WriteLine("Invalid delay");

                Console.WriteLine("Enter the brainfuck code::");
                userinput = Console.ReadLine();
            }

            Console.Clear();
            BrainFuckInterpreter.Execute(userinput);

            Console.ReadLine();
        }
    }
}
