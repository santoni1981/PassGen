using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassGenLib;
using System.Text.RegularExpressions;

namespace PassGenLibTest
{
    [TestClass]
    public class PasswordTests
    {
        private const uint total = 100000;

        [TestMethod]
        public void GenerateRandomPasswordsWithAllCharacters()
        {
            var password = new Password();

            for (int ix = 0; ix < total; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainTextValue.Length.Equals(16));
            }
        }

        [TestMethod]
        public void GenerateRandomPasswordsWithOnlyNumbers()
        {
            var password = new Password(32, Password.PasswordOptions.Numbers);
            var regex = new Regex("[^0-9]");

            for (int ix = 0; ix < total; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainTextValue.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.PlainTextValue));
            }
        }

        [TestMethod]
        public void GenerateRandomPasswordsWithOnlyLetters()
        {
            var password = new Password(32, Password.PasswordOptions.AllLetters);
            var regex = new Regex("[^A-Za-z]");

            for (int ix = 0; ix < total; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainTextValue.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.PlainTextValue));
            }
        }

        [TestMethod]
        public void GenerateRandomPasswordsWithOnlySymbols()
        {
            var password = new Password(32, Password.PasswordOptions.Symbols);
            var regex = new Regex("[^\\!£\\$%&#@\\<\\(\\[\\{\\|\\}\\]\\)\\>\\?\\^\\*\\';:\\-_]");

            for (int ix = 0; ix < total; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainTextValue.Length.Equals(32));
                Assert.IsFalse(regex.IsMatch(password.PlainTextValue));
            }
        }
    }
}
