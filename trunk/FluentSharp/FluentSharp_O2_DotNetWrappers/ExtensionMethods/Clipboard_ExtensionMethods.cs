using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using FluentSharp.O2.DotNetWrappers.DotNet;
using FluentSharp.O2.Kernel.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.ExtensionMethods;
using FluentSharp.O2.DotNetWrappers.Windows;

namespace FluentSharp.O2.DotNetWrappers.ExtensionMethods
{
    public static class Clipboard_ExtensionMethods
    {
        public static void toClipboard(this string newClipboardText)
        {
            newClipboardText.clipboardText_Set();
        }

        public static string clipboardText_Set(this string newClipboardText)
        {
            var sync = new AutoResetEvent(false);
            O2Thread.staThread(
                () =>
                {
                    O2Forms.setClipboardText(newClipboardText);
                    sync.Set();
                });
            sync.WaitOne(2000);
            return newClipboardText;
        }

        public static string clipboardText_Get(this object _object)
        {
            var sync = new AutoResetEvent(false);
            string clipboardText = null;
            O2Thread.staThread(
                () =>
                {
                    clipboardText = O2Forms.getClipboardText();
                    sync.Set();
                });
            sync.WaitOne(2000);
            return clipboardText;
        }



        public static string saveImageFromClipboard(this object _object)
        {
            var sync = new AutoResetEvent(false);
            string savedImage = null;
            O2Thread.staThread(
                () =>
                {
                    var bitmap = new Control().fromClipboardGetImage();
                    if (bitmap.notNull())
                    {
                        savedImage = bitmap.save();
                        savedImage.toClipboard();
                        "Image in clipboard was saved to: {0}".info(savedImage);
                    }
                    sync.Set();
                });

            sync.WaitOne(2000);

            return savedImage;
        }
    }
}
