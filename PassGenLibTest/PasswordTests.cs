using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassGenLib;

namespace PassGenLibTest
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void GenerateRandomPasswords()
        {
            var total = 100000;
            var password = new Password();

            for (int ix = 0; ix < total; ix++)
            {
                password.NewPassword();
                Assert.IsTrue(password.PlainTextValue.Length.Equals(16));
            }
        }
    }
}
