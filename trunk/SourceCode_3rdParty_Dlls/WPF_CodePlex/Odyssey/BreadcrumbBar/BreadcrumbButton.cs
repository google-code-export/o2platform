﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Diagnostics;


#region Licence
// Copyright (c) 2008 Thomas Gerber
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
#endregion
namespace Odyssey.Controls
{
    /// <summary>
    /// A breadcrumb button is part of a BreadcrumbItem and contains  a header and a dropdown button.
    /// </summary>
    [TemplatePart(Name = partMenu)]
    [TemplatePart(Name = partToggle)]
    [TemplatePart(Name = partButton)]
    [TemplatePart(Name = partDropDown)]
    public class BreadcrumbButton : HeaderedItemsControl
    {
        const string partMenu = "PART_Menu";
        const string partToggle = "PART_Toggle";
        const string partButton = "PART_button";
        const string partDropDown = "PART_DropDown";
        static BreadcrumbButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BreadcrumbButton), new FrameworkPropertyMetadata(typeof(BreadcrumbButton)));
        }

        #region Dependency Properties
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(object), typeof(BreadcrumbButton), new UIPropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(BreadcrumbButton), new UIPropertyMetadata(null, SelectedItemChangedEvent));


        #endregion

        #region RoutedEvents
        public static readonly RoutedEvent SelectedItemChanged = EventManager.RegisterRoutedEvent("SelectedItemChanged",
           RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BreadcrumbButton));

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click",
           RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BreadcrumbButton));
        #endregion

        private ContextMenu contextMenu;
        private Control dropDownBtn;

        public BreadcrumbButton()
            : base()
        {
            CommandBindings.Add(new CommandBinding(SelectCommand, SelectCommandExecuted));
            CommandBindings.Add(new CommandBinding(OpenOverflowCommand, OpenOverflowCommandExecuted, OpenOverflowCommandCanExecute));

            InputBindings.Add(new KeyBinding(BreadcrumbButton.SelectCommand, new KeyGesture(Key.Enter)));
            InputBindings.Add(new KeyBinding(BreadcrumbButton.SelectCommand, new KeyGesture(Key.Space)));
            InputBindings.Add(new KeyBinding(BreadcrumbButton.OpenOverflowCommand, new KeyGesture(Key.Down)));
            InputBindings.Add(new KeyBinding(BreadcrumbButton.OpenOverflowCommand, new KeyGesture(Key.Up)));
        }


        private bool isPressed = false;

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsPressed = false;
        }



        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            IsPressed = isPressed = true;
            base.OnMouseLeftButtonDown(e);
        }




        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (isPressed)
            {
                RoutedEventArgs args = new RoutedEventArgs(BreadcrumbButton.ClickEvent);
                RaiseEvent(args);
                selectCommand.Execute(null, this);
            }
            IsPressed = isPressed = false;
            base.OnMouseUp(e);
        }

        private void SelectCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedItem = null;
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent);
            RaiseEvent(args);
        }

        private void OpenOverflowCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            IsDropDownPressed = true;
        }

        private void OpenOverflowCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Items.Count > 0;
        }

        public static RoutedUICommand OpenOverflowCommand
        {
            get { return openOverflowCommand; }
        }

        public static RoutedUICommand SelectCommand
        {
            get { return selectCommand; }
        }

        private static RoutedUICommand openOverflowCommand = new RoutedUICommand("Open Overflow", "OpenOverflowCommand", typeof(BreadcrumbButton));
        private static RoutedUICommand selectCommand = new RoutedUICommand("Select", "SelectCommand", typeof(BreadcrumbButton));


        public override void OnApplyTemplate()
        {
            dropDownBtn = this.GetTemplateChild(partDropDown) as Control;
            contextMenu = this.GetTemplateChild(partMenu) as ContextMenu;
            if (contextMenu != null)
            {
                contextMenu.Opened += new RoutedEventHandler(contextMenu_Opened);
            }
            if (dropDownBtn != null)
            {
                dropDownBtn.MouseDown += new MouseButtonEventHandler(dropDownBtn_MouseDown);
            }
            base.OnApplyTemplate();
        }

        void dropDownBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            IsDropDownPressed ^= true;
        }


        /// <summary>
        /// Gets or sets the Image of the BreadcrumbButton.
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }


        //TODO: Menu needs too long to render if there are too many items (> 20000).
        void contextMenu_Opened(object sender, RoutedEventArgs e)
        {
            contextMenu.Items.Clear();
            contextMenu.ItemTemplate = ItemTemplate;
            contextMenu.ItemTemplateSelector = ItemTemplateSelector;

        //    List<MenuItem> menuItems = new List<MenuItem>();
            foreach (object item in Items)
            {
                if (!(item is MenuItem) && !(item is Separator))
                {

                    RibbonMenuItem menuItem = new RibbonMenuItem();
                    menuItem.DataContext = item;
                    BreadcrumbItem bi = item as BreadcrumbItem;
                    if (bi == null)
                    {
                        BreadcrumbItem parent = TemplatedParent as BreadcrumbItem;
                        if (parent != null) bi = parent.ContainerFromItem(item);
                    }
                    if (bi != null) menuItem.Header = bi.TraceValue;

                    Image image = new Image();
                    image.Source = bi != null ? bi.Image : null;
                    image.SnapsToDevicePixels = true;
                    image.Stretch = Stretch.Fill;
                    image.VerticalAlignment = VerticalAlignment.Center;
                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    image.Width = 16;
                    image.Height = 16;

                    menuItem.Icon = image;

                    menuItem.Click += new RoutedEventHandler(item_Click);
                    if (item != null && item.Equals(SelectedItem)) menuItem.FontWeight = FontWeights.Bold;
                    menuItem.ItemTemplate = ItemTemplate;
                    menuItem.ItemTemplateSelector = ItemTemplateSelector;
                    contextMenu.Items.Add(menuItem);
                }
                else
                {
                    contextMenu.Items.Add(item);
                }
            }
            contextMenu.Placement = PlacementMode.Relative;
            contextMenu.PlacementTarget = dropDownBtn;
            contextMenu.VerticalOffset = dropDownBtn.ActualHeight;
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;
            object dataItem = item.DataContext;
            RemoveSelectedItem(dataItem);
            SelectedItem = dataItem;
        }

        /// <summary>
        /// When a BreadcrumbItem is selected from a dropdown menu, the SelectedItem of the new selected item must be set to null.
        /// Since no event is raised when a DependencyProperty is assigned to it's current value, this cannot be recognized at this place,
        /// therefore the SelectedItem DependencyProperty must previously set to null before setting it to it's new value to raise event
        /// when SelectedItem is changed:
        /// </summary>
        /// <param name="dataItem"></param>
        private void RemoveSelectedItem(object dataItem)
        {
            if (dataItem != null && dataItem.Equals(SelectedItem)) SelectedItem = null;
        }

        /// <summary>
        /// Gets or sets the selectedItem.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void SelectedItemChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BreadcrumbButton button = d as BreadcrumbButton;
            if (button.IsInitialized)
            {
                RoutedEventArgs args = new RoutedEventArgs(SelectedItemChanged);
                button.RaiseEvent(args);
            }
        }

        /// <summary>
        /// Focus this BreadcrumbButton if the focus is currently within the BreadcrumbBar where this BreadcrumbButton is embedded:
        /// </summary>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            isPressed = e.LeftButton == MouseButtonState.Pressed;
            FrameworkElement parent = TemplatedParent as FrameworkElement;
            while (parent != null && !(parent is BreadcrumbBar)) parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            BreadcrumbBar bar = parent as BreadcrumbBar;
            if (bar != null && bar.IsKeyboardFocusWithin) Focus();
            IsPressed = isPressed;
            base.OnMouseEnter(e);
        }


        private static void OverflowPressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BreadcrumbButton button = d as BreadcrumbButton;
            button.OnOverflowPressedChanged();
        }

        protected virtual void OnOverflowPressedChanged()
        {
        }


        /// <summary>
        /// Specifies how to display the BreadcrumbButton.
        /// </summary>
        public enum ButtonMode
        {
            /// <summary>
            /// Display as Breadcrumb.
            /// </summary>
            Breadcrumb,

            /// <summary>
            /// Display as overflow.
            /// </summary>
            Overflow,

            /// <summary>
            /// Display as drop down.
            /// </summary>
            DropDown
        }


        /// <summary>
        /// Gets or sets the ButtonMode for the BreadcrumbButton.
        /// </summary>
        public ButtonMode Mode
        {
            get { return (ButtonMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ButtonMode), typeof(BreadcrumbButton), new UIPropertyMetadata(ButtonMode.Breadcrumb));


        /// <summary>
        /// Occurs when the Button is clicked.
        /// </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(BreadcrumbButton.ClickEvent, value); }
            remove { RemoveHandler(BreadcrumbButton.ClickEvent, value); }
        }

        /// <summary>
        /// Occurs when the SelectedItem is changed.
        /// </summary>
        public event RoutedEventHandler Select
        {
            add { AddHandler(BreadcrumbButton.SelectedItemChanged, value); }
            remove { RemoveHandler(BreadcrumbButton.SelectedItemChanged, value); }
        }


        /// <summary>
        /// Gets or sets whether the button is pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the button is pressed.
        /// </summary>
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(BreadcrumbButton), new UIPropertyMetadata(false));


        /// <summary>
        /// Gets or sets whether the drop down button is pressed.
        /// </summary>
        public bool IsDropDownPressed
        {
            get { return (bool)GetValue(IsDropDownPressedProperty); }
            set { SetValue(IsDropDownPressedProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the drop down button is pressed.
        /// </summary>
        public static readonly DependencyProperty IsDropDownPressedProperty =
            DependencyProperty.Register("IsDropDownPressed", typeof(bool), typeof(BreadcrumbButton), new UIPropertyMetadata(false, OverflowPressedChanged));


        /// <summary>
        /// Gets or sets the DataTemplate for the drop down items.
        /// </summary>
        public DataTemplate DropDownContentTemplate
        {
            get { return (DataTemplate)GetValue(DropDownContentTemplateProperty); }
            set { SetValue(DropDownContentTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DropDownContentTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropDownContentTemplateProperty =
            DependencyProperty.Register("DropDownContentTemplate", typeof(DataTemplate), typeof(BreadcrumbButton), new UIPropertyMetadata(null));



        /// <summary>
        /// Gets or sets whether the drop down button is visible.
        /// </summary>
        public bool IsDropDownVisible
        {
            get { return (bool)GetValue(IsDropDownVisibleProperty); }
            set { SetValue(IsDropDownVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the drop down button is visible.
        /// </summary>
        public static readonly DependencyProperty IsDropDownVisibleProperty =
            DependencyProperty.Register("IsDropDownVisible", typeof(bool), typeof(BreadcrumbButton), new UIPropertyMetadata(true));



        /// <summary>
        /// Gets or sets whether the button is visible.
        /// </summary>
        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the button is visible.
        /// </summary>
        public static readonly DependencyProperty IsButtonVisibleProperty =
            DependencyProperty.Register("IsButtonVisible", typeof(bool), typeof(BreadcrumbButton), new UIPropertyMetadata(true));


        /// <summary>
        /// Gets or sets whether the Image is visible
        /// </summary>
        public bool IsImageVisible
        {
            get { return (bool)GetValue(IsImageVisibleProperty); }
            set { SetValue(IsImageVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the Image is visible
        /// </summary>
        public static readonly DependencyProperty IsImageVisibleProperty =
            DependencyProperty.Register("IsImageVisible", typeof(bool), typeof(BreadcrumbButton), new UIPropertyMetadata(true));




        /// <summary>
        /// Gets or sets whether to use visual background style on MouseOver and/or MouseDown.
        /// </summary>
        public bool EnableVisualButtonStyle
        {
            get { return (bool)GetValue(EnableVisualButtonStyleProperty); }
            set { SetValue(EnableVisualButtonStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether to use visual background style on MouseOver and/or MouseDown.
        /// </summary>
        public static readonly DependencyProperty EnableVisualButtonStyleProperty =
            DependencyProperty.Register("EnableVisualButtonStyle", typeof(bool), typeof(BreadcrumbButton), new UIPropertyMetadata(true));


    }
}
