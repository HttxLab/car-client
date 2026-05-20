using Godot;

namespace CarClient.C_.Screens.Mode;

public partial class DriveMode : PanelContainer {
	
	[Export] private Button _manualDriveButton;
	[Export] private Button _autoDriveButton;
	
	public override void _Ready() {
		_manualDriveButton.Pressed += OnManualPressed;
		_autoDriveButton.Pressed += OnAutoPressed;
	}

	private void OnManualPressed() {
		_manualDriveButton.Disabled = true;
		_autoDriveButton.Disabled = false;
	}
	
	private void OnAutoPressed() {
		_autoDriveButton.Disabled = true;
		_manualDriveButton.Disabled = false;
	}
	
}