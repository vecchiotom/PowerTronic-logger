namespace PTlog
{
    internal class RealTimeDataPoint
    {
        public int elapsedTime { get; private set; }
        public int RPM { get; private set; }
        public int TPS { get; private set; }

        public RealTimeDataPoint(int elapsedTime, int RPM, int TPS)
        {
            this.elapsedTime = elapsedTime;
            this.RPM = RPM;
            this.TPS = TPS;
        }

    }
}
