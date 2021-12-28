using System.Drawing;
using System.Windows.Forms;
using static BotStarter.WindowHelper;

namespace BotStarter.MousePointer
{
    public class Pointer : IPointer
    {
        public void Move(int x, int y)
        {
            Cursor.Position = new Point(x, y);
        }

        public void Pause(int timeout)
        {
            Thread.Sleep(timeout);
        }

        public void LeftClick()
        {

        }

        public void RightClick()
        {
        }
    }
}