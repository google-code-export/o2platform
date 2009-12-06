using System;
using System.IO;


using Majestic12;

/*

Copyright (c) Alex Chudnovsky, Majestic-12 Ltd (UK). 2005+ All rights reserved
Web:		http://www.majestic12.co.uk
E-mail:		alexc@majestic12.co.uk

Redistribution and use in source and binary forms, with or without modification, are 
permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions 
		and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
		and the following disclaimer in the documentation and/or other materials provided with the distribution.
    * Neither the name of the Majestic-12 nor the names of its contributors may be used to endorse or 
		promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/



namespace Majestic12
{
	/// <summary>
	/// HTMLparserTest: example of use of the HTML parser object
	/// </summary>
	class HTMLparserTest
	{
		/// <summary>
		/// If true then an ENTER will be required when running this test in interactive mode (default)
		/// This mode can be switched off via command line switch NODELAY
		/// </summary>
		bool bReadLineDelay=true;

		/// <summary>
		/// If you don't know what this function is, then its probably best for you not to proceed.
		/// 
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			HTMLparserTest oT=new HTMLparserTest();

			try
			{
				// check if we need to benchmark it 
				int iParseTimes=1;

				string sCurDir="";

				string sName="majestic12.html";

				// parse command line for options
				if(args.Length>0)
				{
					string sCmdParamCurDir="-c=";

					string sCmdParamFile="-f=";

					// check for no delay sign
					for(int i=0; i<args.Length; i++)
					{
						// check if working directory is specified in command line
						if(args[i].StartsWith(sCmdParamCurDir))
						{
							sCurDir=args[i].Substring(sCmdParamCurDir.Length,args[i].Length-sCmdParamCurDir.Length);
							continue;
						}

						// check if working directory is specified in command line
						if(args[i].StartsWith(sCmdParamFile))
						{
							sName=args[i].Substring(sCmdParamFile.Length,args[i].Length-sCmdParamFile.Length);
							continue;
						}

						if(args[i].ToLower().Trim()=="nodelay")
							oT.bReadLineDelay=false;
						else
						{
							// must be benchmark number
							try
							{
								iParseTimes=int.Parse(args[i]);

								Console.WriteLine("Running benchmark parsing {0} times",iParseTimes);
							}
							catch
							{
								Console.WriteLine("Benchmark number={0} is incorrect - should be integer",args[i]);
								return;
							}

						}
					}

				}

				// during debugging current directory will be in \bin\debug, so we will fix it here
				// to be where the source code is to be able to open file without having to hard code directory
				// either into project or code
				if(sCurDir!="")
				{
					Directory.SetCurrentDirectory(sCurDir);
				}
				else
				{

					sCurDir=Directory.GetCurrentDirectory().ToLower();
					int iIdx=sCurDir.IndexOf(@"bin\debug");

					if(iIdx==-1)
						iIdx=sCurDir.IndexOf(@"bin\release");

					if(iIdx!=-1)
					{
						sCurDir=sCurDir.Substring(0,iIdx);
						Directory.SetCurrentDirectory(sCurDir);
					}
				}
				// now parse HTML
				oT.Start(iParseTimes,sName);
			}
			catch(Exception oEx)
			{
				Console.WriteLine("Exception: "+oEx);
			}

			// execute a delay if we are running in interactive or benchmark mode
			if(oT.bReadLineDelay)
			{
				Console.WriteLine("Press ENTER to finish the program");
				Console.ReadLine();
			}
		}

		HTMLparserTest()
		{
		}

		/// <summary>
		/// Starts parsing
		/// </summary>
		/// <param name="iParseTimes">Number of times to parse document (useful for benchmarking)</param>
		void Start(int iParseTimes,string sFileName)
		{
			if(!File.Exists(sFileName))
			{
				sFileName=Path.Combine(Directory.GetCurrentDirectory(),"tests"+Path.DirectorySeparatorChar+sFileName);

				if(!File.Exists(sFileName))
				{
					Console.WriteLine("Could not find file in current directory to parse - expected it to be here: "+sFileName);
					return;
				}
			}

			HTMLparser oP=new HTMLparser();

			// This is optional, but if you want high performance then you may
			// want to set chunk hash mode to FALSE. This would result in tag params
			// being added to string arrays in HTMLchunk object called sParams and sValues, with number
			// of actual params being in iParams. See code below for details.
			//
			// When TRUE (and its default) tag params will be added to hashtable HTMLchunk (object).oParams
			oP.SetChunkHashMode(false);

			// if you set this to true then original parsed HTML for given chunk will be kept - 
			// this will reduce performance somewhat, but may be desireable in some cases where
			// reconstruction of HTML may be necessary
			oP.bKeepRawHTML=false;

			// if set to true (it is false by default), then entities will be decoded: this is essential
			// if you want to get strings that contain final representation of the data in HTML, however
			// you should be aware that if you want to use such strings into output HTML string then you will
			// need to do Entity encoding or same string may fail later
			oP.bDecodeEntities=true;

            // we have option to keep most entities as is - only replace stuff like &nbsp; 
            // this is called Mini Entities mode - it is handy when HTML will need
            // to be re-created after it was parsed, though in this case really
            // entities should not be parsed at all
            oP.bDecodeMiniEntities=true;

            if(!oP.bDecodeEntities && oP.bDecodeMiniEntities)
               oP.InitMiniEntities();

			// if set to true, then in case of Comments and SCRIPT tags the data set to oHTML will be
			// extracted BETWEEN those tags, rather than include complete RAW HTML that includes tags too
			// this only works if auto extraction is enabled
			oP.bAutoExtractBetweenTagsOnly=true;

			// if true then comments will be extracted automatically
			oP.bAutoKeepComments=true;

			// if true then scripts will be extracted automatically: 
			oP.bAutoKeepScripts=true;

			// if this option is true then whitespace before start of tag will be compressed to single
			// space character in string: " ", if false then full whitespace before tag will be returned (slower)
			// you may only want to set it to false if you want exact whitespace between tags, otherwise it is just
			// a waste of CPU cycles
			oP.bCompressWhiteSpaceBeforeTag=true;

			// if true (default) then tags with attributes marked as CLOSED (/ at the end) will be automatically
			// forced to be considered as open tags - this is no good for XML parsing, but I keep it for backwards
			// compatibility for my stuff as it makes it easier to avoid checking for same tag which is both closed
			// or open
			oP.bAutoMarkClosedTagsWithParamsAsOpen=false;

			// load HTML from file
			oP.LoadFromFile(sFileName);

			// alternatively you can set HTML to be parsed as follows (bHTML is byte[] array containing data):
			// oP.Init(bHTML);									     

			DateTime oStart=DateTime.Now;

			for(int i=0; i<iParseTimes; i++)
			{
				if(iParseTimes>1)
					BenchMarkParse(oP);
				else
					ParseAndPrint(oP);

				oP.Reset();
			}

			// calculate number of milliseconds we were parsing
			int iMSecs=(int)((DateTime.Now.Ticks-oStart.Ticks)/TimeSpan.TicksPerMillisecond);

			if(iMSecs>0 && iParseTimes>0)
			{
				Console.Error.WriteLine("Parsed {0} time(s), total time {1:0.00} secs, ~{2:0.00} ms per full parse.",iParseTimes,iMSecs*1.0/1000,iMSecs*1.0/iParseTimes);
			}

			oP.Close();
		
		}

		/// <summary>
		/// Parse for benchmarking purposes -- its pure test of HTML parsing object, no extra processing done here
		/// </summary>
		/// <param name="oP">Parser object</param>
		void BenchMarkParse(HTMLparser oP)
		{
			// parser will return us tokens called HTMLchunk -- warning DO NOT destroy it until end of parsing
			// because HTMLparser re-uses this object
			HTMLchunk oChunk=null;

			// we parse until returned oChunk is null indicating we reached end of parsing
			while((oChunk=oP.ParseNext())!=null)
			{
				switch(oChunk.oType)
				{
						// matched open tag, ie <a href="">
					case HTMLchunkType.OpenTag:
						break;

						// matched close tag, ie </a>
					case HTMLchunkType.CloseTag:
						break;

						// matched normal text
					case HTMLchunkType.Text:
						break;

						// matched HTML comment, that's stuff between <!-- and -->
					case HTMLchunkType.Comment:
						break;
				};
			}

		}

		
		/// <summary>
		/// Parses HTML by chunk, prints parsed data on screen and waits for ENTER to go to next chunk
		/// </summary>
		/// <param name="oP">Parser object</param>
		void ParseAndPrint(HTMLparser oP)
		{
			//	bReadLineDelay=false;
			if(bReadLineDelay)
				Console.WriteLine("Parsing HTML, will print each parsed chunk, press ENTER after each to continue");

			// parser will return us tokens called HTMLchunk -- warning DO NOT destroy it until end of parsing
			// because HTMLparser re-uses this object
			HTMLchunk oChunk=null;

			// NOTE: bear in mind that when you deal with content which uses non-Latin chars, then you
			// need to ensure that correct encoding is set, this often set in HTML itself, but sometimes
			// only in HTTP headers for a given page - some pages use BOTH, but browsers seem to 
			// consider HTTP header setting as more important, so it is best to behave in similar way.
			
			// See below for code that deals with META based charset setting, similarly you need to call
			// it here if charset is set in Content-Type header

			// we will track whether encoding was set or not here, this is important
			// because we may have to do re-encoding of text found BEFORE META tag, this typically 
			// happens for TITLE tags only - if we had no encoding set and then had it set, then
			// we need to reencode it, highly annoying, but having garbage in title is even more annoying
			bool bEncodingSet=false;

			// debug:
			oP.SetEncoding(System.Text.Encoding.GetEncoding("iso-8859-1"));

			// we parse until returned oChunk is null indicating we reached end of parsing
			while((oChunk=oP.ParseNext())!=null)
			{
				switch(oChunk.oType)
				{
						// matched open tag, ie <a href="">
					case HTMLchunkType.OpenTag:
						Console.Write("Open tag: "+oChunk.sTag);

						// in order to set correct encoding we need to keep an eye on META tags
						// that hit us on what the encoding should be used, note here
						// that some webpages have TITLE set BEFORE meta-tags, which means you will
						// have to re-encode it in order to get correct representation of text

PrintParams:
						
						if(oChunk.sTag.Length==4 && oChunk.sTag=="meta")
						{
							HandleMetaEncoding(oP,oChunk,ref bEncodingSet);
						};

						// commented out call to code that will do the job for you - long code below
						// is left to demonstrate how to access individual param values
						// Console.WriteLine("{0}",oChunk.GenerateParamsHTML());
                        
						

						if(oChunk.bHashMode)
						{
							if(oChunk.oParams.Count>0)
							{
								foreach(string sParam in oChunk.oParams.Keys)
								{
									string sValue=oChunk.oParams[sParam].ToString();

									if(sValue.Length>0)
										Console.Write(" {0}='{1}'",sParam,sValue);
									else
										Console.Write(" {0}",sParam);
								}

							}
						}
						else
						{
							// this is alternative method of getting params -- it may look less convinient
							// but it saves a LOT of CPU ticks while parsing. It makes sense when you only need
							// params for a few
							if(oChunk.iParams>0)
							{
								for(int i=0; i<oChunk.iParams; i++)
								{
									// here we can use exactly the same single/double quotes as they
									// were used on params

									switch(oChunk.cParamChars[i])
									{
										case (byte)' ':
											if(oChunk.sValues[i].Length==0)
												Console.Write(" {0}",oChunk.sParams[i]);
											else
												Console.Write(" {0}={1}",oChunk.sParams[i],oChunk.sValues[i]);
											break;

										default:
											Console.Write(" {0}={1}{2}{1}",oChunk.sParams[i],(char)oChunk.cParamChars[i],oChunk.sValues[i]);
											break;
									}
										
								}

							}
						
						}

						break;

						// matched close tag, ie </a>
					case HTMLchunkType.CloseTag:
						//Console.Write(oChunk.GenerateHTML());

						Console.Write("Closed tag: "+oChunk.sTag);

						if(oChunk.iParams>0)
							goto PrintParams;

						break;

					// NOTE: you have to call finalisation because it is not done for Scripts or comments
					// Matched data between <script></script> tags
					case HTMLchunkType.Script:
						
						if(!oP.bAutoKeepScripts && !oP.bKeepRawHTML)
							oP.SetRawHTML(oChunk);

						if(oChunk.oHTML.Length>0)
							Console.Write("Script: "+oChunk.oHTML);
						else
							Console.Write("Script: [ignored for performance reasons]");

						if(oChunk.iParams>0)
							goto PrintParams;

						break;

					// NOTE: you have to call finalisation because it is not done for Scripts or comments
					// matched HTML comment, that's stuff between <!-- and -->
					case HTMLchunkType.Comment:

                        //Console.WriteLine("{0}",oChunk.GenerateHTML());

						if(oP.bKeepRawHTML || oP.bAutoKeepComments)
						{
							// by default we won't finalise automatically as comments are often
							// very lenghty and it is costly to create long strings when they are not
							// needed, ie: during indexing of text
							Console.Write("Comment: "+oChunk.oHTML);
						}
						else
						{
							// Even if raw HTML by default was not taken you can get it anyway by
							// uncommenting next line
							//oP.SetRawHTML(oChunk);

							Console.Write("Comment: [ignored for performance reasons]");
						}
						break;

					// matched normal text
					case HTMLchunkType.Text:

						// skip pure whitespace that we are not really interested in
						if(oP.bCompressWhiteSpaceBeforeTag && oChunk.oHTML.Trim().Length==0 && bReadLineDelay)
							continue;

						Console.Write("Text: '{0}'",oChunk.oHTML);
						break;

				};

				if(bReadLineDelay)
					Console.ReadLine();
				else
					Console.WriteLine("");
			}

		}

		/// <summary>
		/// Handles META tags that set page encoding
		/// </summary>
		/// <param name="oChunk">Chunk</param>
		void HandleMetaEncoding(HTMLparser oP,HTMLchunk oChunk,ref bool bEncodingSet)
		{
			// if encoding already set then we should not be trying to set new one
			// this is the logic that major browsers follow - the first Encoding is assumed to be 
			// the correct one
			if(bEncodingSet)
				return;

			if(HTMLparser.HandleMetaEncoding(oP,oChunk,ref bEncodingSet))
			{
				if(!bEncodingSet)
					Console.WriteLine("Failed to set encoding from META: {0}",oChunk.GenerateHTML());
			}
		}

	}
}
