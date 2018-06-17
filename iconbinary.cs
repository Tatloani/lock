using System;
using System.Drawing;
using System.Windows.Forms;

[Serializable]
class Icons
{
    private Icon[] list;

    public Icons(int i)
    {
        if (i < 1)
            throw new Exception("Value most be positive");
        list = new Icon[i];
    }

    public Icon this[int index]
    {
        get
        {
            if (index < 0 || index > list.Length)
                throw new IndexOutOfRangeException("Out of Range Value");
            else return list[index];
        }
        set
        {
            if (index < 0 || index > list.Length)
                throw new IndexOutOfRangeException("Out of Range Value");
            else list[index] = value;
        }
    }
}