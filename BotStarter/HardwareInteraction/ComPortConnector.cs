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

        public void RotateMotor(int amount, int motorIndex, int initialNumber, int goalNumber, int delay)
        {
            string portName = _configuration.GetParameters()["comPortName"];

            SerialPort port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);

            port.Open();
            port.WriteLine($"{amount} {motorIndex} {initialNumber} {goalNumber} {delay}");
            port.Close();
        }

        public void RotateTwoMotors(int amount, int motorIndex1, int motorIndex2, int initialNumber, int goalNumber, int delay)
        {
            string portName = _configuration.GetParameters()["comPortName"];

            SerialPort port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);

            port.Open();
            port.WriteLine($"{amount} {motorIndex1} {motorIndex2} {initialNumber} {goalNumber} {delay}");
            port.Close();
        }

        public void RotateThreeMotors(int amount, int motorIndex1, int motorIndex2, int motorIndex3, int initialNumber, int goalNumber, int delay)
        {
            string portName = _configuration.GetParameters()["comPortName"];

            SerialPort port = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);

            port.Open();
            port.WriteLine($"{amount} {motorIndex1} {motorIndex2} {motorIndex3} {initialNumber} {goalNumber} {delay}");
            port.Close();
        }
    }
}
