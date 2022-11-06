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

namespace Santoni1981.PassGenLib;

public static class SimpleXorEncrypter
{
    public static byte[] Encrypt(string plainText, string key)
    {
        if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(key))
        {
            return null;
        }

        int len = plainText.Length;
        byte[] encrypted = new byte[len];

        for (int i0 = 0, t0 = key.Length; i0 < t0; i0++)
        {
            for (int i1 = 0; i1 < len; i1++)
            {
                int k = ((byte)key[i0] + i1) % 255;
                encrypted[i1] = (byte)(plainText[i1] ^ k);
            }
        }

        return encrypted;
    }

    public static string Decrypt(byte[] encryptedText, string key)
    {
        if (encryptedText == null || encryptedText.Length == 0 || string.IsNullOrEmpty(key))
        {
            return null;
        }

        int len = encryptedText.Length;
        char[] decrypted = new char[len];

        for (int i0 = 0, t0 = key.Length; i0 < t0; i0++)
        {
            for (int i1 = 0; i1 < len; i1++)
            {
                int k = ((byte)key[i0] + i1) % 255;
                decrypted[i1] = (char)(encryptedText[i1] ^ k);
            }
        }

        return new string(decrypted);
    }
}