// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Linq;
using System.Collections.Generic;
using System.Windows.Threading;
using O2.Kernel;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.ExtensionMethods;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace O2.API.Visualization.ExtensionMethods
{
    public static class WPF_Controls_ExtensionMethods
    {    
	
		#region generic methods								
    	
        public static T createInThread_Wpf<T>(this UIElement uiElement)
        {
            return (T)uiElement.wpfInvoke(() => typeof(T).ctor());
        }

        public static T add_Control_Wpf<T>(this UIElement uiElement, T uiElementToAdd)
            where T : UIElement
        {
            if ((uiElement is IAddChild).isFalse())
            {
                "in add_Control_Wpf, the host control must implement the IAddChild interface, and '{0}' did not".error(typeof(T).name());
                return null;
            }
            return (T)uiElement.wpfInvoke(
                () =>
                {
                    try
                    {
                        if (uiElementToAdd != null)
                            (uiElement as IAddChild).AddChild(uiElementToAdd);
                        return uiElementToAdd;
                    }
                    catch (Exception ex)
                    {
                        ex.log("in add_Wpf_Control");
                        return null;
                    }
                });

        }

        public static T add_Control_Wpf<T>(this UIElement uiElement)
            where T : UIElement
        {
            if (uiElement.isNull())
            {
                "in add_Wpf_Control, the host control was null".error();
                return null;
            }
            if ((uiElement is IAddChild).isFalse())
            {
                "in add_Wpf_Control, the host control must implement the IAddChild interface, and '{0}' did not".error(typeof(T).name());
                return null;
            }
            return (T)uiElement.wpfInvoke(
                () =>
                {
                    try
                    {
                        var newControl = typeof(T).ctor();
                        if (newControl.isNull())
                        {
                            "in add_Wpf_Control, could not create control of type: {0}".error(typeof(T).name());
                            return null;
                        }
                        (uiElement as IAddChild).AddChild(newControl);
                        return newControl;
                    }
                    catch (Exception ex)
                    {
                        ex.log("in add_Wpf_Control");
                        return null;
                    }
                });
        }

    	/*public static T add_Control<T>(this ContentControl uiElement) where T : UIElement
    	{
    		return (T)uiElement.wpfInvoke(
    			()=>{
    					try
			            {  
	    					var wpfControl = typeof(T).ctor();					    				
	    					if (wpfControl is UIElement)
	    					{
	    						uiElement.invoke("AddChild",((UIElement)wpfControl));
	    						return (T)wpfControl;			    								    					
	    					}
						}			
			            catch (System.Exception ex)
			            {
			                ex.log("in add_Control");
			            }
			            return null;			    				
    				});     		
    	}
        */
		public static T set<T>(this ContentControl control) where T : UIElement
		{
			return (T)control.wpfInvoke(
    			()=>{ 
    					control.Content = (T)typeof(T).ctor();
    					return control.Content;
    				});
		}  
		
		public static T set<T>(this T control, object value) where T : ContentControl
		{
			return (T)control.wpfInvoke(
    			()=>{ 
    					control.Content = value;
    					return control;
    				});
		}  
		
		public static T newInThread<T>(this ContentControl control) where T : UIElement
		{
			return (T)control.wpfInvoke(
    			()=>{ 
    					return (T)typeof(T).ctor();    					
    				});
		}
		
		public static ContentControl set(this ContentControl control,UIElement uiElement)
		{
			return (ContentControl)control.wpfInvoke(
    			()=>{
    					control.Content = uiElement;
    					return control;
    				});
		}
		
		public static T getFirst<T>(this List<object> list) where  T : UIElement
		{
			foreach(var item in list)
				if (item is T)
					return (T)item;
			return null;
		}


        public static T set_Tag<T>(this T control, object tagObject)
            where T : Control
        {
            control.wpfInvoke(() => control.Tag = tagObject);
            return control;
        }

        public static object get_Tag<T>(this T frameworkElement)
            where T : FrameworkElement
        {
            return (object)frameworkElement.wpfInvoke(() => frameworkElement.Tag);
        }

        public static T get_Tag<T>(this Control control)
        {
            return (T)control.wpfInvoke(
                () =>
                {
                    var tag = control.Tag;
                    if (tag is T)
                        return (T)tag;
                    return default(T);
                });
        }

        /*public static string get_Text<T>(this T control)
            where T : ContentControl
        {
            return (string)control.wpfInvoke(() => control.Content);
        }
        */

        public static string get_Text_Wpf<T>(this T control)
            where T : ContentControl
        {
            return (string)control.wpfInvoke(
                () =>
                {
                    return control.Content;
                });
        }
        /* unfortunatly I can't seem to be call this set_Text able to do this since there are conflits when WPF and WinForms Extension methods are
         * used at the same time */
        public static T set_Text_Wpf<T>(this T control, string value)
            where T : ContentControl
        {
            return (T)control.wpfInvoke(() =>
                        {
                            control.Content = value;
                            return control;
                        });
        }

        public static T set_Content<T>(this T control, string value)
            where T : ContentControl
        {
            return control.set_Text_Wpf(value);
        }

        public static Brush get_Color<T>(this T control)
            where T : Control
        {
            return (Brush)control.wpfInvoke(() => control.Foreground);
        }
		#endregion

        #region WPF child controls

        	public static List<T> allUIElements<T>(this UIElement uiElement)
       		where T : UIElement
       	{
			return (from element in uiElement.allUIElements()
					where element is T
					select (T)element).toList();
       	}
       	
        public static List<UIElement> allUIElements(this UIElement uiElement)
        {        	
        	return (List<UIElement>)uiElement.wpfInvoke(
        		()=>{
        				var uiElements = new List<UIElement>();        				        				
        				
        				uiElements.Add(uiElement);
        				
        				if (uiElement is ContentControl)
        				{
        					var content = (uiElement as ContentControl).Content;
        					if (content is UIElement)
        						uiElements.AddRange((content as UIElement).allUIElements());
        				}
        				if (uiElement is Panel)
        				{
        					var children = (uiElement as Panel).Children;
        					foreach(var child in children)
        						if (child is UIElement)
        							uiElements.AddRange((child as UIElement).allUIElements()); 
        				}
        				return uiElements;
					});        			        	                	
        }
        
        public static List<T> controls_Wpf<T>(this UIElement uiElement)
        	where T : UIElement
        {
        	return uiElement.allUIElements<T>();
        }    
    
        public static List<UIElement> controls_Wpf(this UIElement uiElement)
        {
        	return uiElement.allUIElements();
        }



        #endregion

        #region generic events

        public static T onMouseDoubleClick<T1, T>(this T control, Action<T1> callback)
            where T : Control
        {
            control.MouseDoubleClick += (sender, e) =>
            {
                var tag = control.get_Tag();
                if (tag != null && tag is T1)
                    callback((T1)tag);
            };
            return control;
        }

        #endregion

        #region Label

        public static Label set_Text(this Label label, string value)
    	{
            label.set_Text_Wpf(value);
    		//label.wpfInvoke(()=> label.Content = value);    		
			return label;
    	}
    	
    	#endregion
    	    	        	
    	#region TextBox
		
		public static TextBox set_Text(this TextBox textBox, string value)
    	{
    		textBox.wpfInvoke(()=> textBox.Text = value);    		
			return textBox;
    	}

        public static string get_Text_Wpf(this TextBox textBox)
        {
            return (string)textBox.wpfInvoke(
                () =>
                {
                    return textBox.Text;
                });
        }

        public static TextBox set_Text_Wpf(this TextBox textBox, string text)
        {
            return (TextBox)textBox.wpfInvoke(
                () =>
                {
                    textBox.Text = text;
                    return textBox;
                });
        }

        public static TextBox multiLine(this TextBox textBox)
        {
            return (TextBox)textBox.wpfInvoke(
                () =>
                {
                    textBox.TextWrapping = TextWrapping.Wrap;
                    textBox.AcceptsReturn = true;
                    textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    return textBox;
                });
        }
    	#endregion
               
        #region FrameworkElement

        // this is the generic one
    	public static T prop<T>(this T frameworkElement, string propertyName, object value) where T : FrameworkElement
    	{    		
    		frameworkElement.wpfInvoke(
    			()=>{ PublicDI.reflection.setProperty(propertyName, frameworkElement, value); });    				
			return frameworkElement;
    	}
    	
    	
    	// specific to frameworkElement

        public static T width_Wpf<T>(this T frameworkElement, double width)
            where T : FrameworkElement
        {
            return (T)frameworkElement.wpfInvoke(
                () =>
                {
                    if (width > -1)
                        frameworkElement.Width = width;
                    return frameworkElement;
                });
        }

        public static T height_Wpf<T>(this T frameworkElement, double height)
            where T : FrameworkElement
        {
            return (T)frameworkElement.wpfInvoke(
                () =>
                {
                    if (height > -1)
                        frameworkElement.Height = height;
                    return frameworkElement;
                });
        }

        public static T left_Wpf<T>(this T frameworkElement, double left)
            where T : FrameworkElement
        {
            return (T)frameworkElement.wpfInvoke(
                () =>
                {
                    if (left > -1)
                        frameworkElement.SetValue(Canvas.LeftProperty, (double)left);
                    return frameworkElement;
                });
        }

        public static T top_Wpf<T>(this T frameworkElement, double top)
            where T : FrameworkElement
        {
            return (T)frameworkElement.wpfInvoke(
                () =>
                {
                    if (top > -1)
                        frameworkElement.SetValue(Canvas.TopProperty, (double)top);
                    return frameworkElement;
                });
        }

    	/*public static T width<T>(this T frameworkElement, double value) where T : FrameworkElement
    	{
    		return frameworkElement.prop("Width",value); 
    	}
    	
    	public static T height<T>(this T frameworkElement, double value) where T : FrameworkElement
    	{
    		return frameworkElement.prop("Height",value); 
    	}
        */
        public static double width(this FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.wpfInvoke(() => { return frameworkElement.Width; });
        }

        public static double height(this FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.wpfInvoke(() => { return frameworkElement.Height; });
        }
    	
    	public static T tag<T>(this T frameworkElement, object value) where T : FrameworkElement
    	{
    		return frameworkElement.prop("Tag",value); 
    	}
    	
    	// specific to Panel (Ideally i should be able to merge this with the Control ones, but it doesn't seem to be possible    	
    	public static Canvas background(this Canvas canvas, Brush value) 
    	{
    		return canvas.prop("Background",value); 
    	}
    	// specific to Control    	
    	public static T background<T>(this T frameworkElement, Brush value) 
            where T : Control
    	{
    		return frameworkElement.prop("Background",value); 
    	}

        public static T backColor<T>(this T frameworkElement, Brush value)
            where T : FrameworkElement
        {
            return frameworkElement.prop<T>("Background", value);
        }

    	public static T fontSize<T>(this T frameworkElement, double value) where T : Control
    	{
    		return frameworkElement.prop("FontSize",value); 
    	}
    	
    	public static T fontColor<T>(this T frameworkElement, Brush value) where T : Control
    	{
    		return frameworkElement.prop("Foreground",value); 
    	}
    	
    	public static T foreground<T>(this T frameworkElement, Brush value) where T : Control
    	{
    		return frameworkElement.prop("Foreground",value); 
    	}
    	
    	public static T borderThickness<T>(this T frameworkElement, double value) where T : Control
    	{
    		return frameworkElement.borderThickness(new Thickness(value));    		
    	}
    	
    	public static T borderThickness<T>(this T frameworkElement, Thickness value) where T : Control
    	{
    		return frameworkElement.prop("BorderThickness",value); 
    	}
    	
    	public static T padding<T>(this T frameworkElement, double value) where T : Control
    	{
    		return frameworkElement.padding(new Thickness(value));    		
    	}
    	
    	public static T padding<T>(this T frameworkElement, Thickness value) where T : Control
    	{
    		return frameworkElement.prop("Padding",value); 
    	}
    	
    	public static T borderBrush<T>(this T frameworkElement, Brush value) where T : Control
    	{
    		return frameworkElement.prop("BorderBrush",value); 
    	}
    	
    	public static T vertical<T>(this T frameworkElement, VerticalAlignment value) where T : Control
    	{
    		return frameworkElement.verticalContentAlignment(value);
    	}
    	public static T verticalContentAlignment<T>(this T frameworkElement, VerticalAlignment value) where T : Control
    	{
    		return frameworkElement.prop("VerticalContentAlignment",value); 
    	}
    	
    	public static T horizontal<T>(this T frameworkElement, HorizontalAlignment value) where T : Control
    	{
    		return frameworkElement.horizontalContentAlignment(value);
    	}
    	public static T horizontalContentAlignment<T>(this T frameworkElement, HorizontalAlignment value) where T : Control
    	{
    		return frameworkElement.prop("HorizontalContentAlignment",value); 
    	}
    	
    	// specific to TextBoxBase
    	
    	public static T acceptsReturn<T>(this T frameworkElement, bool value) where T : TextBoxBase
    	{
    		return frameworkElement.prop("AcceptsReturn",value); 
    	}

		public static T acceptsTab<T>(this T frameworkElement, bool value) where T : TextBoxBase
    	{
    		return frameworkElement.prop("AcceptsTab",value); 
    	}    	
    	
    	/*
    	
textBox1.prop("VerticalContentAlignment",VerticalAlignment.Center);  
textBox1.prop("HorizontalContentAlignment",System.Windows.HorizontalAlignment.Center);  
textBox1.prop("",true);

    	*/
    	
    	
    	
    	#endregion

        #region TreeView

        public static TreeView add_WPF_TreeView(this System.Windows.Forms.Control control)
        {
            return control.add_WPF_Control<TreeView>();
        }

        public static TreeViewItem treeViewItem(this UIElement uiElement, object tag)
        {
            return (TreeViewItem)uiElement.wpfInvoke(
                () =>
                {
                    return (tag != null)
                        ? uiElement.treeViewItem(tag.str(), tag)
                        : new TreeViewItem();
                });
        }

        public static TreeViewItem treeViewItem(this UIElement uiElement, string itemText, object tag)
        {
            return (TreeViewItem)uiElement.wpfInvoke(
                () =>
                {
                    if (tag is TreeViewItem) 			// to prevent recursive add
                        return (TreeViewItem)tag;
                    var treeViewItem = new TreeViewItem();
                    treeViewItem.Header = itemText;
                    treeViewItem.Tag = tag;
                    return treeViewItem;
                });
        }

        public static List<TreeViewItem> treeViewItems<T>(this UIElement uiElement, IEnumerable<T> collection)
        {
            return (List<TreeViewItem>)uiElement.wpfInvoke(
                () =>
                {
                    var newTreeViewItems = new List<TreeViewItem>();
                    //newTreeViewItems.Add(uiElement.treeViewItem(collection[0]));
                    foreach (var item in collection)
                        newTreeViewItems.Add(uiElement.treeViewItem(item));
                    return newTreeViewItems;
                });
        }

        public static List<TreeViewItem> add_Nodes<T>(this TreeView treeView, IEnumerable<T> collection)
        {
            var newTreeViewItems = treeView.treeViewItems(collection);
            foreach (var newTreeViewItem in newTreeViewItems)
                treeView.add_Node(newTreeViewItem);
            return newTreeViewItems;
        }

        public static List<TreeViewItem> add_Nodes<T>(this TreeViewItem treeViewItem, IEnumerable<T> collection)
        {
            var newTreeViewItems = treeViewItem.treeViewItems(collection);
            foreach (var newTreeViewItem in newTreeViewItems)
                treeViewItem.add_Node(newTreeViewItem);
            return newTreeViewItems;
        }

        /*public static List<TreeViewItem> add<T>(this TreeView treeView, List<TreeViewItem> treeViewItems)
        {
            foreach(var treeViewItem in treeViewItems)
                treeView.add(treeViewItem);
            return treeViewItems;
        }*/

        public static TreeViewItem add_Node(this TreeView treeView, object newItem)
        {
            return (newItem != null)
                ? treeView.add_Node(newItem.str(), newItem)
                : null;
        }

        public static TreeViewItem add_Node(this TreeView treeView, string itemText, object tag)
        {
            return (TreeViewItem)treeView.wpfInvoke(
                () =>
                {
                    var newItem = treeView.treeViewItem(itemText, tag);
                    treeView.Items.add_Node(newItem);
                    return newItem;
                });
        }

        public static TreeViewItem add_Node(this TreeViewItem treeViewItem, object newItem)
        {
            return (newItem != null)
                ? treeViewItem.add_Node(newItem.str(), newItem)
                : treeViewItem;
        }

        public static TreeViewItem add_Node(this TreeViewItem treeViewItem, string itemText, object tag)
        {
            return (TreeViewItem)treeViewItem.wpfInvoke(
                () =>
                {
                    var newItem = treeViewItem.treeViewItem(itemText, tag);
                    treeViewItem.Items.Add(newItem);
                    return newItem;
                });
        }

        public static TreeViewItem add_Node(this ItemCollection items, TreeViewItem item)
        {
            items.Add(item);
            return item;
        }

        public static TreeView clear(this TreeView treeView)
        {
            return (TreeView)treeView.wpfInvoke(
                () =>
                {
                    treeView.Items.Clear();
                    return treeView;
                });
        }

        public static TreeViewItem clear(this TreeViewItem treeViewItem)
        {
            return (TreeViewItem)treeViewItem.wpfInvoke(
                () =>
                {
                    treeViewItem.Items.Clear();
                    return treeViewItem;
                });
        }

        public static TreeViewItem colapse(this TreeViewItem treeViewItem)
        {
            return (TreeViewItem)treeViewItem.wpfInvoke(
                () =>
                {
                    treeViewItem.IsExpanded = false;
                    return treeViewItem;
                });
        }

        public static TreeViewItem expand(this TreeViewItem treeViewItem)
        {
            return treeViewItem.expand(false);
        }

        public static TreeViewItem expand(this TreeViewItem treeViewItem, bool recursive)
        {
            return (TreeViewItem)treeViewItem.wpfInvoke(
                () =>
                {
                    treeViewItem.IsExpanded = true;
                    if (recursive)
                        foreach (var childItem in treeViewItem.Items)
                            if (childItem is TreeViewItem)
                                ((TreeViewItem)childItem).expand(true);
                    return treeViewItem;
                });
        }

        public static TreeView expand(this TreeView treeView)
        {
            return treeView.expand(false);
        }

        public static TreeView expand(this TreeView treeView, bool recursive)
        {
            return (TreeView)treeView.wpfInvoke(
                () =>
                {
                    foreach (var item in treeView.Items)
                        if (item is TreeViewItem)
                            ((TreeViewItem)item).expand(recursive);
                    return treeView;
                });
        }

        public static TreeView fontSize(this TreeView treeView, double size)
        {
            return treeView.set("FontSize", size);
        }

        // generic setter for treeView properties
        public static TreeView set(this TreeView treeView, string propertyToSet, object value)
        {
            return (TreeView)treeView.wpfInvoke(
                () =>
                {
                    treeView.prop(propertyToSet, value);
                    return treeView;
                });
        }

        public static TreeView beforeExpand<T>(this TreeView treeView, Action<TreeViewItem, T> onBeforeExpand)
        {
            return (TreeView)treeView.wpfInvoke(
                () =>
                {
                    treeView.AddHandler(
                        TreeViewItem.ExpandedEvent,
                        new RoutedEventHandler(
                            (sender, e) =>
                            {
                                if (e.Source is TreeViewItem)
                                {
                                    var treeViewItem = (TreeViewItem)e.Source;
                                    if (treeViewItem.Tag != null && treeViewItem.Tag is T)
                                        onBeforeExpand(treeViewItem, (T)treeViewItem.Tag);
                                }
                            }));
                    return treeView;
                });
        }

        #endregion

        #region Image

        public static Image open(this Image image, string imageLocation)
        {
            return image.open(imageLocation.uri(), -1, -1);
        }

        public static Image open(this Image image, string imageLocation, int width, int height)
        {
            return image.open(imageLocation.uri(), width, height);
        }

        public static Image open(this Image image, Uri imageLocation)
        {
            return image.open(imageLocation, -1, -1);
        }

        public static Image open(this Image image, Uri imageLocation, int width, int height)
        {
            return (Image)image.wpfInvoke(
                () =>
                {
                    if (imageLocation.notNull())
                    {
                        var bitmap = new BitmapImage(imageLocation);
                        image.Source = bitmap;
                        if (width > -1)
                        {
                            image.width_Wpf<Image>((double)width);
                        }
                        if (height > -1)
                        {
                            image.height_Wpf<Image>((double)height);
                        }
                    }
                    return image;
                });
        }

        #endregion

        #region ComboBox

        public static ComboBox add_Item(this ComboBox comboBox, string itemText)
        {
            return comboBox.add_Items(itemText);
        }

        public static ComboBox add_Items(this ComboBox comboBox, params string[] itemTexts)
        {
            return (ComboBox)comboBox.wpfInvoke(
                () =>
                {
                    foreach (var itemText in itemTexts)
                    {
                        var comboBoxItem = new ComboBoxItem();
                        comboBoxItem.Content = itemText;
                        comboBox.Items.Add(comboBoxItem);
                    }
                    return comboBox;
                });
        }

        public static ComboBox selectFirst(this ComboBox comboBox)
        {
            return (ComboBox)comboBox.wpfInvoke(
                () =>
                {
                    if (comboBox.Items.size() > 0)
                        comboBox.SelectedIndex = 0;
                    return comboBox;
                });
        }

        public static string get_Text_Wpf(this ComboBox comboBox)
        {
            return (string)comboBox.wpfInvoke(
                () =>
                {
                    if (comboBox.SelectedItem.notNull() && comboBox.SelectedItem is ComboBoxItem)
                        return (comboBox.SelectedItem as ComboBoxItem).Content;
                    return "";
                });
        }

        #endregion

        #region Button

        public static Button onClick_Wpf(this Button button, Action callback)
        {
            return (Button)button.wpfInvoke(
                () =>
                {
                    button.Click += (sender, e) => callback();
                    return button;
                });
        }

        #endregion

        #region StackPanel

        public static StackPanel add_StackPanel(this UIElement uiElement)
        {
            return uiElement.add_Control_Wpf<StackPanel>();
        }

        #endregion

        #region Grid

        public static Grid add_Grid(this UIElement uiElement)
        {
            return uiElement.add_Control_Wpf<Grid>();
        }

        #endregion

        #region WebBrowser (WPF one which is a wrapper on the WinForms one)

        public static WebBrowser open(this WebBrowser webBrowser, string url)
        {
            if (url.isUri())
            {
                "[WPF WebBrowser] opening page: {0}".debug(url);
                webBrowser.wpfInvoke(() => webBrowser.Navigate(url.uri()));
            }
            return webBrowser;
        }

        #endregion

        #region Control - foreground text Color

        public static T color<T>(this T control, string colorName) where T : Control
        {
            var color = new BrushConverter().ConvertFromString(colorName);
            if (color is Brush)
                control.fontColor((Brush)color);
            return control;
        }


        public static T black<T>(this T control) where T : Control
        {
            return control.fontColor(Brushes.Black);
        }

        public static T blue<T>(this T control) where T : Control
        {
            return control.fontColor(Brushes.Blue);
        }

        public static T red<T>(this T control) where T : Control
        {
            return control.fontColor(Brushes.Red);            
        }
      
        #endregion

        #region Animation

        public static T rotate<T>(this T uiElement)
            where T : UIElement
        {
            return uiElement.rotate(0, 360, 3, false);
        }

        public static T rotate<T>(this T uiElement, bool loopAnimation)
            where T : UIElement
        {
            return uiElement.rotate(0, 360, 3, loopAnimation);
        }

        public static T rotate<T>(this T uiElement, double fromValue, double toValue, int durationInSeconds, bool loopAnimation)
            where T : UIElement
        {
            return (T)uiElement.wpfInvoke(
                () =>
                {
                    DoubleAnimation doubleAnimation = new DoubleAnimation(fromValue, toValue, new Duration(TimeSpan.FromSeconds(durationInSeconds)));
                    RotateTransform rotateTransform = new RotateTransform();
                    uiElement.RenderTransform = rotateTransform;
                    uiElement.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
                    if (loopAnimation)
                        doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    rotateTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation);
                    return uiElement;
                });
        }

        public static T fadeIn<T>(this T uiElement, int durationInSeconds)
            where T : UIElement
        {
            return uiElement.fadeFromTo(0, 1, durationInSeconds, false);
        }

        public static T fadeOut<T>(this T uiElement, int durationInSeconds)
            where T : UIElement
        {
            return uiElement.fadeFromTo(1, 0, durationInSeconds, false);
        }

        public static T fadeFromTo<T>(this T uiElement, double fromOpacity, double toOpacity, int durationInSeconds, bool loopAnimation)
            where T : UIElement
        {
            return (T)uiElement.wpfInvoke(() =>
            {
                var doubleAnimation = new DoubleAnimation(fromOpacity, toOpacity, new Duration(TimeSpan.FromSeconds(durationInSeconds)));
                if (loopAnimation)
                    doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                uiElement.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
                return uiElement;
            });
        }

        #endregion

    }
}
