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
            Nfd.OpenDialog("jpg", "", out string path);
        }
        
        [Test]
        public void OpenDialogMultiple()
        {
            Nfd.OpenDialogMultiple("png", "", out string[] path);
        }
        
        [Test]
        public void SaveDialog()
        {
            Nfd.SaveDialog("tga", "", out string path);
        }
        
        [Test]
        public void PickFolder()
        {
            Nfd.PickFolder("bmp", out string path);
        }
    }
}