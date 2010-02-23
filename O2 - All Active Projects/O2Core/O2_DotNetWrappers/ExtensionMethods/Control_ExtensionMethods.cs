using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Control_ExtensionMethods
    {

        #region Control - add

        public static Control add(this Control hostControl, Control childControl)
        {
            return (Control)hostControl.invokeOnThread(
                    () =>
                    {
                        hostControl.Controls.Add(childControl);
                        return childControl;
                    });
        }

        public static T add<T>(this Control hostControl, params int[] position)
        {
            return hostControl.add_Control<T>(position);
        }

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

        #region Control - inject control
        public static List<Control> injectControl_Top(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Top);
        }

        public static List<Control> injectControl_Top(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Top, splitterDistance, border3D);
        }

        public static List<Control> injectControl_Bottom(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Bottom);
        }

        public static List<Control> injectControl_Bottom(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Bottom, splitterDistance, border3D);
        }

        public static List<Control> injectControl_Left(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Left);
        }

        public static List<Control> injectControl_Left(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Left, splitterDistance, border3D);
        }

        public static List<Control> injectControl_Right(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Right);
        }

        public static List<Control> injectControl_Right(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Right, splitterDistance, border3D);
        }

        public static List<Control> injectControl(this Control controlToWrap, Control controlToInject, AnchorStyles location)
        {
            return controlToWrap.injectControl(controlToInject, location, -1, false, controlToWrap.Text, controlToInject.Text);
        }

        public static List<Control> injectControl(this Control controlToWrap, Control controlToInject, AnchorStyles location, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, location, splitterDistance, border3D, controlToWrap.Text, controlToInject.Text);
        }

        public static List<Control> injectControl(this Control controlToWrap, Control controlToInject, AnchorStyles location, int splitterDistance, bool border3D, string title_1, string title_2)
        {
            try
            {
                var parentControl = controlToWrap.Parent;
                parentControl.clear();
                var controls = new List<Control>();
                SplitContainer splitContainer = parentControl.add_SplitContainer();
                splitContainer.fill();
                if (border3D)
                    splitContainer.border3D();
                switch (location)
                {
                    case AnchorStyles.Top:
                    case AnchorStyles.Left:
                        splitContainer.Panel1.add(controlToInject);
                        splitContainer.Panel2.add(controlToWrap);
                        splitContainer.FixedPanel = FixedPanel.Panel1;
                        if (splitterDistance > -1)
                            splitContainer.SplitterDistance = splitterDistance;
                        splitContainer.Orientation = (location == AnchorStyles.Top) ? Orientation.Horizontal : Orientation.Vertical;
                        break;

                    case AnchorStyles.Bottom:
                    case AnchorStyles.Right:
                        splitContainer.Panel1.add(controlToWrap);
                        splitContainer.Panel2.add(controlToInject);
                        splitContainer.FixedPanel = FixedPanel.Panel2;

                        if (splitterDistance > -1)
                            splitContainer.SplitterDistance = (location == AnchorStyles.Bottom)
                                                                ? splitContainer.Height - splitterDistance
                                                                : splitContainer.Width - splitterDistance;

                        splitContainer.Orientation = (location == AnchorStyles.Bottom) ? Orientation.Horizontal : Orientation.Vertical;
                        break;
                    case AnchorStyles.None:
                        PublicDI.log.error("in injectControl the location provided was AnchorStyles.None");
                        break;
                }
                controls.add(splitContainer)
                        .add(controlToWrap)
                        .add(controlToInject);

                return controls;
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex, "in injectControl");
                return null;
            }
        }

        public static List<Control> insert_Right(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.injectControl_Right(controlToInject, splitterDistance, false);
        }

        public static List<Control> insert_Left(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.injectControl_Left(controlToInject, splitterDistance, false);
        }

        public static List<Control> insert_Above(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.injectControl_Top(controlToInject, splitterDistance, false);
        }

        public static List<Control> insert_Below(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.injectControl_Bottom(controlToInject, splitterDistance, false);
        }
        #endregion
    }
}
