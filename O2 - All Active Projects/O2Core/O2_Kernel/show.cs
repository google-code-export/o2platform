using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;

namespace O2.Kernel
{
    public class show
    {
        public static void info(object _object)
        {
            if (_object != null)
            {
                //var propertyGrid = typeof(PropertyGrid)
                //				   .openControlAsForm<PropertyGrid>(
                //				   		_object.typeName(),
                var propertyGrid = open.viewAscx("ascx_ShowInfo", _object.typeName(), 300, 300);
                propertyGrid.invoke("show", _object);
            }
        }

        public static T control<T>(T type)
        {
            return type;
        }
    }
    
}
