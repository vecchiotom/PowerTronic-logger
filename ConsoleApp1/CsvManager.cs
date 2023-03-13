using CsvHelper;

namespace PTlog
{
    public class CsvManager
    {
        private string _filePath;
        private bool _hasHeader;

        public CsvManager()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PTlog.csv");
            _hasHeader = File.Exists(_filePath) && File.ReadLines(_filePath).GetEnumerator().MoveNext();
        }

        public void WriteHeader(object header)
        {
            // Create a new CSV file and write the header to it
            using (var writer = new StreamWriter(_filePath))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteHeader(header.GetType());
                csv.NextRecord();
            }
            _hasHeader = true;
        }

        public void AppendRow(object data)
        {
            // Append a new row to an existing CSV file
            using (var stream = File.Open(_filePath, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                if (!_hasHeader)
                {
                    WriteHeader(data);
                }
                csv.WriteRecord(data);
                csv.NextRecord();
            }
        }
    }
}
