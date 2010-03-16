//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using CorThread=O2.Debugger.Mdbg.Debugging.CorDebug.CorThread;
using CorValue=O2.Debugger.Mdbg.Debugging.CorDebug.CorValue;
using HResult=O2.Debugger.Mdbg.Debugging.CorDebug.HResult;

namespace O2.Debugger.Mdbg.Debugging.MdbgEngine
{
    /// <summary>
    /// MDbg Thread class.
    /// </summary>
    public sealed class MDbgThread : MarshalByRefObject, IComparable
    {
        private readonly CorDebug.CorThread m_corThread;
        private readonly int m_threadNumber;

        private int m_currentFrameIndex; // 0-based index of the current frame counted from the leafmost frame
        // have value -1, when there is no current frame.

        // Stackwalker builds on ICorDebug stackwalking APIs to produce a MDbgFrame collection.
        // This abstracts which APIs to use (V2 or V3, etc) and which policy to use (eg, what to do 
        // about unmanaged frames + native stackwalking, funclets, etc)
        // Create a new instance each time we want to walk the stack. A given stackWalker instance
        // caches the frames of the current stack.
        internal IMDbgStackWalker m_stackWalker;
        internal MDbgThreadCollection m_threadMgr;

        internal MDbgThread(MDbgThreadCollection threadCollection, CorDebug.CorThread thread, int threadNumber)
        {
            m_corThread = thread;
            m_threadNumber = threadNumber;
            m_threadMgr = threadCollection;
        }

        /// <summary>
        /// Gets or Sets if the Thread is Suspended.
        /// </summary>
        /// <value>NotImplementedException.</value>
        public bool Suspended
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the Thread Number.
        /// </summary>
        /// <value>The Thread Number.</value>
        public int Number
        {
            get { return m_threadNumber; }
        }

        /// <summary>
        /// Gets the Thread Id.
        /// </summary>
        /// <value>Gets the Thread Id.</value>
        public int Id
        {
            get { return m_corThread.Id; }
        }

        /// <summary>
        /// Gets the CorThread.
        /// </summary>
        /// <value>The CorThread.</value>
        public CorDebug.CorThread CorThread
        {
            get { return m_corThread; }
        }

        /// <summary>
        /// Returns current exception on the thread or MDbgValue representing N/A if there is no exception on the thread.
        /// </summary>
        /// <value>The current Exception.</value>
        public MDbgValue CurrentException
        {
            get
            {
                CorDebug.CorValue cv;
                try
                {
                    cv = CorThread.CurrentException;
                }
                catch (COMException e)
                {
                    if (e.ErrorCode == (int) CorDebug.HResult.E_FAIL)
                        cv = null;
                    else
                        throw;
                }
                return new MDbgValue(m_threadMgr.m_process, cv);
            }
        }

        /// <summary>
        /// The Bottom Frame in the stack for this Thread.
        /// </summary>
        /// <value>The Bottom Frame.</value>
        public MDbgFrame BottomFrame
        {
            get
            {
                EnsureCurrentStackWalker();
                return m_stackWalker.GetFrame(0);
            }
        }

        /// <summary>
        /// Returns all the frames for the thread.
        /// </summary>
        /// <value>The frames.</value>
        public IEnumerable Frames
        {
            get { return new MDbgFrameEnumerable(BottomFrame); }
        }

        /// <summary>
        /// Gets or Sets the Current Logical Frame
        /// </summary>
        /// <value>The Current Frame.</value>
        public MDbgFrame CurrentFrame
        {
            get
            {
                EnsureCurrentStackWalker();
                if (m_currentFrameIndex == -1)
                {
                    throw new MDbgNoCurrentFrameException();
                }

                MDbgFrame frame = m_stackWalker.GetFrame(m_currentFrameIndex);
                if (frame == null)
                {
                    throw new MDbgNoCurrentFrameException();
                }

                Debug.Assert(!frame.IsInfoOnly);
                return frame;
            }
            set
            {
                EnsureCurrentStackWalker();

                if (value == null ||
                    value.Thread != this)
                    throw new ArgumentException();

                if (value.IsInfoOnly)
                    throw new InvalidOperationException("virtual frames cannot be set as current frames");

                int frameIndex = m_stackWalker.GetFrameIndex(value);
                if (frameIndex < 0)
                    throw new InvalidOperationException("Cannot set a foreign frame to the thread as a current frame");

                m_currentFrameIndex = frameIndex;
            }
        }

        /// <summary>
        /// Gets if the Thread has a Current Logical Frame.
        /// </summary>
        /// <value>true if it has, else false.</value>
        public bool HaveCurrentFrame
        {
            get
            {
                EnsureCurrentStackWalker();
                return m_currentFrameIndex != -1;
            }
        }

        /// <summary>
        /// Gets the current Source Position.
        /// </summary>
        /// <value>The Source Ponition.</value>
        public MDbgSourcePosition CurrentSourcePosition
        {
            get { return CurrentFrame.SourcePosition; }
        }

        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            return Number - (obj as MDbgThread).Number;
        }

        #endregion

        /// <summary>
        /// Moves the Current Frame up or down.
        /// </summary>
        /// <param name="down">Moves frame down if true, else up.</param>
        public void MoveCurrentFrame(bool down)
        {
            MDbgFrame f = CurrentFrame;
            Debug.Assert(!f.IsInfoOnly);

            bool frameCanBeMoved;
            int idx = m_currentFrameIndex;
            if (down)
            {
                do
                {
                    --idx;
                } while (idx >= 0 && m_stackWalker.GetFrame(idx).IsInfoOnly);
                frameCanBeMoved = (idx >= 0);
            }
            else
            {
                do
                {
                    ++idx;
                    f = m_stackWalker.GetFrame(idx);
                } while (f != null && f.IsInfoOnly);
                frameCanBeMoved = f != null;
            }
            if (!frameCanBeMoved)
            {
                throw new MDbgException("Operation hit " + (down ? "bottom" : "top") + " of the stack.");
            }

            m_currentFrameIndex = idx;
        }


        /// <summary>
        /// This will return an MDbgFrame for the corresponding CorFrame.
        /// Note (the frame needs to correspond to this thread!!!!)
        /// </summary>
        /// <param name="f">The CorFrame to look up.</param>
        /// <returns>The coresponding MDbgFrame.</returns>
        public MDbgFrame LookupFrame(CorDebug.CorFrame f)
        {
            Debug.Assert(f != null);
            return new MDbgILFrame(this, f);
        }

        // The function verifies that the stackwalker for this thread is "current". The stack-walker will become
        // invalid when a debugger performs an operation that invalidats active stackwalkers. Examples of such
        // operations are:
        //  - calling Continue(), calling SetIP() or calling RefreshStack() methods.
        //
        //  If the current stack-walker is not current, a new stack-walker is created for the thread. The function
        //  also sets the current frame if the stack walker is refreshed. 
        // 
        private void EnsureCurrentStackWalker()
        {
            if (m_stackWalker != null && m_stackWalker.IsUsable)
            {
                return;
            }

            m_stackWalker = m_threadMgr.FrameFactory.CreateStackWalker(this);

            // initialize the frame index to be the invalid index
            m_currentFrameIndex = -1;

            // set m_currentFrame to first non-virtual frame
            MDbgFrame f = m_stackWalker.GetFrame(0);
            if (f == null)
            {
                return;
            }

            // we have at least one frame, so set the frame index to 0
            m_currentFrameIndex = 0;
            while (f != null
                   && f.IsInfoOnly)
            {
                f = f.NextUp;
                if (f != null)
                {
                    ++m_currentFrameIndex;
                }
            }
        }

        #region Nested type: MDbgFrameEnumerable

        private class MDbgFrameEnumerable : IEnumerable
        {
            private readonly MDbgFrame m_bottomFrame;

            public MDbgFrameEnumerable(MDbgFrame bottomFrame)
            {
                m_bottomFrame = bottomFrame;
            }

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return new MDbgFrameEnumerator(m_bottomFrame);
            }

            #endregion
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Local variables
        //
        //////////////////////////////////////////////////////////////////////////////////
    }


    internal class MDbgFrameEnumerator : IEnumerator
    {
        private readonly MDbgFrame m_bottomFrame;
        private MDbgFrame m_currentFrame;

        internal MDbgFrameEnumerator(MDbgFrame bottomFrame)
        {
            m_bottomFrame = bottomFrame;
        }

        #region IEnumerator Members

        void IEnumerator.Reset()
        {
            m_currentFrame = null;
        }

        bool IEnumerator.MoveNext()
        {
            if (m_currentFrame == null)
            {
                if (m_bottomFrame == null)
                    return false;

                m_currentFrame = m_bottomFrame;
            }
            else
            {
                MDbgFrame f = m_currentFrame.NextUp;
                if (f == null)
                    return false;
                m_currentFrame = f;
            }
            return true;
        }

        Object IEnumerator.Current
        {
            get
            {
                if (m_currentFrame == null)
                    throw new InvalidOperationException();
                return m_currentFrame;
            }
        }

        #endregion
    }

    /// <summary>
    /// MDbg Thread Collection class.
    /// </summary>
    public sealed class MDbgThreadCollection : MarshalByRefObject, IEnumerable
    {
        private readonly Hashtable m_items = new Hashtable();
        private MDbgThread m_active;
        private IMDbgFrameFactory m_frameFactory;
        private int m_freeThreadNumber;
        internal MDbgProcess m_process;

        internal MDbgThreadCollection(MDbgProcess process)
        {
            m_process = process;
            m_freeThreadNumber = 0;
        }

        /// <summary>
        /// How many threads are in the collection.
        /// </summary>
        /// <value>How many threads.</value>
        public int Count
        {
            get { return m_items.Count; }
        }

        /// <summary>
        /// Allows for indexing of the collection by thread number.
        /// </summary>
        /// <param name="threadNumber">Which thread number to access.</param>
        /// <returns>The MDbgThread at that number.</returns>
        public MDbgThread this[int threadNumber]
        {
            get { return GetThreadFromThreadNumber(threadNumber); }
        }

        /// <summary>
        /// Gets on sets the Active Thread.
        /// </summary>
        /// <value>The Active Thread.</value>
        public MDbgThread Active
        {
            get
            {
                if (m_active == null)
                    throw new MDbgNoActiveInstanceException("No active thread");
                return m_active;
            }
            set
            {
                Debug.Assert(value != null &&
                             m_items.Contains(value.CorThread.Id));
                if (value == null || !m_items.Contains(value.CorThread.Id))
                    throw new ArgumentException();
                m_active = value;
            }
        }

        /// <summary>
        /// Gets if the collection has an Active Thread.
        /// </summary>
        /// <value>true if it has an Active Thread, else false.</value>
        public bool HaveActive
        {
            get { return m_active != null; }
        }

        /// <summary>
        /// Gets or sets the current FrameFactory object for MDbgEngine stackwalking implementation.
        /// </summary>
        public IMDbgFrameFactory FrameFactory
        {
            get
            {
                if (m_frameFactory == null)
                {
                    // we need to get a default frame factory
                    if (m_process.m_engine.m_defaultStackWalkingFrameFactoryProvider != null)
                    {
                        m_frameFactory = m_process.m_engine.m_defaultStackWalkingFrameFactoryProvider();
                    }

                    // default fallback mechanism if we do not have frameFactory provider or
                    // the provider returns null
                    if (m_frameFactory == null)
                    {
                        m_frameFactory = new MDbgV2FrameFactory();
                    }
                }
                return m_frameFactory;
            }
            set
            {
                if (m_frameFactory != null)
                {
                    m_frameFactory.InvalidateStackWalkers();
                }
                m_frameFactory = value;
            }
        }

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            var ret = new MDbgThread[m_items.Count];
            m_items.Values.CopyTo(ret, 0);
            Array.Sort(ret);
            return ret.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Lookup a MDbgThread using a CorThread.
        /// </summary>
        /// <param name="thread">The CorThread to use.</param>
        /// <returns>The rusulting MDbgThread with the same ID.</returns>
        public MDbgThread Lookup(CorDebug.CorThread thread)
        {
            return GetThreadFromThreadId(thread.Id);
        }

        /// <summary>
        /// This function needs to be called when we do "setip" so that callstack will get refreshed.
        /// </summary>
        public void RefreshStack()
        {
            lock (m_process)
            {
                FrameFactory.InvalidateStackWalkers();
            }
        }

        internal void Register(CorDebug.CorThread t)
        {
            // Prevent double-registration. This may happen if we pick up a CorThread 
            // via enumeration before the CreateThread callback.
            if (!m_items.Contains(t.Id))
            {
                m_items.Add(t.Id, new MDbgThread(this, t, m_freeThreadNumber++));
            }
        }

        internal void UnRegister(CorDebug.CorThread t)
        {
            m_items.Remove(t.Id);
            if (m_active != null &&
                m_active.CorThread.Id == t.Id)
            {
                m_active = null;
            }
        }

        internal void Clear()
        {
            m_items.Clear();
            m_active = null;
            m_freeThreadNumber = 0;
        }


        internal void SetActiveThread(CorDebug.CorThread thread)
        {
            if (thread == null)
            {
                m_active = null;
            }
            else
            {
                m_active = GetThreadFromThreadId(thread.Id);
            }
            lock (m_process)
            {
                m_frameFactory.InvalidateStackWalkers(); // @TODO can this line be removed???
            }
        }

        /// <summary>
        /// Gets the MDbgThread with the given ThreadId.
        /// </summary>
        /// <param name="threadId">The ThreadId to look up.</param>
        /// <returns>The MDbgThread.</returns>
        public MDbgThread GetThreadFromThreadId(int threadId)
        {
            // This sometimes fails because we're looking for a thread don't recognize.
            // Need to offer lazy create semantics here.
            var te = (MDbgThread) m_items[threadId];
            if (te == null)
            {
                CorDebug.CorThread t = m_process.CorProcess.GetThread(threadId);
                if (t != null)
                {
                    Register(t);
                    te = (MDbgThread) m_items[threadId];
                }
            }
            return te;
        }

        private MDbgThread GetThreadFromThreadNumber(int threadNumber)
        {
            foreach (MDbgThread te in m_items.Values)
            {
                if (te.Number == threadNumber)
                {
                    return te;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// MDbg Frame class
    /// MDbgFrame is defined as abstract so that we can have
    /// different kind of frames.  There could be e.g. ManagedFrames
    /// (IL code) and NativeFrames (native) corresponding to native
    /// code.
    /// </summary>
    public abstract class MDbgFrame
    {
        /// <summary>
        /// Returns cor-wrapper for frame or throws an exception if N/A.
        /// </summary>
        /// <value>The cor-wrapper for the frame.</value>
        public abstract CorFrame CorFrame { get; }

        /// <summary>
        /// Returns thread owning this frame.
        /// </summary>
        /// <value>The Thread.</value>
        public abstract MDbgThread Thread { get; }

        /// <summary>
        /// Return true if frame has a managed IL code behind it.
        /// </summary>
        /// <value>true if frame is managed, else false.</value>
        public abstract bool IsManaged { get; }

        /// <summary>
        /// Returns true if the frame is informational (i.e. informative/internal).
        /// </summary>
        /// <value>true if frame is informational, else false.</value>
        public abstract bool IsInfoOnly { get; }

        /// <summary>
        /// Returns source position for the frame or null if not available.
        /// </summary>
        /// <value>The Source Position.</value>
        public abstract MDbgSourcePosition SourcePosition { get; }

        /// <summary>
        /// Returns function whose code is executed in this frame.
        /// </summary>
        /// <value>The Function.</value>
        public abstract MDbgFunction Function { get; }

        /// <summary>
        /// Returns next frame up the stack or null if topmost frame.
        /// </summary>
        /// <value>The next frame up.</value>
        public abstract MDbgFrame NextUp { get; }

        /// <summary>
        /// Returns next frame down the stack or null if bottom frame.
        /// </summary>
        /// <value>The next frame up.</value>
        public abstract MDbgFrame NextDown { get; }

        /// <summary>
        /// Returns a string that represents current frame
        /// Currently supported formats:
        /// null or empty string: returns short frame format (just frame name)
        /// "v"                 : returns long frame format (including module &amp; arguments)
        /// </summary>
        /// <param name="format">Which format to use.</param>
        /// <returns>The formatted string that represtents the current frame.</returns>
        public abstract string ToString(string format);

        /// <summary>
        /// Equality testing.  Allows for things like "if(thing1 == thing2)" to work properly.
        /// </summary>
        /// <param name="operand">First Operand.</param>
        /// <param name="operand2">Second Operand.</param>
        /// <returns>true if equal, else false.</returns>
        public static bool operator ==(MDbgFrame operand, MDbgFrame operand2)
        {
            if (ReferenceEquals(operand, operand2))
                return true;
            return operand.Equals(operand2);
        }

        /// <summary>
        /// Inequality testing.  Allows for things like "if(thing1 != thing2)" to work properly.
        /// </summary>
        /// <param name="operand">First Operand.</param>
        /// <param name="operand2">Second Operand.</param>
        /// <returns>true if not equal, else false.</returns>
        public static bool operator !=(MDbgFrame operand, MDbgFrame operand2)
        {
            return !(operand == operand2);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the other object.
        /// </summary>
        /// <param name="value">The object to compare with the current frame.</param>
        public abstract override bool Equals(Object value);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public abstract override int GetHashCode();
    }

    /// <summary>
    /// The MDbgILFrame class is a frame with Intermediate Language code behind it.
    /// </summary>
    public abstract class MDbgFrameBase : MDbgFrame
    {
        private readonly MDbgThread m_thread;

        /// <summary>
        /// Creates an instance of class MDbgFramebase.
        /// </summary>
        /// <param name="thread"></param>
        protected MDbgFrameBase(MDbgThread thread)
        {
            Debug.Assert(thread != null);
            m_thread = thread;
        }

        /// <summary>
        /// Returns thread owning this frame.
        /// </summary>
        /// <value>The Thread.</value>
        public override MDbgThread Thread
        {
            get { return m_thread; }
        }

        /// <summary>
        /// Returns next frame up the stack or null if topmost frame.
        /// </summary>
        /// <value>The next frame up.</value>
        public override MDbgFrame NextUp
        {
            get { return m_thread.m_stackWalker.GetFrame(m_thread.m_stackWalker.GetFrameIndex(this) + 1); }
        }

        /// <summary>
        /// Returns next frame down the stack or null if bottom frame.
        /// </summary>
        /// <value>The next frame up.</value>
        public override MDbgFrame NextDown
        {
            get { return m_thread.m_stackWalker.GetFrame(m_thread.m_stackWalker.GetFrameIndex(this) - 1); }
        }

        /// <summary>
        /// Returns a string that represents current frame using default formatting.
        /// </summary>
        /// <returns>The default formatted string that represtents the current frame.</returns>
        public override string ToString()
        {
            return ToString(null);
        }

        // Type parameter information.
    }

    /// <summary>
    /// The MDbgILFrame class is a frame with Intermediate Language code behind it.
    /// </summary>
    public sealed class MDbgILFrame : MDbgFrameBase
    {
        // @TODO - comment this
        private readonly CorFrame m_frame;

        internal MDbgILFrame(MDbgThread thread, CorFrame frame)
            : base(thread)
        {
            Debug.Assert(frame != null);
            m_frame = frame;
        }

        /// <summary>
        /// Returns cor-wrapper for frame or throws an exception if N/A.
        /// </summary>
        /// <value>The cor-wrapper for the frame.</value>
        public override CorFrame CorFrame
        {
            get { return m_frame; }
        }

        /// <summary>
        /// Return true if frame has a managed IL code behind it.
        /// </summary>
        /// <value>true if frame is managed, else false.</value>
        public override bool IsManaged
        {
            get { return (m_frame.FrameType == CorFrameType.ILFrame); }
        }

        /// <summary>
        /// Returns true if the frame is informational (i.e. informative/internal).
        /// MDbg does not allow users to set an informational frame as the current frame.
        /// </summary>
        /// <value>true if frame is informational, else false.</value>
        public override bool IsInfoOnly
        {
            get
            {
                // Currently we are marking NativeFrames as informational as well.
                // Since they don't have corresponding ILFrames, users can't do much with them anyways.
                // However, once we implement debugging support for dynamic languages, 
                // we should definitely consider unmarking them.
                return ((m_frame.FrameType == CorFrameType.InternalFrame) ||
                        (m_frame.FrameType == CorFrameType.NativeFrame));
            }
        }

        /// <summary>
        /// Returns source position for the frame or null if not available.
        /// </summary>
        /// <value>The Source Position.</value>
        public override MDbgSourcePosition SourcePosition
        {
            get
            {
                MDbgFunction f = null;

                // we can return source position only for ILFrames.
                if (IsManaged)
                    f = Function;

                return (f == null) ? null : f.GetSourcePositionFromFrame(m_frame);
            }
        }

        /// <summary>
        /// Returns function whose code is executed in this frame.
        /// </summary>
        /// <value>The Function.</value>
        public override MDbgFunction Function
        {
            get { return Thread.m_threadMgr.m_process.Modules.LookupFunction(m_frame.Function); }
        }

        /// <summary>
        /// Get a CorType that function for this frame is declared in.
        /// </summary>
        // 
        // If the frame is Class<int>::Func<string>,
        // get a CorType representing 'Class<int>'. 
        // The CorClass would only give us 'Class<T>', and the
        // Function only gives Func<U>
        // Returns 'null' for global methods.
        //
        public CorType FunctionType
        {
            get
            {
                MDbgFunction f = Function;
                CorClass c = f.CorFunction.Class;

                Debug.Assert(TokenUtils.TypeFromToken(c.Token) == CorTokenType.mdtTypeDef);

                // Check if we're the "global" class, in which case this is a global method, so return null.

                //  global type should return NULL token,
                // currently they are returning either 1 or 0 RID.
                if (TokenUtils.RidFromToken(c.Token) == 1
                    || TokenUtils.RidFromToken(c.Token) == 0)
                    return null;


                // ICorDebug API lets us always pass ET_Class
                CorElementType et = CorElementType.ELEMENT_TYPE_CLASS;

                // Get type parameters.
                IEnumerable typars = m_frame.TypeParameters;
                //IEnumerator tyenum = typars.GetEnumerator();

                MDbgModule module = Function.Module;
                int cNumTyParamsOnClass = module.Importer.CountGenericParams(c.Token);


                var args = new CorType[cNumTyParamsOnClass];
                int i = 0;
                foreach (CorType arg in typars)
                {
                    if (i == cNumTyParamsOnClass)
                    {
                        break;
                    }

                    args[i] = arg;
                    i++;
                }

                CorType t = c.GetParameterizedType(et, args);
                return t;
            }
        }

        /// <summary> Return an enumerator that enumerates through just the generic args on this frame's method </summary>
        /// <remarks>Enumerating generic args on the frame gives a single collection including both
        /// class and function args. This is a helper to skip past the class and use just the funciton args.</remarks>
        public IEnumerable FunctionTypeParameters
        {
            get
            {
                MDbgModule module = Function.Module;
                CorClass c = Function.CorFunction.Class;
                int cNumTyParamsOnClass = module.Importer.CountGenericParams(c.Token);
                return m_frame.GetTypeParamEnumWithSkip(cNumTyParamsOnClass);
            }
        }

        /// <summary>
        /// Determines if the wrapped object is equal to another.
        /// </summary>
        /// <param name="value">The object to compare to.</param>
        /// <returns>true if equal, else false.</returns>
        public override bool Equals(Object value)
        {
            if (!(value is MDbgILFrame))
                return false;
            return ((value as MDbgILFrame).m_frame == m_frame);
        }

        /// <summary>
        /// Required to implement MarshalByRefObject.
        /// </summary>
        /// <returns>Hash Code.</returns>
        public override int GetHashCode()
        {
            return m_frame.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents current frame
        /// Currently supported formats:
        /// null or empty string: returns short frame format (just frame name)
        /// "v"                 : returns long frame format (including module &amp; arguments)
        /// </summary>
        /// <param name="format">Which format to use.</param>
        /// <returns>The formatted string that represtents the current frame.</returns>
        public override string ToString(string format)
        {
            string fn;

            switch (m_frame.FrameType)
            {
                case CorFrameType.ILFrame:
                    MDbgSourcePosition sl = SourcePosition;
                    string sp;

                    if (sl != null)
                    {
                        string filePath = sl.Path;
                        if (!Thread.m_threadMgr.m_process.m_engine.Options.ShowFullPaths)
                            filePath = Path.GetFileName(sl.Path);
                        sp = " (" + filePath + ":" + sl.Line.ToString(CultureInfo.CurrentUICulture) + ")";
                    }
                    else
                        sp = " (source line information unavailable)";

                    var sbFuncName = new StringBuilder();

                    MDbgModule module = Function.Module;
                    MDbgProcess proc = Thread.m_threadMgr.m_process;


                    // Get class name w/ generic args.
                    CorType tClass = FunctionType;
                    if (tClass != null)
                        InternalUtil.PrintCorType(sbFuncName, proc, tClass);

                    sbFuncName.Append('.');


                    // Get method name w/ generic args.
                    MethodInfo mi = Function.MethodInfo;
                    sbFuncName.Append(mi.Name);
                    InternalUtil.AddGenericArgs(sbFuncName, proc, FunctionTypeParameters);


                    string stFuncName = sbFuncName.ToString();

                    if (format == "v")
                    {
                        CorModule m = module.CorModule;
                        // verbose frame output
                        // in verbose output we'll print module name + arguments to the functions
                        var sb = new StringBuilder();
                        bool bFirst = true;
                        foreach (MDbgValue v in Function.GetArguments(this))
                        {
                            if (sb.Length != 0)
                                sb.Append(", ");
                            // skip this references
                            if (! (bFirst && v.Name == "this"))
                                sb.Append(v.Name).Append("=").Append(v.GetStringValue(0));
                            bFirst = false;
                        }

                        if (m.IsDynamic || m.IsInMemory)
                        {
                            fn = m.Name;
                        }
                        else
                        {
                            fn = Path.GetFileName(m.Name);
                        }

                        MDbgAppDomain ad = Thread.m_threadMgr.m_process.AppDomains.Lookup(m.Assembly.AppDomain);
                        fn += "#" + ad.Number
                              + "!" + stFuncName + "(" + sb + ") " + sp;
                    }
                    else
                    {
                        fn = stFuncName + sp;
                    }
                    break;
                case CorFrameType.NativeFrame:
                    fn = "[IL Method without Metadata]";
                    break;
                case CorFrameType.InternalFrame:
                    switch (m_frame.InternalFrameType)
                    {
                        case CorDebugInternalFrameType.STUBFRAME_NONE:
                            fn = "None";
                            break;
                        case CorDebugInternalFrameType.STUBFRAME_M2U:
                            fn = "M-->U";
                            break;
                        case CorDebugInternalFrameType.STUBFRAME_U2M:
                            fn = "U-->M";
                            break;
                        case CorDebugInternalFrameType.STUBFRAME_APPDOMAIN_TRANSITION:
                            fn = "AD Switch";
                            break;
                        case CorDebugInternalFrameType.STUBFRAME_LIGHTWEIGHT_FUNCTION:
                            fn = "LightWeight";
                            break;
                        case CorDebugInternalFrameType.STUBFRAME_FUNC_EVAL:
                            fn = "FuncEval";
                            break;
                        case CorDebugInternalFrameType.STUBFRAME_INTERNALCALL:
                            fn = "InternalCall";
                            break;
                        default:
                            fn = "UNKNOWN";
                            break;
                    }
                    fn = "[Internal Frame, '" + fn + "']";
                    break;
                default:
                    fn = "UNKNOWN Frame Type";
                    break;
            }
            return fn;
        }

        // Type parameter information.
    }
}
