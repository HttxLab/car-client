using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Zeroconf;

namespace CarClient.C_.Screens;

public partial class Discovery : Control
{
	
	[Export] private string _serviceType;
	
	public override void _Ready() {
		GetNode<Timer>("Timer").Timeout += OnTimerTimeout;
		OnTimerTimeout(); // Start the discovery immediately
	}

	private void OnTimerTimeout() {
		GD.Print("Starting discovery of vehicles...");
		
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