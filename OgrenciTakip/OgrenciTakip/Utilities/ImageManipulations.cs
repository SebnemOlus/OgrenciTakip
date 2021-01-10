using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgrenciTakip.Utilities
{
    public class ImageManipulations
    {
        public static byte[] GetPhoto(PictureBox pb)
        {
            MemoryStream ms = new MemoryStream();

            pb.Image.Save(ms, pb.Image.RawFormat);

            return ms.GetBuffer();
        }

        public static Image PutPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }
    }
}
