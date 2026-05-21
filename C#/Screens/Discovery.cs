using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CarClient.C_.Components.Discovery;
using CarClient.C_.Vehicle;
using Godot;
using Zeroconf;

namespace CarClient.C_.Screens;

public partial class Discovery : Control
{
	
	[ExportGroup("mDNS")]
	[Export] private string _serviceType;
	
	[ExportGroup("Scenes")]
	[Export] private PackedScene _vehicleScene;

	private VBoxContainer _vehiclesContainer;
	private List<DiscoveredVehicle> _vehicles = [];
	
	public override void _Ready() {
		_vehiclesContainer = GetNode<VBoxContainer>("CenterContainer/MainColumn/Vehicles");
		
		// Add signal handlers
		GetNode<Timer>("Timer").Timeout += OnTimerTimeout;
		
		// Start the discovery immediately
		OnTimerTimeout();
	}

	private void AddVehicle(DiscoveredVehicle vehicle) {
		var node = _vehicleScene.Instantiate<DisplayedVehicle>();
		
		_vehiclesContainer.AddChild(node);
		_vehiclesContainer.Visible = true;
		
		node.SetVehicleDetails(vehicle);
		vehicle.Display = node;
		_vehicles.Add(vehicle);
		
		GD.Print($"Added vehicle: {vehicle.Name} to vehicle list.");
	}
	
	private void OnTimerTimeout() {
		GD.Print("Starting discovery of vehicles...");
		
		// Add test vehicle
		var address = IPEndPoint.Parse("127.0.0.1:8080");
		var v1 = new DiscoveredVehicle(Random.Shared.Next(99).ToString(), Random.Shared.Next(0, 2) > 0, address, (uint)Random.Shared.Next(24), (float)Random.Shared.NextDouble(), Random.Shared.Next(0, 2) > 0 ? Vehicle.Mode.Ai : Vehicle.Mode.Manual);
		AddVehicle(v1);
		
		// Dispatch discovery to a background thread
		Task.Run(DiscoverAsync);
	}

	private async Task DiscoverAsync() {
		IReadOnlyList<IZeroconfHost> responses =
			await ZeroconfResolver.ResolveAsync(_serviceType, scanTime: TimeSpan.FromSeconds(4));
		
		if (responses.Count == 0) {
			GD.Print("No vehicles found. Retrying in 4 seconds...");
			return;
		}

		foreach (var vehicle in responses) {
			GD.Print($"Found Vehicle: {vehicle.DisplayName} at {vehicle.IPAddress}");
		}
	} 
	
}
