namespace DeviceCompanion.Interfaces.Models
{
    public readonly struct SensorState
    {
        public bool IsConnecting { get; private init; }
        public bool IsPolling { get; private init; }
        public bool IsConnected {  get; private init; }

        public SensorState CopyWith(bool? isConnecting = null, bool? isPolling = null, bool? isConnected = null)
        {
            return new SensorState
            {
                IsConnecting = isConnecting ?? this.IsConnecting,
                IsPolling = isPolling ?? this.IsPolling,
                IsConnected = isConnected ?? this.IsConnected,
            };
        }
    }
}
