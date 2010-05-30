using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.XRules.Database._Rules._Interfaces
{    
    public interface ISecretData
    {        
        List<Credential> Credentials { get; set;}
    }
    
    public interface ICredential
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Url { get; set; }
        string CredentialType { get; set; }
        string Description { get; set; }
    }

    [Serializable]
    public class SecretData : ISecretData
    {
        public List<Credential> Credentials { get; set; }

        public SecretData()
        {
            Credentials = new List<Credential>();
        }
    }

    [Serializable]
    public class Credential : ICredential
    {
        [XmlAttribute]
        public string UserName { get; set; }
        [XmlAttribute]
        public string Password { get; set; }
        [XmlAttribute]
        public string CredentialType { get; set; }
        [XmlAttribute]
        public string Url { get; set; }
        [XmlAttribute]
        public string Description { get; set; }

        public Credential() : this("","","")
        { }

        public Credential(string userName, string password, string credentialType) 
        { 
            UserName = userName;
            Password = password;
            CredentialType = credentialType;
            Url ="";
            Description = "";
        }

        public Credential(string userName, string password) : this(userName, password, "")
        {
            
        }

        public override string ToString()
        {
            return UserName ?? base.ToString();
        }

    }

    public static class SecretData_ExtensionMethods
    {
        #region username

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
        #endregion

    }
}
