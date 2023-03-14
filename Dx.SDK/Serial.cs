// Decompiled with JetBrains decompiler
// Type: Dx.SDK.Serial
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Dx.SDK
{
    public class Serial : IDisposable
    {
        private DataLog dataLog = new DataLog();
        private Type DeclaredType = MethodBase.GetCurrentMethod().DeclaringType;
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static int ECUVersionNumber;
        public static byte[] DatFromUI;
        public static bool isMapLoadedToUI;
        public static bool isMapLoadedFromUIToPopup;
        public static bool threadStop;
        private SerialPort _port;
        private ManualResetEvent _mre = new ManualResetEvent(false);
        private int ResponseLength;
        private List<byte> tempArr = new List<byte>();
        public bool connectOptionSelector = false;
        private bool _isDisposed;
        public static int fileIndex = 0;

        public byte LastError { get; private set; }

        public byte[] LastResponse { get; private set; }

        public List<byte[]> bootLoaderText { get; set; }

        public void Connect(string portName)
        {
            if (this._port != null && this._port.IsOpen)
                this._port.Close();
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
                SerialPortFixer.Execute(portName);
            this._port = new SerialPort();
            this._port.PortName = portName;
            this._port.BaudRate = 57600;
            this._port.DataBits = 8;
            this._port.Parity = Parity.None;
            this._port.StopBits = StopBits.One;
            this._port.DtrEnable = true;
            this._port.Encoding = Encoding.GetEncoding(0);
            this._port.Handshake = Handshake.None;
            this._port.RtsEnable = true;
            this._port.Open();
            if (true)
                //Console.WriteLine(">>>>>>>>>>>>>>>>>>Setting event handler");
                this._port.DataReceived += new SerialDataReceivedEventHandler(this.OnDataReceived);
            this._port.ErrorReceived += new SerialErrorReceivedEventHandler(this.OnErrorReceived);
        }

        public void Disconnect()
        {
            if (this._port == null || !this._port.IsOpen)
                return;
            this._port.Close();
            this._port.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._isDisposed)
                return;
            if (disposing)
            {
                if (this._port != null)
                {
                    if (this._port.IsOpen)
                        this._port.Close();
                    this._port.Dispose();
                }
                if (this._mre != null)
                    this._mre.Close();
            }
            this._isDisposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        ~Serial() => this.Dispose(false);

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.LastError = (byte)0;
            try
            {
                if (e.EventType == SerialData.Eof)
                    return;
                if (e.EventType == SerialData.Chars)
                {
                    //Console.WriteLine("actual data");

                    if (true)
                        //Console.WriteLine("Threshhold is:" + _port.ReceivedBytesThreshold + " byte recvd:" + (object)this._port.BytesToRead);
                        if (this._port.BytesToRead == 0)
                            return;
                    int responseLength = this.ResponseLength;
                    if (true)
                        //Console.WriteLine(("Data received expected responseLength=" + responseLength + " Actual received:" + (object)this._port.BytesToRead));
                        if (responseLength == this._port.BytesToRead)
                        {
                            responseLength = this._port.BytesToRead;
                            //Console.WriteLine(responseLength);
                            this.LastResponse = new byte[responseLength];
                            this._port.Read(this.LastResponse, 0, responseLength);
                            //Console.WriteLine(System.Text.Encoding.Default.GetString(LastResponse));
                            if (this.LastResponse[0] != (byte)252 && Serial.logger.IsErrorEnabled)
                                Serial.logger.Error((object)"NO command ack received!!");
                            if (this.LastResponse[responseLength - 1] != (byte)248 && Serial.logger.IsErrorEnabled)
                                Serial.logger.Error((object)"No command complete received!!");
                        }
                        else if (this._port.BytesToRead == 2 && this.connectOptionSelector)
                        {
                            this.LastResponse = new byte[2];
                            this._port.Read(this.LastResponse, 0, 2);
                            this.connectOptionSelector = false;
                            Serial.ECUVersionNumber = 1;
                        }
                        else if (this._port.BytesToRead == 34)
                        {
                            this.LastResponse = new byte[34];
                            this._port.Read(this.LastResponse, 0, 34);
                            //Console.WriteLine(System.Text.Encoding.Default.GetString(LastResponse));
                            Serial.ECUVersionNumber = 2;
                        }
                        else if (this._port.BytesToRead == 1048)
                        {
                            this.LastResponse = new byte[1048];
                            this._port.Read(this.LastResponse, 0, 1048);
                            Serial.ECUVersionNumber = 3;
                        }
                }
                else
                {
                    int bytesToRead = this._port.BytesToRead;
                    byte[] buffer = new byte[bytesToRead];
                    this._port.Read(buffer, 0, bytesToRead);
                    //Console.WriteLine(">>>>>>>>>>>>> this is eof received");
                    foreach (byte num in buffer)
                        //Console.WriteLine(("Recvd:" + (object)num));
                        if (true)
                            Serial.logger.Error((object)"Error Received eof ");
                }
                this._mre.Set();
            }
            catch (Exception ex)
            {
                Serial.logger.Error((object)"Error in parsing received data failed", ex);
            }
        }

        private void Transmit(byte[] data)
        {
            this.connectOptionSelector = true;
            this._mre.Reset();
            this.Transmit(data, 0);
            if (!this._mre.WaitOne(10000, false))
                throw new TimeoutException("Timeout waiting for response!");
        }

        private void Transmit(byte[] data, int responseLength)
        {
            this.ResponseLength = responseLength;
            if (this._isDisposed)
                throw new ObjectDisposedException(nameof(Serial), "Serial object already disposed");
            if (!this._port.IsOpen)
                throw new InvalidOperationException("Port is not open!");
            if (this._port.BytesToWrite > 0)
                throw new InvalidOperationException("Transmit timeout!");
            if (this._port.BytesToRead > 0)
                this.ClearReadBuffer();
            try
            {
                if (responseLength > 0)
                {
                    if (true)
                        //Console.WriteLine(("Setting threshold to length:" + (object)responseLength));
                        this._port.ReceivedBytesThreshold = responseLength;
                }
                else
                {
                    if (true)
                        //Console.WriteLine("Setting threshold to length:2");
                        this._port.ReceivedBytesThreshold = 2;
                }
                if (true)
                    //Console.WriteLine(("Writing data lenght:" + (object)data.Length));
                    this._port.Write(data, 0, data.Length);
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                throw new IOException("Transmit error :" + ex.Message, ex);
            }
        }

        private void Transmit2(int responseLength)
        {
            this.ResponseLength = responseLength;
            if (this._isDisposed)
                throw new ObjectDisposedException(nameof(Serial), "Serial object already disposed");
            if (!this._port.IsOpen)
                throw new InvalidOperationException("Port is not open!");
            if (this._port.BytesToWrite > 0)
                throw new InvalidOperationException("Transmit timeout!");
            if (this._port.BytesToRead > 0)
                this.ClearReadBuffer();
            try
            {
                if (responseLength > 0)
                {
                    if (true)
                        //Console.WriteLine(("Setting threshold to length:" + (object)responseLength));
                        this._port.ReceivedBytesThreshold = responseLength;
                }
                else
                {
                    if (true)
                        //Console.WriteLine("Setting threshold to length:2");
                        this._port.ReceivedBytesThreshold = 2;
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Transmit error :" + ex.Message, ex);
            }
        }

        private void TransmitAndWait(byte[] data, int responseLength)
        {
            this._mre.Reset();
            this.Transmit(data, responseLength);
            if (!this._mre.WaitOne(5000, false))
                throw new TimeoutException("Timeout waiting for response!");
        }

        private void TransmitAndWaitDelay(byte[] data, int responseLength)
        {
            this._mre.Reset();
            this.Transmit(data, responseLength);
            if (!this._mre.WaitOne(20000, false))
                throw new TimeoutException("Timeout waiting for response!");
            if (this.LastResponse.Length != responseLength)
                throw new InvalidOperationException("Failed Response:" + (object)this.LastResponse.Length);
        }

        private void JustTransmit(byte[] data) => this._port.Write(data, 0, data.Length);

        private void TransmitOnly(byte[] data, int resp)
        {
            this._mre.Reset();
            this.Transmit(data, resp);
            if (!this._mre.WaitOne(120000, false))
                throw new TimeoutException("Timeout waiting for response!");
        }

        private void WaitForCommand(int delay, int responseLength)
        {
            int millisecondsTimeout = delay;
            this._mre.Reset();
            this.Transmit2(responseLength);
            if (!this._mre.WaitOne(millisecondsTimeout, false))
                throw new TimeoutException("Timeout waiting for response!");
            if (this.LastResponse.Length != delay)
                throw new InvalidOperationException("Failed Response:" + (object)this.LastResponse.Length);
        }

        private void ClearReadBuffer()
        {
            int bytesToRead = this._port.BytesToRead;
            if (bytesToRead <= 0)
                return;
            this._port.Read(new byte[bytesToRead], 0, bytesToRead);
        }

        private void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (!Serial.logger.IsErrorEnabled)
                return;
            Serial.logger.Error((object)("Error received on serial port" + e.ToString()));
        }

        private static byte[] InitializeData(Command command) => new byte[1]
        {
      (byte) command
        };

        private static byte[] FormatResponse(byte[] data)
        {
            byte[] numArray = new byte[data.Length - 2];
            int num = 0;
            for (int index = 1; index < data.Length - 1; ++index)
                numArray[num++] = data[index];
            return numArray;
        }

        public static string GetStringFromByteArray(byte[] byteArray)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in byteArray)
                stringBuilder.Append(num).Append(',');
            --stringBuilder.Length;
            return stringBuilder.ToString();
        }

        public SimpleCommandResponsetData CallConnectCommand()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                byte[] data = Serial.InitializeData(this.GetConnectCommand());
                //Console.WriteLine(("Connect command: " + this.GetConnectCommand().ToString()));
                this.Transmit(data);
                Thread.Sleep(200);
                //Console.WriteLine(("LastResponse.Length: " + (object)this.LastResponse.Length));
                if (this.LastResponse.Length == 1048 || this.LastResponse.Length == 34)
                {
                    if (this.LastResponse[0] == (byte)252 && this.LastResponse[this.LastResponse.Length - 1] == (byte)248)
                    {
                        commandResponsetData.logData = this.LastResponse;
                        commandResponsetData.Success = true;
                        if (true)
                            //Console.WriteLine("Connect success!!!");
                            //Console.WriteLine(("response.Success: " + commandResponsetData.Success));
                            return commandResponsetData;
                    }
                    //Console.WriteLine(("response.Success: " + commandResponsetData.Success));
                }
                if (this.LastResponse.Length == 2)
                {
                    if (this.LastResponse[0] == (byte)252 && this.LastResponse[this.LastResponse.Length - 1] == (byte)248)
                    {
                        commandResponsetData.logData = this.LastResponse;
                        commandResponsetData.Success = true;
                        if (true)
                            //Console.WriteLine("Connect success!!!");
                            //Console.WriteLine(("response.Success: " + (object)commandResponsetData.Success));
                            return commandResponsetData;
                    }
                    //Console.WriteLine(("response.Success: " + (object)commandResponsetData.Success));
                }
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error connecting to ECU");
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error executing connect command");
                throw;
            }
        }

        public SimpleCommandResponsetData NormalConnectDevice()
        {
            SimpleCommandResponsetData commandResponsetData1 = new SimpleCommandResponsetData();
            try
            {
                SimpleCommandResponsetData commandResponsetData2 = this.SendConnectCommand(6);
                if (!commandResponsetData2.Success)
                    return commandResponsetData2;
                commandResponsetData2.Success = true;
                return commandResponsetData2;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error executing connect command");
                throw;
            }
        }

        public SimpleCommandResponsetData ConnectCommandToBootLoaderRecovery()
        {
            SimpleCommandResponsetData bootLoaderRecovery = new SimpleCommandResponsetData();
            try
            {
                bootLoaderRecovery = this.SendConnectCommand(6);
                if (!bootLoaderRecovery.Success)
                    return bootLoaderRecovery;
                bootLoaderRecovery.Success = true;
                return bootLoaderRecovery;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)("Error executing connect command" + (object)ex));
            }
            return bootLoaderRecovery;
        }

        public ECUData CallReadCommand()
        {
            try
            {
                SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
                byte[] data = Serial.InitializeData(Command.ReadECUData);
                if (Serial.ECUVersionNumber == 2)
                    this.TransmitAndWait(data, 2395);
                if (Serial.ECUVersionNumber == 3)
                    this.TransmitAndWait(data, 2495);
                commandResponsetData.logData = this.LastResponse;
                return new ECUData(Serial.FormatResponse(this.LastResponse));
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Exception in call read command");
                throw;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData CallWriteDeviceIDCommand(byte[] deviceIDData)
        {
            try
            {
                SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
                this.TransmitAndWait(Serial.InitializeData(Command.WriteDeviceID), 1);
                if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)252)
                {
                    this.TransmitAndWait(deviceIDData, 1);
                    if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)248)
                        commandResponsetData.Success = true;
                }
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Exception in call read command");
                throw;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public RealTimeData CallRealTimeCommand()
        {
            try
            {
                SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
                this.TransmitAndWait(Serial.InitializeData(Command.RealTime), 73);
                commandResponsetData.logData = this.LastResponse;
                return new RealTimeData(Serial.FormatResponse(this.LastResponse));
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call Realtime command", ex);
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData CallWriteCommand(byte[] writeData)
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                this.TransmitAndWait(Serial.InitializeData(Command.Write), 1);
                if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)252)
                {
                    commandResponsetData.logData = this.LastResponse;
                    this.TransmitAndWait(writeData, 1);
                    if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)248)
                    {
                        commandResponsetData.logData2 = this.LastResponse;
                        commandResponsetData.Success = true;
                        return commandResponsetData;
                    }
                }
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call write command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData CallBurnCommand(byte[] writeData)
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                Thread.Sleep(500);
                this.TransmitAndWait(Serial.InitializeData(Command.Write), 1);
                if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)252)
                {
                    commandResponsetData.logData = this.LastResponse;
                    this.TransmitAndWait(writeData, 1);
                    if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)248)
                    {
                        commandResponsetData.logData2 = this.LastResponse;
                        Thread.Sleep(1000);
                        this.TransmitAndWait(Serial.InitializeData(Command.Burn), 1);
                        if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)252)
                            commandResponsetData.Success = true;
                        Thread.Sleep(3000);
                        return commandResponsetData;
                    }
                }
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call burn command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData CallLockBurnCommand(byte[] writeData)
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                this.TransmitAndWait(Serial.InitializeData(Command.Write), 1);
                if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)252)
                {
                    commandResponsetData.logData = this.LastResponse;
                    writeData[717] = (byte)0;
                    this.TransmitAndWait(writeData, 1);
                    if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)248)
                    {
                        commandResponsetData.logData2 = this.LastResponse;
                        this.TransmitAndWait(Serial.InitializeData(Command.Burn), 1);
                        if (this.LastResponse.Length == 1 && this.LastResponse[0] == (byte)252)
                            commandResponsetData.Success = true;
                        Thread.Sleep(3000);
                        return commandResponsetData;
                    }
                }
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call burn command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData UpgradeFirmwareRecSendCharA()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                byte[] data = Serial.InitializeData(Command.charLowerCaseA);
                //Console.WriteLine(("Connect command: " + Command.charLowerCaseA.ToString()));
                this.TransmitAndWaitDelay(data, 2);
                if (this.LastResponse.Length == 2)
                {
                    commandResponsetData.logData = this.LastResponse;
                    if (this.LastResponse[0] == (byte)252 && this.LastResponse[1] == (byte)244)
                    {
                        commandResponsetData.Success = true;
                        //Console.WriteLine(("response.Success: " + (object)commandResponsetData.Success));
                        return commandResponsetData;
                    }
                    if (this.LastResponse[0] != (byte)252 || this.LastResponse[1] != (byte)245)
                        return commandResponsetData;
                    commandResponsetData.Success = false;
                    //Console.WriteLine(("response.Success: " + (object)commandResponsetData.Success));
                    return commandResponsetData;
                }
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)("Bootloader response error did not receive ack :received length:" + (object)this.LastResponse.Length + " received data:" + (object)this.LastResponse[0]));
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData UpgradeFirmwareRecBLMEnter()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                this.JustTransmit(Serial.InitializeData(Command.BootLoaderBLM));
                Thread.Sleep(500);
                commandResponsetData.Success = true;
                //Console.WriteLine(("BLU Sending Connect 2 response: " + (object)commandResponsetData.Success));
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData UpgradeFirmwareRecSendCharB()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                this.TransmitAndWait(Serial.InitializeData(Command.charLowerCaseB), 1);
                if (this.LastResponse.Length == 1)
                {
                    commandResponsetData.logData = this.LastResponse;
                    if (this.LastResponse[0] == (byte)252)
                    {
                        this.TransmitOnly(this.bootLoaderText[Serial.fileIndex], 1);
                        if (this.LastResponse.Length == 1)
                        {
                            commandResponsetData.logData2 = this.LastResponse;
                            if (this.LastResponse[0] == (byte)242)
                            {
                                ++Serial.fileIndex;
                                commandResponsetData.Success = true;
                                return commandResponsetData;
                            }
                            if (this.LastResponse[0] == (byte)243)
                            {
                                commandResponsetData.Success = false;
                                return commandResponsetData;
                            }
                        }
                    }
                    return commandResponsetData;
                }
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)("Bootloader response error did not receive ack :received length:" + (object)this.LastResponse.Length + " received data:" + (object)this.LastResponse[0]));
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData UpgradeFirmwareRecSendCharD()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                this.TransmitAndWait(Serial.InitializeData(Command.charLowerCaseD), 2);
                if (this.LastResponse.Length == 2)
                {
                    commandResponsetData.logData = this.LastResponse;
                    if (this.LastResponse[0] == (byte)252 && this.LastResponse[1] == (byte)248)
                    {
                        commandResponsetData.Success = true;
                        return commandResponsetData;
                    }
                }
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData PTUpgradeFirmware()
        {
            SimpleCommandResponsetData commandResponsetData1 = new SimpleCommandResponsetData();
            try
            {
                this.TransmitAndWait(Serial.InitializeData(Command.ProcessorTwo), 2);
                if (this.LastResponse.Length == 2)
                {
                    if (this.LastResponse[0] != (byte)252 || this.LastResponse[1] != (byte)248)
                        return commandResponsetData1;
                    SimpleCommandResponsetData commandResponsetData2 = this.SendConnectCommand(6);
                    if (!commandResponsetData2.Success)
                        return commandResponsetData2;
                    commandResponsetData2.Success = true;
                    return commandResponsetData2;
                }
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)("Bootloader response error did not receive ack :received length:" + (object)this.LastResponse.Length + " received data:" + (object)this.LastResponse[0]));
                return commandResponsetData1;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData UpgradeFirmware()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                this.JustTransmit(Serial.InitializeData(Command.BootLoaderBLM));
                Thread.Sleep(500);
                commandResponsetData.Success = true;
                //Console.WriteLine(("BLU Sending Connect 2 response: " + (object)commandResponsetData.Success));
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData SendConnectCommand(int count)
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            commandResponsetData.Success = true;
            for (int index = 0; index <= count; ++index)
            {
                byte[] data = Serial.InitializeData(this.GetConnectCommand());
                //Console.WriteLine(("BLU Sending Connect: " + (object)index));
                this.Transmit(data);
                Thread.Sleep(200);
                if (this.LastResponse.Length == 2 || this.LastResponse.Length == 34 || this.LastResponse.Length == 1048)
                {
                    if (this.LastResponse[0] == (byte)252 && this.LastResponse[this.LastResponse.Length - 1] == (byte)248)
                    {
                        commandResponsetData.isInBootloaderMode = false;
                        commandResponsetData.Success = true;
                    }
                    if (this.LastResponse[0] == (byte)200 && this.LastResponse[this.LastResponse.Length - 1] == (byte)248)
                    {
                        commandResponsetData.isInBootloaderMode = true;
                        commandResponsetData.Success = true;
                    }
                }
                else
                {
                    commandResponsetData.Success = false;
                    return commandResponsetData;
                }
            }
            return commandResponsetData;
        }

        private Command GetConnectCommand() => Command.Connect;

        public SimpleCommandResponsetData UpgradeFirmwareStep2()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                //Console.WriteLine("Before sending char A - Serial");
                this.TransmitAndWait(Serial.InitializeData(Command.charLowerCaseA), 2);
                if (this.LastResponse.Length == 2)
                {
                    //Console.WriteLine("After sending char A - Serial");
                    commandResponsetData.logData = this.LastResponse;
                    if (this.LastResponse[0] == (byte)252 && this.LastResponse[1] == (byte)244)
                    {
                        commandResponsetData.Success = true;
                        return commandResponsetData;
                    }
                    if (this.LastResponse[0] != (byte)252 || this.LastResponse[1] != (byte)245)
                        return commandResponsetData;
                    commandResponsetData.Success = false;
                    return commandResponsetData;
                }
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)("Bootloader response error did not receive ack :received length:" + (object)this.LastResponse.Length + " received data:" + (object)this.LastResponse[0]));
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData UpgradeFirmwareStep3()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                //Console.WriteLine("Before sending char B - Serial");
                this.TransmitAndWait(Serial.InitializeData(Command.charLowerCaseB), 1);
                if (this.LastResponse.Length == 1)
                {
                    if (Serial.logger.IsInfoEnabled)
                        //Console.WriteLine("After sending char B - Serial");
                        commandResponsetData.logData = this.LastResponse;
                    if (this.LastResponse[0] == (byte)252)
                    {
                        this.TransmitOnly(this.bootLoaderText[Serial.fileIndex], 1);
                        if (this.LastResponse.Length == 1)
                        {
                            commandResponsetData.logData2 = this.LastResponse;
                            if (this.LastResponse[0] == (byte)242)
                            {
                                ++Serial.fileIndex;
                                commandResponsetData.Success = true;
                                return commandResponsetData;
                            }
                            if (this.LastResponse[0] == (byte)243)
                            {
                                commandResponsetData.Success = false;
                                return commandResponsetData;
                            }
                        }
                    }
                    return commandResponsetData;
                }
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)("Bootloader response error did not receive ack :received length:" + (object)this.LastResponse.Length + " received data:" + (object)this.LastResponse[0]));
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public SimpleCommandResponsetData UpgradeFirmwareStep4()
        {
            SimpleCommandResponsetData commandResponsetData = new SimpleCommandResponsetData();
            try
            {
                if (Serial.logger.IsInfoEnabled)
                    //Console.WriteLine("Before sending char D - Serial");
                    this.TransmitAndWait(Serial.InitializeData(Command.charLowerCaseD), 2);
                if (this.LastResponse.Length == 2)
                {
                    commandResponsetData.logData = this.LastResponse;
                    if (this.LastResponse[0] == (byte)252 && this.LastResponse[1] == (byte)248)
                    {
                        if (Serial.logger.IsInfoEnabled)
                            //Console.WriteLine("After sending char D - Serial");
                            commandResponsetData.Success = true;
                        return commandResponsetData;
                    }
                }
                return commandResponsetData;
            }
            catch (Exception ex)
            {
                if (Serial.logger.IsErrorEnabled)
                    Serial.logger.Error((object)"Error in call boor loader command");
                throw ex;
            }
            finally
            {
                this.ClearReadBuffer();
            }
        }

        public void SendE()
        {
            this.TransmitAndWaitDelay(Serial.InitializeData(Command.charLowerCaseE), 2);
            if (this.LastResponse.Length != 2)
                return;
            byte num1 = this.LastResponse[0];
            byte num2 = this.LastResponse[1];
        }

        private string ToString2(byte[] bytes)
        {
            string empty = string.Empty;
            foreach (byte num in bytes)
                empty += num.ToString();
            return empty;
        }
    }
}
