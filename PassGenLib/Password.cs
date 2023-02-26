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

namespace Santoni1981.PassGenLib;

public sealed class Password
{
    private const string LowercaseLetters = "abcdefghikjlmnopqrstuvwxyz";
    private const string UppercaseLetters = "ABCDEFGHIKJLMNOPQRSTUVWXYZ";
    private const string Numbers = "0123456789";
    private const string Symbols = @"!£$%&#@<([{|}])>?^*';:-_+/\.,";
    private readonly PasswordOptions _passwordOptions;

    [Flags]
    public enum PasswordOptions
    {
        LowercaseLetters = 1,
        UppercaseLetters = 2,
        AllLetters = LowercaseLetters | UppercaseLetters,
        Numbers = 4,
        Symbols = 8,
        AllLettersAndNumbers = AllLetters | Numbers,
        AllLettersAndSymbols = AllLetters | Symbols,
        NumbersAndSymbols = Numbers | Symbols,
        All = AllLetters | Numbers | Symbols
    }

    private string GenerateRandomPassword(uint length)
    {
        // This is an internal method to shuffle the words inside a text.
        static string ShuffleWords(string text)
        {
            ArgumentNullException.ThrowIfNull(text, nameof(text));

            if (text.Length <= 1)
            {
                // If the text is empty or contains only one character,
                // then there is nothing to shuffle.
                return text;
            }

            StringBuilder inputString = new(text);
            StringBuilder outputString = new();
            Random r = new();

            while (inputString.Length > 0)
            {
                int pos = r.Next(inputString.ToString().Length);
                outputString.Append(inputString[pos]);
                inputString.Remove(pos, 1);
            }

            return outputString.ToString();
        }

        if (length == 0)
        {
            // If the specified length of the password is equal to zero
            // then return a string empty.
            return string.Empty;
        }

        // Gets the characters to generate the password and mixes them.
        string allowedCharactersShuffled = ShuffleWords(AllowedCharacters);

        StringBuilder sb = new();
        Random r = new();

        for (uint i0 = 0u; i0 < length; i0++)
        {
            char ch = allowedCharactersShuffled[r.Next(allowedCharactersShuffled.Length)];
            sb.Append(ch);
        }

        return sb.ToString();
    }

    private static string GetAllowedCharacters(PasswordOptions options)
    {
        StringBuilder ac = new();

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

    public Password(uint length = 16u, PasswordOptions passwordOptions = PasswordOptions.All)
    {
        Length = length;
        _passwordOptions = passwordOptions;
        NewPassword();
    }

    public string AllowedCharacters => GetAllowedCharacters(_passwordOptions);

    public uint Length { get; }

    public string PlainText { get; private set; }

    public void NewPassword() => PlainText = GenerateRandomPassword(Length);

    public override string ToString() => PlainText;
}