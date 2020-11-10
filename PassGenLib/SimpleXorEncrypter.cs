namespace Santoni1981.PassGenLib
{
    public static class SimpleXorEncrypter
    {
        public static byte[] Encrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(key))
                return null;

            var len = plainText.Length;
            var len_k = key.Length;
            var encrypted = new byte[len];

            for (var idx_k = 0; idx_k < len_k; idx_k++)
                for (var idx = 0; idx < len; idx++)
                {
                    var k = (((byte)key[idx_k] + idx) % 255);
                    encrypted[idx] = (byte)(plainText[idx] ^ k);
                }

            return encrypted;
        }

        public static string Decrypt(byte[] encryptedText, string key)
        {
            if ((encryptedText == null || encryptedText.Length == 0) || string.IsNullOrEmpty(key))
                return null;

            var len = encryptedText.Length;
            var len_k = key.Length;
            var decrypted = new char[len];

            for (var idx_k = 0; idx_k < len_k; idx_k++)
                for (var idx = 0; idx < len; idx++)
                {
                    var k = (((byte)key[idx_k] + idx) % 255);
                    decrypted[idx] = (char)(encryptedText[idx] ^ k);
                }

            return new string(decrypted);
        }
    }
}
