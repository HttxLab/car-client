using System.Net;
using CarClient.C_.Components.Discovery;

namespace CarClient.C_.Vehicle;

public class DiscoveredVehicle(string name, bool locked, IPEndPoint endPoint, uint firmware, float voltage, Mode mode) {

    public DisplayedVehicle Display { get; set; }
    
    public string Name { get; } = name;
    public bool Locked { get; } = locked;
    
    public IPEndPoint EndPoint { get; } = endPoint;
    
    public uint Firmware { get; } = firmware;
    public float Voltage { get; } = voltage;
    public Mode Mode { get; } = mode;

}