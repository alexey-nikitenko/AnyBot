using System.IO.Ports;

namespace BotStarter
{
    internal class ComPortConnector : IComPortConnector
    {
        public void RotateMotor(int motorIndex, int magicNumber)
        {
            var ports = SerialPort.GetPortNames();

            foreach (var portName in ports)
            {
                if (portName != null)
                {
                    SerialPort port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                    port.Open();
                    port.WriteLine($"{motorIndex} {magicNumber}#");
                    port.Close();
                }
            }
        }
    }
}
