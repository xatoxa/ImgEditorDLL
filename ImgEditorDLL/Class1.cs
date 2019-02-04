using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImgEditorDLL
{
    [Guid("A304F49A-73FC-4F09-9E36-07A5E0FBA716")]
    internal interface IMyClass
    {
        [DispId(1)]

        Image Cut_image(Image image, uint x1, uint y1, uint x2, uint y2);

        Image Insert_image(Image image1, uint x1, uint y1, Image image2);

    }


    [Guid("EFF09595-1513-4451-AD0E-C6D59AC2B3D4"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IMyEvents
    {

    }


    [Guid("C2788AF5-3211-4F9D-B7FA-457C3A3F3FE4"), ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(IMyEvents))]
    public class MyClass : IMyClass
    {
        public Image Cut_image(Image image, uint x1, uint y1, uint x2, uint y2)
        {
            if (image == null)
            {
                MessageBox.Show("image not found;\nimage == NULL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (x2 > image.Width)
            {
                MessageBox.Show("x2 exceeds the width of image1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (y2 > image.Height)
            {
                MessageBox.Show("y2 exceeds the height of image1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (x1 > (image.Width - x2))
            {
                MessageBox.Show("x1 exceeds the new width of image1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (y1 > (image.Height - y2))
            {
                MessageBox.Show("y1 exceeds the new height of image1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }


            Bitmap bmp = image as Bitmap;

            Bitmap result = bmp.Clone(new Rectangle((int)x1, (int)y1, (int)x2, (int)y2), bmp.PixelFormat);

            return result;
        }



        public Image Insert_image(Image image1, uint x1, uint y1, Image image2)
        {
            if (image1 == null)
            {
                MessageBox.Show("image1 not found;\nimage1 == NULL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (image2 == null)
            {
                MessageBox.Show("image 2 not found;\nimage2 == NULL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (x1 > image1.Width)
            {
                MessageBox.Show("x exceeds the width of image1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (y1 > image1.Height)
            {
                MessageBox.Show("y exceeds the height of image1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }


            Bitmap result = new Bitmap(image1.Width, image1.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(image1, 0, 0, image1.Width, image1.Height);
                g.DrawImage(image2, x1, y1, image2.Width, image2.Height);

                return result;
            }
        }
    }
}
