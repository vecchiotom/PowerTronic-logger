// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ECUData
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Dx.SDK
{
    public class ECUData
    {
        private const long rpmConstant = 30000000;
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(ECUData));
        public static bool blnLoadData2;
        private List<float> fuel1DataArray;
        private List<float> fuel2DataArray;
        private List<float> fuel3DataArray;
        private List<int> ignition1DataArray;
        private List<int> ignition2DataArray;
        private List<int> ignition3DataArray;
        private float spMap1;
        private float spMap2;
        private float spMap3;
        private float spMap4;
        private int spMap5;
        private int spMap6;
        private int spMap7;
        private int spMap8;
        private List<int> ignition4DataArray;
        private List<int> calibDataArray1;
        private List<int> calibDataArray2;
        private List<int> coolantTempInjectionArray;
        private List<int> coolantTempIgnitionArray;
        private List<int> airTempInjectionArray;
        private List<int> airTempIgnitionArray;
        private List<float> batteryInjectorOpeningTimeArray;
        private List<float> batteryIgnitionDwellTimeArray;
        private List<int> accelerationEnrichArray;
        private int limiterRpm;
        private int launchRpm;
        private int vtecRpm;
        private int injectionOverallTrim;
        private int limiterMode;
        private List<int> idleControlArray;
        private int configurationMode;
        private int driver1TriggerRpm;
        private int driver2TriggerRpm;
        private int driver3TriggerRpm;
        private int idleBase;
        private int idleCrankingDelta;
        private float throttleIdleIncrease;
        private int liftOffIdleIncrease;
        private int idlePeriod;
        private int idleClkp;
        private int idleClks;
        private float acIdleIncrease;
        private int acInjectionTrim;
        private int acIgnitionTrim;
        private List<int> altitudeInjectionCorrectionArray;
        private List<int> altitudeIgnitionCorrectionArray;
        private int idleControl1;
        private int qsType;
        private int idleControl2;
        private int idleControl3;
        private int idleControl4;
        private int idleControl5;
        private int idleControl6;
        private int idleControl7;
        private int idleControl8;
        private int idleControl9;
        private int idleControl10;
        private Serial serial = new Serial();

        public bool blnLoadData { get; set; }

        public List<float> Fuel1DataArray
        {
            get => this.fuel1DataArray;
            set => this.fuel1DataArray = value;
        }

        public List<float> Fuel2DataArray
        {
            get => this.fuel2DataArray;
            set => this.fuel2DataArray = value;
        }

        public List<float> Fuel3DataArray
        {
            get => this.fuel3DataArray;
            set => this.fuel3DataArray = value;
        }

        public List<int> Ignition1DataArray
        {
            get => this.ignition1DataArray;
            set => this.ignition1DataArray = value;
        }

        public List<int> Ignition2DataArray
        {
            get => this.ignition2DataArray;
            set => this.ignition2DataArray = value;
        }

        public List<int> Ignition3DataArray
        {
            get => this.ignition3DataArray;
            set => this.ignition3DataArray = value;
        }

        public float SpMap1
        {
            get => this.spMap1;
            set => this.spMap1 = value;
        }

        public float SpMap2
        {
            get => this.spMap2;
            set => this.spMap2 = value;
        }

        public float SpMap3
        {
            get => this.spMap3;
            set => this.spMap3 = value;
        }

        public float SpMap4
        {
            get => this.spMap4;
            set => this.spMap4 = value;
        }

        public int SpMap5
        {
            get => this.spMap5;
            set => this.spMap5 = value;
        }

        public int SpMap6
        {
            get => this.spMap6;
            set => this.spMap6 = value;
        }

        public int SpMap7
        {
            get => this.spMap7;
            set => this.spMap7 = value;
        }

        public int SpMap8
        {
            get => this.spMap8;
            set => this.spMap8 = value;
        }

        public int spMapDummy1 { get; set; }

        public int spMapDummy2 { get; set; }

        public List<int> Ignition4DataArray
        {
            get => this.ignition4DataArray;
            set => this.ignition4DataArray = value;
        }

        public List<int> CalibDataArray1
        {
            get => this.calibDataArray1;
            set => this.calibDataArray1 = value;
        }

        public byte[] MapDecr { get; set; }

        public List<int> CalibDataArray2
        {
            get => this.calibDataArray2;
            set => this.calibDataArray2 = value;
        }

        public List<int> CoolantTempInjectionArray
        {
            get => this.coolantTempInjectionArray;
            set => this.coolantTempInjectionArray = value;
        }

        public List<int> CoolantTempIgnitionArray
        {
            get => this.coolantTempIgnitionArray;
            set => this.coolantTempIgnitionArray = value;
        }

        public List<int> AirTempInjectionArray
        {
            get => this.airTempInjectionArray;
            set => this.airTempInjectionArray = value;
        }

        public List<int> AirTempIgnitionArray
        {
            get => this.airTempIgnitionArray;
            set => this.airTempIgnitionArray = value;
        }

        public List<float> BatteryInjectorOpeningTimeArray
        {
            get => this.batteryInjectorOpeningTimeArray;
            set => this.batteryInjectorOpeningTimeArray = value;
        }

        public List<float> BatteryIgnitionDwellTimeArray
        {
            get => this.batteryIgnitionDwellTimeArray;
            set => this.batteryIgnitionDwellTimeArray = value;
        }

        public List<int> AccelerationEnrichArray
        {
            get => this.accelerationEnrichArray;
            set => this.accelerationEnrichArray = value;
        }

        public int LimiterRpm
        {
            get => this.limiterRpm;
            set => this.limiterRpm = value;
        }

        public int LaunchRpm
        {
            get => this.launchRpm;
            set => this.launchRpm = value;
        }

        public int VtecRpm
        {
            get => this.vtecRpm;
            set => this.vtecRpm = value;
        }

        public int InjectionOverallTrim
        {
            get => this.injectionOverallTrim;
            set => this.injectionOverallTrim = value;
        }

        public int LimiterMode
        {
            get => this.limiterMode;
            set => this.limiterMode = value;
        }

        public List<int> IdleControlArray
        {
            get => this.idleControlArray;
            set => this.idleControlArray = value;
        }

        public int ConfigurationMode
        {
            get => this.configurationMode;
            set => this.configurationMode = value;
        }

        public int Driver1TriggerRpm
        {
            get => this.driver1TriggerRpm;
            set => this.driver1TriggerRpm = value;
        }

        public int Driver2TriggerRpm
        {
            get => this.driver2TriggerRpm;
            set => this.driver2TriggerRpm = value;
        }

        public int Driver3TriggerRpm
        {
            get => this.driver3TriggerRpm;
            set => this.driver3TriggerRpm = value;
        }

        public int IdleBase
        {
            get => this.idleBase;
            set => this.idleBase = value;
        }

        public int IdleCrankingDelta
        {
            get => this.idleCrankingDelta;
            set => this.idleCrankingDelta = value;
        }

        public float ThrottleIdleIncrease
        {
            get => this.throttleIdleIncrease;
            set => this.throttleIdleIncrease = value;
        }

        public int LiftOffIdleIncrease
        {
            get => this.liftOffIdleIncrease;
            set => this.liftOffIdleIncrease = value;
        }

        public int IdlePeriod
        {
            get => this.idlePeriod;
            set => this.idlePeriod = value;
        }

        public int IdleClkp
        {
            get => this.idleClkp;
            set => this.idleClkp = value;
        }

        public int IdleClks
        {
            get => this.idleClks;
            set => this.idleClks = value;
        }

        public float AcIdleIncrease
        {
            get => this.acIdleIncrease;
            set => this.acIdleIncrease = value;
        }

        public int AcInjectionTrim
        {
            get => this.acInjectionTrim;
            set => this.acInjectionTrim = value;
        }

        public int AcIgnitionTrim
        {
            get => this.acIgnitionTrim;
            set => this.acIgnitionTrim = value;
        }

        public List<int> AltitudeInjectionCorrectionArray
        {
            get => this.altitudeInjectionCorrectionArray;
            set => this.altitudeInjectionCorrectionArray = value;
        }

        public List<int> AltitudeIgnitionCorrectionArray
        {
            get => this.altitudeIgnitionCorrectionArray;
            set => this.altitudeIgnitionCorrectionArray = value;
        }

        public int IdleControl1
        {
            get => this.idleControl1;
            set => this.idleControl1 = value;
        }

        public int QsType
        {
            get => this.qsType;
            set => this.qsType = value;
        }

        public int IdleControl2
        {
            get => this.idleControl2;
            set => this.idleControl2 = value;
        }

        public int IdleControl3
        {
            get => this.idleControl3;
            set => this.idleControl3 = value;
        }

        public int IdleControl4
        {
            get => this.idleControl4;
            set => this.idleControl4 = value;
        }

        public int IdleControl5
        {
            get => this.idleControl5;
            set => this.idleControl5 = value;
        }

        public int IdleControl6
        {
            get => this.idleControl6;
            set => this.idleControl6 = value;
        }

        public int IdleControl7
        {
            get => this.idleControl7;
            set => this.idleControl7 = value;
        }

        public int IdleControl8
        {
            get => this.idleControl8;
            set => this.idleControl8 = value;
        }

        public int IdleControl9
        {
            get => this.idleControl9;
            set => this.idleControl9 = value;
        }

        public int IdleControl10
        {
            get => this.idleControl10;
            set => this.idleControl10 = value;
        }

        public List<byte> tempProp { get; set; }

        public List<byte> getByteArray()
        {
            List<byte> byteArray = new List<byte>();
            try
            {
                foreach (double fuel1Data in this.fuel1DataArray)
                {
                    float num = (float)((fuel1Data + 25.0) * 5.0);
                    byteArray.Add(Convert.ToByte(num));
                }
                foreach (int ignition1Data in this.ignition1DataArray)
                    byteArray.Add((byte)(ignition1Data + 25));
                foreach (int coolantTempInjection in this.coolantTempInjectionArray)
                    byteArray.Add((byte)(coolantTempInjection + 100));
                foreach (int coolantTempIgnition in this.CoolantTempIgnitionArray)
                    byteArray.Add((byte)(coolantTempIgnition + 100));
                foreach (int airTempInjection in this.airTempInjectionArray)
                    byteArray.Add((byte)(airTempInjection + 100));
                foreach (int airTempIgnition in this.airTempIgnitionArray)
                    byteArray.Add((byte)(airTempIgnition + 100));
                foreach (float injectorOpeningTime in this.batteryInjectorOpeningTimeArray)
                {
                    float num = injectorOpeningTime / 100f;
                    byteArray.Add(Convert.ToByte(num));
                }
                foreach (float ignitionDwellTime in this.batteryIgnitionDwellTimeArray)
                {
                    float num = ignitionDwellTime * 10f;
                    byteArray.Add(Convert.ToByte(num));
                }
                foreach (int accelerationEnrich in this.accelerationEnrichArray)
                    byteArray.Add((byte)accelerationEnrich);
                byte[] numArray1 = new byte[2];
                if (this.limiterRpm == 0)
                    this.limiterRpm = 1;
                long num1 = 30000000L / (long)this.limiterRpm;
                int num2 = (int)num1 & (int)byte.MaxValue;
                int num3 = (int)(num1 >> 8) & (int)byte.MaxValue;
                numArray1[0] = (byte)num3;
                numArray1[1] = (byte)num2;
                byteArray.Add(numArray1[0]);
                byteArray.Add(numArray1[1]);
                if (this.launchRpm == 0)
                    this.launchRpm = 1;
                long num4 = 30000000L / (long)this.launchRpm;
                int num5 = (int)num4 & (int)byte.MaxValue;
                int num6 = (int)(num4 >> 8) & (int)byte.MaxValue;
                numArray1[0] = (byte)num6;
                numArray1[1] = (byte)num5;
                byteArray.Add(numArray1[0]);
                byteArray.Add(numArray1[1]);
                byte[] numArray2 = new byte[2];
                if (this.vtecRpm == 0)
                    this.vtecRpm = 1;
                long num7 = 30000000L / (long)this.vtecRpm;
                int num8 = (int)num7 & (int)byte.MaxValue;
                int num9 = (int)(num7 >> 8) & (int)byte.MaxValue;
                numArray2[0] = (byte)num9;
                numArray2[1] = (byte)num8;
                byteArray.Add(numArray2[0]);
                byteArray.Add(numArray2[1]);
                byteArray.Add((byte)(this.injectionOverallTrim + 100));
                byteArray.Add((byte)this.limiterMode);
                byteArray.Add((byte)this.QsType);
                byteArray.Add((byte)this.idleControl1);
                byteArray.Add((byte)this.idleControl2);
                byteArray.Add((byte)this.idleControl3);
                byteArray.Add((byte)this.idleControl4);
                byteArray.Add((byte)this.idleControl5);
                byteArray.Add((byte)this.idleControl6);
                byteArray.Add((byte)this.idleControl7);
                byteArray.Add((byte)this.idleControl8);
                byteArray.Add((byte)this.idleControl9);
                byteArray.Add((byte)this.idleControl10);
                byteArray.Add((byte)this.configurationMode);
                byteArray.Add((byte)this.driver1TriggerRpm);
                byteArray.Add((byte)this.driver2TriggerRpm);
                byteArray.Add((byte)this.driver3TriggerRpm);
                byteArray.Add((byte)this.idleBase);
                byteArray.Add((byte)this.idleCrankingDelta);
                byteArray.Add((byte)this.throttleIdleIncrease);
                byteArray.Add((byte)this.liftOffIdleIncrease);
                byteArray.Add((byte)this.idlePeriod);
                byteArray.Add((byte)this.idleClkp);
                byteArray.Add((byte)this.idleClks);
                byteArray.Add((byte)this.acIdleIncrease);
                byteArray.Add((byte)this.acInjectionTrim);
                byteArray.Add((byte)(this.acIgnitionTrim + 100));
                for (int index = 0; index < 10; ++index)
                    byteArray.Add((byte)(this.altitudeInjectionCorrectionArray[index] + 100));
                for (int index = 0; index < 10; ++index)
                    byteArray.Add((byte)(this.altitudeIgnitionCorrectionArray[index] + 100));
                foreach (double fuel2Data in this.fuel2DataArray)
                {
                    float num10 = (float)((fuel2Data + 25.0) * 5.0);
                    byteArray.Add(Convert.ToByte(num10));
                }
                foreach (int ignition2Data in this.ignition2DataArray)
                    byteArray.Add((byte)(ignition2Data + 25));
                foreach (float fuel3Data in this.fuel3DataArray)
                    byteArray.Add(Convert.ToByte(fuel3Data));
                foreach (int ignition3Data in this.ignition3DataArray)
                    byteArray.Add((byte)ignition3Data);
                foreach (int ignition4Data in this.ignition4DataArray)
                    byteArray.Add((byte)(ignition4Data + 25));
                foreach (int num11 in this.calibDataArray1)
                    byteArray.Add((byte)num11);
                foreach (int num12 in this.calibDataArray2)
                    byteArray.Add((byte)num12);
                byteArray.Add((byte)((double)this.spMap1 * 10.0 + 100.0));
                byteArray.Add((byte)((double)this.spMap2 * 10.0 + 100.0));
                byteArray.Add((byte)((double)this.spMap3 * 10.0 + 100.0));
                byteArray.Add((byte)((double)this.spMap4 * 10.0 + 100.0));
                byteArray.Add((byte)(this.spMap5 + 100));
                byteArray.Add((byte)(this.spMap6 + 100));
                byteArray.Add((byte)(this.spMap7 + 100));
                byteArray.Add((byte)(this.spMap8 + 100));
                byteArray.Add((byte)this.spMapDummy1);
                byteArray.Add((byte)this.spMapDummy2);
                byte[] mapDecr = this.MapDecr;
                foreach (byte num13 in mapDecr)
                    byteArray.Add(num13);
                int num14 = 100 - mapDecr.Length;
                for (int index = 0; index < num14; ++index)
                    byteArray.Add((byte)0);
                return byteArray;
            }
            catch (Exception ex)
            {
                ECUData.logger.Error((object)"Error converting object to byte array", ex);
                throw ex;
            }
        }

        public ECUData()
        {
            this.fuel1DataArray = new List<float>();
            this.fuel2DataArray = new List<float>();
            this.fuel3DataArray = new List<float>();
            this.ignition1DataArray = new List<int>();
            this.ignition2DataArray = new List<int>();
            this.ignition3DataArray = new List<int>();
            this.ignition4DataArray = new List<int>();
            this.calibDataArray1 = new List<int>();
            this.calibDataArray2 = new List<int>();
            this.coolantTempInjectionArray = new List<int>();
            this.coolantTempIgnitionArray = new List<int>();
            this.airTempInjectionArray = new List<int>();
            this.airTempIgnitionArray = new List<int>();
            this.batteryInjectorOpeningTimeArray = new List<float>();
            this.batteryIgnitionDwellTimeArray = new List<float>();
            this.accelerationEnrichArray = new List<int>();
            this.idleControlArray = new List<int>();
            this.altitudeInjectionCorrectionArray = new List<int>();
            this.AltitudeIgnitionCorrectionArray = new List<int>();
        }

        public bool SaveToFile(string filePath)
        {
            FileStream fileStream = (FileStream)null;
            try
            {
                fileStream = File.Open(filePath, FileMode.OpenOrCreate);
                if (fileStream == null)
                    return false;
                byte[] array = this.getByteArray().ToArray();
                if (array.Length > 0 && array.Length == 2493)
                    fileStream.Write(array, 0, 2493);
                return true;
            }
            catch (Exception ex)
            {
                if (ECUData.logger.IsErrorEnabled)
                    ECUData.logger.Error((object)"Error saving file:", ex);
                return false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        public bool LoadOldSystemFile(string filePath)
        {
            try
            {
                List<string> stringList1 = new List<string>();
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string str;
                    while ((str = streamReader.ReadLine()) != null)
                        stringList1.Add(str);
                }
                if (stringList1.Count <= 0)
                    return false;
                int num1 = 0;
                this.fuel1DataArray = new List<float>();
                for (int index = 15; index >= 0; --index)
                {
                    string[] strArray = stringList1[index].Split(',');
                    List<string> stringList2 = new List<string>();
                    foreach (string str in strArray)
                        stringList2.Add(str);
                    if (stringList2.Count >= 20)
                    {
                        stringList2.Reverse();
                        foreach (string s in stringList2)
                        {
                            if (s.Trim().Length > 0)
                                this.fuel1DataArray.Add(float.Parse(s));
                        }
                    }
                }
                num1 = 15;
                this.ignition1DataArray = new List<int>();
                for (int index = 32; index >= 16; --index)
                {
                    string[] strArray = stringList1[index].Split(',');
                    List<string> stringList3 = new List<string>();
                    foreach (string str in strArray)
                        stringList3.Add(str);
                    if (stringList3.Count >= 20)
                    {
                        stringList3.Reverse();
                        foreach (string s in stringList3)
                        {
                            if (s.Trim().Length > 0)
                                this.ignition1DataArray.Add(int.Parse(s));
                        }
                    }
                }
                int num2 = 32;
                this.coolantTempInjectionArray = new List<int>();
                List<string> stringList4 = stringList1;
                int index1 = num2;
                int num3 = index1 + 1;
                string[] strArray1 = stringList4[index1].Split(',');
                if (strArray1.Length >= 10)
                {
                    foreach (string s in strArray1)
                    {
                        if (s.Trim().Length > 0)
                            this.coolantTempInjectionArray.Add(int.Parse(s));
                    }
                }
                this.coolantTempIgnitionArray = new List<int>();
                List<string> stringList5 = stringList1;
                int index2 = num3;
                int num4 = index2 + 1;
                string[] strArray2 = stringList5[index2].Split(',');
                if (strArray2.Length >= 10)
                {
                    foreach (string s in strArray2)
                    {
                        if (s.Trim().Length > 0)
                            this.coolantTempIgnitionArray.Add(int.Parse(s));
                    }
                }
                this.airTempInjectionArray = new List<int>();
                List<string> stringList6 = stringList1;
                int index3 = num4;
                int num5 = index3 + 1;
                string[] strArray3 = stringList6[index3].Split(',');
                if (strArray3.Length >= 10)
                {
                    foreach (string s in strArray3)
                    {
                        if (s.Trim().Length > 0)
                            this.airTempInjectionArray.Add(int.Parse(s));
                    }
                }
                this.airTempIgnitionArray = new List<int>();
                List<string> stringList7 = stringList1;
                int index4 = num5;
                int num6 = index4 + 1;
                string[] strArray4 = stringList7[index4].Split(',');
                if (strArray4.Length >= 10)
                {
                    foreach (string s in strArray4)
                    {
                        if (s.Trim().Length > 0)
                            this.airTempIgnitionArray.Add(int.Parse(s));
                    }
                }
                this.batteryInjectorOpeningTimeArray = new List<float>();
                List<string> stringList8 = stringList1;
                int index5 = num6;
                int num7 = index5 + 1;
                string[] strArray5 = stringList8[index5].Split(',');
                if (strArray5.Length >= 10)
                {
                    foreach (string s in strArray5)
                    {
                        if (s.Trim().Length > 0)
                            this.batteryInjectorOpeningTimeArray.Add(float.Parse(s));
                    }
                }
                this.batteryIgnitionDwellTimeArray = new List<float>();
                List<string> stringList9 = stringList1;
                int index6 = num7;
                int num8 = index6 + 1;
                string[] strArray6 = stringList9[index6].Split(',');
                if (strArray6.Length >= 10)
                {
                    foreach (string s in strArray6)
                    {
                        if (s.Trim().Length > 0)
                            this.batteryIgnitionDwellTimeArray.Add(float.Parse(s));
                    }
                }
                this.accelerationEnrichArray = new List<int>();
                List<string> stringList10 = stringList1;
                int index7 = num8;
                int num9 = index7 + 1;
                string[] strArray7 = stringList10[index7].Split(',');
                if (strArray7.Length >= 10)
                {
                    foreach (string s in strArray7)
                    {
                        if (s.Trim().Length > 0)
                            this.accelerationEnrichArray.Add(int.Parse(s));
                    }
                }
                List<string> stringList11 = stringList1;
                int index8 = num9;
                int num10 = index8 + 1;
                string s1 = stringList11[index8];
                if (s1.Trim().Length > 0)
                    this.limiterRpm = int.Parse(s1);
                List<string> stringList12 = stringList1;
                int index9 = num10;
                int num11 = index9 + 1;
                string s2 = stringList12[index9];
                if (s2.Trim().Length > 0)
                    this.launchRpm = int.Parse(s2);
                List<string> stringList13 = stringList1;
                int index10 = num11;
                int num12 = index10 + 1;
                string s3 = stringList13[index10];
                if (s3.Trim().Length > 0)
                    this.vtecRpm = int.Parse(s3);
                List<string> stringList14 = stringList1;
                int index11 = num12;
                int num13 = index11 + 1;
                string s4 = stringList14[index11];
                if (s4.Trim().Length > 0)
                    this.limiterMode = int.Parse(s4);
                List<string> stringList15 = stringList1;
                int index12 = num13;
                int num14 = index12 + 1;
                string s5 = stringList15[index12];
                if (s5.Trim().Length > 0)
                    this.injectionOverallTrim = int.Parse(s5);
                int num15 = num14 + 5;
                this.idleControlArray = new List<int>();
                List<string> stringList16 = stringList1;
                int index13 = num15;
                int num16 = index13 + 1;
                string[] strArray8 = stringList16[index13].Split(',');
                if (strArray8.Length >= 10)
                {
                    foreach (string s6 in strArray8)
                    {
                        if (s6.Trim().Length > 0)
                            this.idleControlArray.Add(int.Parse(s6));
                    }
                }
                List<string> stringList17 = stringList1;
                int index14 = num16;
                int num17 = index14 + 1;
                string s7 = stringList17[index14];
                if (s7.Trim().Length > 0)
                    this.configurationMode = int.Parse(s7);
                List<string> stringList18 = stringList1;
                int index15 = num17;
                int num18 = index15 + 1;
                string s8 = stringList18[index15];
                if (s8.Trim().Length > 0)
                    this.driver1TriggerRpm = int.Parse(s8);
                List<string> stringList19 = stringList1;
                int index16 = num18;
                int num19 = index16 + 1;
                string s9 = stringList19[index16];
                if (s9.Trim().Length > 0)
                    this.idleBase = int.Parse(s9);
                List<string> stringList20 = stringList1;
                int index17 = num19;
                int num20 = index17 + 1;
                string s10 = stringList20[index17];
                if (s10.Trim().Length > 0)
                    this.idleCrankingDelta = int.Parse(s10);
                List<string> stringList21 = stringList1;
                int index18 = num20;
                int num21 = index18 + 1;
                string s11 = stringList21[index18];
                if (s11.Trim().Length > 0)
                    this.throttleIdleIncrease = (float)int.Parse(s11);
                List<string> stringList22 = stringList1;
                int index19 = num21;
                int num22 = index19 + 1;
                string s12 = stringList22[index19];
                if (s12.Trim().Length > 0)
                    this.liftOffIdleIncrease = int.Parse(s12);
                List<string> stringList23 = stringList1;
                int index20 = num22;
                int num23 = index20 + 1;
                string s13 = stringList23[index20];
                if (s13.Trim().Length > 0)
                    this.idlePeriod = int.Parse(s13);
                List<string> stringList24 = stringList1;
                int index21 = num23;
                int num24 = index21 + 1;
                string s14 = stringList24[index21];
                if (s14.Trim().Length > 0)
                    this.idleClkp = int.Parse(s14);
                List<string> stringList25 = stringList1;
                int index22 = num24;
                int num25 = index22 + 1;
                string s15 = stringList25[index22];
                if (s15.Trim().Length > 0)
                    this.idleClks = int.Parse(s15);
                List<string> stringList26 = stringList1;
                int index23 = num25;
                int num26 = index23 + 1;
                string s16 = stringList26[index23];
                if (s16.Trim().Length > 0)
                    this.acIdleIncrease = (float)int.Parse(s16);
                List<string> stringList27 = stringList1;
                int index24 = num26;
                int num27 = index24 + 1;
                string s17 = stringList27[index24];
                if (s17.Trim().Length > 0)
                    this.acInjectionTrim = int.Parse(s17);
                List<string> stringList28 = stringList1;
                int index25 = num27;
                int num28 = index25 + 1;
                string s18 = stringList28[index25];
                if (s18.Trim().Length > 0)
                    this.acIgnitionTrim = int.Parse(s18);
                this.altitudeInjectionCorrectionArray = new List<int>();
                List<string> stringList29 = stringList1;
                int index26 = num28;
                int num29 = index26 + 1;
                string[] strArray9 = stringList29[index26].Split(',');
                if (strArray9.Length >= 10)
                {
                    foreach (string s19 in strArray9)
                    {
                        if (s19.Trim().Length > 0)
                            this.altitudeInjectionCorrectionArray.Add(int.Parse(s19));
                    }
                }
                this.AltitudeIgnitionCorrectionArray = new List<int>();
                List<string> stringList30 = stringList1;
                int index27 = num29;
                num1 = index27 + 1;
                string[] strArray10 = stringList30[index27].Split(',');
                if (strArray10.Length >= 10)
                {
                    foreach (string s20 in strArray10)
                    {
                        if (s20.Trim().Length > 0)
                            this.AltitudeIgnitionCorrectionArray.Add(int.Parse(s20));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ECUData.logger.IsErrorEnabled)
                    ECUData.logger.Error((object)"Error reading file", ex);
                return false;
            }
            finally
            {
            }
        }

        public bool LoadFromFile(string filePath)
        {
            FileStream fileStream = (FileStream)null;
            try
            {
                fileStream = File.OpenRead(filePath);
                if (fileStream != null)
                {
                    if (fileStream.Length >= 2493L)
                    {
                        byte[] numArray = new byte[2493];
                        fileStream.Read(numArray, 0, 2493);
                        this.loadData(numArray, true);
                        return true;
                    }
                    if (fileStream.Length >= 2393L)
                    {
                        byte[] numArray = new byte[2393];
                        fileStream.Read(numArray, 0, 2393);
                        this.loadData(numArray, true);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ECUData.logger.IsErrorEnabled)
                    ECUData.logger.Error((object)"Error reading file:", ex);
                return false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        public ECUData(byte[] rawData) => this.loadData(rawData, false);

        private void loadData(byte[] rawData, bool isStoredFile)
        {
            try
            {
                int num1 = 0;
                this.fuel1DataArray = new List<float>();
                for (int index = 0; index < 320; ++index)
                    this.fuel1DataArray.Add((float)((double)Convert.ToSingle(rawData[index]) / 5.0 - 25.0));
                this.ignition1DataArray = new List<int>();
                for (int index = 320; index < 640; ++index)
                    this.ignition1DataArray.Add((int)rawData[index] - 25);
                this.coolantTempInjectionArray = new List<int>();
                for (int index = 640; index < 650; ++index)
                    this.coolantTempInjectionArray.Add((int)rawData[index] - 100);
                this.coolantTempIgnitionArray = new List<int>();
                for (int index = 650; index < 660; ++index)
                    this.coolantTempIgnitionArray.Add((int)rawData[index] - 100);
                this.airTempInjectionArray = new List<int>();
                for (int index = 660; index < 670; ++index)
                    this.airTempInjectionArray.Add((int)rawData[index] - 100);
                this.airTempIgnitionArray = new List<int>();
                for (int index = 670; index < 680; ++index)
                    this.airTempIgnitionArray.Add((int)rawData[index] - 100);
                this.batteryInjectorOpeningTimeArray = new List<float>();
                for (int index = 680; index < 700; ++index)
                    this.batteryInjectorOpeningTimeArray.Add(Convert.ToSingle(rawData[index]) * 100f);
                this.accelerationEnrichArray = new List<int>();
                int index1;
                for (index1 = 700; index1 < 710; ++index1)
                    this.accelerationEnrichArray.Add((int)rawData[index1]);
                byte[] numArray1 = rawData;
                int index2 = index1;
                int num2 = index2 + 1;
                byte num3 = numArray1[index2];
                byte[] numArray2 = rawData;
                int index3 = num2;
                int num4 = index3 + 1;
                byte num5 = numArray2[index3];
                if (num3 != (byte)0 && num5 != (byte)0)
                {
                    int num6 = ((int)num3 << 8) + (int)num5;
                    if (this.limiterRpm == 0)
                        this.limiterRpm = 1;
                    this.limiterRpm = (int)(30000000L / (long)num6);
                    if (this.limiterRpm > 25000)
                        this.limiterRpm = 25000;
                }
                byte[] numArray3 = rawData;
                int index4 = num4;
                int num7 = index4 + 1;
                byte num8 = numArray3[index4];
                byte[] numArray4 = rawData;
                int index5 = num7;
                int num9 = index5 + 1;
                byte num10 = numArray4[index5];
                if (num8 != (byte)0 && num10 != (byte)0)
                {
                    int num11 = ((int)num8 << 8) + (int)num10;
                    if (num11 == 0)
                    {
                        num11 = 1;
                        this.launchRpm = 1;
                    }
                    this.launchRpm = (int)(30000000L / (long)num11);
                    if (this.launchRpm > 25000)
                        this.launchRpm = 25000;
                }
                byte[] numArray5 = rawData;
                int index6 = num9;
                int num12 = index6 + 1;
                byte num13 = numArray5[index6];
                byte[] numArray6 = rawData;
                int index7 = num12;
                int num14 = index7 + 1;
                byte num15 = numArray6[index7];
                if (num13 != (byte)0 && num15 != (byte)0)
                {
                    int num16 = ((int)num13 << 8) + (int)num15;
                    if (num16 == 0)
                    {
                        num16 = 1;
                        this.vtecRpm = 1;
                    }
                    this.vtecRpm = (int)(30000000L / (long)num16);
                    if (this.vtecRpm > 25000)
                        this.vtecRpm = 25000;
                }
                byte[] numArray7 = rawData;
                int index8 = num14;
                int num17 = index8 + 1;
                this.injectionOverallTrim = (int)numArray7[index8] - 100;
                byte[] numArray8 = rawData;
                int index9 = num17;
                int num18 = index9 + 1;
                this.limiterMode = (int)numArray8[index9];
                if (isStoredFile)
                    this.limiterMode = 1;
                byte[] numArray9 = rawData;
                int index10 = num18;
                int num19 = index10 + 1;
                this.QsType = (int)numArray9[index10];
                byte[] numArray10 = rawData;
                int index11 = num19;
                int num20 = index11 + 1;
                this.idleControl1 = (int)numArray10[index11];
                byte[] numArray11 = rawData;
                int index12 = num20;
                int num21 = index12 + 1;
                this.idleControl2 = (int)numArray11[index12];
                byte[] numArray12 = rawData;
                int index13 = num21;
                int num22 = index13 + 1;
                this.idleControl3 = (int)numArray12[index13];
                byte[] numArray13 = rawData;
                int index14 = num22;
                int num23 = index14 + 1;
                this.idleControl4 = (int)numArray13[index14];
                byte[] numArray14 = rawData;
                int index15 = num23;
                int num24 = index15 + 1;
                this.idleControl5 = (int)numArray14[index15];
                byte[] numArray15 = rawData;
                int index16 = num24;
                int num25 = index16 + 1;
                this.idleControl6 = (int)numArray15[index16];
                byte[] numArray16 = rawData;
                int index17 = num25;
                int num26 = index17 + 1;
                this.idleControl7 = (int)numArray16[index17];
                byte[] numArray17 = rawData;
                int index18 = num26;
                int num27 = index18 + 1;
                this.idleControl8 = (int)numArray17[index18];
                byte[] numArray18 = rawData;
                int index19 = num27;
                int num28 = index19 + 1;
                this.idleControl9 = (int)numArray18[index19];
                byte[] numArray19 = rawData;
                int index20 = num28;
                int num29 = index20 + 1;
                this.idleControl10 = (int)numArray19[index20];
                byte[] numArray20 = rawData;
                int index21 = num29;
                int num30 = index21 + 1;
                this.configurationMode = (int)numArray20[index21];
                byte[] numArray21 = rawData;
                int index22 = num30;
                int num31 = index22 + 1;
                this.driver1TriggerRpm = (int)numArray21[index22];
                byte[] numArray22 = rawData;
                int index23 = num31;
                int num32 = index23 + 1;
                this.driver2TriggerRpm = (int)numArray22[index23];
                byte[] numArray23 = rawData;
                int index24 = num32;
                int num33 = index24 + 1;
                this.driver3TriggerRpm = (int)numArray23[index24];
                byte[] numArray24 = rawData;
                int index25 = num33;
                int num34 = index25 + 1;
                this.idleBase = (int)numArray24[index25];
                byte[] numArray25 = rawData;
                int index26 = num34;
                int num35 = index26 + 1;
                this.idleCrankingDelta = (int)numArray25[index26];
                byte[] numArray26 = rawData;
                int index27 = num35;
                int num36 = index27 + 1;
                this.throttleIdleIncrease = (float)numArray26[index27];
                byte[] numArray27 = rawData;
                int index28 = num36;
                int num37 = index28 + 1;
                this.liftOffIdleIncrease = (int)numArray27[index28];
                byte[] numArray28 = rawData;
                int index29 = num37;
                int num38 = index29 + 1;
                this.idlePeriod = (int)numArray28[index29];
                byte[] numArray29 = rawData;
                int index30 = num38;
                int num39 = index30 + 1;
                this.idleClkp = (int)numArray29[index30];
                byte[] numArray30 = rawData;
                int index31 = num39;
                int num40 = index31 + 1;
                this.idleClks = (int)numArray30[index31];
                byte[] numArray31 = rawData;
                int index32 = num40;
                int num41 = index32 + 1;
                this.acIdleIncrease = (float)numArray31[index32];
                byte[] numArray32 = rawData;
                int index33 = num41;
                int num42 = index33 + 1;
                this.acInjectionTrim = (int)numArray32[index33];
                byte[] numArray33 = rawData;
                int index34 = num42;
                int num43 = index34 + 1;
                this.acIgnitionTrim = (int)numArray33[index34] - 100;
                this.altitudeInjectionCorrectionArray = new List<int>();
                for (int index35 = 0; index35 < 10; ++index35)
                    this.altitudeInjectionCorrectionArray.Add((int)rawData[num43++] - 100);
                this.AltitudeIgnitionCorrectionArray = new List<int>();
                for (int index36 = 0; index36 < 10; ++index36)
                    this.AltitudeIgnitionCorrectionArray.Add((int)rawData[num43++] - 100);
                this.fuel2DataArray = new List<float>();
                for (int index37 = 763; index37 < 1083; ++index37)
                    this.fuel2DataArray.Add((float)((double)Convert.ToSingle(rawData[index37]) / 5.0 - 25.0));
                this.ignition2DataArray = new List<int>();
                for (int index38 = 1083; index38 < 1403; ++index38)
                    this.ignition2DataArray.Add((int)rawData[index38] - 25);
                this.fuel3DataArray = new List<float>();
                for (int index39 = 1403; index39 < 1723; ++index39)
                    this.fuel3DataArray.Add(Convert.ToSingle(rawData[index39]));
                this.ignition3DataArray = new List<int>();
                for (int index40 = 1723; index40 < 2043; ++index40)
                    this.ignition3DataArray.Add((int)rawData[index40]);
                this.ignition4DataArray = new List<int>();
                for (int index41 = 2043; index41 < 2363; ++index41)
                    this.ignition4DataArray.Add((int)rawData[index41] - 25);
                this.calibDataArray1 = new List<int>();
                for (int index42 = 2363; index42 < 2373; ++index42)
                    this.calibDataArray1.Add((int)rawData[index42]);
                this.calibDataArray2 = new List<int>();
                int index43;
                for (index43 = 2373; index43 < 2383; ++index43)
                    this.calibDataArray2.Add((int)rawData[index43]);
                byte[] numArray34 = rawData;
                int index44 = index43;
                int num44 = index44 + 1;
                this.spMap1 = (float)((int)numArray34[index44] - 100) / 10f;
                byte[] numArray35 = rawData;
                int index45 = num44;
                int num45 = index45 + 1;
                this.spMap2 = (float)((int)numArray35[index45] - 100) / 10f;
                byte[] numArray36 = rawData;
                int index46 = num45;
                int num46 = index46 + 1;
                this.spMap3 = (float)((int)numArray36[index46] - 100) / 10f;
                byte[] numArray37 = rawData;
                int index47 = num46;
                int num47 = index47 + 1;
                this.spMap4 = (float)((int)numArray37[index47] - 100) / 10f;
                byte[] numArray38 = rawData;
                int index48 = num47;
                int num48 = index48 + 1;
                this.spMap5 = (int)numArray38[index48] - 100;
                byte[] numArray39 = rawData;
                int index49 = num48;
                int num49 = index49 + 1;
                this.spMap6 = (int)numArray39[index49] - 100;
                byte[] numArray40 = rawData;
                int index50 = num49;
                int num50 = index50 + 1;
                this.spMap7 = (int)numArray40[index50] - 100;
                byte[] numArray41 = rawData;
                int index51 = num50;
                int num51 = index51 + 1;
                this.spMap8 = (int)numArray41[index51] - 100;
                byte[] numArray42 = rawData;
                int index52 = num51;
                int num52 = index52 + 1;
                this.spMapDummy1 = (int)numArray42[index52];
                byte[] numArray43 = rawData;
                int index53 = num52;
                num1 = index53 + 1;
                this.spMapDummy2 = (int)numArray43[index53];
                this.tempProp = new List<byte>();
                if (rawData.Length != 2493)
                    return;
                for (int index54 = 2393; index54 < 2493; ++index54)
                    this.tempProp.Add(rawData[index54]);
                this.MapDecr = this.tempProp.ToArray();
            }
            catch (Exception ex)
            {
                ECUData.logger.Error((object)(">>>>>>READ ERROR>>" + ex.StackTrace + "\r\n" + (object)ex.InnerException));
                ECUData.logger.Error((object)"Error parsing input stream..", ex);
                throw ex;
            }
        }
    }
}
