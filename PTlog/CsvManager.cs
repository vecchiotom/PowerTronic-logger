using System.Collections.Generic;
using System.Threading;
using CsvHelper;

namespace PTlog
{
    public class CsvManager
    {
        private readonly string _filePath;
        private bool _hasHeader;
        private readonly Queue<object> _dataQueue;
        private readonly Thread _writerThread;

        public CsvManager()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PTlog.csv");
            _hasHeader = File.Exists(_filePath) && File.ReadLines(_filePath).GetEnumerator().MoveNext();
            _dataQueue = new Queue<object>();
            _writerThread = new Thread(WriteData);
            _writerThread.Start();
            //Console.WriteLine("Writer Thread started");
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
                //Console.WriteLine("entering lock");
                object data;
                lock (_dataQueue)
                {
                    if (_dataQueue.Count == 0)
                    {
                        Monitor.Wait(_dataQueue);
                        continue;
                    }
                    data = _dataQueue.Dequeue();
                    // Write the data to the CSV file
                    using (var stream = File.Open(_filePath, FileMode.Append))
                    using (var writer = new StreamWriter(stream))
                    using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        RealTimeDataPoint point = data as RealTimeDataPoint;
                        if (!_hasHeader)
                        {
                            csv.WriteHeader(point.GetType());
                            //Console.WriteLine("Writing header");
                            csv.NextRecord();
                            _hasHeader = true;
                        }
                        csv.WriteRecord(point);
                        csv.NextRecord();
                    }
                }
            }
        }
    }
}