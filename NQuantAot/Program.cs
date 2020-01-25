using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using nQuant;

namespace NQuant
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;
            foreach (string arg in args)
            {
                WuQuantizer q = new WuQuantizer();
                Bitmap b = (Bitmap)Bitmap.FromFile(arg);
                if (b.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    Bitmap b2 = new Bitmap(b.Width, b.Height, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(b2))
                        g.DrawImage(b, new Rectangle(0, 0, b2.Width, b2.Height));
                    b.Dispose();
                    b = b2;
                }
                Image i = q.QuantizeImage(b, 0, 1);//FIXME: make options
                i.Save(Path.GetFileNameWithoutExtension(arg) + Environment.TickCount + ".png", ImageFormat.Png);
            }
        }
    }
}
