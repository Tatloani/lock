using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

public class Lock
{
    static NotifyIcon Ico;
    static Icons iconos;

    public static void Main()
    {
        if(File.Exists("Icons"))
        {
            Stream openFileStream = File.OpenRead("Icons");
            BinaryFormatter deserializer = new BinaryFormatter();
            iconos = (Icons)deserializer.Deserialize(openFileStream);
            openFileStream.Close();
        }else
        {
            iconos = new Icons(4);
            iconos[0] = new System.Drawing.Icon(@"..\..\icos\allred.ico");
            iconos[1] = new System.Drawing.Icon(@"..\..\icos\Ngreen.ico");
            iconos[2] = new System.Drawing.Icon(@"..\..\icos\allgreen.ico");
            iconos[3] = new System.Drawing.Icon(@"..\..\icos\Cgreen.ico");

            Stream SaveFileStream = File.Create("Icons");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, iconos);
            SaveFileStream.Close();
        }
        Init();
        new Thread(Validate).Start();
        Application.Run();
        Environment.Exit(0);
    }

    static void Validate()
    {
        for (;;)
        {
            Ico.Icon = iconos[getStatus()];
        }
    }

    private static void Init()
    {
        Ico = new NotifyIcon();
        Ico.Icon = iconos[0];
        Ico.Visible = true;
        Ico.Text = "Status of Numlock & Capslock";
        Ico.DoubleClick += new EventHandler(Dispose);
    }
    
    private static void Dispose(object obj, EventArgs e)
    {
        Ico.Dispose();
        Application.Exit();        
    }


    private static byte getStatus()
    {
        if (!Control.IsKeyLocked(Keys.NumLock) && !Control.IsKeyLocked(Keys.CapsLock))
            return 0;
        if (Control.IsKeyLocked(Keys.NumLock) && !Control.IsKeyLocked(Keys.CapsLock))
            return 1;
        if (Control.IsKeyLocked(Keys.NumLock) && Control.IsKeyLocked(Keys.CapsLock))
            return 2;
        else return 3;
    }
}
