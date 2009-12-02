// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Text;
using O2.Debugger.Mdbg.Debugging.CorPublish.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorPublish.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorPublish.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorPublish
{
    public sealed class CorPublish
    {
        private readonly ICorPublish m_publish;

        public CorPublish()
        {
            m_publish = new CorpubPublishClass();
        }

        public IEnumerable EnumProcesses()
        {
            ICorPublishProcessEnum pIEnum;
            m_publish.EnumProcesses(COR_PUB_ENUMPROCESS.COR_PUB_MANAGEDONLY, out pIEnum);
            return (pIEnum == null) ? null : new CorPublishProcessEnumerator(pIEnum);
        }

        public CorPublishProcess GetProcess(int pid)
        {
            ICorPublishProcess proc;
            m_publish.GetProcess((uint) pid, out proc);
            return (proc == null) ? null : new CorPublishProcess(proc);
        }
    }

    public sealed class CorPublishProcess
    {
        private readonly ICorPublishProcess m_process;

        internal CorPublishProcess(ICorPublishProcess iprocess)
        {
            m_process = iprocess;
        }

        public string DisplayName
        {
            get
            {
                uint size;
                m_process.GetDisplayName(0, out size, null);
                var szName = new StringBuilder((int) size);
                m_process.GetDisplayName((uint) szName.Capacity, out size, szName);
                return szName.ToString();
            }
        }

        public int ProcessId
        {
            get
            {
                uint pid;
                m_process.GetProcessID(out pid);
                return (int) pid;
            }
        }

        public bool IsManaged
        {
            get
            {
                int bManaged;
                m_process.IsManaged(out bManaged);
                return (bManaged != 0);
            }
        }

        public IEnumerable EnumAppDomains()
        {
            ICorPublishAppDomainEnum pIEnum;
            m_process.EnumAppDomains(out pIEnum);
            return (pIEnum == null) ? null : new CorPublishAppDomainEnumerator(pIEnum);
        }
    }

    internal class CorPublishProcessEnumerator :
        IEnumerable, IEnumerator, ICloneable
    {
        private readonly ICorPublishProcessEnum m_enum;
        private CorPublishProcess m_proc;

        internal CorPublishProcessEnumerator(ICorPublishProcessEnum e)
        {
            m_enum = e;
        }

        //
        // ICloneable interface
        //

        #region ICloneable Members

        public Object Clone()
        {
            ICorPublishEnum clone = null;
            m_enum.Clone(out clone);
            return new CorPublishProcessEnumerator((ICorPublishProcessEnum) clone);
        }

        #endregion

        //
        // IEnumerable interface
        //

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #endregion

        //
        // IEnumerator interface
        //

        #region IEnumerator Members

        public bool MoveNext()
        {
            ICorPublishProcess a;
            uint c = 0;
            int r = m_enum.Next(1, out a, out c);
            if (r == 0 && c == 1) // S_OK && we got 1 new element
                m_proc = new CorPublishProcess(a);
            else
                m_proc = null;
            return m_proc != null;
        }

        public void Reset()
        {
            m_enum.Reset();
            m_proc = null;
        }

        public Object Current
        {
            get { return m_proc; }
        }

        #endregion
    }

    public sealed class CorPublishAppDomain
    {
        private readonly ICorPublishAppDomain m_appDomain;

        internal CorPublishAppDomain(ICorPublishAppDomain appDomain)
        {
            m_appDomain = appDomain;
        }

        public int Id
        {
            get
            {
                uint id;
                m_appDomain.GetID(out id);
                return (int) id;
            }
        }

        public string Name
        {
            get
            {
                uint size;
                m_appDomain.GetName(0, out size, null);
                var szName = new StringBuilder((int) size);
                m_appDomain.GetName((uint) szName.Capacity, out size, szName);
                return szName.ToString();
            }
        }
    }


    internal class CorPublishAppDomainEnumerator :
        IEnumerable, IEnumerator, ICloneable
    {
        private readonly ICorPublishAppDomainEnum m_enum;
        private CorPublishAppDomain m_appDomain;

        internal CorPublishAppDomainEnumerator(ICorPublishAppDomainEnum appDomainEnumerator)
        {
            m_enum = appDomainEnumerator;
        }

        //
        // ICloneable interface
        //

        #region ICloneable Members

        public Object Clone()
        {
            ICorPublishEnum clone = null;
            m_enum.Clone(out clone);
            return new CorPublishAppDomainEnumerator((ICorPublishAppDomainEnum) clone);
        }

        #endregion

        //
        // IEnumerable interface
        //

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #endregion

        //
        // IEnumerator interface
        //

        #region IEnumerator Members

        public bool MoveNext()
        {
            ICorPublishAppDomain a;
            uint c = 0;
            int r = m_enum.Next(1, out a, out c);
            if (r == 0 && c == 1) // S_OK && we got 1 new element
                m_appDomain = new CorPublishAppDomain(a);
            else
                m_appDomain = null;
            return m_appDomain != null;
        }

        public void Reset()
        {
            m_enum.Reset();
            m_appDomain = null;
        }

        public Object Current
        {
            get { return m_appDomain; }
        }

        #endregion
    }
}
