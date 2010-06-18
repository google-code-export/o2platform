using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Jint
{
    public interface IMethodInvoker
    {
        /// <summary>
        /// Searches a method with parameters compatible with the specified ECMAScript ones
        /// </summary>
        /// <param name="subject">Object to call the method on</param>
        /// <param name="method">Name of the method to call</param>
        /// <param name="parameters">Parameters of the method</param>
        /// <param name="generics">Generic types for the method</param>
        /// <returns>A compatible MethodInfo if any</returns>
        MethodInfo Invoke(object subject, string method, object[] parameters, Type[] generics);

        /// <summary>
        /// Converts a set of parameters to the compatible objects while calling a specific method
        /// </summary>
        /// <param name="parameters">The parameters to call</param>
        /// <param name="pis">The type descriptors</param>
        /// <param name="subject">The obejct to call the method on</param>
        void GetAppropriateParameters(object[] parameters, Type[] pis, object subject);
        void GetAppropriateParameters(object[] parameters, ParameterInfo[] pis, object subject);

        /// <summary>
        /// Converts a set of parameters to the compatible objects while calling a specific method
        /// </summary>
        /// <param name="parameters">The parameters to call</param>
        /// <param name="pis">The type descriptors</param>
        /// <param name="subject">The obejct to call the method on</param>
        /// <returns>True if the compatible parameters are found</returns>
        bool TryGetAppropriateParameters(object[] parameters, Type[] pis, object subject);
        bool TryGetAppropriateParameters(object[] parameters, ParameterInfo[] pis, object subject);
        
    }
}
