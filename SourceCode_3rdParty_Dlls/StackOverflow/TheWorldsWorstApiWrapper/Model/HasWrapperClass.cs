using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    [DataContract]
    public abstract class HasWrapperClass
    {
        internal abstract Type WrapperClass { get; }
    }
}
