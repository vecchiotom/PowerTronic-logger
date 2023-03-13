using Dx.SDK;
using System.Text;

namespace PTlog
{
    internal class ThreadHandler
    {
        public static bool communicating = false;

        public ThreadHandler()
        {
            // TODO
        }

        public static void handleRealTime(object data)
        {
            //Parallel.Invoke(() => { this.writeCSV(data); });
            Console.WriteLine(data.ToString());
        }

        private static byte[] FormatResponse(byte[] data)
        {
            byte[] numArray = new byte[data.Length - 2];
            int num = 0;
            for (int index = 1; index < data.Length - 1; ++index)
                numArray[num++] = data[index];
            return numArray;
        }

        public static void handleConnect(object data)
        {
        }
        public static void handleConnect2(object data)
        {
            ThreadHandler.communicating = true;
            //Parallel.Invoke(() => { this.writeCSV(data); });
            Console.WriteLine(data.ToString());
        }

        public static void handleError(object data)
        {
            ThreadHandler.communicating = true;
            //Parallel.Invoke(() => { this.writeCSV(data); });
            Console.WriteLine(data.ToString());
        }

        private void writeCSV(object data)
        {
            //using (var writer = new StreamWriter("path\\to\\file.csv")) ;
        }
    }
}
