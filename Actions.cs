namespace OSSP_Lab2
{
    internal class Actions
    {
        public static readonly Dictionary<int, string> actionNames = new Dictionary<int, string>
        {
            {0, "Wrong action used" },
            {1, "Scan for files" },
            {2, "Show file names" },
            {3, "Show full path of files" },
            {4, "Show info about files" },
            {5, "Show text from chosen file" },
            {6, "Count sum of numbers in chosen file" },
            {7, "Clear console screan" },
            {8, "Send help message" },
            {9, "Exit the program" }
        };
        public static string[] SearchFiles(out string[] files)
        {
            files = FileScanner.SearchForFiles();
            var resultOfAction = new string[] { $"{files.Length} files found" };
            Console.WriteLine(resultOfAction[0]);
            return resultOfAction;
        }

        public static string[] ShowFileNames(string[] files)
        {
            var resultOfAction = new string[files.Length];
            for (int i = 0; i < files.Length; ++i)
            {
                var fileName = files[i].Split("\\")[^1];
                resultOfAction[i] = fileName;
                Console.WriteLine($"{i + 1}. {fileName}");
            }
            return resultOfAction;
        }

        public static string[] ShowFullPaths(string[] files)
        {
            var resultOfAction = new string[files.Length];
            for (int i = 0; i < files.Length; ++i)
            {
                var fileName = files[i];
                resultOfAction[i] = fileName;
                Console.WriteLine($"{i + 1}. {fileName}");
            }
            return resultOfAction;
        }

        public static string[] ShowFilesInfo(string[] files)
        {
            var resultOfAction = new string[files.Length];
            for (int i = 0; i < files.Length; ++i)
            {
                var fileName = files[i];
                if (File.Exists(fileName) == false)
                    resultOfAction[i] = $"File {fileName} doesn't exist anymore";
                else
                    resultOfAction[i] = FileScanner.GetFileInfo(fileName);
                Console.Write($"{i + 1}. ");
                Console.WriteLine(resultOfAction[i]);
                Console.WriteLine();
            }
            return resultOfAction;
        }

        public static string[] ShowTextFromFile(string[] files)
        {
            Console.WriteLine("Type number of filename to read its content: ");
            ShowFileNames(files);
            var input = Console.ReadLine();

            if (int.TryParse(input, out int number) == false || number - 1 >= files.Length)
            {
                Console.WriteLine("Wrong input, reselect action to try again");
                return new string[] { "Wrong input" };
            }

            var fileName = files[number - 1];
            if (File.Exists(fileName) == false) 
            {
                Console.WriteLine("Sorry, but file doesn't exist anymore\nRescan files typing 1");
                return new string[] { "File doesn't exist" };
            }

            Console.WriteLine(files[number - 1]);
            Console.WriteLine(File.ReadAllText(fileName));

            return new string[] { "File contents shown" };
        }

        public static string[] SumNumbersInFiles(string[] files)
        {
            Console.WriteLine("Type number to use as default value for bad readings: ");
            var input = Console.ReadLine();
            if (float.TryParse(input, out float defaultValue) == false)
                Console.WriteLine("Wrong input, default value set to 0");

            var resultOfAction = new string[files.Length];
            for (int i = 0; i < files.Length; ++i)
            {
                var fileName = files[i];
                if (File.Exists(fileName) == false)
                    resultOfAction[i] = $"File {fileName} doesn't exist anymore";
                else
                    resultOfAction[i] = sumNumbersInFile(fileName, defaultValue);
                Console.Write($"{i + 1}. ");
                Console.WriteLine(resultOfAction[i]);
                Console.WriteLine();
            }
            return resultOfAction;
        }

        public static void ClearScrean()
        {
            Console.Clear();
            PrintHelp();
        }

        public static void PrintHelp()
        {
            Console.WriteLine("Type number of action you want to use and press enter afterwards");
            Console.WriteLine("1 - Scan working folder for .dat files (program pre-scans when launched)");
            Console.WriteLine("2 - Print file name that were found");
            Console.WriteLine("3 - Show full paths to found files");
            Console.WriteLine("4 - Show information about found files");
            Console.WriteLine("5 - Show text from file (user will be prompted to pick number corresponding to a file)");
            Console.WriteLine("6 - Count the sum of numbers in found files");
            Console.WriteLine("7 - Clear screan and print this message");
            Console.WriteLine("8 - Print this message again");
            Console.WriteLine("9 - Close the program");
        }

        private static string sumNumbersInFile(string fileName, float defaultVaule)
        {
            float sum = 0;
            var contents = File.ReadAllText(fileName).Split(new char[] { ' ', '\n', ';' });
            foreach (string line in contents)
            {
                if (line.Length == 0)
                    continue;
                if (float.TryParse(line, out float value) == false)
                {
                    Console.WriteLine($"Replaced {line} with {defaultVaule}");
                    value = defaultVaule;
                }
                sum += value;
            }

            return $"{fileName} : {sum}";
        }
    }
}
