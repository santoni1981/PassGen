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
namespace Santoni1981.PassGenLib
{
    public static class SimpleXorEncrypter
    {
        public static byte[] Encrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(key))
            {
                return null;
            }

            int len = plainText.Length;
            int len_k = key.Length;
            byte[] encrypted = new byte[len];

            for (int idx_k = 0; idx_k < len_k; idx_k++)
            { 
                for (int idx = 0; idx < len; idx++)
                {
                    int k = (((byte)key[idx_k] + idx) % 255);
                    encrypted[idx] = (byte)(plainText[idx] ^ k);
                }
            }

            return encrypted;
        }

        public static string Decrypt(byte[] encryptedText, string key)
        {
            if ((encryptedText == null || encryptedText.Length == 0) || string.IsNullOrEmpty(key))
            { 
                return null;
            }

            int len = encryptedText.Length;
            int len_k = key.Length;
            char[] decrypted = new char[len];

            for (int idx_k = 0; idx_k < len_k; idx_k++)
            { 
                for (int idx = 0; idx < len; idx++)
                {
                    int k = (((byte)key[idx_k] + idx) % 255);
                    decrypted[idx] = (char)(encryptedText[idx] ^ k);
                }
            }

            return new string(decrypted);
        }
    }
}
