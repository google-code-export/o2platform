/*
    Copyright © 2004, The KPD-Team
    All rights reserved.
    http://www.mentalis.org/

  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions
  are met:

    - Redistributions of source code must retain the above copyright
       notice, this list of conditions and the following disclaimer. 

    - Neither the name of the KPD-Team, nor the names of its contributors
       may be used to endorse or promote products derived from this
       software without specific prior written permission. 

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
  THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
  (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
  SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
  HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
  STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
  OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// This application is based on Michael Howard's original DropMyrights
// utility, but it incorporates some new featuers
//   - doesn't briefly show a console window
//   - supports passing command-line arguments to the called application
//   - command line parameters are in a different order [this change was
//     required to add support for passing command-line parameters]

public class DropMyRights 	{
	private static void PrintUsage() {
		Console.WriteLine(
			"DropMyRights.NET v" + Application.ProductVersion + "\r\n" +
			"Usage is:\r\n" +
			"  DropMyRights.exe [N|C|U] {path} {command-line parameters}\r\n\r\n" +
			"Where:\r\n" +
			"  N = run as normal user (default).\r\n" +
			"  C = run as constrained user.\r\n" +
			"  U = run as an untrusted user." +
			"  path is the full path to an executable to run.\r\n" +
			"  command-line parameters are the optional parameters of the executable.\r\n"
			);
	}
	[STAThread]
	static int Main(string[] args) {
		OperatingSystem os = Environment.OSVersion;
		if (os.Platform != PlatformID.Win32NT || os.Version.Major < 5 || (os.Version.Major == 5 && os.Version.Minor == 0)) {
			Console.WriteLine("This program requires Windows XP, Windows Server 2003 or higher.");
			return -1;
		}
		
		if (args.Length == 0) {
			PrintUsage();
			return 0;
		}

		// start the executable with less privileges
		int saferLevel, status = 0;
		switch(args[0].ToLower()) {
			case "c":
				saferLevel = SAFER_LEVELID_CONSTRAINED; 
				break;
			case "u":
				saferLevel = SAFER_LEVELID_UNTRUSTED;
				break;
			default:
				saferLevel = SAFER_LEVELID_NORMALUSER;
				break;
		}
		if (args[1].Length >= MAX_PATH)
			return ERROR_INVALID_PARAMETER;
		string commandline = "";
		for(int i = 2; i < args.Length; i++) {
			commandline += args[i] + " ";
		}

		IntPtr authzLevel = IntPtr.Zero;
		if (SaferCreateLevel(SAFER_SCOPEID_USER, saferLevel, 0, ref authzLevel, IntPtr.Zero) != 0) {
			IntPtr token = IntPtr.Zero;
			if (SaferComputeTokenFromLevel(authzLevel, IntPtr.Zero, out token, 0, IntPtr.Zero) != 0) {
				STARTUPINFO si = new STARTUPINFO();
				si.cb = Marshal.SizeOf(typeof(STARTUPINFO));
	    
				PROCESS_INFORMATION pi;
				if (CreateProcessAsUser(token, args[1], commandline, IntPtr.Zero, IntPtr.Zero, 0, CREATE_NEW_CONSOLE, IntPtr.Zero, IntPtr.Zero, ref si, out pi) != 0) {
					CloseHandle(pi.hProcess);
					CloseHandle(pi.hThread);
				} else {
					status = Marshal.GetLastWin32Error();
				} 
			} else {
				status = Marshal.GetLastWin32Error();
			}

			SaferCloseLevel(authzLevel);

		} else {
			status = Marshal.GetLastWin32Error();
		}

		return status;
	}
	[DllImport("advapi32.dll", SetLastError=true)]
	private static extern int SaferCreateLevel(int dwScopeId, int dwLevelId, int OpenFlags, ref IntPtr pLevelHandle, IntPtr lpReserved);
	[DllImport("advapi32.dll", SetLastError=true)]
	private static extern int SaferComputeTokenFromLevel(IntPtr LevelHandle, IntPtr InAccessToken, out IntPtr OutAccessToken, int dwFlags, IntPtr lpReserved);
	[DllImport("advapi32.dll", SetLastError=true)]
	private static extern int SaferCloseLevel(IntPtr hLevelHandle);
	[DllImport("advapi32.dll", SetLastError=true, CharSet=CharSet.Auto)]
	private static extern int CreateProcessAsUser(IntPtr hToken, string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, int bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, IntPtr lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);
	[DllImport("kernel32.dll", SetLastError=true)]
	private static extern int CloseHandle(IntPtr hObject);

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
	private struct STARTUPINFO {
		public int cb;
		public string lpReserved;
		public string lpDesktop;
		public string lpTitle;
		public int dwX;
		public int dwY;
		public int dwXSize;
		public int dwYSize;
		public int dwXCountChars;
		public int dwYCountChars;
		public int dwFillAttribute;
		public int dwFlags;
		public int wShowWindow;
		public int cbReserved2;
		public int pReserved2;
		public int hStdInput;
		public int hStdOutput;
		public int hStdError;
	}
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
	private struct PROCESS_INFORMATION {
		public IntPtr hProcess;
		public IntPtr hThread;
		public int dwProcessId;
		public int dwThreadId;
	}

	private const int SAFER_LEVELID_NORMALUSER = 0x20000;
	private const int SAFER_LEVELID_CONSTRAINED = 0x10000;
	private const int SAFER_LEVELID_UNTRUSTED = 0x01000;
	private const int MAX_PATH = 260;
	private const int ERROR_INVALID_PARAMETER = 87;
	private const int SAFER_SCOPEID_USER = 2;
	private const int CREATE_NEW_CONSOLE = 0x00000010;
}