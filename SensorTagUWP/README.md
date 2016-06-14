# Connecting an actual device
The final step in implementing a Cloud Service for IoT solution is to connect the solution to actual devices. If all of the proceeding technical guides have been effective, the Cloud Service Provider (CSP) will now have a functioning IoT solution deployed to Azure, a simulated device, and a Power BI dashboard. Once the real device is connected, it should smoothly pass data through the IoT solution and into the Power BI dashboard, just like the simulated device does.

## Technical Guide

This technical guide will show you how to leverage a basic field gateway to gather data from a sensor and send it to the Cloud Service for IoT solution hosted in Azure. When you've completed this guide, you will have provisioned and configured the following resources:

+	A Windows Universal App that can serve as a field gateway between a real device and the Cloud Service for IoT solution

Before starting this guide, you will need to install a couple of tools on your local PC, this guide assumes you are running an RTM version of Windows 10. 

Prerequisites

+	A device to connect to the application. The device used in this guide is a [Texas Instrument SensorTag](http://www.ti.com/ww/en/wireless_connectivity/sensortag2015/index.html?INTC=SensorTag&HQS=sensortag) This device should cost around $29 on the Texas Instrument website.
+	A bluetooth enabled laptop or PC
+	A provisioned Azure Subscription: This guide is designed for Cloud Service Providers (CSP), but any relevant Azure Subscription will suffice.
     +	Provisioned CSP account and customer subscription in portal
     +	Administrative access to a CSP Subscription (this assumes you already have a co-admin login for a CSP subscription)
+	Visual Studio 2015 Community Edition or greater (Visual Studio Professional is the version used for this document) 
 
## Connect the SensorTag to your PC

Once you acquire a TI SensorTag, the first step is to connect it to your PC.
Connecting the SensorTag consists of a few simple steps:
1. 	Remove the battery tab
2.	Turn on the device by holding the power button for three seconds


3.	Once the device is powered own, check the LED status light to ensure it shows a blinking Green light. This means it is ready to connect.
4.	Pair the device. Open the Bluetooth settings on your PC or Laptop and pair the device. Once paired you should see CC2650 SensorTag listed in connected Bluetooth devices.
 
## Run the application

The accompanying solution (SensorTagUniversal.sln) in the source folder of this repo will pull data from the SensorTag device and send it to Azure. To run the application, pull down the solution from the repository. Open the solution in Visual Studio, it should load a similarly named project. To run the application, Start Debugging on the Local Machine. 
 

## Bluetooth Error

If running the application produces an error (Exception of Type 'X2CodingLab.SensorTag.Exceptions.DeviceNotFoundException' was thrown), then there was likely an error connecting the SensorTag to the PC/Laptop. Double check Bluetooth connectivity and try again.
 
## Connected Device
If the device was properly connected and the application was able to access the device, a message box will prompt with the message: “Device Connected”. The application will also display the model name and serial number of the device.

## Interacting with telemetry data
To make it easier to understand how the solution works, data pulls and data publications are currently manual processes.
### Pulling data
To pull data, first initialize the device by clicking the initialize button. This will prepare the device to send a stream of environmental data.  Next click Read Data to get the most recent readings from the device.

### Send Data to IoT
+	To publish the telemetry data to you IoT solution, click on **Connect to IoT Hub**.
+	Enter you’re the URL of your IoT Hub
+	Then enter the **Device Key**
+	Click **Send to IoT** to package and send the captured telemetry data to the IoT solution in Azure.

This process can be repeated as often as you like. Each time it will add data to the IoT hub. If you’ve been following along with the technical guides, at this point data should be flowing from the device to the Cloud Services for IoT, and into Power BI dashboards in seconds.