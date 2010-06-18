﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Diagnostics;
using System.Timers;
using System.Windows.Shapes;
using System.Windows.Data;
using Odyssey.Controls.Ribbon.Interfaces;
using Odyssey.Controls.Interfaces;
using System.Collections;

#region Copyright
// Odyssey.Controls.Ribbonbar
// (c) copyright 2009 Thomas Gerber
// This source code and files, is licensed under The Microsoft Public License (Ms-PL)
#endregion
namespace Odyssey.Controls
{
    [ContentProperty("Items")]
    [TemplatePart(Name = partRecentItemsList)]
    [TemplatePart(Name = partAppButton)]
    [TemplatePart(Name = partAppButtonClone)]
    public class RibbonApplicationMenu : MenuItem,IKeyTipControl
    {
        const string partRecentItemsList = "PART_RecentItemsList";
        const string partAppButton = "PART_AppButton";
        const string partAppButtonClone = "PART_AppButtonClone";

        static RibbonApplicationMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonApplicationMenu), new FrameworkPropertyMetadata(typeof(RibbonApplicationMenu)));
        }

        public RibbonApplicationMenu()
            : base()
        {
            AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(OnMenuItemClick));
        }

        #region Obsolete
#if false
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            RibbonMenuItem rItem = item as RibbonMenuItem;
            if (rItem != null)
            {
                rItem.MouseEnter += new MouseEventHandler(rItem_MouseEnter);
                rItem.MouseLeave += new MouseEventHandler(rItem_MouseLeave);
            }
        }


        private System.Timers.Timer timer;

        private Timer EnsureTimer()
        {
            if (timer == null)
            {
                timer = new Timer(500);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            }
            return timer;
        }

        delegate void MyFunc();

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            RibbonMenuItem item = selectedItem;
            if (item != null)
            {

                MyFunc d = delegate() { item.IsSubmenuOpen = true; };
                this.Dispatcher.BeginInvoke(d);
            }
        }

        private RibbonMenuItem selectedItem;

        void rItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (timer != null) timer.Stop();
            selectedItem = null;
            RibbonMenuItem item = sender as RibbonMenuItem;
            item.IsSubmenuOpen = false;
        }

        void rItem_MouseEnter(object sender, MouseEventArgs e)
        {
            RibbonMenuItem item = sender as RibbonMenuItem;
            if (item.HasItems)
            {
                selectedItem = item;
                EnsureTimer().Start();
            }
        }
#endif
        #endregion


        void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }


        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is RibbonMenuItem || item is Separator;
        }

        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {
            return new RibbonMenuItem();
        }


        private FrameworkElement recentItemsList;
        private RibbonDropDownButton appButton;
        private FrameworkElement appButtonClone;


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            appButton = GetTemplateChild(partAppButton) as RibbonDropDownButton;
            appButtonClone = GetTemplateChild(partAppButtonClone) as FrameworkElement;
            appButton.PopupOpened += new RoutedEventHandler(appButton_PopupOpened);
            appButton.PopupClosed += new RoutedEventHandler(appButton_PopupClosed);

            recentItemsList = GetTemplateChild(partRecentItemsList) as FrameworkElement;
        }

        void appButton_PopupClosed(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }


        void appButton_PopupOpened(object sender, RoutedEventArgs e)
        {
            AdjustApplicationButtons();
            IsOpen = true;
        }



        /// <summary>
        /// Ensures that both ApplicationMenu buttons are at the same screen location:
        /// </summary>
        private void AdjustApplicationButtons()
        {
            if (appButtonClone != null && appButton != null)
            {
                Point p = appButton.PointToScreen(new Point());
                Point p2 = appButtonClone.PointFromScreen(p);

                double dx = p2.X + Canvas.GetLeft(appButtonClone);
                double dy = p2.Y  + Canvas.GetTop(appButtonClone);
                appButtonClone.Visibility = dy >= -20 ? Visibility.Visible : Visibility.Hidden;
                Canvas.SetLeft(appButtonClone, dx);
                Canvas.SetTop(appButtonClone, dy);
            }
        }

        /// <summary>
        /// Gets or sets the content of the footer for the ApplicationMenu.
        /// This is a dependency property.
        /// </summary>
        public object Footer
        {
            get { return (object)GetValue(FooterProperty); }
            set { SetValue(FooterProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Footer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register("Footer", typeof(object), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));



        public object MenuHeader
        {
            get { return (object)GetValue(MenuHeaderProperty); }
            set { SetValue(MenuHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuHeaderProperty =
            DependencyProperty.Register("MenuHeader", typeof(object), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));



        public DataTemplate MenuHeaderTemplate
        {
            get { return (DataTemplate)GetValue(MenuHeaderTemplateProperty); }
            set { SetValue(MenuHeaderTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuHeaderTemplateProperty =
            DependencyProperty.Register("MenuHeaderTemplate", typeof(DataTemplate), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));





        /// <summary>
        /// Gets or sets the DataTemplate for the footer.
        /// This is a dependency property.
        /// </summary>
        public DataTemplate FooterTemplate
        {
            get { return (DataTemplate)GetValue(FooterTemplateProperty); }
            set { SetValue(FooterTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FooterTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FooterTemplateProperty =
            DependencyProperty.Register("FooterTemplate", typeof(DataTemplate), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));




        public object RecentItemsSource
        {
            get { return (object)GetValue(RecentItemsSourceProperty); }
            set { SetValue(RecentItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecentItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecentItemsSourceProperty =
            DependencyProperty.Register("RecentItemsSource", typeof(object), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));





        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(RibbonApplicationMenu),
            new UIPropertyMetadata(false, OpenPropertyChanged));

        static void OpenPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            RibbonApplicationMenu menu = (RibbonApplicationMenu)o;
            bool newValue = (bool)e.NewValue;

            if (menu.appButton != null)
            {
                menu.appButton.IsDropDownPressed = newValue;
            }
            if (newValue) menu.OnMenuOpened(); else menu.OnMenuClosed();
        }

        private object toolTip;

        public event EventHandler Opened;

        public event EventHandler OnClosed;

        protected virtual void OnMenuClosed()
        {
            // restore the tooltip when the menu is closed:
            ToolTip = toolTip;
            if (OnClosed != null) OnClosed(this, EventArgs.Empty);
        }

        protected virtual void OnMenuOpened()
        {
            // when the menu is open, the tooltip must not be shown:
            toolTip = this.ToolTip;
            ToolTip = null;
            if (Opened != null) Opened(this, EventArgs.Empty);

        }


        /// Gets or sets the control that represents the recent items list.
        /// This is a dependency property.
        /// </summary>
        public object RecentItemsList
        {
            get { return (object)GetValue(RecentItemsListProperty); }
            set { SetValue(RecentItemsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RectentItemsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecentItemsListProperty =
            DependencyProperty.Register("RecentItemsList", typeof(object), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));



        /// <summary>
        /// Gets or sets the DataTemplate for the <see>RecentItemsList</see>.
        /// This is a dependency property.
        /// </summary>
        public DataTemplate RecentItemsListTemplate
        {
            get { return (DataTemplate)GetValue(RecentItemsListTemplateProperty); }
            set { SetValue(RecentItemsListTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RectentItemsListTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecentItemsListTemplateProperty =
            DependencyProperty.Register("RecentItemsListTemplate", typeof(DataTemplate), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));




        public ImageSource MenuButtonImage
        {
            get { return (ImageSource)GetValue(MenuButtonImageProperty); }
            set { SetValue(MenuButtonImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuButtonImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuButtonImageProperty =
            DependencyProperty.Register("MenuButtonImage", typeof(ImageSource), typeof(RibbonApplicationMenu), new UIPropertyMetadata(null));




        /// <summary>
        /// Gets the rectangle where to place the sub menus.
        /// </summary>
        /// <param name="ribbonMenuItem">The ApplicationMenu class</param>
        /// <returns>A rectangle.</returns>
        internal Rect GetSubMenuRect(Visual visual)
        {
            if (recentItemsList != null)
            {
                Rect rect = new Rect(0.0, 0.0, recentItemsList.ActualWidth, recentItemsList.ActualHeight);
                rect = recentItemsList.TransformToVisual(visual).TransformBounds(rect);
                return rect;
            }
            else return Rect.Empty;
        }

        #region IKeyboardCommand Members

        void IKeyTipControl.ExecuteKeyTip()
        {
            this.Focus();
            this.IsOpen = true;
        }

        #endregion

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                return GetLogicalChildren().GetEnumerator();
            }
        }

        private IEnumerable<object> GetLogicalChildren()
        {
            if (Header != null) yield return Header;
            IEnumerator e = base.LogicalChildren;
            while (e.MoveNext())
            {
                yield return e.Current;
            }
            if (Footer != null) yield return Footer;
        }
    }
}
