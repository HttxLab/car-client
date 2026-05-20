using Godot;

namespace CarClient.Scripts.Components.Discovery;

public partial class Service : PanelContainer {

	[Export] private string _name;
	[Export] private bool _isReady;
	
	
	public override void _Ready() {
	}
	
}
