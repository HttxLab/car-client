using Godot;

namespace CarClient.C_;

public partial class Controller : Node {

	[Export] private PackedScene _discovery;
	[Export] private PackedScene _dashboard;
	
	private Node _currentScreen;
	
	public enum Screens {
		Discovery,
		Dashboard
	}
	
	public override void _Ready() {
		// We want to start with the discovery screen
		SwitchScreen(Screens.Discovery);
	}

	public void SwitchScreen(Screens screen) {
		GD.Print($"Switching to screen: {screen}");
		_currentScreen?.QueueFree();

		_currentScreen = screen switch {
			Screens.Discovery => _discovery.Instantiate(),
			Screens.Dashboard => _dashboard.Instantiate(),
			_ => _currentScreen
		};
		AddChild(_currentScreen);
	}
	
}