#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//

[assembly: AssemblyTitle("TweetSharp")]
[assembly: AssemblyDescription("Short, sweet, social")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dimebrain")]
[assembly: AssemblyProduct("TweetSharp")]
[assembly: AssemblyCopyright("Copyright © Dimebrain 2010")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AllowPartiallyTrustedCallers]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.0.0.0")]

//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//       When specifying the KeyFile, the location of the KeyFile should be
//       relative to the project output directory which is
//       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//


[assembly: AssemblyFileVersionAttribute("1.0.0.0")]
[assembly:
    InternalsVisibleTo(
        "TweetSharp.Futures, PublicKey=00240000048000009400000006020000002400005253413100040000010001008966c531777613a65ba705c822b0d319eaa18b0dbe4701a0f154f863d3a39a67a7864242bd6a0b1ae001665e13e17d3479e8b81e4bdb23ac15672b18a4da62c547f9a8125daa3c94d443755c88bcc55df1ac78a94c3df8af48a3fd2101fa4608d18ee05fcd9689e8bd4bd28d70dd52d32bc044b50b2b51ab750c88be2b47f9b5"
        )]
[assembly:
    InternalsVisibleTo(
        "TweetSharp.Extras, PublicKey=00240000048000009400000006020000002400005253413100040000010001008966c531777613a65ba705c822b0d319eaa18b0dbe4701a0f154f863d3a39a67a7864242bd6a0b1ae001665e13e17d3479e8b81e4bdb23ac15672b18a4da62c547f9a8125daa3c94d443755c88bcc55df1ac78a94c3df8af48a3fd2101fa4608d18ee05fcd9689e8bd4bd28d70dd52d32bc044b50b2b51ab750c88be2b47f9b5"
        )]
//[assembly: InternalsVisibleTo("TweetSharp.UnitTests, PublicKey=00240000048000009400000006020000002400005253413100040000010001008966c531777613a65ba705c822b0d319eaa18b0dbe4701a0f154f863d3a39a67a7864242bd6a0b1ae001665e13e17d3479e8b81e4bdb23ac15672b18a4da62c547f9a8125daa3c94d443755c88bcc55df1ac78a94c3df8af48a3fd2101fa4608d18ee05fcd9689e8bd4bd28d70dd52d32bc044b50b2b51ab750c88be2b47f9b5")]