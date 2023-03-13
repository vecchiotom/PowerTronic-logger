// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ConnectCommand
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.Reflection;

namespace Dx.SDK
{
    public class ConnectCommand : ICommand
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(ConnectCommand));
        private Serial serial = (Serial)null;

        public string PortName { get; set; }

        public byte[] sendData { get; set; }

        public void Execute()
        {
            try
            {
                this.serial.Connect(this.PortName);
                ECUManager.Instance.publishResponseToQueue((object)this.serial.CallConnectCommand(), "ECU_CONNECT_RESPONSE");
            }
            catch (InvalidOperationException ex)
            {
                if (ConnectCommand.logger.IsErrorEnabled)
                    ConnectCommand.logger.Error((object)"Error in ECUConnect command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error connecting to ECU", "SDK_ERROR");
            }
            catch (Exception ex)
            {
                if (ConnectCommand.logger.IsErrorEnabled)
                    ConnectCommand.logger.Error((object)"Error in ECUConnect command", ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error connecting to ECU", "SDK_ERROR");
            }
        }

        public Serial Serial
        {
            get => this.serial;
            set => this.serial = value;
        }
    }
}
