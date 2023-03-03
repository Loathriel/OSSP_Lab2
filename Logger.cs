namespace OSSP_Lab2
{
    internal class Logger
    {
        private static readonly string fileName = "Logs\\log_" + DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
        private static Logger? Instance;

        private Logger() 
        {
            if (Directory.Exists("Logs") == false)
                Directory.CreateDirectory("Logs");
            using (StreamWriter sw = File.CreateText(fileName))
                sw.WriteLog("Program launched\r\n");
        }

        public static void LogActions(string actionName, string[] results)
        {
            var logText = new System.Text.StringBuilder();
            logText.Append("Called action: ");
            logText.AppendLine(actionName);
            if (results.Length > 0)
            {
                logText.AppendLine("Result: ");
                foreach (string result in results)
                    logText.AppendLine(result);
            }
            using (StreamWriter sw = File.AppendText(fileName))
                sw.WriteLog(logText.ToString());
        }

        public static void Start() 
        {
            if (Instance == null)
                Instance = new Logger();
        }
    }

    static class StreamWriterExtensions
    {
        private const string lineSeparator = "================================\r\n";
        private const string timeFormat = "yyyy-MM-dd HH:mm:ss";

        public static void WriteLog(this StreamWriter fileStreamWriter, string text)
        {
            fileStreamWriter.WriteLine(DateTime.Now.ToString(timeFormat));
            fileStreamWriter.WriteLine(text);
            fileStreamWriter.WriteLine(lineSeparator);
        }
    }
}
