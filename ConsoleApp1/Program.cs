using Dx.SDK;
using PTlog;
using System.IO.Ports;
using System.Management;
using System.Text;

List<string> stringList = new List<string>();
if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
{
    ManagementObjectCollection objectCollection = new ManagementObjectSearcher("Select * from Win32_SerialPort").Get();
    foreach (ManagementObject managementObject in objectCollection)
    {
        if (managementObject["Name"].ToString().StartsWith("Silicon Labs CP210x"))
        {
            managementObject["Name"].ToString();
            int startIndex = managementObject["Name"].ToString().IndexOf("(") + 1;
            int length = managementObject["Name"].ToString().IndexOf(")") - startIndex;
            string str = managementObject["Name"].ToString().Substring(startIndex, length);
            if (!stringList.Contains(str))
                stringList.Add(str);
        }
    }
}
else if (System.Environment.OSVersion.Platform == PlatformID.Unix || System.Environment.OSVersion.Platform == PlatformID.MacOSX)
{
    stringList.Add("/dev/ttyUSB0");
}

if (stringList.Count > 0)
{
    Console.WriteLine("POWERTRONIC ECU FOUND: " + stringList[0]);
    Console.WriteLine("Starting connection sequence...");

    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "ECU_REAL_TIME_DATA",
        Method = new Action<object>((data) =>
        {
            RealTimeData d = data as RealTimeData;
            Console.WriteLine(d.Rpm + " RPM");
            Console.WriteLine(d.TpsForGraph + "% TPS");
            Console.WriteLine(d.AirTemp + "C Temp");
        })
    });
    ECUManager.Instance.AddHandler(new ECUSubscription()
    {
        Topic = "ECU_CONNECT_RESPONSE",
        Method = new Action<object>((data) =>
        {
            SimpleCommandResponsetData commandResponsetData = data as SimpleCommandResponsetData;
            //Parallel.Invoke(() => { this.writeCSV(data); });
            Console.WriteLine("CONNECT RESPONSE:");
            if (commandResponsetData.Success)
            {
                string str1 = Encoding.UTF8.GetString(commandResponsetData.logData);
                Console.WriteLine("Connection successful");
                Console.WriteLine("lblReceived: " + str1.Substring(1, 16));
                Console.WriteLine("lblHW1: " + str1.Substring(1, 8));
                Console.WriteLine("lblSW1: " + str1.Substring(9, 8));
                Console.WriteLine("lblHW2: " + str1.Substring(17, 8));
                Console.WriteLine("lblSW2: " + str1.Substring(25, 8));
                //Console.WriteLine("SERIAL: " + str1.Substring(33, 14));
                if (str1.Length > 34)
                {
                    string[] strArray = str1.Substring(47).Split('\n');
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (string str2 in strArray)
                        stringBuilder.Append(str2.ToString()).AppendLine();
                    Console.WriteLine("EXTRA: " + stringBuilder.ToString());
                }
                Console.WriteLine("Sending RealTime command to ECU...");
                ECUManager.Instance.ConfigHandler.RealTimeConfig = new RealTimeConfiguration()
                {
                    TpsFunction = new RealTimeFunction()
                    {
                        Expression = "x"
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
            Console.WriteLine(data.ToString());
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
        Console.WriteLine(stringList[0]);
        ECUManager.Instance.ECUConnect(stringList[0]);
        //ECUManager.Instance.StartRealTime();
        Console.WriteLine("CONNECT COMMAND SENT");
        Console.WriteLine(ECUManager.Instance.isRealTimeStarted);

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
    Console.WriteLine("No PowerTronic connected.");
}

