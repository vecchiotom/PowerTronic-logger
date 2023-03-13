// Decompiled with JetBrains decompiler
// Type: Dx.SDK.RealTimeData
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
    public class RealTimeData
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(RealTimeData));
        private int rpm;
        private int dummy1;
        private int injectionCorrected;
        private int injectionCorrectedForGraph;
        private int dummy2;
        private int ignitionFinal;
        private int ignitionFinalForGraph;
        private int airTemp;
        private int airTempForGraph;
        private int engineTemp;
        private int engineTempForGraph;
        private int dummy3;
        private int map;
        private int mapForGraph;
        private int tps;
        private int tpsForGraph;
        private int dummy4;
        private int mapPositionX;
        private int mapPositionY;
        private int dummy5;
        private int dummy6;
        private int dummy7;
        private int dummy8;
        private int coolantTempPos;
        private int airTempPos;
        private int accelerationEnrichPos;
        private int batteryPos;
        private int altitudePos;
        private float injector1PW;
        private float injector2PW;
        private float injector3PW;
        private float injector4PW;
        private float timing1;
        private float timing2;
        private float timing3;
        private float timing4;
        private float duty1;
        private float duty2;
        private float duty3;
        private float duty4;
        private int injectionDuty1;
        private int injectionDuty2;
        private int injectionDuty3;
        private int injectionDuty4;
        private int skp1Sig;
        private int skp2Sig;
        private int skp3Sig;
        private int skp4Sig;
        private int dummy13;
        private int dummy14;
        private int tractionCounter;
        private int dummy16;
        private int secondChipPresent;
        private int mapNumber;
        private int idle_Temp;
        private int indexTraction;
        private int dummy18;
        private int dummy19;
        private int dummy20;
        private int dummy21;
        private int dummy22;
        private int dummy23;
        private int dummy24;
        private int dummy25;
        private int tractionStatus1;
        private int tractionStatus2;
        private int tractionStatus3;
        private int tractionStatus4;
        private int tractionRPM1;
        private int tractionRPM2;
        private int tractionRPM3;
        private int tractionRPM4;

        public int rpm1 { get; set; }

        public int rpm2 { get; set; }

        public int ipw1 { get; set; }

        public int ipw2 { get; set; }

        public int injector1PW1 { get; set; }

        public int injector1PW2 { get; set; }

        public int injector2PW1 { get; set; }

        public int injector2PW2 { get; set; }

        public int injector3PW1 { get; set; }

        public int injector3PW2 { get; set; }

        public int injector4PW1 { get; set; }

        public int injector4PW2 { get; set; }

        public int labRPM { get; set; }

        public int labIPW { get; set; }

        public int labStdAct { get; set; }

        public int labDutyStock { get; set; }

        public int labDutyPT { get; set; }

        public int labLambda { get; set; }

        public int labMagPr { get; set; }

        public int labThrPos { get; set; }

        public int labIAE { get; set; }

        public int labIndRPM { get; set; }

        public int labIndLoad { get; set; }

        public int labIgnBase { get; set; }

        public int labInjBase { get; set; }

        public int labFuelCutLim { get; set; }

        public int labLimMod { get; set; }

        public int labIndECT { get; set; }

        public int labIndIAT { get; set; }

        public int labIndAE { get; set; }

        public int labIndBatt { get; set; }

        public int labIndAlt { get; set; }

        public int Rpm
        {
            get => this.rpm;
            set => this.rpm = value;
        }

        public int Dummy1
        {
            get => this.dummy1;
            set => this.dummy1 = value;
        }

        public int InjectionCorrected
        {
            get => this.injectionCorrected;
            set
            {
                this.injectionCorrected = value;
                this.InjectionCorrectedForGraph = value;
            }
        }

        public int InjectionCorrectedForGraph
        {
            get => this.injectionCorrectedForGraph;
            set => this.injectionCorrectedForGraph = (int)ConfigurationHandler.Instance.RealTimeConfig.getInjectionCorrectedFunctionValue(value);
        }

        public int Dummy2
        {
            get => this.dummy2;
            set => this.dummy2 = value;
        }

        public int IgnitionFinal
        {
            get => this.ignitionFinal;
            set
            {
                this.ignitionFinal = value;
                this.IgnitionFinalForGraph = value;
            }
        }

        public int IgnitionFinalForGraph
        {
            get => this.ignitionFinalForGraph;
            set => this.ignitionFinalForGraph = (int)ConfigurationHandler.Instance.RealTimeConfig.getIgnitionFinalFunctionValue(value);
        }

        public int AirTemp
        {
            get => this.airTemp;
            set
            {
                this.airTemp = value;
                this.AirTempForGraph = value;
            }
        }

        public int AirTempForGraph
        {
            get => this.airTempForGraph;
            set => this.airTempForGraph = (int)ConfigurationHandler.Instance.RealTimeConfig.getAirTempFunctionValue(value);
        }

        public int EngineTemp
        {
            get => this.engineTemp;
            set
            {
                this.engineTemp = value;
                this.EngineTempForGraph = value;
            }
        }

        public int EngineTempForGraph
        {
            get => this.engineTempForGraph;
            set => this.engineTempForGraph = (int)ConfigurationHandler.Instance.RealTimeConfig.getEngineTempFunctionValue(value);
        }

        public int Dummy3
        {
            get => this.dummy3;
            set => this.dummy3 = value;
        }

        public int Map
        {
            get => this.map;
            set
            {
                this.map = value;
                this.MapForGraph = value;
            }
        }

        public int MapForGraph
        {
            get => this.mapForGraph;
            set => this.mapForGraph = (int)ConfigurationHandler.Instance.RealTimeConfig.getMapFunctionValue(value);
        }

        public int Tps
        {
            get => this.tps;
            set
            {
                this.tps = value;
                this.TpsForGraph = value;
            }
        }

        public int TpsForGraph
        {
            get => this.tpsForGraph;
            set => this.tpsForGraph = (int)ConfigurationHandler.Instance.RealTimeConfig.getTpsFunctionValue(value);
        }

        public int Dummy4
        {
            get => this.dummy4;
            set => this.dummy4 = value;
        }

        public int MapPositionX
        {
            get => this.mapPositionX;
            set => this.mapPositionX = value;
        }

        public int MapPositionY
        {
            get => this.mapPositionY;
            set => this.mapPositionY = value;
        }

        public int Dummy5
        {
            get => this.dummy5;
            set => this.dummy5 = value;
        }

        public int Dummy6
        {
            get => this.dummy6;
            set => this.dummy6 = value;
        }

        public int Dummy7
        {
            get => this.dummy7;
            set => this.dummy7 = value;
        }

        public int Dummy8
        {
            get => this.dummy8;
            set => this.dummy8 = value;
        }

        public int CoolantTempPos
        {
            get => this.coolantTempPos;
            set => this.coolantTempPos = value;
        }

        public int AirTempPos
        {
            get => this.airTempPos;
            set => this.airTempPos = value;
        }

        public int AccelerationEnrichPos
        {
            get => this.accelerationEnrichPos;
            set => this.accelerationEnrichPos = value;
        }

        public int BatteryPos
        {
            get => this.batteryPos;
            set => this.batteryPos = value;
        }

        public int AltitudePos
        {
            get => this.altitudePos;
            set => this.altitudePos = value;
        }

        public float Injector1PW
        {
            get => this.injector1PW;
            set => this.injector1PW = value;
        }

        public float Injector2PW
        {
            get => this.injector2PW;
            set => this.injector2PW = value;
        }

        public float Injector3PW
        {
            get => this.injector3PW;
            set => this.injector3PW = value;
        }

        public float Injector4PW
        {
            get => this.injector4PW;
            set => this.injector4PW = value;
        }

        public float Timing1
        {
            get => this.timing1;
            set => this.timing1 = value;
        }

        public float Timing2
        {
            get => this.timing2;
            set => this.timing2 = value;
        }

        public float Timing3
        {
            get => this.timing3;
            set => this.timing3 = value;
        }

        public float Timing4
        {
            get => this.timing4;
            set => this.timing4 = value;
        }

        public float Duty1
        {
            get => this.duty1;
            set => this.duty1 = value;
        }

        public float Duty2
        {
            get => this.duty2;
            set => this.duty2 = value;
        }

        public float Duty3
        {
            get => this.duty3;
            set => this.duty3 = value;
        }

        public float Duty4
        {
            get => this.duty4;
            set => this.duty4 = value;
        }

        public int InjectionDuty1
        {
            get => this.injectionDuty1;
            set => this.injectionDuty1 = value;
        }

        public int InjectionDuty2
        {
            get => this.injectionDuty2;
            set => this.injectionDuty2 = value;
        }

        public int InjectionDuty3
        {
            get => this.injectionDuty3;
            set => this.injectionDuty3 = value;
        }

        public int InjectionDuty4
        {
            get => this.injectionDuty4;
            set => this.injectionDuty4 = value;
        }

        public int Skp1Sig
        {
            get => this.skp1Sig;
            set => this.skp1Sig = value;
        }

        public int Skp2Sig
        {
            get => this.skp2Sig;
            set => this.skp2Sig = value;
        }

        public int Skp3Sig
        {
            get => this.skp3Sig;
            set => this.skp3Sig = value;
        }

        public int Skp4Sig
        {
            get => this.skp4Sig;
            set => this.skp4Sig = value;
        }

        public int Dummy13
        {
            get => this.dummy13;
            set => this.dummy13 = value;
        }

        public int Dummy14
        {
            get => this.dummy14;
            set => this.dummy14 = value;
        }

        public int TractionCounter
        {
            get => this.tractionCounter;
            set => this.tractionCounter = value;
        }

        public int Dummy16
        {
            get => this.dummy16;
            set => this.dummy16 = value;
        }

        public int SecondChipPresent
        {
            get => this.secondChipPresent;
            set => this.secondChipPresent = value;
        }

        public int MapNumber
        {
            get => this.mapNumber;
            set => this.mapNumber = value;
        }

        public int Idle_Temp
        {
            get => this.idle_Temp;
            set => this.idle_Temp = value;
        }

        public int IndexTraction
        {
            get => this.indexTraction;
            set => this.indexTraction = value;
        }

        public int Dummy18
        {
            get => this.dummy18;
            set => this.dummy18 = value;
        }

        public int Dummy19
        {
            get => this.dummy19;
            set => this.dummy19 = value;
        }

        public int Dummy20
        {
            get => this.dummy20;
            set => this.skp2Sig = value;
        }

        public int Dummy21
        {
            get => this.dummy21;
            set => this.dummy21 = value;
        }

        public int Dummy22
        {
            get => this.dummy22;
            set => this.dummy22 = value;
        }

        public int Dummy23
        {
            get => this.dummy23;
            set => this.dummy23 = value;
        }

        public int Dummy24
        {
            get => this.dummy24;
            set => this.dummy24 = value;
        }

        public int Dummy25
        {
            get => this.dummy25;
            set => this.dummy25 = value;
        }

        public int TractionStatus1
        {
            get => this.tractionStatus1;
            set => this.tractionStatus1 = value;
        }

        public int TractionStatus2
        {
            get => this.tractionStatus2;
            set => this.tractionStatus2 = value;
        }

        public int TractionStatus3
        {
            get => this.tractionStatus3;
            set => this.tractionStatus3 = value;
        }

        public int TractionStatus4
        {
            get => this.tractionStatus4;
            set => this.tractionStatus4 = value;
        }

        public int TractionRPM1
        {
            get => this.tractionRPM1;
            set => this.tractionRPM1 = value;
        }

        public int TractionRPM2
        {
            get => this.tractionRPM2;
            set => this.tractionRPM2 = value;
        }

        public int TractionRPM3
        {
            get => this.tractionRPM3;
            set => this.tractionRPM3 = value;
        }

        public int TractionRPM4
        {
            get => this.tractionRPM4;
            set => this.tractionRPM4 = value;
        }

        public RealTimeData()
        {
        }

        public bool SaveToFile(string filePath)
        {
            FileStream fileStream = (FileStream)null;
            try
            {
                fileStream = File.Open(filePath, FileMode.Append);
                if (fileStream == null)
                    return false;
                byte[] array = this.getByteArray().ToArray();
                if (array.Length > 0 && array.Length == 23)
                    fileStream.Write(array, 0, 23);
                return true;
            }
            catch (Exception ex)
            {
                if (RealTimeData.logger.IsErrorEnabled)
                    RealTimeData.logger.Error((object)"Error saving file:", ex);
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

        public List<byte> getByteArray()
        {
            List<byte> byteArray = new List<byte>();
            try
            {
                byte[] numArray = new byte[2];
                byte[] bytes = BitConverter.GetBytes(this.rpm);
                byteArray.Add(bytes[0]);
                byteArray.Add(bytes[1]);
                byteArray.Add((byte)this.dummy1);
                byteArray.Add((byte)this.injectionCorrected);
                byteArray.Add((byte)this.dummy2);
                byteArray.Add((byte)this.ignitionFinal);
                byteArray.Add((byte)this.airTemp);
                byteArray.Add((byte)this.engineTemp);
                byteArray.Add((byte)this.dummy3);
                byteArray.Add((byte)this.map);
                byteArray.Add((byte)this.tps);
                byteArray.Add((byte)this.dummy4);
                byteArray.Add((byte)this.mapPositionX);
                byteArray.Add((byte)this.mapPositionY);
                byteArray.Add((byte)this.dummy5);
                byteArray.Add((byte)this.dummy6);
                byteArray.Add((byte)this.dummy7);
                byteArray.Add((byte)this.dummy8);
                byteArray.Add((byte)this.coolantTempPos);
                byteArray.Add((byte)this.airTempPos);
                byteArray.Add((byte)this.accelerationEnrichPos);
                byteArray.Add((byte)this.batteryPos);
                byteArray.Add((byte)this.altitudePos);
                byteArray.Add((byte)this.injector1PW);
                byteArray.Add((byte)this.injector2PW);
                byteArray.Add((byte)this.injector3PW);
                byteArray.Add((byte)this.injector4PW);
                byteArray.Add((byte)this.timing1);
                byteArray.Add((byte)this.timing2);
                byteArray.Add((byte)this.timing3);
                byteArray.Add((byte)this.timing4);
                byteArray.Add((byte)this.duty1);
                byteArray.Add((byte)this.duty2);
                byteArray.Add((byte)this.duty3);
                byteArray.Add((byte)this.duty4);
                byteArray.Add((byte)this.injectionDuty1);
                byteArray.Add((byte)this.injectionDuty2);
                byteArray.Add((byte)this.injectionDuty3);
                byteArray.Add((byte)this.injectionDuty4);
                byteArray.Add((byte)this.skp1Sig);
                byteArray.Add((byte)this.skp2Sig);
                byteArray.Add((byte)this.skp3Sig);
                byteArray.Add((byte)this.skp4Sig);
                byteArray.Add((byte)this.dummy13);
                byteArray.Add((byte)this.dummy14);
                byteArray.Add((byte)this.tractionCounter);
                byteArray.Add((byte)this.dummy16);
                byteArray.Add((byte)this.secondChipPresent);
                byteArray.Add((byte)this.mapNumber);
                byteArray.Add((byte)this.idle_Temp);
                byteArray.Add((byte)this.indexTraction);
                byteArray.Add((byte)this.dummy18);
                byteArray.Add((byte)this.dummy19);
                byteArray.Add((byte)this.dummy20);
                byteArray.Add((byte)this.dummy21);
                byteArray.Add((byte)this.tractionStatus1);
                byteArray.Add((byte)this.tractionStatus2);
                byteArray.Add((byte)this.tractionStatus3);
                byteArray.Add((byte)this.tractionStatus4);
                byteArray.Add((byte)this.tractionRPM1);
                byteArray.Add((byte)this.tractionRPM2);
                byteArray.Add((byte)this.tractionRPM3);
                byteArray.Add((byte)this.tractionRPM4);
                return byteArray;
            }
            catch (Exception ex)
            {
                if (RealTimeData.logger.IsErrorEnabled)
                    RealTimeData.logger.Error((object)"Error in RealtimeData getByteArray", ex);
                throw ex;
            }
        }

        public RealTimeData(byte[] rawData)
        {
            try
            {
                int num1;
                if (rawData.Length == 71)
                {
                    int num2 = 0;
                    byte[] numArray1 = rawData;
                    int index1 = num2;
                    int num3 = index1 + 1;
                    byte num4 = numArray1[index1];
                    byte[] numArray2 = rawData;
                    int index2 = num3;
                    int num5 = index2 + 1;
                    byte num6 = numArray2[index2];
                    int num7 = ((int)num4 << 8) + (int)num6;
                    if (num7 == 0)
                        num7 = 1;
                    int num8 = 15000000 / num7;
                    this.rpm = num8 >= 25000 ? 25000 : num8;
                    byte[] numArray3 = rawData;
                    int index3 = num5;
                    int num9 = index3 + 1;
                    this.Dummy1 = (int)numArray3[index3];
                    byte[] numArray4 = rawData;
                    int index4 = num9;
                    int num10 = index4 + 1;
                    this.InjectionCorrected = (int)numArray4[index4];
                    byte[] numArray5 = rawData;
                    int index5 = num10;
                    int num11 = index5 + 1;
                    this.Dummy2 = (int)numArray5[index5];
                    byte[] numArray6 = rawData;
                    int index6 = num11;
                    int num12 = index6 + 1;
                    this.IgnitionFinal = (int)numArray6[index6];
                    byte[] numArray7 = rawData;
                    int index7 = num12;
                    int num13 = index7 + 1;
                    this.AirTemp = (int)numArray7[index7];
                    byte[] numArray8 = rawData;
                    int index8 = num13;
                    int num14 = index8 + 1;
                    this.EngineTemp = (int)numArray8[index8];
                    byte[] numArray9 = rawData;
                    int index9 = num14;
                    int num15 = index9 + 1;
                    this.Dummy3 = (int)numArray9[index9];
                    byte[] numArray10 = rawData;
                    int index10 = num15;
                    int num16 = index10 + 1;
                    this.Map = (int)numArray10[index10];
                    byte[] numArray11 = rawData;
                    int index11 = num16;
                    int num17 = index11 + 1;
                    this.Tps = (int)numArray11[index11];
                    byte[] numArray12 = rawData;
                    int index12 = num17;
                    int num18 = index12 + 1;
                    this.Dummy4 = (int)numArray12[index12];
                    byte[] numArray13 = rawData;
                    int index13 = num18;
                    int num19 = index13 + 1;
                    this.MapPositionX = (int)numArray13[index13];
                    byte[] numArray14 = rawData;
                    int index14 = num19;
                    int num20 = index14 + 1;
                    this.MapPositionY = (int)numArray14[index14];
                    byte[] numArray15 = rawData;
                    int index15 = num20;
                    int num21 = index15 + 1;
                    this.Dummy5 = (int)numArray15[index15];
                    byte[] numArray16 = rawData;
                    int index16 = num21;
                    int num22 = index16 + 1;
                    this.Dummy6 = (int)numArray16[index16];
                    byte[] numArray17 = rawData;
                    int index17 = num22;
                    int num23 = index17 + 1;
                    this.Dummy7 = (int)numArray17[index17];
                    byte[] numArray18 = rawData;
                    int index18 = num23;
                    int num24 = index18 + 1;
                    this.Dummy8 = (int)numArray18[index18];
                    byte[] numArray19 = rawData;
                    int index19 = num24;
                    int num25 = index19 + 1;
                    this.CoolantTempPos = (int)numArray19[index19];
                    byte[] numArray20 = rawData;
                    int index20 = num25;
                    int num26 = index20 + 1;
                    this.AirTempPos = (int)numArray20[index20];
                    byte[] numArray21 = rawData;
                    int index21 = num26;
                    int num27 = index21 + 1;
                    this.AccelerationEnrichPos = (int)numArray21[index21];
                    byte[] numArray22 = rawData;
                    int index22 = num27;
                    int num28 = index22 + 1;
                    this.BatteryPos = (int)numArray22[index22];
                    byte[] numArray23 = rawData;
                    int index23 = num28;
                    num1 = index23 + 1;
                    this.AltitudePos = (int)numArray23[index23];
                }
                if (rawData.Length != 71)
                    return;
                int num29 = 0;
                byte[] numArray24 = rawData;
                int index24 = num29;
                int num30 = index24 + 1;
                byte num31 = numArray24[index24];
                this.rpm1 = (int)num31;
                byte[] numArray25 = rawData;
                int index25 = num30;
                int num32 = index25 + 1;
                byte num33 = numArray25[index25];
                this.rpm2 = (int)num33;
                int num34 = ((int)num31 << 8) + (int)num33;
                if (num34 == 0)
                    num34 = 1;
                int num35 = 15000000 / num34;
                this.labRPM = num35 >= 25000 ? 25000 : num35;
                byte[] numArray26 = rawData;
                int index26 = num32;
                int num36 = index26 + 1;
                this.Dummy1 = (int)numArray26[index26];
                byte[] numArray27 = rawData;
                int index27 = num36;
                int num37 = index27 + 1;
                byte num38 = numArray27[index27];
                this.ipw1 = (int)num38;
                byte[] numArray28 = rawData;
                int index28 = num37;
                int num39 = index28 + 1;
                byte num40 = numArray28[index28];
                this.ipw2 = (int)num40;
                this.labIPW = ((int)num38 << 8) + (int)num40;
                byte[] numArray29 = rawData;
                int index29 = num39;
                int num41 = index29 + 1;
                this.labStdAct = (int)numArray29[index29];
                byte[] numArray30 = rawData;
                int index30 = num41;
                int num42 = index30 + 1;
                this.labDutyStock = (int)numArray30[index30];
                byte[] numArray31 = rawData;
                int index31 = num42;
                int num43 = index31 + 1;
                this.labDutyPT = (int)numArray31[index31];
                byte[] numArray32 = rawData;
                int index32 = num43;
                int num44 = index32 + 1;
                this.labLambda = (int)numArray32[index32];
                byte[] numArray33 = rawData;
                int index33 = num44;
                int num45 = index33 + 1;
                this.labMagPr = (int)numArray33[index33];
                byte[] numArray34 = rawData;
                int index34 = num45;
                int num46 = index34 + 1;
                this.labThrPos = (int)numArray34[index34];
                byte[] numArray35 = rawData;
                int index35 = num46;
                int num47 = index35 + 1;
                this.labIndAE = (int)numArray35[index35];
                byte[] numArray36 = rawData;
                int index36 = num47;
                int num48 = index36 + 1;
                this.labIndRPM = (int)numArray36[index36];
                byte[] numArray37 = rawData;
                int index37 = num48;
                int num49 = index37 + 1;
                this.labIndLoad = (int)numArray37[index37];
                byte[] numArray38 = rawData;
                int index38 = num49;
                int num50 = index38 + 1;
                this.labIgnBase = (int)numArray38[index38];
                byte[] numArray39 = rawData;
                int index39 = num50;
                int num51 = index39 + 1;
                this.labInjBase = (int)numArray39[index39];
                byte[] numArray40 = rawData;
                int index40 = num51;
                int num52 = index40 + 1;
                this.labFuelCutLim = (int)numArray40[index40];
                byte[] numArray41 = rawData;
                int index41 = num52;
                int num53 = index41 + 1;
                this.labLimMod = (int)numArray41[index41];
                byte[] numArray42 = rawData;
                int index42 = num53;
                int num54 = index42 + 1;
                this.labIndECT = (int)numArray42[index42];
                byte[] numArray43 = rawData;
                int index43 = num54;
                int num55 = index43 + 1;
                this.labIndIAT = (int)numArray43[index43];
                byte[] numArray44 = rawData;
                int index44 = num55;
                int num56 = index44 + 1;
                this.labIAE = (int)numArray44[index44];
                byte[] numArray45 = rawData;
                int index45 = num56;
                int num57 = index45 + 1;
                this.labIndBatt = (int)numArray45[index45];
                byte[] numArray46 = rawData;
                int index46 = num57;
                int num58 = index46 + 1;
                this.labIndAlt = (int)numArray46[index46];
                byte[] numArray47 = rawData;
                int index47 = num58;
                int num59 = index47 + 1;
                byte num60 = numArray47[index47];
                this.injector1PW1 = (int)num60;
                byte[] numArray48 = rawData;
                int index48 = num59;
                int num61 = index48 + 1;
                byte num62 = numArray48[index48];
                this.injector1PW2 = (int)num62;
                this.injector1PW = (float)(((int)num60 << 8) + (int)num62) / 1000f;
                byte[] numArray49 = rawData;
                int index49 = num61;
                int num63 = index49 + 1;
                byte num64 = numArray49[index49];
                this.injector2PW1 = (int)num64;
                byte[] numArray50 = rawData;
                int index50 = num63;
                int num65 = index50 + 1;
                byte num66 = numArray50[index50];
                this.injector2PW2 = (int)num66;
                this.injector2PW = (float)(((int)num64 << 8) + (int)num66) / 1000f;
                byte[] numArray51 = rawData;
                int index51 = num65;
                int num67 = index51 + 1;
                byte num68 = numArray51[index51];
                this.injector3PW1 = (int)num68;
                byte[] numArray52 = rawData;
                int index52 = num67;
                int num69 = index52 + 1;
                byte num70 = numArray52[index52];
                this.injector3PW2 = (int)num70;
                this.injector3PW = (float)(((int)num68 << 8) + (int)num70) / 1000f;
                byte[] numArray53 = rawData;
                int index53 = num69;
                int num71 = index53 + 1;
                byte num72 = numArray53[index53];
                this.injector4PW1 = (int)num72;
                byte[] numArray54 = rawData;
                int index54 = num71;
                int num73 = index54 + 1;
                byte num74 = numArray54[index54];
                this.injector4PW2 = (int)num74;
                this.injector4PW = (float)(((int)num72 << 8) + (int)num74) / 1000f;
                byte[] numArray55 = rawData;
                int index55 = num73;
                int num75 = index55 + 1;
                this.timing1 = (float)numArray55[index55];
                byte[] numArray56 = rawData;
                int index56 = num75;
                int num76 = index56 + 1;
                this.timing2 = (float)numArray56[index56];
                byte[] numArray57 = rawData;
                int index57 = num76;
                int num77 = index57 + 1;
                this.timing3 = (float)numArray57[index57];
                byte[] numArray58 = rawData;
                int index58 = num77;
                int num78 = index58 + 1;
                this.timing4 = (float)numArray58[index58];
                byte[] numArray59 = rawData;
                int index59 = num78;
                int num79 = index59 + 1;
                this.duty1 = (float)((int)numArray59[index59] * 2);
                byte[] numArray60 = rawData;
                int index60 = num79;
                int num80 = index60 + 1;
                this.duty2 = (float)((int)numArray60[index60] * 2);
                byte[] numArray61 = rawData;
                int index61 = num80;
                int num81 = index61 + 1;
                this.duty3 = (float)((int)numArray61[index61] * 2);
                byte[] numArray62 = rawData;
                int index62 = num81;
                int num82 = index62 + 1;
                this.duty4 = (float)((int)numArray62[index62] * 2);
                byte[] numArray63 = rawData;
                int index63 = num82;
                int num83 = index63 + 1;
                this.injectionDuty1 = (int)numArray63[index63];
                byte[] numArray64 = rawData;
                int index64 = num83;
                int num84 = index64 + 1;
                this.injectionDuty2 = (int)numArray64[index64];
                byte[] numArray65 = rawData;
                int index65 = num84;
                int num85 = index65 + 1;
                this.injectionDuty3 = (int)numArray65[index65];
                byte[] numArray66 = rawData;
                int index66 = num85;
                int num86 = index66 + 1;
                this.injectionDuty4 = (int)numArray66[index66];
                byte[] numArray67 = rawData;
                int index67 = num86;
                int num87 = index67 + 1;
                this.skp1Sig = (int)numArray67[index67];
                byte[] numArray68 = rawData;
                int index68 = num87;
                int num88 = index68 + 1;
                this.skp2Sig = (int)numArray68[index68];
                byte[] numArray69 = rawData;
                int index69 = num88;
                int num89 = index69 + 1;
                this.skp3Sig = (int)numArray69[index69];
                byte[] numArray70 = rawData;
                int index70 = num89;
                int num90 = index70 + 1;
                this.skp4Sig = (int)numArray70[index70];
                byte[] numArray71 = rawData;
                int index71 = num90;
                int num91 = index71 + 1;
                this.dummy13 = (int)numArray71[index71];
                byte[] numArray72 = rawData;
                int index72 = num91;
                int num92 = index72 + 1;
                this.dummy14 = (int)numArray72[index72];
                byte[] numArray73 = rawData;
                int index73 = num92;
                int num93 = index73 + 1;
                byte num94 = numArray73[index73];
                byte[] numArray74 = rawData;
                int index74 = num93;
                int num95 = index74 + 1;
                byte num96 = numArray74[index74];
                this.tractionCounter = ((int)num94 << 8) + (int)num96;
                byte[] numArray75 = rawData;
                int index75 = num95;
                int num97 = index75 + 1;
                this.secondChipPresent = (int)numArray75[index75];
                byte[] numArray76 = rawData;
                int index76 = num97;
                int num98 = index76 + 1;
                this.mapNumber = (int)numArray76[index76];
                byte[] numArray77 = rawData;
                int index77 = num98;
                int num99 = index77 + 1;
                this.idle_Temp = (int)numArray77[index77];
                byte[] numArray78 = rawData;
                int index78 = num99;
                int num100 = index78 + 1;
                this.indexTraction = (int)numArray78[index78];
                byte[] numArray79 = rawData;
                int index79 = num100;
                int num101 = index79 + 1;
                this.tractionStatus1 = (int)numArray79[index79];
                byte[] numArray80 = rawData;
                int index80 = num101;
                int num102 = index80 + 1;
                this.tractionStatus2 = (int)numArray80[index80];
                byte[] numArray81 = rawData;
                int index81 = num102;
                int num103 = index81 + 1;
                this.tractionStatus3 = (int)numArray81[index81];
                byte[] numArray82 = rawData;
                int index82 = num103;
                int num104 = index82 + 1;
                this.tractionStatus4 = (int)numArray82[index82];
                byte[] numArray83 = rawData;
                int index83 = num104;
                int num105 = index83 + 1;
                this.tractionRPM1 = (int)numArray83[index83];
                byte[] numArray84 = rawData;
                int index84 = num105;
                int num106 = index84 + 1;
                this.tractionRPM2 = (int)numArray84[index84];
                byte[] numArray85 = rawData;
                int index85 = num106;
                int num107 = index85 + 1;
                this.tractionRPM3 = (int)numArray85[index85];
                byte[] numArray86 = rawData;
                int index86 = num107;
                int num108 = index86 + 1;
                this.tractionRPM4 = (int)numArray86[index86];
                byte[] numArray87 = rawData;
                int index87 = num108;
                int num109 = index87 + 1;
                this.dummy18 = (int)numArray87[index87];
                byte[] numArray88 = rawData;
                int index88 = num109;
                int num110 = index88 + 1;
                this.dummy19 = (int)numArray88[index88];
                byte[] numArray89 = rawData;
                int index89 = num110;
                int num111 = index89 + 1;
                this.dummy20 = (int)numArray89[index89];
                byte[] numArray90 = rawData;
                int index90 = num111;
                int num112 = index90 + 1;
                this.dummy21 = (int)numArray90[index90];
                byte[] numArray91 = rawData;
                int index91 = num112;
                int num113 = index91 + 1;
                this.dummy22 = (int)numArray91[index91];
                byte[] numArray92 = rawData;
                int index92 = num113;
                int num114 = index92 + 1;
                this.dummy23 = (int)numArray92[index92];
                byte[] numArray93 = rawData;
                int index93 = num114;
                int num115 = index93 + 1;
                this.dummy24 = (int)numArray93[index93];
                byte[] numArray94 = rawData;
                int index94 = num115;
                num1 = index94 + 1;
                this.dummy25 = (int)numArray94[index94];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
