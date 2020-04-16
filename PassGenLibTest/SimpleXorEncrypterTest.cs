using Microsoft.VisualStudio.TestTools.UnitTesting;
using Santoni1981.PassGenLib;

namespace Santoni1981.PassGenLibTest
{
    [TestClass]
    public class SimpleXorEncrypterTest
    {
        private string Key = "ThisIsTheKeyUsedToEncodeTheString";
        private string PlainTextMessage = "Hello, World!";
        private byte[] EncryptedMessage = new byte[] { 47, 13, 5, 6, 4, 64, 77, 57, 0, 2, 29, 22, 82 };

        [TestMethod]
        public void EncryptPlainText()
        {
            var result = SimpleXorEncrypter.Encrypt(this.PlainTextMessage, this.Key);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Length, this.EncryptedMessage.Length);

            for (var ix = 0; ix < this.EncryptedMessage.Length; ++ix)
            {
                Assert.AreEqual(result[ix], this.EncryptedMessage[ix]);
            }
        }

        [TestMethod]
        public void DecryptToPlainText()
        {
            var result = SimpleXorEncrypter.Decrypt(this.EncryptedMessage, this.Key);
            Assert.AreEqual(result, this.PlainTextMessage);
        }
    }
}
