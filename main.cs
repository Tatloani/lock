using System;
using System.Threading;
using Lock.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Lock
{
    class Lock
    {
        private static Icon[] icons;
        private static NotifyIcon Notifyicon;
        private static Thread x;

        public static void Main()
        {
            icons = new Icon[4];
            icons[0] = Resources.allred;
            icons[1] = Resources.Ngreen;
            icons[2] = Resources.allgreen;
            icons[3] = Resources.Cgreen;

            Notifyicon = new NotifyIcon();
            Notifyicon.Text = "Status of numlock and capslock";
            Notifyicon.Icon = icons[getStatus()];
            Notifyicon.Visible = true;
            Notifyicon.MouseDoubleClick += new MouseEventHandler(Noticon_MouseDoubleClick);
            
            x = new Thread(Run);
            x.Start();
            Application.Run();
        }

        private static void Noticon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Notifyicon.Dispose();
            x.Abort();
            Application.Exit();
            Environment.Exit(0);
        }

        private static void Run()
        {
            /*
            while (true)
                Notifyicon.Icon = icons[getStatus()];
            */
            inicio:
            Notifyicon.Icon = icons[getStatus()];
            Thread.Sleep(100);
            goto inicio;
        }

        private static byte getStatus()
        {
            if (!Control.IsKeyLocked(Keys.NumLock) && !Control.IsKeyLocked(Keys.CapsLock))
                return 0;
            else if (Control.IsKeyLocked(Keys.NumLock) && !Control.IsKeyLocked(Keys.CapsLock))
                return 1;
            else if (Control.IsKeyLocked(Keys.NumLock) && Control.IsKeyLocked(Keys.CapsLock))
                return 2;
            else return 3;
        }
    }
}
