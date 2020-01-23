using System;
using System.Text;
using System.Threading.Tasks;

namespace PassGenLib
{
    public class Password
    {
        private uint length;

        public Password() : this(16u)
        {
        }

        public Password(uint length)
        {
            this.length = length;
            this.NewPassword();
        }

        private string GenerateRandomPassword(uint length)
        {
            // This is an internal method to shuffle the words inside a text.
            static string ShuffleWords(string text)
            {
                var sb = new StringBuilder(text);
                var retStr = new StringBuilder();
                var r = new Random();

                while (sb.Length > 0)
                {
                    var pos = r.Next(sb.ToString().Length);
                    retStr.Append(sb[pos]);
                    sb.Remove(pos, 1);
                }

                return retStr.ToString();
            }

            if (length == 0)
            {
                // If the specified length of the password is equal to zero
                // then return a string empty.
                return string.Empty;
            }

            var allowedCharacters = ShuffleWords(this.AllowedCharacters);
            var sb = new StringBuilder();
            var r = new Random();

            for (var ix = 0u; ix < length; ix++)
            {
                var ch = allowedCharacters[r.Next(allowedCharacters.Length)];
                sb.Append(ch);
            }

            return sb.ToString();
        }

        public void NewPassword()
        {
            this.PlainTextValue = this.GenerateRandomPassword(this.length);
        }

        public string AllowedCharacters => "AaBbCcDdEeFfGgHhIiKkJjLlMmNnOoPpQqRrSsTtUuVvWwXxYyZ0123456789!£$%&#@<([{|}])>?^*';:-_";

        public string PlainTextValue { get; internal set; }
    }
}
