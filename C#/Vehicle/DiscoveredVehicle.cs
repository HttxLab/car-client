using System.Net;
using CarClient.C_.Components.Discovery;

namespace CarClient.C_.Vehicle;

public class DiscoveredVehicle(string name, bool isReady, IPEndPoint endPoint, uint firmware, float voltage, VehicleMode mode) {

    public DisplayedVehicle Display { get; set; }
    
    public string Name { get; } = name;
    public bool IsReady { get; } = isReady;
    
    public IPEndPoint EndPoint { get; } = endPoint;
    
    public uint Firmware { get; } = firmware;
    public float Voltage { get; } = voltage;
    public VehicleMode Mode { get; } = mode;

}