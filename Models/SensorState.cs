namespace DeviceCompanionAvalonia.Models
{
    public struct SensorState
    {
        public bool IsConnecting { get; set; }
        public bool IsPolling { get; set; }
        public bool IsConnected {  get; set; }

        public SensorState CopyWith(bool? IsConnecting = null, bool? IsPolling = null, bool? IsConnected = null)
        {
            return new()
            {
                IsConnecting = IsConnecting ?? this.IsConnecting,
                IsPolling = IsPolling ?? this.IsPolling,
                IsConnected = IsConnected ?? this.IsConnected,
            };
        }
    }
}
