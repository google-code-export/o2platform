namespace Amazon.S3.Util
{
    using Amazon.S3;
    using Amazon.S3.Model;
    using Amazon.Util;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class AmazonS3Util
    {
        private static Dictionary<string, string> extensionToMime = new Dictionary<string, string>(150);

        static AmazonS3Util()
        {
            extensionToMime[".ai"] = "application/postscript";
            extensionToMime[".aif"] = "audio/x-aiff";
            extensionToMime[".aifc"] = "audio/x-aiff";
            extensionToMime[".aiff"] = "audio/x-aiff";
            extensionToMime[".asc"] = "text/plain";
            extensionToMime[".au"] = "audio/basic";
            extensionToMime[".avi"] = "video/x-msvideo";
            extensionToMime[".bcpio"] = "application/x-bcpio";
            extensionToMime[".bin"] = "application/octet-stream";
            extensionToMime[".c"] = "text/plain";
            extensionToMime[".cc"] = "text/plain";
            extensionToMime[".ccad"] = "application/clariscad";
            extensionToMime[".cdf"] = "application/x-netcdf";
            extensionToMime[".class"] = "application/octet-stream";
            extensionToMime[".cpio"] = "application/x-cpio";
            extensionToMime[".cpp"] = "text/plain";
            extensionToMime[".cpt"] = "application/mac-compactpro";
            extensionToMime[".cs"] = "text/plain";
            extensionToMime[".csh"] = "application/x-csh";
            extensionToMime[".css"] = "text/css";
            extensionToMime[".dcr"] = "application/x-director";
            extensionToMime[".dir"] = "application/x-director";
            extensionToMime[".dms"] = "application/octet-stream";
            extensionToMime[".doc"] = "application/msword";
            extensionToMime[".docx"] = "application/msword";
            extensionToMime[".dot"] = "application/msword";
            extensionToMime[".drw"] = "application/drafting";
            extensionToMime[".dvi"] = "application/x-dvi";
            extensionToMime[".dwg"] = "application/acad";
            extensionToMime[".dxf"] = "application/dxf";
            extensionToMime[".dxr"] = "application/x-director";
            extensionToMime[".eps"] = "application/postscript";
            extensionToMime[".etx"] = "text/x-setext";
            extensionToMime[".exe"] = "application/octet-stream";
            extensionToMime[".ez"] = "application/andrew-inset";
            extensionToMime[".f"] = "text/plain";
            extensionToMime[".f90"] = "text/plain";
            extensionToMime[".fli"] = "video/x-fli";
            extensionToMime[".gif"] = "image/gif";
            extensionToMime[".gtar"] = "application/x-gtar";
            extensionToMime[".gz"] = "application/x-gzip";
            extensionToMime[".h"] = "text/plain";
            extensionToMime[".hdf"] = "application/x-hdf";
            extensionToMime[".hh"] = "text/plain";
            extensionToMime[".hqx"] = "application/mac-binhex40";
            extensionToMime[".htm"] = "text/html";
            extensionToMime[".html"] = "text/html";
            extensionToMime[".ice"] = "x-conference/x-cooltalk";
            extensionToMime[".ief"] = "image/ief";
            extensionToMime[".iges"] = "model/iges";
            extensionToMime[".igs"] = "model/iges";
            extensionToMime[".ips"] = "application/x-ipscript";
            extensionToMime[".ipx"] = "application/x-ipix";
            extensionToMime[".jpe"] = "image/jpeg";
            extensionToMime[".jpeg"] = "image/jpeg";
            extensionToMime[".jpg"] = "image/jpeg";
            extensionToMime[".js"] = "application/x-javascript";
            extensionToMime[".kar"] = "audio/midi";
            extensionToMime[".latex"] = "application/x-latex";
            extensionToMime[".lha"] = "application/octet-stream";
            extensionToMime[".lsp"] = "application/x-lisp";
            extensionToMime[".lzh"] = "application/octet-stream";
            extensionToMime[".m"] = "text/plain";
            extensionToMime[".man"] = "application/x-troff-man";
            extensionToMime[".me"] = "application/x-troff-me";
            extensionToMime[".mesh"] = "model/mesh";
            extensionToMime[".mid"] = "audio/midi";
            extensionToMime[".midi"] = "audio/midi";
            extensionToMime[".mime"] = "www/mime";
            extensionToMime[".mov"] = "video/quicktime";
            extensionToMime[".movie"] = "video/x-sgi-movie";
            extensionToMime[".mp2"] = "audio/mpeg";
            extensionToMime[".mp3"] = "audio/mpeg";
            extensionToMime[".mpe"] = "video/mpeg";
            extensionToMime[".mpeg"] = "video/mpeg";
            extensionToMime[".mpg"] = "video/mpeg";
            extensionToMime[".mpga"] = "audio/mpeg";
            extensionToMime[".ms"] = "application/x-troff-ms";
            extensionToMime[".msi"] = "application/x-ole-storage";
            extensionToMime[".msh"] = "model/mesh";
            extensionToMime[".nc"] = "application/x-netcdf";
            extensionToMime[".oda"] = "application/oda";
            extensionToMime[".pbm"] = "image/x-portable-bitmap";
            extensionToMime[".pdb"] = "chemical/x-pdb";
            extensionToMime[".pdf"] = "application/pdf";
            extensionToMime[".pgm"] = "image/x-portable-graymap";
            extensionToMime[".pgn"] = "application/x-chess-pgn";
            extensionToMime[".png"] = "image/png";
            extensionToMime[".pnm"] = "image/x-portable-anymap";
            extensionToMime[".pot"] = "application/mspowerpoint";
            extensionToMime[".ppm"] = "image/x-portable-pixmap";
            extensionToMime[".pps"] = "application/mspowerpoint";
            extensionToMime[".ppt"] = "application/mspowerpoint";
            extensionToMime[".ppz"] = "application/mspowerpoint";
            extensionToMime[".pre"] = "application/x-freelance";
            extensionToMime[".prt"] = "application/pro_eng";
            extensionToMime[".ps"] = "application/postscript";
            extensionToMime[".qt"] = "video/quicktime";
            extensionToMime[".ra"] = "audio/x-realaudio";
            extensionToMime[".ram"] = "audio/x-pn-realaudio";
            extensionToMime[".ras"] = "image/cmu-raster";
            extensionToMime[".rgb"] = "image/x-rgb";
            extensionToMime[".rm"] = "audio/x-pn-realaudio";
            extensionToMime[".roff"] = "application/x-troff";
            extensionToMime[".rpm"] = "audio/x-pn-realaudio-plugin";
            extensionToMime[".rtf"] = "text/rtf";
            extensionToMime[".rtx"] = "text/richtext";
            extensionToMime[".scm"] = "application/x-lotusscreencam";
            extensionToMime[".set"] = "application/set";
            extensionToMime[".sgm"] = "text/sgml";
            extensionToMime[".sgml"] = "text/sgml";
            extensionToMime[".sh"] = "application/x-sh";
            extensionToMime[".shar"] = "application/x-shar";
            extensionToMime[".silo"] = "model/mesh";
            extensionToMime[".sit"] = "application/x-stuffit";
            extensionToMime[".skd"] = "application/x-koan";
            extensionToMime[".skm"] = "application/x-koan";
            extensionToMime[".skp"] = "application/x-koan";
            extensionToMime[".skt"] = "application/x-koan";
            extensionToMime[".smi"] = "application/smil";
            extensionToMime[".smil"] = "application/smil";
            extensionToMime[".snd"] = "audio/basic";
            extensionToMime[".sol"] = "application/solids";
            extensionToMime[".spl"] = "application/x-futuresplash";
            extensionToMime[".src"] = "application/x-wais-source";
            extensionToMime[".step"] = "application/STEP";
            extensionToMime[".stl"] = "application/SLA";
            extensionToMime[".stp"] = "application/STEP";
            extensionToMime[".sv4cpio"] = "application/x-sv4cpio";
            extensionToMime[".sv4crc"] = "application/x-sv4crc";
            extensionToMime[".swf"] = "application/x-shockwave-flash";
            extensionToMime[".t"] = "application/x-troff";
            extensionToMime[".tar"] = "application/x-tar";
            extensionToMime[".tcl"] = "application/x-tcl";
            extensionToMime[".tex"] = "application/x-tex";
            extensionToMime[".tif"] = "image/tiff";
            extensionToMime[".tiff"] = "image/tiff";
            extensionToMime[".tr"] = "application/x-troff";
            extensionToMime[".tsi"] = "audio/TSP-audio";
            extensionToMime[".tsp"] = "application/dsptype";
            extensionToMime[".tsv"] = "text/tab-separated-values";
            extensionToMime[".txt"] = "text/plain";
            extensionToMime[".unv"] = "application/i-deas";
            extensionToMime[".ustar"] = "application/x-ustar";
            extensionToMime[".vcd"] = "application/x-cdlink";
            extensionToMime[".vda"] = "application/vda";
            extensionToMime[".vrml"] = "model/vrml";
            extensionToMime[".wav"] = "audio/x-wav";
            extensionToMime[".wrl"] = "model/vrml";
            extensionToMime[".xbm"] = "image/x-xbitmap";
            extensionToMime[".xlc"] = "application/vnd.ms-excel";
            extensionToMime[".xll"] = "application/vnd.ms-excel";
            extensionToMime[".xlm"] = "application/vnd.ms-excel";
            extensionToMime[".xls"] = "application/vnd.ms-excel";
            extensionToMime[".xlw"] = "application/vnd.ms-excel";
            extensionToMime[".xml"] = "text/xml";
            extensionToMime[".xpm"] = "image/x-xpixmap";
            extensionToMime[".xwd"] = "image/x-xwindowdump";
            extensionToMime[".xyz"] = "chemical/x-pdb";
            extensionToMime[".zip"] = "application/zip";
        }

        public static NameValueCollection CreateHeaderEntry(string key, string value)
        {
            NameValueCollection values = new NameValueCollection();
            values.Add(key, value);
            return values;
        }

        public static bool DoesS3BucketExist(string bucketName, AmazonS3 s3Client)
        {
            bool flag;
            if (s3Client == null)
            {
                throw new ArgumentNullException("s3Client", "The s3Client cannot be null!");
            }
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentNullException("bucketName", "The bucketName cannot be null or the empty string!");
            }
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
            request.BucketName = bucketName;
            request.Expires = new DateTime(0x7e3, 12, 0x1f);
            request.Verb = HttpVerb.HEAD;
            HttpWebRequest request2 = WebRequest.Create(s3Client.GetPreSignedURL(request)) as HttpWebRequest;
            request2.Method = "HEAD";
            try
            {
                request2.GetResponse();
                flag = true;
            }
            catch (WebException exception)
            {
                using (HttpWebResponse response = exception.Response as HttpWebResponse)
                {
                    if (response != null)
                    {
                        HttpStatusCode statusCode = response.StatusCode;
                        return ((statusCode != HttpStatusCode.NotFound) && (statusCode != HttpStatusCode.BadRequest));
                    }
                    flag = false;
                }
            }
            return flag;
        }

        public static string GenerateChecksumForContent(string content, bool fBase64Encode)
        {
            byte[] inArray = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(content));
            if (fBase64Encode)
            {
                return Convert.ToBase64String(inArray);
            }
            return BitConverter.ToString(inArray).Replace("-", string.Empty);
        }

        public static string GenerateChecksumForStream(Stream input, bool fbase64Encode)
        {
            string str = null;
            BufferedStream inputStream = new BufferedStream(input, 0x100000);
            byte[] inArray = MD5.Create().ComputeHash(inputStream);
            if (fbase64Encode)
            {
                str = Convert.ToBase64String(inArray);
            }
            else
            {
                str = BitConverter.ToString(inArray).Replace("-", string.Empty);
            }
            inputStream.Position = 0L;
            return str;
        }

        public static Stream MakeStreamSeekable(Stream input)
        {
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[0x8000];
            int count = 0;
            using (input)
            {
                while ((count = input.Read(buffer, 0, 0x8000)) > 0)
                {
                    stream.Write(buffer, 0, count);
                }
            }
            stream.Position = 0L;
            return stream;
        }

        public static string MimeTypeFromExtension(string ext)
        {
            if (extensionToMime.ContainsKey(ext))
            {
                return extensionToMime[ext];
            }
            return "application/octet-stream";
        }

        public static void SetObjectStorageClass(S3Object s3Object, S3StorageClass sClass, AmazonS3 s3Client)
        {
            SetObjectStorageClass(s3Object.Key, s3Object.BucketName, sClass, s3Client);
        }

        public static void SetObjectStorageClass(string bucketName, string key, S3StorageClass sClass, AmazonS3 s3Client)
        {
            if ((sClass > S3StorageClass.ReducedRedundancy) || (sClass < S3StorageClass.Standard))
            {
                throw new ArgumentException("Invalid value specified for storage class.");
            }
            if (s3Client == null)
            {
                throw new ArgumentNullException("s3Client", "Please specify an S3 Client to make service requests.");
            }
            CopyObjectRequest request = new CopyObjectRequest();
            request.SourceBucket = request.DestinationBucket = bucketName;
            request.SourceKey = request.DestinationKey = key;
            request.StorageClass = sClass;
            s3Client.CopyObject(request);
        }

        public static string Sign(string data, SecureString key, KeyedHashAlgorithm algorithm)
        {
            if (key == null)
            {
                throw new AmazonS3Exception("The AWS Secret Access Key specified is NULL");
            }
            return AWSSDKUtils.HMACSign(data, key, algorithm);
        }

        public static string UrlEncode(string data, bool path)
        {
            return AWSSDKUtils.UrlEncode(data, path);
        }

        public static bool ValidateV2Bucket(string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentNullException("bucketName", "Please specify a bucket name");
            }
            if (bucketName.StartsWith("s3.amazonaws.com"))
            {
                return false;
            }
            int index = bucketName.IndexOf(".s3.amazonaws.com");
            if (index > 0)
            {
                bucketName = bucketName.Substring(0, index);
            }
            if (((bucketName.Length < 3) || (bucketName.Length > 0x3f)) || (bucketName.StartsWith(".") || bucketName.EndsWith(".")))
            {
                return false;
            }
            Regex regex = new Regex(@"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$");
            if (regex.IsMatch(bucketName))
            {
                return false;
            }
            Regex regex2 = new Regex(@"^[a-z0-9]([a-z0-9\-]*[a-z0-9])?$");
            foreach (string str in bucketName.Split(@"\.".ToCharArray()))
            {
                if (!regex2.IsMatch(str))
                {
                    return false;
                }
            }
            return true;
        }

        public static string FormattedCurrentTimestamp
        {
            get
            {
                return AWSSDKUtils.FormattedCurrentTimestampGMT;
            }
        }
    }
}

