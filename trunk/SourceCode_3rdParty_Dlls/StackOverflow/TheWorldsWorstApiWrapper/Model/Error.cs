using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace TheWorldsWorst.ApiWrapper.Model
{
    [DataContract]
    public class Error : HasWrapperClass
    {
        [DataContract]
        class ErrorWrapper
        {
            [DataMember]
            Error error;
        }

        internal override Type WrapperClass
        {
            get { return typeof(ErrorWrapper); }
        }

        [DataMember(Name="code")]
        public int Code { get; internal set; }
        [DataMember(Name="message")]
        public string Message { get; internal set; }
    }
}
