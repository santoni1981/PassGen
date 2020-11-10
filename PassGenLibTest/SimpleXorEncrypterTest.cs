using Microsoft.VisualStudio.TestTools.UnitTesting;
using Santoni1981.PassGenLib;

namespace Santoni1981.PassGenLibTest
{
    [TestClass]
    public class SimpleXorEncrypterTest
    {
        private const string Key = "ThisIsTheKeyUsedToEncodeTheString";
        private readonly string plainTextMessage = "Hello, World!";
        private readonly byte[] encryptedMessage = new byte[] { 47, 13, 5, 6, 4, 64, 77, 57, 0, 2, 29, 22, 82 };

        [TestMethod]
        public void EncryptPlainText()
        {
            var result = SimpleXorEncrypter.Encrypt(this.plainTextMessage, SimpleXorEncrypterTest.Key);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Length, this.encryptedMessage.Length);

            for (var ix = 0; ix < this.encryptedMessage.Length; ++ix)
            {
                Assert.AreEqual(result[ix], this.encryptedMessage[ix]);
            }
        }

        [TestMethod]
        public void DecryptToPlainText()
        {
            var result = SimpleXorEncrypter.Decrypt(this.encryptedMessage, SimpleXorEncrypterTest.Key);

            Assert.AreEqual(result, this.plainTextMessage);
        }

        [TestMethod]
        public void EncryptAndDecryptAGeneratedPassword()
        {
            var randomPassword = new Password(1024).PlainText;
            var randomKey = new Password(1024).PlainText;
            var encrypted = SimpleXorEncrypter.Encrypt(randomPassword, randomKey);
            var plainText = SimpleXorEncrypter.Decrypt(encrypted, randomKey);

            Assert.AreEqual(randomPassword, plainText);
        }

        [TestMethod]
        public void EncryptWithoutKey()
        {
            var result = SimpleXorEncrypter.Encrypt(this.plainTextMessage, string.Empty);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DecryptWithoutKey()
        {
            var result = SimpleXorEncrypter.Decrypt(this.encryptedMessage, string.Empty);

            Assert.IsNull(result);
        }
    }
}
