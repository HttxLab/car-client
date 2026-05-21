using System.Collections.Generic;
using CarClient.C_.Vehicle;
using Godot;
using Godot.Collections;

namespace CarClient.C_.Components.Discovery;

public partial class DisplayedVehicle : PanelContainer {

	[ExportGroup("Icons")]
	[Export] private Array<string> _icons;

	[ExportGroup("Ready")]
	[Export] private Color _readyColor;
	[Export] private StyleBoxFlat _readyStyle;
	
	[ExportGroup("Not Ready")]
	[Export] private Color _notReadyColor;
	[Export] private StyleBoxFlat _notReadyStyle;

	private Label _icon;

	private Label _name;
	private Label _badge;
	private Label _address;

	private Label _batteryValue;
	private Label _modeValue;
	
	private DiscoveredVehicle _vehicle;

	public override void _Ready() {
		_icon = GetNode<Label>("Layout/LeftInfo/IconPanel/IconLabel");

		_name = GetNode<Label>("Layout/LeftInfo/TextRows/NameRow/Name");
		_badge = GetNode<Label>("Layout/LeftInfo/TextRows/NameRow/Badge");
		_address = GetNode<Label>("Layout/LeftInfo/TextRows/IPRow");

		_batteryValue = GetNode<Label>("Layout/RightActions/TxtRecords/Battery/Value");
		_modeValue = GetNode<Label>("Layout/RightActions/TxtRecords/Mode/Value");
		
		// Add signal handler
		GetNode<Button>("Layout/RightActions/Button").Pressed += OnPressed;
	}

	private void OnPressed() {
		if (_vehicle != null) {
			// Connect
		}
	}

	public void SetVehicleDetails(DiscoveredVehicle vehicle) {
		_vehicle = vehicle;
		
		_icon.Text = _icons.PickRandom();
		_name.Text = vehicle.Name;

		if (vehicle.IsReady) {
			_badge.Text = "READY";
			_badge.AddThemeStyleboxOverride("normal", _readyStyle);
			_badge.AddThemeColorOverride("font_color", _readyColor);
		} else {
			_badge.Text = "NOT READY";
			_badge.AddThemeStyleboxOverride("normal", _notReadyStyle);
			_badge.AddThemeColorOverride("font_color", _notReadyColor);
		}
		
		_address.Text = $"{vehicle.EndPoint.Address}:{vehicle.EndPoint.Port}";

		_batteryValue.Text = $"{vehicle.Voltage} V";
		_modeValue.Text = $"{vehicle.Mode}";
	}
}