using Sodium;
using System;
using System.IO;
using System.Linq;

namespace RBX_Alt_Manager.Classes
{
    internal class Cryptography
    {
        public static byte[] RAMHeader = new byte[] { 82, 111, 98, 108, 111, 120, 32, 65, 99, 99, 111, 117, 110, 116, 32, 77, 97, 110, 97, 103, 101, 114, 32, 99, 114, 101, 97, 116, 101, 100, 32, 98, 121, 32, 105, 99, 51, 119, 48, 108, 102, 50, 50, 32, 64, 32, 103, 105, 116, 104, 117, 98, 46, 99, 111, 109, 32, 46, 46, 46, 46, 46, 46, 46 };

        public static byte[] Encrypt(string Content, byte[] Password)
        {
            if (string.IsNullOrEmpty(Content)) throw new ArgumentNullException("Missing Content");

            var Salt = PasswordHash.ArgonGenerateSalt();
            var DerivedKey = PasswordHash.ArgonHashBinary(Password, Salt, PasswordHash.StrengthArgon.Moderate, 32);
            var Nonce = SecretBox.GenerateNonce();
            var Encrypted = SecretBox.Create(Content, Nonce, DerivedKey);

            var OutputStream = new MemoryStream();
            var OutputWriter = new BinaryWriter(OutputStream);

            OutputWriter.Write(RAMHeader);

            OutputWriter.Write(Salt);
            OutputStream.Write(Nonce, 0, Nonce.Length);
            OutputStream.Write(Encrypted, 0, Encrypted.Length);

            return OutputStream.ToArray();
        }

        public static byte[] Decrypt(byte[] Encrypted, byte[] Password)
        {
            var InputStream = new MemoryStream(Encrypted);
            var InputReader = new BinaryReader(InputStream);

            if (!RAMHeader.SequenceEqual(InputReader.ReadBytes(RAMHeader.Length))) throw new ArgumentNullException("Missing RAMHeader");

            var Salt = InputReader.ReadBytes(16);
            var Nonce = InputReader.ReadBytes(24);
            var CipherText = InputReader.ReadBytes(Encrypted.Length);

            var derivedKey = PasswordHash.ArgonHashBinary(Password, Salt, PasswordHash.StrengthArgon.Moderate, 32);

            return SecretBox.Open(CipherText, Nonce, derivedKey);
        }
    }
}