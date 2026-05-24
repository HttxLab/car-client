using System.Net;

namespace CarClient.C_.Vehicle.Discovery;

public class ConnectableVehicle(IPEndPoint endPoint) {

    public IPEndPoint EndPoint { get; set; } = endPoint;

}