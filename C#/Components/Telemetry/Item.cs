using Godot;

namespace CarClient.C_.Components.Telemetry;

public partial class Item : HBoxContainer {
	
	[Export] private Texture2D _icon;
	[Export] private Color _fontColor;

	[Export] private bool _hasNeighbor;
	
	public override void _Ready() {
		GetNode<TextureRect>("Icon").Texture = _icon;
		GetNode<Label>("Label").AddThemeColorOverride("font_color", _fontColor);
		GetNode<VSeparator>("Separator").Visible = _hasNeighbor;
	}
	
}
