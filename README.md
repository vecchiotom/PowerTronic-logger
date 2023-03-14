# PowerTronic ECU Datalogger

This is a C# program that interfaces with PowerTronic ECUs to datalog data such as RPM, AFR values, and other useful datapoints for creating a proper engine map. The main purpose of this program is to save the data in a CSV file named PTlog.csv.

## Requirements

To run this program, you need:

- A PowerTronic ECU
- A computer running Windows, MacOS, or Linux
- The .NET Core 3.1 or later runtime installed on your computer

## Usage

1. Connect your PowerTronic ECU to your computer via USB.
2. Run the program.
3. The program will automatically detect the PowerTronic ECU and start logging data to PTlog.csv.
4. To stop logging data, simply close the program.
