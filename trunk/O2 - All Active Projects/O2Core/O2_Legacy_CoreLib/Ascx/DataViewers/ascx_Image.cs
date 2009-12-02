using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using O2.Legacy.CoreLib;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    public partial class ascx_Image : UserControl
    {
        public bool bMouseDown;
        public bool bTemp;
        public int iOriginal_X;
        public int iOriginal_Y;
        public int iPictureSize_Height;
        public int iPictureSize_Width;

        public ascx_Image()
        {
            InitializeComponent();
        }

        public ascx_Image(String sImageToLoad)
        {
            InitializeComponent();
            loadImageIntoPictureBox(sImageToLoad);
        }

        public ascx_Image(Image iImageToLoad)
        {
            InitializeComponent();
            picBoxImage.Image = iImageToLoad;
        }

        // this will take a screenshot of the sender
        /*public ascx_Image(Object oSender)
        {
        }*/

        public void updateImage(Image iImage)
        {
            picBoxImage.Image = iImage;
            picBoxImage.Refresh();
        }

        public void loadImageIntoPictureBox(String sImageToLoad)
        {
            if (false == File.Exists(sImageToLoad))
            {
                sImageToLoad = (Path.Combine(DI.o2CorLibConfig.CurrentExecutableDirectory, sImageToLoad));
                if (false == File.Exists(sImageToLoad))
                {
                    //sImageToLoad = (Path.Combine(DI.o2CorLibConfig.sCurrentExecutableDirectory, sImageToLoad));
                    //if (false == File.Exists(sImageToLoad))
                    //{
                    DI.log.error("In ascx_Image could not load image: {0}");
                    return;
                    //}
                }
            }
            picBoxImage.ImageLocation = sImageToLoad;
        }

        public void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            picBoxImage.Image = null;
            if (oVar.GetType().Name == "Bitmap")
                picBoxImage.Image = (Bitmap) oVar;
            String sDataReceived = oVar.ToString();
            if (File.Exists(sDataReceived))
                picBoxImage.Image = Image.FromFile(sDataReceived);
        }

        public void ascx_DropObject1_Load(object sender, EventArgs e)
        {
        }

        private void picBoxImage_Click(object sender, EventArgs e)
        {
        }

        private void picBoxImage_DoubleClick(object sender, EventArgs e)
        {
            var mevMouseEventArgs = (MouseEventArgs) e;
            switch (mevMouseEventArgs.Button)
            {
                case MouseButtons.Left:
                    DI.log.debug("left click");
                    iPictureSize_Width = picBoxImage.Width = picBoxImage.Width*2;
                    iPictureSize_Height = picBoxImage.Height = picBoxImage.Height*2;
                    picBoxImage.Left -= iPictureSize_Width/2 - mevMouseEventArgs.X;
                    picBoxImage.Top -= iPictureSize_Height/2 - mevMouseEventArgs.Y;
                    movePicture(picBoxImage.Left, picBoxImage.Top);
                    break;
                case MouseButtons.Right:
                    DI.log.debug("right click");
                    iPictureSize_Width = picBoxImage.Width = picBoxImage.Width/2;
                    iPictureSize_Height = picBoxImage.Height = picBoxImage.Height/2;

                    //    picBoxImage.Left += iPictureSize_Width  + mevMouseEventArgs.X;

                    //picBoxImage.Top -= iPictureSize_Height / 2 - mevMouseEventArgs.Y;

                    movePicture(picBoxImage.Left, picBoxImage.Top);
                    break;
            }
        }

        private void picBoxImage_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true;
            iOriginal_X = e.X;
            iOriginal_Y = e.Y;
            iPictureSize_Width = picBoxImage.Width;
            iPictureSize_Height = picBoxImage.Height;
            DI.log.debug("bMouseDown = true");
        }

        private void picBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMouseDown)
            {
                DI.log.debug("x:{0}, y{1}", e.X.ToString(), e.Y.ToString());

                int iNewLeftValue = picBoxImage.Left - ((iOriginal_X - e.X)/1);
                int iNewTopValue = picBoxImage.Top - ((iOriginal_Y - e.Y)/1);
                movePicture(iNewLeftValue, iNewTopValue);
            }
        }

        private void picBoxImage_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;
            DI.log.debug("bMouseDown = false");
        }

        public void movePicture(int iNewLeftValue, int iNewTopValue)
        {
            if (iNewLeftValue < (-iPictureSize_Width + 30))
                iNewLeftValue = (-iPictureSize_Width + 30);
            if (iNewLeftValue > (iPictureSize_Width - 30))
                iNewLeftValue = (iPictureSize_Width - 30);
            picBoxImage.Left = iNewLeftValue;

            if (iNewTopValue < (-iPictureSize_Height + 30))
                iNewTopValue = (-iPictureSize_Height + 30);
            if (iNewTopValue > (iPictureSize_Height - 30))
                iNewTopValue = (iPictureSize_Height - 30);
            picBoxImage.Top = iNewTopValue;

            picBoxImage.Refresh();
        }
    }
}