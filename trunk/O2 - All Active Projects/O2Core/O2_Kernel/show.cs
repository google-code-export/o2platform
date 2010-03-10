using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;
using O2.Kernel.CodeUtils;

namespace O2.Kernel
{
    public class show
    {
        public static object propertyGrid()
        {
            return open.viewAscx("ascx_ShowInfo", "Property Grid", 300, 300);
        }
        public static void info(object _object)
        {
            O2Kernel_O2Thread.mtaThread(
                () =>
                {
                    if (_object != null)
                    {
                        //var propertyGrid = typeof(PropertyGrid)
                        //				   .openControlAsForm<PropertyGrid>(
                        //				   		_object.typeName(),
                        var propertyGrid = open.viewAscx("ascx_ShowInfo", _object.typeName(), 300, 300);
                        propertyGrid.invoke("show", _object);
                    }
                });
        }

        public static T control<T>(T type)
        {
            return type;
        }
    }
    
}
