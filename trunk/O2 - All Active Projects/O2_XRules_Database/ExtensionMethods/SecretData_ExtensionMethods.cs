using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database._Rules._Interfaces;

namespace O2.XRules.Database.ExtensionMethods
{
    public static class SecretData_ExtensionMethods
    {
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

        public static string username(this ICredential credential)
        {
            return credential.UserName;
        }

        public static string password(this ICredential credential)
        {
            return credential.Password;
        }
    }
}
