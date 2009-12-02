// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using O2.Cmd.SpringMvc.Objects;
using O2.Cmd.SpringMvc.Xsd;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Misc;

namespace O2.Cmd.SpringMvc.Classes
{
    public class LoadSpringMvcData
    {

        const string SPRING_MVC_CLASS_REQUEST_MAPPING = "org.springframework.web.bind.annotation.RequestMapping";
        const string SPRING_MVC_CLASS_CONTROLLER = "org.springframework.stereotype.Controller";

        public static List<SpringMvcController> createSpringMvcControllersFromXmlAttributeFile(string xmlAttributeFile)
        {
            var springMvcControllers = new List<SpringMvcController>();
            try
            {

                var javaAttributes =
                    (JavaAttributeMappings)
                    Serialize.getDeSerializedObjectFromXmlFile(xmlAttributeFile, typeof (JavaAttributeMappings));
                if (javaAttributes != null && javaAttributes.@class != null)
                {
                    // map classes
                    foreach (var javaAttributeClass in javaAttributes.@class)
                    {
                        javaAttributeClass.sourceFile = javaAttributeClass.sourceFile; // SourceCodeMappingsUtils.mapFile(javaAttributeClass.sourceFile);
                        // check if this class is a controller
                        if (isClassASpringMvcController(javaAttributeClass))
                        {

                            //var controllerForCurrentClass = new SpringMvcController();
                            //controllerForCurrentClass.ClassName = javaAttributeClass.name;
                            // set RequestMapping attribute
                            var controllerForCurrentClass = getRequestMapping(javaAttributeClass);

                            //controllerForCurrentClass.URL.Controller = controllerForCurrentClass;
                            // add class methods
                            mapMethods(javaAttributeClass.method, javaAttributeClass.name, controllerForCurrentClass, springMvcControllers, javaAttributeClass.sourceFile);

                            // add controllerForCurrentClass to the list of SpringMvcController
                            if (controllerForCurrentClass!= null)
                                springMvcControllers.Add(controllerForCurrentClass);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);                
            }
            
            return springMvcControllers;
        }

        private static void mapMethods(method[] methodsToMap,string javaClassName, SpringMvcController controllerForCurrentClass, List<SpringMvcController> springMvcControllers, string fileName)
        {
            if (methodsToMap != null)            
                foreach(var methodToMap in methodsToMap)
                {
                    var mappedController = getRequestMapping(methodToMap);
                    if (mappedController != null) // || controllerForCurrentClass != null) 
                    {
                        if (mappedController.HttpRequestUrl == null && controllerForCurrentClass!=null)
                            mappedController.HttpRequestUrl = controllerForCurrentClass.HttpRequestUrl;

                        mappedController.JavaClass = javaClassName;
                        mappedController.JavaFunction = (FilteredSignature.createFilteredSignatureFromJavaMethod(mappedController.JavaClass, methodToMap.name, methodToMap.descriptor).sFunctionNameAndParamsAndReturnClass);
                        mappedController.JavaClassAndFunction = string.Format("{0}.{1}", mappedController.JavaClass, mappedController.JavaFunction);
                        mappedController.AutoWiredJavaObjects = getAutoWiredJavaObjects(methodToMap);
                        mappedController.FileName = fileName;
                        mappedController.LineNumber = (uint)methodToMap.lineNumber;
                        springMvcControllers.Add(mappedController);
                    }

                    //springMvcController.URL = ;
                    //methodToMap.attribute
                    // methodToMap.SpringMvcMapping
                }            
        }

        private static List<SpringMvcParameter> getAutoWiredJavaObjects(method methodToMap)
        {
            var springMvcParamters = new List<SpringMvcParameter>();
            if (methodToMap.parameterAnnotation != null)
                foreach (var parameterAnnotation in methodToMap.parameterAnnotation)
                {
                    var springMvcParameter = new SpringMvcParameter();
                    switch (parameterAnnotation.typeName)
                    { 
                        case "org.springframework.web.bind.annotation.RequestParam":
                        case "org.springframework.web.bind.annotation.ModelAttribute":
                        case "org.springframework.web.bind.annotation.PathVariable":
                            springMvcParameter.autoWiredMethodUsed = parameterAnnotation.typeName.Replace("org.springframework.web.bind.annotation.", "");
                            if (parameterAnnotation.member != null)
                                springMvcParameter.name = decodeString(parameterAnnotation.member.memberValue).Replace("\"","");
                            break;
                        
                        default:
                            if (parameterAnnotation.typeName !=null)
                                springMvcParameter.autoWiredMethodUsed = parameterAnnotation.typeName;
                            break;
                    }
                    springMvcParamters.Add(springMvcParameter);             
                }
            return springMvcParamters;
        }

        public static bool isClassASpringMvcController(@class javaAttributeClass)
        {
            return null != getAnnotation(javaAttributeClass, SPRING_MVC_CLASS_CONTROLLER);                     
        }

        public static SpringMvcController getRequestMapping(@class javaAttributeClass)
        {
            var requestMappingAnnotation = getAnnotation(javaAttributeClass,SPRING_MVC_CLASS_REQUEST_MAPPING);            
            var mappedControler = getRequestMapping(requestMappingAnnotation);
            if (mappedControler != null)
            {
                mappedControler.JavaClass = javaAttributeClass.name;
                mappedControler.FileName = javaAttributeClass.sourceFile;
                mappedControler.LineNumber = 0;
            }
            return mappedControler;
        }

        public static SpringMvcController getRequestMapping(method methodToAnalyze)
        {
            var requestMappingAnnotation = getAnnotation(methodToAnalyze,SPRING_MVC_CLASS_REQUEST_MAPPING);
            if (requestMappingAnnotation!= null && requestMappingAnnotation.member == null)      // in this case make the method name the name of the current method
                requestMappingAnnotation.member = new [] {new member {memberName = "method", memberValue = methodToAnalyze.name}};
            return getRequestMapping(requestMappingAnnotation);
        }

        public static SpringMvcController getRequestMapping(annotation javaAnnotation)
        {
            if (javaAnnotation != null)
            {
                var newController = new SpringMvcController();
                if (javaAnnotation.member != null)
                    foreach (var member in javaAnnotation.member)
                        switch (member.memberName)
                        {
                            case "method":
                                newController.HttpRequestMethod = member.memberValue;
                                newController.HttpRequestMethod = newController.HttpRequestMethod.Replace("{org.springframework.web.bind.annotation.RequestMethod.", "").Replace("}", "");
                                break;
                            case "value":
                                newController.HttpRequestUrl = decodeString(member.memberValue);
                                newController.HttpRequestUrl = newController.HttpRequestUrl.Replace("{\"", "").Replace("\"}", "");
                                break;
                            case "params":
                                newController.HttpMappingParameter = decodeString(member.memberValue);
                                newController.HttpMappingParameter = newController.HttpMappingParameter.Replace("{\"", "").Replace("\"}", "");

                                break;
                        }
                return newController;
            }
            return null;
        }

        /// <summary>
        /// gets the annotation object (_note that it will return the first match)
        /// </summary>
        /// <param name="javaAttributeClass"></param>
        /// <param name="annotationTypeToRetrieve"></param>
        /// <returns></returns>
        public static annotation getAnnotation(@class javaAttributeClass, string annotationTypeToRetrieve)
        {
            if (javaAttributeClass != null && javaAttributeClass.attribute != null)
                foreach (var attributeToAnalyze in javaAttributeClass.attribute)
                {
                    var annotationValue = getAnnotation(attributeToAnalyze, annotationTypeToRetrieve);
                    if (annotationValue != null)
                        return annotationValue;
                }
            return null;
        }

        public static annotation getAnnotation(method methodToAnalyze, string annotationTypeToRetrieve)
        {
            if (methodToAnalyze != null && methodToAnalyze.attribute != null)
                foreach (var attributeToAnalyze in methodToAnalyze.attribute)
                {
                    var annotationValue = getAnnotation(attributeToAnalyze, annotationTypeToRetrieve);
                    if (annotationValue != null)
                        return annotationValue;
                }
            return null;
        }
        
        public static annotation getAnnotation(attribute attributeToAnalyze, string annotationTypeToRetrieve)
        {
            if (attributeToAnalyze.annotation != null)
                foreach (var classAnnotation in attributeToAnalyze.annotation)
                    if (classAnnotation.typeName == annotationTypeToRetrieve)
                        return classAnnotation;
            return null;
        }


        // hack to decode Annotation.py created string
        public static string decodeString(string stringToDecode)
        {
            return stringToDecode.Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">");
        }
    }
}
