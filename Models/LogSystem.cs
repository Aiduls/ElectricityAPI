namespace ElectricityAPI1.Models
{
    public static class LogSystem
    {
        public static string fileName = $"Logs/log{DateTime.Now:yyyyMMddHHmmss}.txt";
        public static void init()
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, true))
            {
                streamWriter.WriteLine($"START: App started running at {DateTime.Now}. Be sure to install and run all the prerequisites.");
                streamWriter.Close();
            }
        }
        public static void log(string message, string title = "")
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, true))
            {
                streamWriter.WriteLine($"{DateTime.Now} Info: {title} {message}");
                streamWriter.Close();
            }
        }
        public static void logWarning(string message, string title = "")
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, true))
            {
                streamWriter.WriteLine($"{DateTime.Now} Warning: {title} {message}");
                streamWriter.Close();
            }
        }
        public static void logError(string message, string title = "")
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, true))
            {
                streamWriter.WriteLine($"{DateTime.Now} ERROR: {title} {message}");
                streamWriter.Close();
            }
        }
    }
}
