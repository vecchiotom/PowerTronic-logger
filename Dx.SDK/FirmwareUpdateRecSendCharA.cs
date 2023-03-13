// Decompiled with JetBrains decompiler
// Type: Dx.SDK.FirmwareUpdateRecSendCharA
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.IO;
using System.Reflection;

namespace Dx.SDK
{
    public class FirmwareUpdateRecSendCharA : ICommand
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(FirmwareUpdateRecSendCharA));

        public Serial Serial { get; set; }

        public byte[] sendData { get; set; }

        public void Execute()
        {
            try
            {
                if (FirmwareUpdateRecSendCharA.logger.IsDebugEnabled)
                    FirmwareUpdateRecSendCharA.logger.Debug((object)"Calling ECU to BootLoader");
                ECUManager.Instance.PauseRealTime();
                ECUManager.Instance.publishResponseToQueue((object)this.Serial.UpgradeFirmwareRecSendCharA(), "ECU_FIRMWARE_UPGRADE_SEND_CHAR_A_TOPIC_RESPONSE");
            }
            catch (IOException ex)
            {
                if (FirmwareUpdateRecSendCharA.logger.IsErrorEnabled)
                    FirmwareUpdateRecSendCharA.logger.Error((object)"Error in realtime command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Connection error!!", "CONNECTION_ERROR");
            }
            catch (InvalidOperationException ex)
            {
                if (!FirmwareUpdateRecSendCharA.logger.IsErrorEnabled)
                    return;
                FirmwareUpdateRecSendCharA.logger.Error((object)"Error in burn command", (Exception)ex);
            }
            catch (Exception ex)
            {
                if (!FirmwareUpdateRecSendCharA.logger.IsErrorEnabled)
                    return;
                FirmwareUpdateRecSendCharA.logger.Error((object)"Error in burn command", ex);
            }
            finally
            {
                ECUManager.Instance.ResumeRealTime(false);
            }
        }
    }
}
