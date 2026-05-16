using Godot;

namespace CarClient.Scripts;

public partial class Main : Control
{
	[Export] private TextureRect _forwards;
	[Export] private TextureRect _backwards;
	[Export] private TextureRect _left;
	[Export] private TextureRect _right;
	
	public override void _Ready() {
		GD.Print("Starting Car Client...");
	}
	
	public override void _Process(double delta) {
		
	}
}
