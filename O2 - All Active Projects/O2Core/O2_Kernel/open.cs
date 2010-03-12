using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;
using O2.Kernel.CodeUtils;

namespace O2.Kernel
{
    public class open
    {
        public static Control directory()
        {
            var directory = viewAscx("ascx_Directory");
            directory.invoke("simpleMode_withAddressBar");
            return directory;
        }

        public static TextBox file(string fileToView)
        {
            var title = "Text file: " + fileToView;
            return title.openControlAsForm<TextBox>(800, 400, "add_TextBox", true, "set_Text", O2Kernel_Files.getFileContents(fileToView));            
        }

        public static RichTextBox document(string fileToView)
        {
            var title = "RTF file: " + fileToView;
            return title.openControlAsForm<RichTextBox>(800, 400, "add_RichTextBox", null, "set_Rtf", O2Kernel_Files.getFileContents(fileToView));
        }        


        public static PictureBox image(object imageToLoad)
        {
            var title = "image: " + imageToLoad;
            return title.openControlAsForm<PictureBox>(800, 400, "add_PictureBox", null ,"load", imageToLoad);
        }

        public static object web()
        {
            return webBrowser("");
        }

        public static object web(string url)
        {
            return webBrowser(url);
        }

        public static object link(string url)
        {
            return webBrowser(url);
        }

        public static object browser()
        {
            return webBrowser("");
        }

        public static object browser(string url)
        {
            return webBrowser(url);
        }

        public static object webBrowser()
        {
            return webBrowser("");
        }

        public static object webBrowser(string url)
        {
            var browser = "O2_External_IE.dll".type("O2BrowserIE").invokeStatic("openAsForm");
            if (url.valid())
                return browser.invoke("openSync", url);
            return browser;
        }

        public static Control viewAscx(string controlName)
        {
            return controlName.viewsAscxControl();
        }

        public static Control viewAscx(string controlName, string title, int width, int height)
        {
            return controlName.viewsAscxControl(title, width, height);
        }

        public static T asForm<T>() where T : Control
        {
            return typeof(T).openControlAsForm<T>(typeof(T).Name,500,400);
        }
    }
}
