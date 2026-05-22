using Godot;

namespace CarClient.C_;

public partial class Controller : Node {

	public static Controller Instance { get; private set; }

	[Export] private PackedScene _discovery;
	[Export] private PackedScene _dashboard;
	[Export] private PackedScene _tlsPrompt;
	
	private Node _currentScreen;
	
	public enum Screens {
		Discovery,
		Dashboard,
		TlsPrompt
	}
	
	public override void _Ready() {
		Instance = this;
		
		// We want to start with the discovery screen
		SwitchScreen(Screens.Discovery);
	}

	public void SwitchScreen(Screens screen) {
		GD.Print($"Switching to screen: {screen}");
		_currentScreen?.QueueFree();

		_currentScreen = screen switch {
			Screens.Discovery => _discovery.Instantiate(),
			Screens.Dashboard => _dashboard.Instantiate(),
			Screens.TlsPrompt => _tlsPrompt.Instantiate(),
			_ => _currentScreen
		};
		AddChild(_currentScreen);
	}
	
}