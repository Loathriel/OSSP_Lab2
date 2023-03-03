namespace OSSP_Lab2
{
    internal class Program
    {
        // Main function
        // Only loop to use program constatly
        // And files variable to store path to files
        static void Main(string[] args)
        {
            Logger.Start();
            Actions.PrintHelp();
            bool keepLooping = true;
            string[] files = FileScanner.SearchForFiles();
            while (keepLooping)
            {
                string[] resultOfAction = Array.Empty<string>();
                var input = Console.ReadLine();
                int.TryParse(input, out int mode);
                switch (mode)
                {
                    case 1:
                        resultOfAction = Actions.SearchFiles(out files);
                    break;

                    case 2:
                        resultOfAction = Actions.ShowFileNames(files);
                    break;

                    case 3:
                        resultOfAction = Actions.ShowFullPaths(files);
                    break;

                    case 4:
                        resultOfAction = Actions.ShowFilesInfo(files);
                    break;

                    case 5:
                        resultOfAction = Actions.ShowTextFromFile(files);
                    break;

                    case 6:
                        resultOfAction = Actions.SumNumbersInFiles(files);
                    break;

                    case 7:
                        Actions.ClearScrean();
                    break;

                    case 8:
                        Actions.PrintHelp();
                    break;

                    case 9:
                        keepLooping = false;
                    break;

                    default:
                        Console.WriteLine("Wrong input, try again or type 8 for help");
                        mode = 0;
                    break;
                }
                Console.WriteLine("===============================================================");
                Logger.LogActions(Actions.actionNames[mode], resultOfAction);
            }
        }
    }
}