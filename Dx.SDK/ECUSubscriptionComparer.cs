// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ECUSubscriptionComparer
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using System.Collections.Generic;

namespace Dx.SDK
{
    public class ECUSubscriptionComparer : IEqualityComparer<ECUSubscription>
    {
        public bool Equals(ECUSubscription x, ECUSubscription y) => x.Topic == y.Topic && x.Method == y.Method;

        public int GetHashCode(ECUSubscription obj) => object.ReferenceEquals((object)obj, (object)null) ? 0 : obj.Topic.GetHashCode() ^ obj.Method.GetHashCode();
    }
}
