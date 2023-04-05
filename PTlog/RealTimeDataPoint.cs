namespace PTlog
{
    internal class RealTimeDataPoint
    {
        public int elapsedTime { get; private set; }
        public int RPM { get; private set; }
        public int TPS { get; private set; }
        public float AFR { get; private set; }

        public RealTimeDataPoint(int elapsedTime, int RPM, int TPS, float AFR)
        {
            this.elapsedTime = elapsedTime;
            this.RPM = RPM;
            this.TPS = TPS;
            this.AFR = AFR;
        }

    }
}
