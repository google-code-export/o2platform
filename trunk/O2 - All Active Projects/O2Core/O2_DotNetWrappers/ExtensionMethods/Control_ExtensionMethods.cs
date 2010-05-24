﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using System.Drawing;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Control_ExtensionMethods
    {

        #region Control - add

        public static T add<T>(this Control hostControl, T childControl)
            where T : Control
        {
            return (T)hostControl.invokeOnThread(
                    () =>
                    {
                        hostControl.Controls.Add(childControl);
                        return childControl;
                    });
        }

        public static T add<T>(this Control hostControl, params int[] position) where T : Control
        {
            return hostControl.add_Control<T>(position);
        }

        public static T add_Control<T>(this Control hostControl, T childControl)
            where T : Control
        {
            return (T)hostControl.add(childControl);
        }

        public static Control add_Control(this Control control, Type childControlType)            
        {
            return (Control)control.invokeOnThread(
                                 () =>
                                 {
                                     try
                                     {
                                         var childControl = (Control)PublicDI.reflection.createObjectUsingDefaultConstructor(childControlType);
                                         if (control != null)
                                         {
                                             childControl.Dock = DockStyle.Fill;
                                             control.Controls.Add(childControl);
                                             return childControl;
                                         }
                                     }
                                     catch (Exception ex)
                                     {
                                         ex.log("in Control.add_Control");
                                     }
                                     return null;
                                 });
        }          

        public static T add_Control<T>(this Control hostControl, params int[] position) where T : Control
        {
            var values = new[] { -1, -1, -1, -1 };
            for (int i = 0; i < position.Length; i++)
                values[i] = position[i];
            return hostControl.add_Control<T>(values[0], values[1], values[2], values[3]);
        }

        public static T add_Control<T>(this Control hostControl, int top, int left, int width, int height) where T : Control
        {
            return (T)hostControl.invokeOnThread(
                () =>
                {
                    var newControl = (Control)typeof(T).ctor();
                    if (newControl != null)
                    {
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
                    }
                    return null;
                });
        }

        public static List<T> add_Controls<T>(this Control control, List<T> controlsToAdd)
            where T : Control
        {
            foreach (var controlToAdd in controlsToAdd)
                control.add_Control(controlToAdd);
            return controlsToAdd;
        }

        public static T append_Control<T>(this Control control)
            where T : Control
        {
            return control.Parent.add_Control<T>(control.Top, control.Left + control.Width + 5);
        }

        #endregion

        #region Control - get

        public static T get<T>(this Control controlToSearch) where T : Control
        {
            foreach (var control in controlToSearch.controls())
                if (control is T)
                    return (T)control;
                else
                {
                    var childMatch = control.get<T>();
                    if (childMatch != null)
                        return childMatch;
                }
            return null;
        }

        public static T get<T>(this List<Control> controls) where T : Control
        {
            foreach (Control control in controls)
                if (control.type() == typeof(T))
                    return (T)control;
            return null;
        }

        public static T castOrCreate<T>(this Control control, object objectToCheck, Func<T> createFunction)
        {
            if (objectToCheck != null && objectToCheck is T)
                return (T)objectToCheck;
            control.clear();
            return createFunction();
        }

        public static T parent<T>(this List<Control> controls) where T : Control
        {
            foreach (var control in controls)
            {
                var match = control.parent<T>();
                if (match != null)
                    return (T)match;
            }
            return null;
        }

        public static T parent<T>(this Control control) where T : Control
        {
            if (control != null && control.Parent != null)
            {
                var parent = control.Parent;
                if (parent is T)
                    return (T)parent;
                var match = parent.parent<T>();
                if (match != null)
                    return (T)match;
            }
            return null;
        }

        #endregion

        #region Control - misc

        public static Form parentForm(this Control control)
        {
            return control.parent<Form>();
        }

        public static T set_Text<T>(this T control, string text)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {

                    control.Text = text;
                    return (T)control;
                });
        }

        public static T createInThread<T>(this Control control)
            where T : Control
        {
            return control.newInThread<T>();
        }

        public static T newInThread<T>(this Control control)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    try
                    {
                        return (T)typeof(T).ctor();
                    }
                    catch (Exception ex)
                    {
                        ex.log("in Control.invokeOnThread");
                        return null;
                    }
                });
        }

        public static T fill<T>(this T control)
            where T : Control
        {
            return control.fill(true);
        }


        public static T fill<T>(this T control, bool status)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    control.Dock = (status) ? DockStyle.Fill : DockStyle.None;
                    return (T)control;
                });

        }

        public static T enabled<T>(this T control)
            where T : Control
        {
            return control.enabled(true);
        }

        public static T enabled<T>(this T control, bool state)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    control.Enabled = state;
                    return (T)control;
                });
        }

        public static T visible<T>(this T control)
            where T : Control
        {
            return control.visible(true);
        }

        public static T visible<T>(this T control, bool state)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    control.Visible = state;
                    return (T)control;
                });
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

        public static void close(this Control control)
        {
            control.invokeOnThread(
                () =>
                {
                    if (control is UserControl)
                    {
                        var userControl = (UserControl)control;
                        if (userControl.ParentForm != null)
                            userControl.ParentForm.Close();
                    }
                });
        }

        public static T backColor<T>(this T control, Color color)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    control.BackColor = color;
                    return control;
                });
        }

        public static T backColor<T>(this T control, string colorName)
            where T : Control
        {
            return control.backColor(Color.FromName(colorName));
        }

        public static List<Control> controls(this Control control)
        {
            return control.controls(false);
        }

        public static List<Control> controls(this Control control, bool recursiveSearch)
        {
            if (recursiveSearch)
            {    
                var allControls = control.controls();
                foreach(var childControl in control.controls())
                    allControls.AddRange(childControl.controls(true));
                return allControls;
            }
            else
                return control.Controls.list();
        }

        public static T controls<T>(this Control rootControl, int depthToReturn)
            where T : Control
        {
            var controls = rootControl.controls(depthToReturn);
            return controls.control<T>();
        }

        public static List<Control> controls(this Control control, int depthToReturn)
        {
            var allControls = new List<Control>();
            allControls.add(control);
            for (int i = 0; i < depthToReturn; i++)
                allControls = allControls.controls(false);
            return allControls;
        }

        public static List<Control> controls(this List<Control> controls)
        {
            return controls.controls(false);
        }

        public static List<Control> controls(this List<Control> controls, bool includeRoot)
        {
            var allControls = new List<Control>();
            foreach (var control in controls)
            {
                if (includeRoot)
                    allControls.add(control);
                allControls.add(control.controls());
            }
            return allControls;
        }

        public static T control<T>(this List<Control> controls)
            where T : Control
        {
            foreach (var control in controls)
                if (control is T)
                    return (T)control;
            return null;
        }

        public static List<Control> list(this Control.ControlCollection controlCollection)
        {
            var controls = new List<Control>();
            foreach (Control control in controlCollection)
                controls.Add(control);
            return controls;
        }

        public static T focus<T>(this T control) where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    control.Focus();
                    return control;
                });
        }

        public static T top<T>(this T control, int top)
            where T : Control
        {
            return (T)control.invokeOnThread(
                    () =>
                    {
                        if (top > -1)
                            control.Top = top;
                        return control;
                    });
        }

        public static T left<T>(this T control, int left)
            where T : Control
        {
            return (T)control.invokeOnThread(
                    () =>
                    {
                        if (left > -1)
                            control.Left = left;
                        return control;
                    });
        }

        public static T width<T>(this T control, int value)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    if (value > -1)
                        control.Width = value;
                    return (T)control;
                });
        }

        public static T height<T>(this T control, int value)
            where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    if (value > -1)
                        control.Height = value;
                    return (T)control;
                });
        }

        public static int width(this Control control)
        {
            return (int)control.invokeOnThread(
                () =>
                {
                    return control.Width;
                });
        }

        public static int height(this Control control)
        {
            return (int)control.invokeOnThread(
                () =>
                {
                    return control.Height;
                });
        }

        public static Bitmap fromClipboardGetImage(this Control control)
        {
            return (Bitmap)control.invokeOnThread(
                () =>
                {
                    if (Clipboard.ContainsImage())
                    {
                        return Clipboard.GetImage();
                    }
                    "in fromClipboardGetImage, the Clipboard doesn't contain an image".debug();
                    return null;
                });
        }

        public static T front<T>(this T control)
            where T : Control
        {
            return control.bringToFront();
        }
        public static T bringToFront<T>(this T control)
            where T : Control
        {
            control.invokeOnThread(() => control.BringToFront());
            return control;
        }

        public static T back<T>(this T control)
            where T : Control
        {
            return control.sendToBack();
        }

        public static T sendToBack<T>(this T control)
            where T : Control
        {
            control.invokeOnThread(() => control.SendToBack());
            return control;
        }

        #endregion

        #region Control - events 

        public static T onDrop<T>(this T control, Action<string> onDropFileOrFolder)
            where T : Control
        {
            return (T)control.invokeOnThread(() =>
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
                    return control;
                });
            //
        }

        public static T onClosed<T>(this T control, MethodInvoker onClosed)
            where T : Control
        {
            var parentForm = control.parentForm();
            if (parentForm == null)
            {
                "in Control.onClosed, provided form value was null".error();
                return null;
            }
            parentForm.Closed += (sender, e) => onClosed();
            return control;
        }


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
                    if (control.Controls.IsReadOnly)
                        "cannot clear control list for type {0} since it is marked as read only".format(control.typeName()).error();
                        control.Controls.Clear();                    
                    return control;
                });
        }

        public static T removeOtherControls<T>(this Control hostControl, T controlToKeep) where T : Control
        {
            return (T)hostControl.invokeOnThread(
                () =>
                {
                    foreach (var control in hostControl.controls())
                        if (control != controlToKeep)
                            hostControl.Controls.Remove(control);
                    return controlToKeep;
                });
        }
             
        public static T remove<T>(this T hostControl, Control controlToRemove) where T : Control
        {
            return (T)hostControl.invokeOnThread(
                () =>
                {
                    foreach (var control in hostControl.controls(true))
                        if (control != controlToRemove)
                            hostControl.Controls.Remove(controlToRemove);
                    return hostControl;
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
                if (controlToWrap == null || controlToInject == null)
                    return new List<Control>();
                return (List<Control>)controlToWrap.invokeOnThread(
                    () =>
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
                                try
                                {
                                    //if (splitterDistance > -1 && splitterDistance > splitContainer.Panel1MinSize && 
                                    //                             splitterDistance < ( splitContainer.Width -  splitContainer.Panel2MinSize))                                    
                                    if (splitterDistance > 0)
                                    {                                        
                                        splitContainer.SplitterDistance = splitterDistance;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    "Could not set Splitter Distance to value {0} : {1}".format(splitterDistance,ex.Message).error();
                                }
                                splitContainer.Orientation = (location == AnchorStyles.Top) ? Orientation.Horizontal : Orientation.Vertical;
                                break;

                            case AnchorStyles.Bottom:
                            case AnchorStyles.Right:
                                splitContainer.Panel1.add(controlToWrap);
                                splitContainer.Panel2.add(controlToInject);
                                splitContainer.Orientation = (location == AnchorStyles.Bottom) ? Orientation.Horizontal : Orientation.Vertical;
                                splitContainer.FixedPanel = FixedPanel.Panel2;

                                if (splitterDistance > -1)
                                {

                                    var newSplitterDistance = (location == AnchorStyles.Bottom)
                                                                        ? splitContainer.Height - splitterDistance
                                                                        : splitContainer.Width - splitterDistance;
                                    if (newSplitterDistance > 0)
                                    {                                     
                                        splitContainer.SplitterDistance = newSplitterDistance;
                                    }
                                    else
                                        "Could not set Splitter Distance since it was a negative value: {0}".format(newSplitterDistance).error();
                                }                                
                                break;
                            case AnchorStyles.None:
                                PublicDI.log.error("in injectControl the location provided was AnchorStyles.None");
                                break;
                        }
                        controls.add(splitContainer)
                                .add(controlToWrap)
                                .add(controlToInject);

                        return controls;
                    });
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex, "in injectControl");
                return null;
            }
        }

        public static List<Control> insert_Right(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.insert_Right(controlToInject, splitterDistance, false);
        }

        public static List<Control> insert_Left(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.insert_Left(controlToInject, splitterDistance, false);
        }

        public static List<Control> insert_Above(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.insert_Above(controlToInject, splitterDistance, false);
        }

        public static List<Control> insert_Below(this Control controlToWrap, Control controlToInject, int splitterDistance)
        {
            return controlToWrap.insert_Below(controlToInject, splitterDistance, false);
        }

        public static List<Control> insert_Above(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Top);
        }

        public static List<Control> insert_Above(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Top, splitterDistance, border3D);
        }        

        public static T insert_Above<T>(this Control control) where T : Control
        {
            return control.insert_Above<T>(-1);
        }

        public static T insert_Above<T>(this Control control, int distance) where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    //var newControl = control.add_Control<T>();
                    var newControl = control.newInThread<T>();
                    newControl.fill();
                    control.insert_Above(newControl, distance);
                    return newControl;
                });
        }

        public static List<Control> insert_Below(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Bottom);
        }

        public static List<Control> insert_Below(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Bottom, splitterDistance, border3D);
        }

        public static T insert_Below<T>(this Control control) where T : Control
        {
            return control.insert_Below<T>(-1);
        }

        public static T insert_Below<T>(this Control control, int distance) where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    var newControl = control.add_Control<T>();
                    if (newControl == null)
                        return null;
                    newControl.fill();
                    control.insert_Below(newControl, distance);
                    return newControl;
                });
        }

        public static List<Control> insert_Left(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Left);
        }

        public static List<Control> insert_Left(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Left, splitterDistance, border3D);
        }

        public static T insert_Left<T>(this Control control) where T : Control
        {
            return control.insert_Left<T>(-1);
        }

        public static T insert_Left<T>(this Control control, int distance) where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    var newControl = control.add_Control<T>();
                    newControl.fill();
                    control.insert_Left(newControl, distance);
                    return newControl;
                });
        }

        public static List<Control> insert_Right(this Control controlToWrap, Control controlToInject)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Right);
        }

        public static List<Control> insert_Right(this Control controlToWrap, Control controlToInject, int splitterDistance, bool border3D)
        {
            return controlToWrap.injectControl(controlToInject, AnchorStyles.Right, splitterDistance, border3D);
        }

        public static T insert_Right<T>(this Control control) where T : Control
        {
            return control.insert_Right<T>(-1);
        }

        public static T insert_Right<T>(this Control control, int distance) where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    var newControl = control.add_Control<T>();
                    newControl.fill();
                    control.insert_Right(newControl, distance);
                    return newControl;
                });
        }

        #endregion

        #region Control - KeyUp

        public static T onKeyPress<T>(this T control, Func<Keys, bool> callback)
            where T : Control
        {
            control.KeyDown += (sender, e) => e.Handled = callback(e.KeyData);
            return control;
        }
    	

        public static T onKeyPress<T>(this T control, Action<Keys> callback)
            where T : Control
        {
            control.KeyUp += (sender, e) => callback(e.KeyData);
            return control;
        }

        public static T onKeyPress<T>(this T control, Action<Keys, String> callback)
            where T : Control
        {
            control.KeyUp += (sender, e) => callback(e.KeyData, control.Text);
            return control;
        }

        public static T onKeyPress<T>(this T control, Keys onlyFireOnKey, MethodInvoker callback)
            where T : Control
        {
            control.KeyUp += (sender, e) =>
            {
                if (e.KeyData == onlyFireOnKey)
                    callback();
            };
            return control;
        }

        public static T onKeyPress<T>(this T control, Keys onlyFireOnKey, Action<String> callback)
            where T : Control
        {
            control.KeyUp += (sender, e) =>
            {
                if (e.KeyData == onlyFireOnKey)
                    callback(control.Text);
            };
            return control;
        }

        public static T onEnter<T>(this T control, Action<String> callback)
            where T : Control
        {
            return control.onKeyPress(Keys.Enter, callback);
        }

        #endregion

        #region Control - Sync

        public static T syncTextBoxWithControl<T>(this T control, Action<string> onTextChanged)
            where T : Control
        {
            return control.syncTextBoxWithControl(true, onTextChanged);
        }

        public static T syncTextBoxWithControl<T>(this T control, bool onlyFireOnEnter, Action<string> onTextChanged)
            where T : Control
        {
            var textBox = control.insert_Above<TextBox>(20);
            textBox.KeyUp += (sender, e) =>
            {
                if (onlyFireOnEnter.isFalse() || e.KeyCode == Keys.Enter)
                    onTextChanged(textBox.get_Text());
            };
            return control;
        }

        public static T syncComboBoxWithControl<T>(this T control, Action<string> onTextChanged)
            where T : Control
        {
            return control.syncComboBoxWithControl(true, true, onTextChanged);
        }

        public static T syncComboBoxWithControl<T>(this T control, bool onlyFireOnEnter, bool addToHistory, Action<string> onTextChanged)
            where T : Control
        {
            var comboBox = control.insert_Above<ComboBox>(20);
            comboBox.KeyUp += (sender, e) =>
            {
                //	comboBox.invokeOnThread(
                //		()=>{
                var text = comboBox.Text;
                if (onlyFireOnEnter.isFalse() || e.KeyCode == Keys.Enter)
                {
                    onTextChanged(text);
                    if (addToHistory)
                    {
                        comboBox.Items.Insert(0, text);
                        comboBox.Text = "";
                    }
                }
                //			});					
            };
            return control;
        }

        #endregion
       
        #region Form

        public static Form onClosed<T>(this Form form, MethodInvoker onClosed)
        {
            if (form == null)
            {
                "in Form.onClosed, provided form value was null".error();
                return null;
            }
            form.Closed += (sender, e) => onClosed();
            return form;
        }

        #endregion

        #region SendKeys

        public static T sendKeys<T>(this T control, string textToSend) where T : Control
        {
            return (T)control.invokeOnThread(
                () =>
                {
                    control.focus();
                    SendKeys.Send(textToSend);
                    return control;
                });
        }

        public static T sendEnter<T>(this T control) where T : Control
        {
            return control.sendKeys("".line());
        }

        #endregion 
    }
}
