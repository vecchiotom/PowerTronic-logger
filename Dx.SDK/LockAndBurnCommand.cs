// Decompiled with JetBrains decompiler
// Type: Dx.SDK.LockAndBurnCommand
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.IO;
using System.Reflection;

namespace Dx.SDK
{
    internal class LockAndBurnCommand : ICommand
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(LockAndBurnCommand));

        public Serial Serial { get; set; }

        public byte[] sendData { get; set; }

        public void Execute()
        {
            try
            {
                if (LockAndBurnCommand.logger.IsDebugEnabled)
                    LockAndBurnCommand.logger.Debug((object)"Calling ECU Lock and Burn");
                ECUManager.Instance.PauseRealTime();
                ECUManager.Instance.publishResponseToQueue((object)this.Serial.CallLockBurnCommand(this.sendData), "ECU_LOCK_BURN_RESPONSE_TOPIC");
            }
            catch (IOException ex)
            {
                if (LockAndBurnCommand.logger.IsErrorEnabled)
                    LockAndBurnCommand.logger.Error((object)"Error in realtime command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Connection error!!", "CONNECTION_ERROR");
            }
            catch (InvalidOperationException ex)
            {
                if (!LockAndBurnCommand.logger.IsErrorEnabled)
                    return;
                LockAndBurnCommand.logger.Error((object)"Error in lock and burn command", (Exception)ex);
            }
            catch (Exception ex)
            {
                if (!LockAndBurnCommand.logger.IsErrorEnabled)
                    return;
                LockAndBurnCommand.logger.Error((object)"Error in burn command", ex);
            }
            finally
            {
                ECUManager.Instance.ResumeRealTime(true);
            }
        }
    }
}
