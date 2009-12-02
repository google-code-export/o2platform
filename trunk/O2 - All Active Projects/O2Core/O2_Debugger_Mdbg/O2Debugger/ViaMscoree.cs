using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using mscoree;

namespace O2.Debugger.Mdbg.NewCode
{
    public class ViaMscoree
    {
        // code sample adapted from http://blogs.msdn.com/jackg/archive/2007/06/11/enumerating-appdomains.aspx
        public static IList<AppDomain> GetAppDomains()
        {
            IList<AppDomain> _IList = new List<AppDomain>();
            IntPtr enumHandle = IntPtr.Zero;
            var host = new CorRuntimeHostClass();
            try
            {
                host.EnumDomains(out enumHandle);
                while (true)
                {
                    object domain;
                    host.NextDomain(enumHandle, out domain);
                    if (domain == null) break;
                    var appDomain = (AppDomain) domain;

                    _IList.Add(appDomain);
                }
                return _IList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            finally
            {
                host.CloseEnum(enumHandle);
                Marshal.ReleaseComObject(host);
            }
        }
    }
}