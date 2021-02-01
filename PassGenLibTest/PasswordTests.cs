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
using System.Text.RegularExpressions;

namespace Santoni1981.PassGenLibTest
{
    [TestClass]
    public class PasswordTests
    {
        private const uint TotalNumberOfPasswordsForTest = 100000;

        [TestMethod]
        public void GenerateRandomPasswordsWithAllCharacters()
        {
            var password = new Password();

            for (int ix = 0; ix < PasswordTests.TotalNumberOfPasswordsForTest; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainText.Length.Equals(16));
            }
        }

        [TestMethod]
        public void GenerateRandomPasswordsWithOnlyNumbers()
        {
            var password = new Password(32, Password.PasswordOptions.Numbers);
            var regex = new Regex("[^0-9]");

            for (int ix = 0; ix < PasswordTests.TotalNumberOfPasswordsForTest; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainText.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.PlainText));
            }
        }

        [TestMethod]
        public void GenerateRandomPasswordsWithOnlyLetters()
        {
            var password = new Password(32, Password.PasswordOptions.AllLetters);
            var regex = new Regex("[^A-Za-z]");

            for (int ix = 0; ix < PasswordTests.TotalNumberOfPasswordsForTest; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainText.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.PlainText));
            }
        }

        [TestMethod]
        public void GenerateRandomPasswordsWithOnlySymbols()
        {
            var password = new Password(32, Password.PasswordOptions.Symbols);
            var regex = new Regex("[^\\!£\\$%&#@\\<\\(\\[\\{\\|\\}\\]\\)\\>\\?\\^\\*\\';:\\-_+\\\\/\\.,]");

            for (int ix = 0; ix < PasswordTests.TotalNumberOfPasswordsForTest; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainText.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.PlainText));
            }
        }
    }
}
