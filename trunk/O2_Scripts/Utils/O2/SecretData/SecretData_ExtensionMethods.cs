﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
//O2File:ISecretData.cs

namespace O2.XRules.Database.Utils.O2
{

	public static class SecretData_ExtensionMethods
    {
        #region credential

        public static List<Credential> credentialTypes(this ISecretData secretData, string credentialType)
        {            
            var credentials = new List<Credential>();
            if (credentialType.valid().isFalse())
                if (secretData != null && secretData.Credentials != null)
                    return secretData.Credentials;
                else
                    return credentials;
            
            foreach (var credential in secretData.Credentials)
                if (credential.CredentialType == credentialType)
                    credentials.add(credential);
            return credentials;
        }
                
            public static ICredential credential(this string fileWithSecretData, string credentialType)
        {
            if (fileWithSecretData.fileExists())
            {
                var secretData = fileWithSecretData.deserialize<SecretData>();
                return secretData.credential(credentialType);
            }
            return null;
        }

        public static ICredential credential(this SecretData secretData, string credentialType)
        {
            if (secretData != null)
            {
                var credentials = secretData.credentialTypes(credentialType);
                if (credentials != null && credentials.size() > 0)
                    return credentials[0];
            }
            return null;
        }
        
        public static List<Credential> credentials(this string fileWithSecretData)
    	{
    		if (fileWithSecretData.fileExists())
            {
            	var secretData = fileWithSecretData.deserialize<SecretData>();
    			return secretData.Credentials;
            }
           	return new List<Credential>();
    	}
    	
    	public static List<Credential> credentialTypes(this string fileWithSecretData, string credentialType)
    	{
    		if (fileWithSecretData.fileExists())
            {
    			var secretData = fileWithSecretData.deserialize<SecretData>();
    			return secretData.credentialTypes(credentialType);
    		}
    		return new List<Credential>();
    	}
        
                
        #endregion
        
        #region username

        public static string username(this ISecretData secretData)
        {
            return secretData.username("", 0);
        }

        public static string username(this ISecretData secretData, string credentialType)
        {
            return secretData.username(credentialType,0);
        }
        
        public static string username(this ISecretData secretData, int index)
        {
            return secretData.username("", index);
        }

        public static string username(this ISecretData secretData, string credentialType, int index)
        {
            var credentials = secretData.credentialTypes(credentialType);
            if (index < credentials.size())
                return credentials[index].UserName;
            return "";
        }

        public static List<ICredential> usernames(this ISecretData secretData)
        {
            return secretData.usernames("");
        }
        public static List<ICredential> usernames(this ISecretData secretData, string credentialType)
        {
            var usernames = from credential in secretData.credentialTypes(credentialType)
                            select (ICredential)credential;
            return usernames.ToList();
        }
        
        public static string username(this ICredential credential)
        {
        	if (credential.notNull())
            	return credential.UserName;
            return "";
        }

        #endregion

        #region get_User

        public static ICredential get_User(this ISecretData secretData, string userName)
        {
            return secretData.get_User("", userName);
        }

        public static ICredential get_User(this ISecretData secretData, string credentialType, string userName)
        {
            if (secretData != null && secretData.Credentials != null)
                foreach (var credential in secretData.Credentials)
                    if (credential.UserName == userName)
                        if (credentialType.valid().isFalse() || credentialType == credential.CredentialType)
                            return credential;
            return null;
        }

        #endregion
        
        #region password

        public static string password(this ISecretData secretData)
        {
            return secretData.password("", 0);
        }

        public static string password(this ISecretData secretData, string credentialType)
        {
            return secretData.password(credentialType, 0);
        }

        public static string password(this ISecretData secretData, int index)
        {
            return secretData.password("", index);
        }

        public static string password(this ISecretData secretData, string credentialType, int index)
        {
            var credentials = secretData.credentialTypes(credentialType);
            if (index < credentials.size())
                return credentials[index].Password;
            return "";
        }

        public static string password(this ISecretData secretData, string credentialType, string username)
        {
            foreach (var credential in secretData.Credentials)
                if (credential.UserName == username && credential.CredentialType == credentialType)
                    return credential.Password;
            return "";
        }
                
        public static string password(this ICredential credential)
        {
            return credential.Password;
        }
        #endregion


    }   
}
