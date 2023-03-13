// Decompiled with JetBrains decompiler
// Type: Dx.SDK.QueueManager
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using System.Collections;

namespace Dx.SDK
{
    public sealed class QueueManager
    {
        private static readonly object syncObject = new object();

        private QueueManager()
        {
        }

        public static Queue GetQueue
        {
            get
            {
                lock (QueueManager.syncObject)
                    return Queue.Synchronized(new Queue());
            }
        }
    }
}
