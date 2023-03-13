// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ConfigurationHandler
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

namespace Dx.SDK
{
    public class ConfigurationHandler
    {
        private static readonly object syncObject = new object();
        private static volatile ConfigurationHandler _instance = (ConfigurationHandler)null;

        private ConfigurationHandler()
        {
        }

        public static ConfigurationHandler Instance
        {
            get
            {
                if (ConfigurationHandler._instance == null)
                {
                    lock (ConfigurationHandler.syncObject)
                    {
                        if (ConfigurationHandler._instance == null)
                            ConfigurationHandler._instance = new ConfigurationHandler();
                    }
                }
                return ConfigurationHandler._instance;
            }
        }

        public RealTimeConfiguration RealTimeConfig { get; set; }
    }
}
