// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ECUManager
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Dx.SDK
{
    public class ECUManager : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(ECUManager));
        private static readonly object syncObject = new object();
        private static volatile ECUManager _instance = (ECUManager)null;
        private bool _isDisposed;
        private Serial serialPort = (Serial)null;
        private Thread rtThread = (Thread)null;
        private List<ECUSubscription> handlers;
        private QueueHandler callBackQueue = (QueueHandler)null;
        private QueueHandler commandQueue = (QueueHandler)null;
        private ConfigurationHandler configHandler = (ConfigurationHandler)null;
        private ManualResetEvent manualPauseResume = new ManualResetEvent(true);
        public bool isRealTimeStarted = false;
        public static bool enableRealTime = true;
        private string realtimeFilePath = string.Empty;
        private bool realtimeFileSet = false;
        private int realtimeFileEventsTotalCount = 0;
        private int realtimeFileEventsCurrPos = 0;
        private bool recordOn = false;
        private bool replayOn = false;
        private List<byte[]> recordedEventList = (List<byte[]>)null;

        public ConfigurationHandler ConfigHandler
        {
            get => this.configHandler;
            set => this.configHandler = value;
        }

        private ECUManager()
        {
            this.serialPort = new Serial();
            this.handlers = new List<ECUSubscription>();
            this.rtThread = new Thread(new ThreadStart(this.PollRealTime));
            this.callBackQueue = new QueueHandler();
            this.commandQueue = new QueueHandler();
            this.commandQueue.QueueDequeued += new DequeuHandler(this.commandQueue_QueueDequeued);
            this.configHandler = ConfigurationHandler.Instance;
            this.callBackQueue.QueueDequeued += new DequeuHandler(this.callBackQueue_QueueDequeued);
            this.recordedEventList = new List<byte[]>();
        }

        private void commandQueue_QueueDequeued(object data)
        {
            try
            {
                if (!(data is ICommand command))
                    return;
                command.Execute();
            }
            catch (Exception ex)
            {
                this.publishResponseToQueue((object)"Error in command execute", "SDK_ERROR");
            }
        }

        public static ECUManager Instance
        {
            get
            {
                if (ECUManager._instance == null)
                {
                    lock (ECUManager.syncObject)
                    {
                        if (ECUManager._instance == null)
                            ECUManager._instance = new ECUManager();
                    }
                }
                return ECUManager._instance;
            }
        }

        private void callBackQueue_QueueDequeued(object data)
        {
            ECUMessage ecuMessage = data as ECUMessage;
            this.NotifyHandlers(ecuMessage.Data, ecuMessage.Topic);
        }

        private void NotifyHandlers(object data, string topic)
        {
            foreach (ECUSubscription handler in this.handlers)
            {
                if (handler.Topic == topic)
                    handler.Method.BeginInvoke(data, (AsyncCallback)null, (object)null);
            }
        }

        public void QueueUpCommandToExecute(ICommand command) => this.commandQueue.Enqueue((object)command);

        public void AddHandler(ECUSubscription sub)
        {
            lock (this.handlers)
            {
                if (this.handlers.Contains(sub))
                    return;
                this.handlers.Add(sub);
                if (!ECUManager.logger.IsDebugEnabled) ;
            }
        }

        public void RemoveHandler(ECUSubscription sub)
        {
            lock (this.handlers)
            {
                if (!this.handlers.Contains(sub))
                    return;
                this.handlers.Remove(sub);
            }
        }

        public void ECUConnect(string portName)
        {
            try
            {
                this.serialPort.Connect(portName);
                this.publishResponseToQueue((object)this.serialPort.CallConnectCommand(), "ECU_CONNECT_RESPONSE");
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    //Console.WriteLine("Error in ECUConnect command", ex.Message);
                this.publishResponseToQueue((object)"Error connecting to ECU", "SDK_ERROR");
            }
        }

        public void FMNormalConnect(string portName)
        {
            try
            {
                this.serialPort.Connect(portName);
                this.publishResponseToQueue((object)this.serialPort.NormalConnectDevice(), "ECU_NORMAL_CONNECT_TO_DEVICE_RESPONSE");
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in ECUConnect command", ex);
                this.publishResponseToQueue((object)"Error connecting to ECU", "SDK_ERROR");
            }
        }

        public void BootLoaderConnectForRecovery(string portName)
        {
            try
            {
                ECUManager.logger.Info((object)("Port number: " + portName));
                this.serialPort.Connect(portName);
                this.publishResponseToQueue((object)this.serialPort.ConnectCommandToBootLoaderRecovery(), "ECU_CONNECT_BOOTLOADER_RECOVERY_RESPONSE");
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in ECUConnect command", ex);
                this.publishResponseToQueue((object)"Error connecting to ECU", "SDK_ERROR");
            }
        }

        public void BootLoaderConnectForProcSecRecovery()
        {
            try
            {
                this.publishResponseToQueue((object)this.serialPort.UpgradeFirmwareRecBLMEnter(), "ECU_FIRMWARE_UPGRADE_REC_BLM_ENTER_TOPIC_RESPONSE");
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in ECUConnect command", ex);
                this.publishResponseToQueue((object)"Error connecting to ECU", "SDK_ERROR");
            }
        }

        private void PollRealTime()
        {
            try
            {
                this.isRealTimeStarted = true;
                while (true)
                {
                    if (ECUManager.enableRealTime)
                    {
                        this.manualPauseResume.WaitOne(-1);
                        if (ECUManager.logger.IsDebugEnabled)
                            ECUManager.logger.Debug((object)"polling real time thread");
                        this.ECURealTime();
                        Thread.Sleep(90);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!ECUManager.logger.IsErrorEnabled)
                    return;
                ECUManager.logger.Error((object)"Error in Realtime poll thread", ex);
            }
        }

        public void StartRealTime()
        {
            if (!this.isRealTimeStarted)
            {
                if (this.rtThread != null)
                {
                    this.rtThread.Start();
                }
                else
                {
                    this.rtThread = new Thread(new ThreadStart(this.PollRealTime));
                    this.rtThread.Start();
                }
            }
            else
                this.ResumeRealTime(false);
        }

        public void PauseRealTime()
        {
            this.manualPauseResume.Reset();
            this.commandQueue.ClearAll();
        }

        public void ResumeRealTime(bool sleep)
        {
            if (sleep)
                Thread.Sleep(2000);
            this.commandQueue.ClearAll();
            this.manualPauseResume.Set();
        }

        public void ECURead()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new ReadCommand()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in read command", ex);
                this.publishResponseToQueue((object)"Error Reading ECU data", "SDK_ERROR");
            }
        }

        private void ECURealTime()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new RealTimeCommand()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in realtime command", ex);
                this.publishResponseToQueue((object)"Error Reading realtime data1", "SDK_ERROR");
            }
        }

        public void WriteDeviceID(byte[] data)
        {
            try
            {
                ProCommands proCommands = new ProCommands();
                proCommands.Serial = this.serialPort;
                byte[] numArray = data;
                proCommands.sendData = numArray;
                this.QueueUpCommandToExecute((ICommand)proCommands);
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in read command", ex);
                this.publishResponseToQueue((object)"Error Reading ECU data", "SDK_ERROR");
            }
        }

        public void ECUWrite(ECUData data)
        {
            try
            {
                if (data != null)
                {
                    WriteCommand writeCommand = new WriteCommand();
                    writeCommand.Serial = this.serialPort;
                    byte[] array = data.getByteArray().ToArray();
                    writeCommand.sendData = array;
                    this.QueueUpCommandToExecute((ICommand)writeCommand);
                }
                else
                    this.publishResponseToQueue((object)"ECU Data not well formed", "SDK_ERROR");
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in write command", ex);
                this.publishResponseToQueue((object)"Error sending data to ECU", "SDK_ERROR");
            }
        }

        public void GetDataFromECU(ECUData data)
        {
            try
            {
                if (data != null)
                {
                    Serial.DatFromUI = data.getByteArray().ToArray();
                    Serial.isMapLoadedFromUIToPopup = true;
                }
                else
                    this.publishResponseToQueue((object)"ECU Data not well formed - Error Getting data from ECU", "SDK_ERROR");
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in command", ex);
                this.publishResponseToQueue((object)"Error detting data from ECU", "SDK_ERROR");
            }
        }

        public void ECUBurn(ECUData data)
        {
            try
            {
                BurnCommand burnCommand = new BurnCommand();
                burnCommand.Serial = this.serialPort;
                byte[] array = data.getByteArray().ToArray();
                burnCommand.sendData = array;
                this.QueueUpCommandToExecute((ICommand)burnCommand);
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in burn command", ex);
                this.publishResponseToQueue((object)"Error burning data to ECU", "SDK_ERROR");
            }
        }

        public void ECULockBurn(ECUData data)
        {
            try
            {
                LockAndBurnCommand lockAndBurnCommand = new LockAndBurnCommand();
                lockAndBurnCommand.Serial = this.serialPort;
                byte[] array = data.getByteArray().ToArray();
                lockAndBurnCommand.sendData = array;
                this.QueueUpCommandToExecute((ICommand)lockAndBurnCommand);
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in burn command", ex);
                this.publishResponseToQueue((object)"Error burning data to ECU", "SDK_ERROR");
            }
        }

        public void PTBLMConnect()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new PTFirmwareUpdateCommand()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void BLMConnect()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateCommand()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void SendingCharA()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateStep2Command()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void RecSendCharA()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateRecSendCharA()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void RecBLMEnter()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateRecBLMEnter()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void RecSendCharB(List<byte[]> bootLoaderFile)
        {
            try
            {
                this.serialPort.bootLoaderText = bootLoaderFile;
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateRecSendCharB()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void RecSendCharD()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateRecSendCharD()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void RecStartProcTwo()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateRecStartProcTwo()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void BLMConnectStep3(List<byte[]> bootLoaderFile)
        {
            try
            {
                this.serialPort.bootLoaderText = bootLoaderFile;
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateStep3Command()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void BLMConnectStep4()
        {
            try
            {
                this.QueueUpCommandToExecute((ICommand)new FirmwareUpdateStep4Command()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void UpgradeFirmwareToECU(List<byte[]> bootLoaderFile)
        {
            try
            {
                this.serialPort.bootLoaderText = bootLoaderFile;
                this.QueueUpCommandToExecute((ICommand)new UpgradeFirmwareCommand()
                {
                    Serial = this.serialPort
                });
            }
            catch (Exception ex)
            {
                if (ECUManager.logger.IsErrorEnabled)
                    ECUManager.logger.Error((object)"Error in boot loader enabling", ex);
                this.publishResponseToQueue((object)"Error in boot loader enabling", "SDK_ERROR");
            }
        }

        public void publishResponseToQueue(object data, string topic)
        {
            try
            {
                //Console.WriteLine("PUBLISHING " + topic);
                Action<object> m = this.handlers.Find(handler => { return handler.Topic == topic; }).Method;
                Thread t = new Thread(() => { if (m != null) { m(data);} }) ;
                t.Start();
                /*this.callBackQueue.Enqueue((object)new ECUMessage()
                {
                    Data = data,
                    Topic = topic
                });*/
            }
            catch (Exception ex)
            {
                //Console.WriteLine(("Error in publishing to queue topic:" + topic), ex);
            }
        }

        public void Disconnect()
        {
            try
            {
                this.PauseRealTime();
                Thread.Sleep(100);
                this.serialPort.Disconnect();
            }
            catch (Exception ex)
            {
                if (!ECUManager.logger.IsErrorEnabled)
                    return;
                ECUManager.logger.Error((object)"Error disconnecting port", ex);
            }
        }

        public void LoadRealtimeFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
                else
                {
                    byte[] numArray = File.ReadAllBytes(filePath);
                    if (numArray.Length % 23 == 0)
                    {
                        this.realtimeFileEventsTotalCount = numArray.Length / 23;
                        this.realtimeFileEventsCurrPos = 0;
                        int num = 0;
                        while (num < numArray.Length)
                            ++num;
                    }
                    else
                        ECUManager.logger.Error((object)"ERROR File maybe corrupted or invalid");
                }
                this.realtimeFilePath = filePath;
                this.realtimeFileSet = true;
            }
            catch (Exception ex)
            {
                ECUManager.logger.Error((object)"Error loading realtime file", ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._isDisposed || !disposing)
                return;
            if (this.serialPort != null)
                this.serialPort.Dispose();
            if (this.rtThread != null && this.rtThread.IsAlive)
                this.rtThread.Abort();
            if (this.commandQueue != null)
            {
                this.commandQueue.Stop();
                this.commandQueue.Dispose();
            }
            if (this.callBackQueue != null)
            {
                this.callBackQueue.Stop();
                this.callBackQueue.Dispose();
            }
            if (this.manualPauseResume != null)
                this.manualPauseResume.Close();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }
    }
}
