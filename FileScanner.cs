namespace OSSP_Lab2
{
    internal class FileScanner
    {
        private static readonly string workingPath = Directory.GetCurrentDirectory(); // variable with full path to working folder

        // Set default values for the task
        public static string[] SearchForFiles(string extension = "*.dat") => SearchForFiles(extension, new string[] { "problem" });

        // Actual function that searches the working folder, as well as all subpaths provided
        // for files with given extension
        public static string[] SearchForFiles(string extension, string[] subpaths)
        {
            var foundFiles = Directory.EnumerateFiles(workingPath, extension);
            foreach (string subpath in subpaths)
            {
                var fullSubpath = Path.Combine(workingPath, subpath);
                if (Directory.Exists(fullSubpath))
                {
                    var filesInSubpath = Directory.EnumerateFiles(fullSubpath, extension);
                    foundFiles = foundFiles.Union(filesInSubpath);
                }
            }
            
            return foundFiles.ToArray();
        }

        // Creates a string containing all info about a file
        public static string GetFileInfo(string fileName)
        {
            const string timeFormat = "yyyy-MM-dd HH:mm:ss";

            if (File.Exists(fileName) == false)
                return $"File {fileName} doesn't exist anymore";

            var fileInfo = new FileInfo(fileName);
            var fileInfoInText = new System.Text.StringBuilder();

            fileInfoInText.Append("File name: ");
            fileInfoInText.AppendLine(fileInfo.Name);

            fileInfoInText.Append("File extension: ");
            fileInfoInText.AppendLine(fileInfo.Extension);

            fileInfoInText.Append("Location on disc: ");
            fileInfoInText.AppendLine(fileInfo.DirectoryName);

            fileInfoInText.Append("Size in bytes: ");
            fileInfoInText.AppendLine(fileInfo.Length.ToString());

            fileInfoInText.Append("Created: ");
            fileInfoInText.AppendLine(fileInfo.CreationTime.ToString(timeFormat));

            fileInfoInText.Append("Last opened: ");
            fileInfoInText.AppendLine(fileInfo.LastAccessTime.ToString(timeFormat));

            fileInfoInText.Append("Last changed: ");
            fileInfoInText.AppendLine(fileInfo.LastWriteTime.ToString(timeFormat));

            fileInfoInText.Append("Is read only: ");
            fileInfoInText.AppendLine(fileInfo.IsReadOnly.ToString());

            string isHidden = ((int)fileInfo.Attributes / 2 % 2 == 1).ToString();
            fileInfoInText.Append("Is hidden: ");
            fileInfoInText.AppendLine(isHidden);

            return fileInfoInText.ToString();
        }
    }
}
