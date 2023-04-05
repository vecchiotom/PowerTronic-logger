using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Globalization;

namespace PTlog
{
    internal class RS232
    {
        private SerialPort port;
        public  float latestAFR { get; private set; }
        public RS232(string _port) {
            port = new SerialPort(_port, 9600, Parity.None, 8, StopBits.One);
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.Open();

        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string _buffer = port.ReadLine();
            _buffer = _buffer.TrimEnd('\r', '\n');
            try
            {
                latestAFR = float.Parse(_buffer, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {

            }

        }

    }
}
