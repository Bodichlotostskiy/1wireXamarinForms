﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.DalSemi.OneWire.Container
{

	public class HumidityContainerConstants
	{
		// high temperature alarm
		public const int ALARM_HIGH = 1;

		// low temperature alarm
		public const int ALARM_LOW = 0;

	}

	/// <summary>
	/// 
	///     1-Wire Humidity interface class for basic Humidity measuring
	///     operations. This class should be implemented for each Humidity
	///     type 1-Wire device.
	///
	///     The HumidityContainer methods can be organized into the following categories:
	///     Information
	///   		GetHumidity
	///     	GetHumidityResolution
	///     	GetHumidityAlarm
	///     	GetHumidityAlarmResolution
	///     	GetHumidityResolution
	///     	GetHumidityResolutions
	///     	HasSelectableHumidityResolution
	///     	HasHumidityAlarms
	///     	IsRelative
	///
	///     Options
	///     	DoHumidityConvert
	///     	SetHumidityAlarm}
	///     	SetHumidityResolution}
	///
	///     I/O
	///     	ReadDevice
	///     	WriteDevice
	/// 
	/// 
	/// </summary>
	public interface IHumidityContainer : IOneWireSensor
	{
		///	Humidity Feature methods

		/// <summary>
		/// Checks to see if humidity value given is a 'relative' humidity value.
		/// </summary>
		/// <returns>true if this HumidityContainer provides a relative humidity reading</returns>
		bool IsRelative();

		/// <summary>
		/// Checks to see if this Humidity measuring device has high/low trip alarms.
		/// </summary>
		/// <returns>true if this HumidityContainer has high/low trip alarms</returns>
		bool HasHumidityAlarms();

		/// <summary>
		/// Checks to see if this device has selectable Humidity resolution.
		/// </summary>
		/// <returns>true if this HumidityContainer has selectable Humidity resolution</returns>
		bool HasSelectableHumidityResolution();

		/// <summary>
		/// Get an array of available Humidity resolutions in percent humidity (0 to 100).
		/// </summary>
		/// <returns>
		/// byte array of available Humidity resolutions in percent with
		/// minimum resolution as the first element and maximum resolution
		/// as the last element.
		/// </returns>
		double[] GetHumidityResolutions();

		/// <summary>
		/// Gets the Humidity alarm resolution in percent.
		/// </summary>
		/// <returns>Humidity alarm resolution in percent for this 1-wire device</returns>
		/// <exception cref="OneWireException">Device does not support Humidity alarms</exception>
		double GetHumidityAlarmResolution();

		//--------
		//-------- Humidity I/O Methods
		//--------

		/// <summary>
		/// Performs a Humidity conversion.
		/// </summary>
		/// <exception cref="OneWireIOException">
		/// on a 1-Wire communication error such as 
		/// reading an incorrect CRC from a 1-Wire device.  This could be
		/// caused by a physical interruption in the 1-Wire Network due to 
		/// shorts or a newly arriving 1-Wire device issuing a 'presence pulse'.
		/// </exception>
		/// <exception cref="OneWireException">on a communication or setup error with the 1-Wire adapter</exception>
		void DoHumidityConvert(byte[] state);

		//--------
		//-------- Humidity 'get' Methods
		//--------

		/// <summary>
		/// Gets the humidity expressed as a percent value (0.0 to 100.0) of humidity.
		/// </summary>
		/// <param name="state">state byte array with device state information</param>
		/// <returns>humidity expressed as a percent</returns>
		double GetHumidity(byte[] state);

		/// <summary>
		/// Gets the current Humidity resolution in percent from the
		/// state data retrieved from the ReadDevice() method
		/// </summary>
		/// <param name="state">state byte array with device state information</param>
		/// <returns>
		/// Humidity resolution in percent for this 1-wire device
		/// </returns>
		double GetHumidityResolution(byte[] state);

		/// <summary>
		/// Gets the specified Humidity alarm value in percent from the
		/// state data retrieved from the ReadDevice() method.
		/// </summary>
		/// <param name="alarmType">
		/// Valid value: HumidityContainerConstants.ALARM_HIGH or HumidityContainerConstants.ALARM_LOW</param>
		/// <param name="state">byte array with device state information</param>
		/// <returns>alarm trip values in percent for this 1-wire device</returns>
		/// <exception cref="OneWireException">Device does not support Humidity alarms</exception>
		double GetHumidityAlarm(int alarmType, byte[] state);

		//--------
		//-------- Humidity 'set' Methods
		//--------

		/// <summary>
		/// Sets the Humidity alarm value in percent in the provided state data.
		/// Use the method WriteDevice() with this data to finalize the change to the device.
		/// </summary>
		/// <param name="alarmType">Valid value: HumidityContainerConstants.ALARM_HIGH or HumidityContainerConstants.ALARM_LOW</param>
		/// <param name="alarmValue">alarm trip value in percent</param>
		/// <param name="state">byte array with device state information</param>
		/// <exception cref="OneWireException">Device does not support Humidity alarms</exception>
		void SetHumidityAlarm(int alarmType, double alarmValue, byte[] state);

		/// <summary>
		/// Sets the current Humidity resolution in percent in the provided
		/// state data. Use the method WriteDevice() with this data to finalize the change to the device.
		/// </summary>
		/// <param name="resolution">Humidity resolution in percent</param>
		/// <param name="state">byte array with device state information</param>
		/// <exception cref="OneWireException">Device does not support selectable humidity resolution</exception>
		void SetHumidityResolution(double resolution, byte[] state);
	}
}
