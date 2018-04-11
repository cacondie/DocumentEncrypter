using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace DocumentEncrypter
{
    public class Encrypter
    {
        private RSACng _rsa;
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        private DirectoryInfo _directory;

        public Encrypter(DirectoryInfo directory)
        {
            _rsa = new RSACng(4096);
            _privateKey = _rsa.ExportParameters(true);
            _publicKey = _rsa.ExportParameters(false);
            _directory = directory;
        }
        public Encrypter(FileInfo publicKey, DirectoryInfo directory)
        {
            _rsa = new RSACng(4096);
            string key = File.ReadAllText(publicKey.FullName);
            _rsa.FromXmlString(key);
            _directory = directory;
        }
 
        public void Encrypt(FileInfo filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath.FullName);
            byte[] encrypted = _rsa.Encrypt(bytes,RSAEncryptionPadding.Pkcs1);
            File.WriteAllBytes(filePath.FullName + ".enc",encrypted);
        }
        public void Decrypt(FileInfo filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath.FullName);
            byte[] decrypted = _rsa.Decrypt(bytes, RSAEncryptionPadding.Pkcs1);
            File.WriteAllBytes(filePath.FullName.Replace(".enc",""),decrypted);
        }

        public void GenerateKeys()
        {
            string privateKey = _rsa.ToXmlString(true);
            string publicKey = _rsa.ToXmlString(false);

            if (_directory.Exists)
            {
                File.WriteAllText(_directory.FullName + @"\privateKey.xml", privateKey);
                File.WriteAllText(_directory.FullName + @"\publicKey.xml", publicKey);
            }
        }
    }
}
