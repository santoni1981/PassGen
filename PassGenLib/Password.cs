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
                StringBuilder sb = new StringBuilder(text);
                StringBuilder retStr = new StringBuilder();
                Random r = new Random();

                while (sb.Length > 0)
                {
                    int pos = r.Next(sb.ToString().Length);
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
            string allowedCharactersShuffled = ShuffleWords(this.AllowedCharacters);

            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            for (uint ix = 0u; ix < length; ix++)
            {
                char ch = allowedCharactersShuffled[r.Next(allowedCharactersShuffled.Length)];
                sb.Append(ch);
            }

            return sb.ToString();
        }

        private string GetAllowedCharacters(PasswordOptions options)
        {
            StringBuilder ac = new StringBuilder();

            if ((options & PasswordOptions.LowercaseLetters) == PasswordOptions.LowercaseLetters)
                ac.Append(LowercaseLetters);

            if ((options & PasswordOptions.UppercaseLetters) == PasswordOptions.UppercaseLetters)
                ac.Append(UppercaseLetters);

            if ((options & PasswordOptions.Numbers) == PasswordOptions.Numbers)
                ac.Append(Numbers);

            if ((options & PasswordOptions.Symbols) == PasswordOptions.Symbols)
                ac.Append(Symbols);

            return ac.ToString();
        }

        public void NewPassword() => this.PlainText = this.GenerateRandomPassword(this.Length, this.passwordOptions);

        public string AllowedCharacters { get; private set; }

        public uint Length { get; internal set; }

        public string PlainText { get; internal set; }

        public override string ToString() => this.PlainText;
    }
}
