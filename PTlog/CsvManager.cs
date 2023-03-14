using CsvHelper;
using System.IO;

namespace PTlog
{
    public class CsvManager : IDisposable
    {
        private readonly string _filePath;
        private bool _hasHeader;
        private readonly Queue<object> _dataQueue;
        private readonly Thread _writerThread;
        private bool _disposed;

        public CsvManager()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PTlog.csv");
            if (File.Exists(_filePath))
                File.Delete(_filePath);
            _hasHeader = File.Exists(_filePath) && File.ReadLines(_filePath).GetEnumerator().MoveNext();
            _dataQueue = new Queue<object>();
            _writerThread = new Thread(WriteData);
            _writerThread.IsBackground = true;
            _writerThread.Start();
        }

        public void WriteHeader(object header)
        {
            lock (_dataQueue)
            {
                // Enqueue the header to be written
                _dataQueue.Enqueue(header);
                Monitor.PulseAll(_dataQueue);
            }
        }

        public void AppendRow(object data)
        {
            lock (_dataQueue)
            {
                // Enqueue the data to be written
                _dataQueue.Enqueue(data);
                Monitor.PulseAll(_dataQueue);
            }
        }

        private void WriteData()
        {
            while (true)
            {
                // Dequeue the next item to be written
                object data;
                lock (_dataQueue)
                {
                    if (_dataQueue.Count == 0)
                    {
                        Monitor.Wait(_dataQueue);
                        continue;
                    }
                    data = _dataQueue.Dequeue();
                }

                try
                {
                    // Write the data to the CSV file
                    using (var stream = File.Open(_filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    using (var writer = new StreamWriter(stream))
                    using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        RealTimeDataPoint point = data as RealTimeDataPoint;
                        if (!_hasHeader)
                        {
                            csv.WriteHeader(point.GetType());
                            Console.WriteLine("Writing header");
                            csv.NextRecord();
                            _hasHeader = true;
                        }
                        csv.WriteRecord(point);
                        csv.NextRecord();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine($"Failed to write data to CSV file: {e.Message}");
                }
                finally
                {
                    // Dispose of file-related objects
                    data = null;
                }
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                lock (_dataQueue)
                {
                    // Unblock the writer thread and let it finish
                    Monitor.PulseAll(_dataQueue);
                }

                _writerThread.Join();

                // Dispose of file-related objects
                _dataQueue.Clear();
            }
        }
    }
}