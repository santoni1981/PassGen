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
        private readonly string _plainTextMessage = "Hello, World!";
        private readonly byte[] _encryptedMessage = new byte[] { 47, 13, 5, 6, 4, 64, 77, 57, 0, 2, 29, 22, 82 };

        [TestMethod]
        public void EncryptPlainText()
        {
            byte[] result = SimpleXorEncrypter.Encrypt(_plainTextMessage, Key);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Length, _encryptedMessage.Length);

            for (int ix = 0; ix < _encryptedMessage.Length; ++ix)
            {
                Assert.AreEqual(result[ix], _encryptedMessage[ix]);
            }
        }

        [TestMethod]
        public void DecryptToPlainText()
        {
            string result = SimpleXorEncrypter.Decrypt(_encryptedMessage, Key);

            Assert.AreEqual(result, _plainTextMessage);
        }

        [TestMethod]
        public void EncryptAndDecryptAGeneratedPassword()
        {
            string randomPassword = new Password(1024).PlainText;
            string randomKey = new Password(1024).PlainText;
            byte[] encrypted = SimpleXorEncrypter.Encrypt(randomPassword, randomKey);
            string plainText = SimpleXorEncrypter.Decrypt(encrypted, randomKey);

            Assert.AreEqual(randomPassword, plainText);
        }

        [TestMethod]
        public void EncryptWithoutKey()
        {
            byte[] result = SimpleXorEncrypter.Encrypt(_plainTextMessage, string.Empty);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DecryptWithoutKey()
        {
            string result = SimpleXorEncrypter.Decrypt(_encryptedMessage, string.Empty);

            Assert.IsNull(result);
        }
    }
}
