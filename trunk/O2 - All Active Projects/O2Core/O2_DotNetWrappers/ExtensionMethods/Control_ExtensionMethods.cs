using System;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Control_ExtensionMethods
    {

        #region Control - add

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

        public static T add<T>(this Control hostControl, params int[] position)
        {
            return hostControl.add_Control<T>(position);
        }
   
        public static T add_Control<T>(this Control hostControl, params int[] position)
        {
            var values = new[] { -1, -1, -1, -1 };
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

        #region Control - misc

        public static Control fill(this Control control)
        {
            control.Dock = DockStyle.Fill;
            return control;
        }

        public static void enabled(this Control control, bool state)
        {
            control.invokeOnThread(() => control.invoke("set_Enabled", state));
        }

        public static void visible(this Control control, bool state)
        {
            control.invokeOnThread(() => control.invoke("set_Visible", state));
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

        #endregion

        #region Control - events

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
        

        #endregion

        #region Control - remove

        public static void remove_ContextMenu(this Control control)
        {
            control.ContextMenuStrip = null;
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

        #endregion

        #region Control - location

        public static Control location(this Control control, int top)
        {
            return control.location(top, -1, -1, -1);
        }

        public static Control location(this Control control, int top, int left)
        {
            return control.location(top, left, -1, -1);
        }

        public static Control location(this Control control, int top, int left, int width)
        {
            return control.location(top, left, width, -1);
        }

        public static Control location(this Control control, int top, int left, int width, int height)
        {
            return (Control)control.invokeOnThread(
                () =>
                {
                    control.Dock = DockStyle.None;
                    if (top > -1)
                        control.Top = top;
                    if (left > -1)
                        control.Left = left;
                    if (top > -1)
                        control.Top = top;
                    if (width > -1)
                        control.Width = width;
                    if (height > -1)
                        control.Height = height;
                    return control;
                });
        }

        #endregion

        #region Control - align
        public static Control align_Left(this Control control, Control controlToAlignWith)
        {
            return control.align_Left(controlToAlignWith, 0);
        }

        public static Control align_Left(this Control control, Control controlToAlignWith, int border)
        {
            return (Control)control.invokeOnThread(
                () =>
                {
                    control.Dock = DockStyle.None;
                    control.Left = controlToAlignWith.Left + border;
                    control.left();
                    return control;
                });
        }

        public static Control align_Right(this Control control, Control controlToAlignWith)
        {
            return control.align_Right(controlToAlignWith, 0);
        }

        public static Control align_Right(this Control control, Control controlToAlignWith, int border)
        {
            return (Control)control.invokeOnThread(
                        () =>
                        {
                            control.Dock = DockStyle.None;
                            control.Width = controlToAlignWith.Width - control.Left - border;
                            control.right();
                            return control;
                        });
        }

        public static Control align_Top(this Control control, Control controlToAlignWith)
        {
            return control.align_Top(controlToAlignWith, 0);
        }

        public static Control align_Top(this Control control, Control controlToAlignWith, int border)
        {
            return (Control)control.invokeOnThread(
                        () =>
                        {
                            control.Dock = DockStyle.None;
                            control.Top = controlToAlignWith.Top + border;
                            control.top();
                            return control;
                        });
        }

        public static Control align_Bottom(this Control control, Control controlToAlignWith)
        {
            return control.align_Bottom(controlToAlignWith, 0);
        }

        public static Control align_Bottom(this Control control, Control controlToAlignWith, int border)
        {
            return (Control)control.invokeOnThread(
                        () =>
                        {
                            control.Dock = DockStyle.None;
                            PublicDI.log.debug("controlToAlignWith.Height: {0}", controlToAlignWith.Height);
                            PublicDI.log.debug("control.Top: {0}", control.Top);
                            control.Height = controlToAlignWith.Height - control.Top - border;
                            control.bottom();
                            return control;
                        });
        }
        #endregion

        #region Control - anchor

        public static Control anchor(this Control control)
        {
            control.Anchor = AnchorStyles.None;
            return control;
        }

        public static Control top(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Top;
            return control;
        }

        public static Control bottom(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Bottom;
            return control;
        }

        public static Control left(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Left;
            return control;
        }

        public static Control right(this Control control)
        {
            control.Anchor = control.Anchor | AnchorStyles.Right;
            return control;
        }

        public static Control anchor_TopLeft(this Control control)
        {
            control.anchor().top().left();
            return control;
        }

        public static Control anchor_BottomLeft(this Control control)
        {
            control.anchor().bottom().left();
            return control;
        }

        public static Control anchor_TopRight(this Control control)
        {
            control.anchor().top().right();
            return control;
        }

        public static Control anchor_BottomRight(this Control control)
        {
            control.anchor().bottom().right();
            return control;
        }

        public static Control anchor_TopLeftRight(this Control control)
        {
            control.anchor().top().left().right();
            return control;
        }

        public static Control anchor_BottomLeftRight(this Control control)
        {
            control.anchor().bottom().left().right();
            return control;
        }

        public static Control anchor_All(this Control control)
        {
            control.anchor().top().right().bottom().left();
            return control;
        }

        #endregion


    }
}
