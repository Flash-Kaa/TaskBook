using System;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace ZadachniCK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int topDisplay, leftDisplay = 4;

            while (true)
            {
                topDisplay = 0;
                WriteWithIndent("#### _commands_ ####");
                WriteWithIndent("[open]\t[create]");
                WriteWithIndent("");

                switch (ReadCommand())
                {
                    case "create":
                        CreateTasks();
                        break;

                    case "open":
                        OpenTasks();
                        break;

                    default:
                        Console.WriteLine("Error");
                        break;
                }

                Console.WriteLine("Press to continue");
                Console.ReadLine();
                Console.Clear();
            }

            void WriteWithIndent(string text)
            {
                Console.SetCursorPosition(leftDisplay, ++topDisplay);
                Console.WriteLine(text);
            }
        }

        private static string ReadCommand()
        {
            Console.Write(" => ");
            return Console.ReadLine().Trim().ToLower();
        }

        private static void CreateTasks()
        {
            var filePath = ChooseFilePath();
            new FileInfo(filePath).Create().Close();

            Process.Start(filePath).WaitForExit();

            Console.WriteLine("File created");
        }

        private static string ChooseFilePath()
        {
            var date = "";
            do
            {
                Console.WriteLine("\n\nEnter date for your tasks:");
                date = ReadCommand();
            } while (date == "" || date.Any(b => Char.IsLetter(b)) || !date.Contains('.'));

            Console.WriteLine("Date choose");

            return $@"C:\Users\admin\OneDrive\Рабочий стол\FWorld\FiLeS\Tasks\{date}.txt";
        }

        private static void OpenTasks()
        {
            var filePath = ChooseFilePath();

            var fileText = File.ReadAllText(filePath).Split('\n');
            for(int i = 0; i < fileText.Length; i++)
                Console.WriteLine((i + 1) + ")\t" + fileText[i]);
        }
    }
}