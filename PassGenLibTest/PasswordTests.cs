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
                Assert.IsTrue(password.Text.Length.Equals(16));
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
                Assert.IsTrue(password.Text.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.Text));
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
                Assert.IsTrue(password.Text.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.Text));
            }
        }

        [TestMethod]
        public void GenerateRandomPasswordsWithOnlySymbols()
        {
            var password = new Password(32, Password.PasswordOptions.Symbols);
            var regex = new Regex("[^\\!£\\$%&#@\\<\\(\\[\\{\\|\\}\\]\\)\\>\\?\\^\\*\\';:\\-_+\\\\/]");

            for (int ix = 0; ix < PasswordTests.TotalNumberOfPasswordsForTest; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.Text.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.Text));
            }
        }
    }
}
