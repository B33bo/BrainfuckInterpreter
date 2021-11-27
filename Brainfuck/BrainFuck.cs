using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Brainfuck
{
    public static class BrainFuckInterpreter
    {
        public static int Delay = 0;
        private static byte[] cells = new byte[10];
        private static int Pointer;

        public static void Execute(string Code)
        {
            string Text = "";
            for (int i = 0; i < Code.Length; i++)
            {
                if (Pointer >= cells.Length)
                    cells = cells.Add<byte>(0);

                switch (Code[i])
                {
                    default:
                        break;
                    case '+':
                        cells[Pointer]++;
                        break;
                    case '-':
                        cells[Pointer]--;
                        break;
                    case '.':
                        Text += Convert.ToChar(cells[Pointer]);
                        break;
                    case ',':
                        string Userinput = Console.ReadLine();

                        if (Userinput.Length == 0)
                            break;
                        cells[Pointer] = (byte)Userinput[0];

                        Console.Clear();
                        break;
                    case '>':
                        Pointer++;
                        break;
                    case '<':
                        Pointer--;
                        break;
                    case '[':
                        i += HandleLoop(i, Code);
                        break;
                    case ']':
                        break;
                }

                if (Delay != 0)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = 1;
                    Console.Write(Text);

                    Console.CursorLeft = 0;
                    Console.CursorTop = 2;
                    PrintCells();

                    Thread.Sleep(Delay);
                }
            }

            Console.CursorLeft = 0;
            Console.CursorTop = 1;
            Console.Write(Text);

            Console.CursorLeft = 0;
            Console.CursorTop = 2;
            PrintCells();
        }

        private static void RunUntilZero(string Code)
        {
            Execute(Code);
            if (cells[Pointer] != 0)
                RunUntilZero(Code);
        }

        private static int HandleLoop(int index, string code)
        {
            int indentLevel = 0;
            string codeToExecute = "";

            for (int i = index + 1; i < code.Length; i++)
            {
                if (code[i] == '[')
                    indentLevel++;
                else if (code[i] == ']')
                {
                    if (indentLevel == 0)
                    {
                        RunUntilZero(codeToExecute);
                        return codeToExecute.Length;
                    }
                    indentLevel--;
                }

                codeToExecute += code[i];
            }

            return -1;
        }

        private static T[] Add<T>(this T[] array, T item)
        {
            T[] newArray = new T[array.Length+1];

            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            newArray[^1] = item;

            return newArray;
        }

        public static void PrintCells()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < cells.Length; i++)
            {
                if (i == Pointer)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write($"[{cells[i]}]");
                Console.ResetColor();
            }
        }
    }
}
