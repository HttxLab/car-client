using System.Collections.Generic;
using CarClient.C_.Vehicle;
using Godot;
using Godot.Collections;

namespace CarClient.C_.Components.Discovery;

public partial class DisplayedVehicle : PanelContainer {

	[ExportGroup("Icons")]
	[Export] private Array<string> _icons;

	[ExportGroup("Unlocked")]
	[Export] private Color _badgeUnlockedColor;
	[Export] private StyleBoxFlat _badgeUnlockedStyle;
	
	[ExportGroup("Locked")]
	[Export] private StyleBoxFlat _lockedStyle;
	[Export] private Color _badgeLockedColor;
	[Export] private StyleBoxFlat _badgeLockedStyle;
	
	private Label _icon;

	// Information
	private Label _name;
	private Label _badge;
	private Label _address;

	// Txt Records
	private HBoxContainer _txtRecords;
	private Label _batteryValue;
	private Label _modeValue;
	
	// LockedInfo
	private Label _lockedInfo;
	
	// Buttons
	private Button _button;
	
	private DiscoveredVehicle _vehicle;

	public override void _Ready() {
		_icon = GetNode<Label>("Layout/LeftInfo/IconPanel/IconLabel");

		_name = GetNode<Label>("Layout/LeftInfo/TextRows/NameRow/Name");
		_badge = GetNode<Label>("Layout/LeftInfo/TextRows/NameRow/Badge");
		_address = GetNode<Label>("Layout/LeftInfo/TextRows/Address");

		_txtRecords = GetNode<HBoxContainer>("Layout/RightActions/TxtRecords");
		_batteryValue = GetNode<Label>("Layout/RightActions/TxtRecords/Battery/Value");
		_modeValue = GetNode<Label>("Layout/RightActions/TxtRecords/Mode/Value");
		
		_lockedInfo = GetNode<Label>("Layout/RightActions/LockedInfo");
		
		_button = GetNode<Button>("Layout/RightActions/Button");
		
		// Add signal handler
		GetNode<Button>("Layout/RightActions/Button").Pressed += OnPressed;
	}

	private void OnPressed() {
		if (_vehicle != null) {
			// Connect
			Controller.Instance.SwitchScreen(Controller.Screens.TlsPrompt);
		}
	}

	public void SetVehicleDetails(DiscoveredVehicle vehicle) {
		_vehicle = vehicle;
		
		_icon.Text = _icons.PickRandom();
		_name.Text = vehicle.Name;
		
		_address.Text = $"{vehicle.EndPoint.Address}:{vehicle.EndPoint.Port}";

		_batteryValue.Text = $"{vehicle.Voltage} V";
		_modeValue.Text = $"{vehicle.Mode}";
		
		// Set style based on vehicle state
		if(vehicle.Locked) {
			SetLocked();
		} else {
			SetUnlocked();
		}
	}

	private void SetUnlocked() {
		_lockedInfo.Visible = false;
		_txtRecords.Visible = true;
		
		_badge.Text = "READY";
		_badge.AddThemeStyleboxOverride("normal", _badgeUnlockedStyle);
		_badge.AddThemeColorOverride("font_color", _badgeUnlockedColor);
		
		_button.Disabled = false;
		_button.Text = "CONNECT";
	}
	
	private void SetLocked() {
		AddThemeStyleboxOverride("panel", _lockedStyle);
		
		_lockedInfo.Visible = true;
		_txtRecords.Visible = false;
		
		_badge.Text = "NOT READY";
		_badge.AddThemeStyleboxOverride("normal", _badgeLockedStyle);
		_badge.AddThemeColorOverride("font_color", _badgeLockedColor);
		
		_button.Disabled = true;
		_button.Text = "LOCKED";
	}
	
}