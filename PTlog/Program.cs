using Dx.SDK;
using PTlog;
using System.Reflection;
using System.Text;

DateTime now = DateTime.MinValue;
TimeSpan time;
int milliseconds = 0;
int elapsed = 0;
CsvManager csvManager = new CsvManager();
bool verbose = args.Contains("--verbose"), csv = args.Contains("--csv"), afr = args.Any(arg => arg.StartsWith("--afr="));
string searchName = afr ? args.First(arg => arg.StartsWith("--afr=")).Substring(6) : null;
List<string> stringList = SerialPortFixer.GetPortNames();
string afrPort = afr ? SerialPortFixer.GetPortNames(searchName)[0] : "";
RS232 rs232 = null;

if (!String.IsNullOrEmpty(afrPort))
{
    Console.WriteLine("FOUND RS232 ADAPTER: " + afrPort);
    rs232 = new RS232(afrPort);
    Parallel.Invoke(new Action(() => { while (true) { Console.WriteLine(rs232.latestAFR); } }));
}

if (stringList.Count > 0)
{
    Console.WriteLine("POWERTRONIC ECU FOUND: " + stringList[0]);
    if (verbose)
        Console.WriteLine("Starting connection sequence...");


    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "ECU_REAL_TIME_DATA",
        Method = new Action<object>((data) =>
        {
            RealTimeData d = data as RealTimeData;
            if (now == DateTime.MinValue)
            {
                now = DateTime.Now;
                time = now.TimeOfDay;
                milliseconds = (int)time.TotalMilliseconds;
                elapsed = 0;
            }
            else
            {
                int last = milliseconds;
                now = DateTime.Now;
                time = now.TimeOfDay;
                milliseconds = (int)time.TotalMilliseconds;
                elapsed += milliseconds - last;
            }
            if (csv)
            {
                csvManager.AppendRow(new RealTimeDataPoint(elapsed, d.Rpm, d.TpsForGraph));
            }
            if (verbose)
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, currentLineCursor - 1);
                Console.Write($"{elapsed}ms {d.Rpm} RPM {d.TpsForGraph}% TPS {d.AirTemp}C Temp {rs232.latestAFR}".PadRight(Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }

        })
    });
    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "ECU_CONNECT_RESPONSE",
        Method = new Action<object>((data) =>
        {
            SimpleCommandResponsetData commandResponsetData = data as SimpleCommandResponsetData;
            //Parallel.Invoke(() => { this.writeCSV(data); });
            if (commandResponsetData.Success)
            {
                string str1 = Encoding.UTF8.GetString(commandResponsetData.logData);
                if (verbose) 
                    Console.WriteLine(
                        "Connection successful\n" +
                        "lblReceived: " + str1.Substring(1, 16) + "\n" +
                        "lblHW1: " + str1.Substring(1, 8) + "\n" +
                        "lblSW1: " + str1.Substring(9, 8) + "\n" +
                        "lblHW2: " + str1.Substring(17, 8) + "\n" +
                        "lblSW2: " + str1.Substring(25, 8) + "\n"
                    );
                // Check if the length of str1 is greater than 34
                if (str1.Length > 34)
                {
                    // Get a substring starting at index 47, split the resulting string by newline characters, and store it in an array
                    string[] strArray = str1.Substring(47).Split('\n');

                    // Create a StringBuilder to construct the output string
                    StringBuilder stringBuilder = new StringBuilder();

                    // Iterate over the elements in the array and append them to the StringBuilder with a newline character
                    foreach (string str2 in strArray)
                        stringBuilder.Append(str2.ToString()).AppendLine();

                    // If verbose is true, print the output string with a prefix
                    if (verbose)
                        Console.WriteLine("EXTRA: " + stringBuilder.ToString());
                }
                if (verbose)
                    Console.WriteLine("Sending RealTime command to ECU...");
                ECUManager.Instance.ConfigHandler.RealTimeConfig = new RealTimeConfiguration()
                {
                    TpsFunction = new RealTimeFunction()
                    {
                        Expression = "(x-25)/180"
                    },
                    MapFunction = new RealTimeFunction()
                    {
                        Expression = "x"
                    },
                    AirTempFunction = new RealTimeFunction()
                    {
                        Expression = "x"
                    },
                    EngineTempFunction = new RealTimeFunction()
                    {
                        Expression = "x"
                    },
                    IgnitionFinalFunction = new RealTimeFunction()
                    {
                        Expression = "x"
                    },
                    InjectionCorrectedFunction = new RealTimeFunction()
                    {
                        Expression = "x"
                    },
                    InjectorDutyFunction = new RealTimeFunction()
                    {
                        Expression = "x"
                    },
                    EnableCustomFunctions = false
                };

                ECUManager.Instance.StartRealTime();
            }
            else
            {
                if (verbose)
                    Console.WriteLine("Connection failed");

            }

        })
    });
    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "ECU_NORMAL_CONNECT_TO_DEVICE_RESPONSE",
        Method = new Action<object>(ThreadHandler.handleConnect2)
    });
    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "SDK_ERROR",
        Method = new Action<object>(ThreadHandler.handleError)
    });
    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "ECU_TPS_REAL_TIME_DATA",
        Method = new Action<object>((data) =>
        {
        })
    });
    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "CONNECTION_ERROR",
        Method = new Action<object>(ThreadHandler.handleConnect2)
    });
    try
    {
        ECUManager.enableRealTime = true;
        ECUManager.Instance.ECUConnect(stringList[0]);
        //ECUManager.Instance.StartRealTime();
        if (verbose)
            Console.WriteLine("CONNECT COMMAND SENT");

        /*while (!ThreadHandler.communicating)
        {
            
        }*/
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message + "\n Connection to ECU failed.");
    }
    //ECUManager.enableRealTime = false;
    //ECUManager.Instance.Disconnect();
}
else
{
    if (verbose)
        Console.WriteLine("No PowerTronic connected.");
}

AppDomain.CurrentDomain.ProcessExit += (s, e) =>
{
    csvManager.Dispose();
};