using System.IO.Ports;

namespace BotStarter.HardwareInteraction
{
    public class ComPortConnector : IComPortConnector
    {
        //public void RotateMotor(int motorIndex, int magicNumber)
        //{
        //    var ports = SerialPort.GetPortNames();

        //    foreach (var portName in ports)
        //    {
        //        if (portName != null)
        //        {
        //            SerialPort port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
        //            port.Open();
        //            port.WriteLine($"{motorIndex} {magicNumber}#");
        //            port.Close(); 
        //        }
        //    }
        //}

        public void RotateMotor(int motorIndex, int magicNumber)
        {
            SerialPort port = new SerialPort("COM7", 115200, Parity.None, 8, StopBits.One);
            port.Open();
            port.WriteLine($"{motorIndex} {magicNumber} #");
            port.Close();
        }

        public void RotateMotor(int motorIndex, int initialNumber, int goalNumber, int delay)
        {
            SerialPort port = new SerialPort("COM7", 115200, Parity.None, 8, StopBits.One);
            port.Open();
            port.WriteLine($"{motorIndex} {initialNumber} {goalNumber} {delay} #");
            port.Close();
        }
    }
}
