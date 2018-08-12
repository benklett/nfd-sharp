using System;
using NfdSharp;
using NUnit.Framework;

namespace NfdSharpTest
{
    [TestFixture]
    public class NfdTests
    {
        [Test]
        public void OpenDialog()
        {
            var res = Nfd.OpenDialog("jpg,jpeg,JPG;png", "", out string path);
            Console.WriteLine(res);
            Console.WriteLine(path);
        }
        
        [Test]
        public void OpenDialogMultiple()
        {
            var res = Nfd.OpenDialogMultiple("png", "", out string[] path);
            Console.WriteLine(res);
            foreach (var p in path)
            {
                Console.WriteLine(p);
            }
        }
        
        [Test]
        public void SaveDialog()
        {
            var res = Nfd.SaveDialog("tga", "", out string path);
            Console.WriteLine(res);
            Console.WriteLine(path);
        }
        
        [Test]
        public void PickFolder()
        {
            var res = Nfd.PickFolder("bmp", out string path);
            Console.WriteLine(res);
            Console.WriteLine(path);
        }
    }
}