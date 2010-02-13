using System;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Control_ExtensionMethods
    {
        #region Control

        public static Control add_Control(this Control control, Type childControlType)
        {
            return (Control)control.invokeOnThread(
                                 () =>
                                 {
                                     var childControl =
                                         (Control)
                                         PublicDI.reflection.createObjectUsingDefaultConstructor(childControlType);
                                     if (control != null)
                                     {
                                         childControl.Dock = DockStyle.Fill;
                                         control.Controls.Add(childControl);
                                         return childControl;
                                     }
                                     return null;
                                 });
        }

        public static Control clear(this Control control)
        {
            return (Control)control.invokeOnThread(
                () =>
                {
                    control.Controls.Clear();
                    return control;
                });
        }

        public static Control fill(this Control control)
        {
            control.Dock = DockStyle.Fill;
            return control;
        }

        public static Control mapToWidth(this Control hostControl, Control control, bool alignToTop)
        {
            if (alignToTop)
                control.anchor_TopLeftRight();
            else
                control.anchor_BottomLeftRight();

            const int pad = 5;
            control.Left = hostControl.Left + pad;
            control.Width = hostControl.Width - pad - pad;
            return control;
        }

        public static void onDrop(this Control control, Action<string> onDropFileOrFolder)
        {
            control.AllowDrop = true;
            control.DragEnter += (sender, e) => Dnd.setEffect(e);
            control.DragDrop += (sender, e)
                 =>
            {
                var fileOrFolder = Dnd.tryToGetFileOrDirectoryFromDroppedObject(e);
                if (fileOrFolder.valid())
                    onDropFileOrFolder(fileOrFolder);
            };
            //
        }
        //		

        public static T add<T>(this Control hostControl, params int[] position)
        {
            return hostControl.add_Control<T>(position);
        }
        public static T add_Control<T>(this Control hostControl, params int[] position)
        {
            var values = new [] { -1, -1, -1, -1 };
            for (int i = 0; i < position.Length; i++)
                values[i] = position[i];
            return hostControl.add_Control<T>(values[0], values[1], values[2], values[3]);
        }

        public static T add_Control<T>(this Control hostControl, int top, int left, int width, int height)
        {
            return (T)hostControl.invokeOnThread(
                () =>
                {
                    var newControl = (Control)typeof(T).ctor();
                    if (top == -1 && left == -1 && width == -1 && height == -1)
                        newControl.fill();
                    else
                    {
                        if (top > -1)
                            newControl.Top = top;
                        if (left > -1)
                            newControl.Left = left;
                        if (width > -1)
                            newControl.Width = width;
                        if (height > -1)
                            newControl.Height = height;
                    }
                    hostControl.Controls.Add(newControl);
                    return newControl;
                });
        }

        #endregion

    }
}
