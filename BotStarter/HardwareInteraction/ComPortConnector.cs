using System.IO.Ports;

namespace BotStarter.HardwareInteraction
{
    public class ComPortConnector : IComPortConnector
    {
        IConfiguration _configuration;
        public ComPortConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RotateMotor(int motorIndex, int initialNumber, int goalNumber, int delay)
        {
            string portName = _configuration.GetParameters()["comPortName"];

            SerialPort port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);

            port.Open();
            port.WriteLine($"{motorIndex} {initialNumber} {goalNumber} {delay} #");
            port.Close();
        }
    }
}
