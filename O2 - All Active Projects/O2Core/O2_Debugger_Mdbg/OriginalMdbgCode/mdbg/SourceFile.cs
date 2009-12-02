// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------
using System;
using System.Collections;
using System.IO;
using System.Text;
using O2.Debugger.Mdbg.Tools.Mdbg;
using O2.Debugger.Mdbg.Tools.Mdbg;
using O2.Debugger.Mdbg.Tools.Mdbg;

namespace O2.Debugger.Mdbg.mdbg
{
    internal class MDbgSourceFileMgr : IMDbgSourceFileMgr
    {
        private readonly Hashtable m_sourceCache = new Hashtable();

        #region IMDbgSourceFileMgr Members

        public IMDbgSourceFile GetSourceFile(string path)
        {
            String s = String.Intern(path);
            var source = (MDbgSourceFile) m_sourceCache[s];

            if (source == null)
            {
                source = new MDbgSourceFile(s);
                m_sourceCache.Add(s, source);
            }
            return source;
        }

        public void ClearDocumentCache()
        {
            m_sourceCache.Clear();
        }

        #endregion
    }

    internal class MDbgSourceFile : IMDbgSourceFile
    {
        private readonly string m_path;
        private ArrayList m_lines;

        public MDbgSourceFile(string path)
        {
            m_path = path;
            try
            {
                Initialize();
            }
            catch (FileNotFoundException)
            {
                throw new MDbgShellException("Could not find source: " + m_path);
            }
        }

        #region IMDbgSourceFile Members

        public string Path
        {
            get { return m_path; }
        }

        public string this[int lineNumber]
        {
            get
            {
                if (m_lines == null)
                {
                    Initialize();
                }
                if ((lineNumber < 1) || (lineNumber > m_lines.Count))
                    throw new MDbgShellException(string.Format("Could not retrieve line {0} from file {1}.",
                                                               lineNumber, Path));

                return (string) m_lines[lineNumber - 1];
            }
        }

        public int Count
        {
            get
            {
                if (m_lines == null)
                {
                    Initialize();
                }
                return m_lines.Count;
            }
        }

        #endregion

        protected void Initialize()
        {
            StreamReader sr = null;
            try
            {
                // Encoding.Default doesn�t port between machines, but it's used just in case the source isn�t Unicode
                sr = new StreamReader(m_path, Encoding.Default, true);
                m_lines = new ArrayList();

                string s = sr.ReadLine();
                while (s != null)
                {
                    m_lines.Add(s);
                    s = sr.ReadLine();
                }
            }
            finally
            {
                if (sr != null)
                    sr.Close(); // free resources in advance
            }
        }
    }
}
