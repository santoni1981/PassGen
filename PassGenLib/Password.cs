using System;
using System.Text;

namespace Santoni1981.PassGenLib
{
    public class Password
    {
        private const string LowercaseLetters = "abcdefghikjlmnopqrstuvwxyz";
        private const string UppercaseLetters = "ABCDEFGHIKJLMNOPQRSTUVWXYZ";
        private const string Numbers = "0123456789";
        private const string Symbols = "!£$%&#@<([{|}])>?^*';:-_+/\\.,";
        private PasswordOptions passwordOptions;

        [Flags]
        public enum PasswordOptions
        {
            LowercaseLetters = 1,
            UppercaseLetters = 2,
            AllLetters = (LowercaseLetters | UppercaseLetters),
            Numbers = 4,
            Symbols = 8,
            AllLettersAndNumbers = (AllLetters | Numbers),
            AllLettersAndSymbols = (AllLetters | Symbols),
            NumbersAndSymbols = (Numbers | Symbols),
            All = (AllLetters | Numbers | Symbols)
        }

        public Password()
            : this(16u, PasswordOptions.All)
        {
        }

        public Password(uint length)
            : this(length, PasswordOptions.All)
        {
        }

        public Password(uint length, PasswordOptions passwordOptions)
        {
            this.Length = length;
            this.passwordOptions = passwordOptions;
            this.NewPassword();
        }

        private string GenerateRandomPassword(uint length, PasswordOptions options)
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

            // Get the allowed characters to generate the password...
            this.AllowedCharacters = this.GetAllowedCharacters(options);
            // ...and shuffle the allowed characters string.
            var allowedCharactersShuffled = ShuffleWords(this.AllowedCharacters);

            var sb = new StringBuilder();
            var r = new Random();

            for (var ix = 0u; ix < length; ix++)
            {
                var ch = allowedCharactersShuffled[r.Next(allowedCharactersShuffled.Length)];
                sb.Append(ch);
            }

            return sb.ToString();
        }

        private string GetAllowedCharacters(PasswordOptions options)
        {
            var ac = new StringBuilder();

            if ((options & PasswordOptions.LowercaseLetters) == PasswordOptions.LowercaseLetters)
            {
                ac.Append(LowercaseLetters);
            }

            if ((options & PasswordOptions.UppercaseLetters) == PasswordOptions.UppercaseLetters)
            {
                ac.Append(UppercaseLetters);
            }

            if ((options & PasswordOptions.Numbers) == PasswordOptions.Numbers)
            {
                ac.Append(Numbers);
            }

            if ((options & PasswordOptions.Symbols) == PasswordOptions.Symbols)
            {
                ac.Append(Symbols);
            }

            return ac.ToString();
        }

        public void NewPassword() => this.Text = this.GenerateRandomPassword(this.Length, this.passwordOptions);

        public string AllowedCharacters { get; private set; }

        public uint Length { get; internal set; }

        public string Text { get; internal set; }

        public override string ToString() => this.Text;
    }
}
