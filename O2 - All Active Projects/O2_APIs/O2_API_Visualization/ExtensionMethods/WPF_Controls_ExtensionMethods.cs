// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.Windows.Threading;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System;
using System.Windows.Markup;

//O2File:WPF_Threading_ExtensionMethods.cs

namespace O2.API.Visualization.ExtensionMethods
{
    public static class WPF_Controls_ExtensionMethods
    {    
	
		#region generic methods								
    	
    	public static T add_Control<T>(this ContentControl uiElement) where T : UIElement
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
		
		#endregion
		
		#region Label
		
		public static Label set_Text(this Label label, string value)
    	{
    		label.wpfInvoke(()=> label.Content = value);    		
			return label;
    	}
    	
    	#endregion
    	    	        	
    	#region TextBox
		
		public static TextBox set_Text(this TextBox textBox, string value)
    	{
    		textBox.wpfInvoke(()=> textBox.Text = value);    		
			return textBox;
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
    	
    	public static T width<T>(this T frameworkElement, double value) where T : FrameworkElement
    	{
    		return frameworkElement.prop("Width",value); 
    	}
    	
    	public static T height<T>(this T frameworkElement, double value) where T : FrameworkElement
    	{
    		return frameworkElement.prop("Height",value); 
    	}

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
    	public static T background<T>(this T frameworkElement, Brush value) where T : Control
    	{
    		return frameworkElement.prop("Background",value); 
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
    }
}
