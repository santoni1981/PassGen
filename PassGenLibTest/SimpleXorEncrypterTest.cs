/*
 * MIT License
 *
 * Copyright (c) 2021 Marco Santoni
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
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
