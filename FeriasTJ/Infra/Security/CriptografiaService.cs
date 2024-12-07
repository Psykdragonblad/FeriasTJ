using FeriasTJ.Application.Interface;
using System.Security.Cryptography;
using System.Text;

namespace FeriasTJ.Infra.Security
{
    public class CriptografiaService : ICriptografiaSerivce
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public CriptografiaService()
        {
            var key = "teste";
            using var sha256 = SHA256.Create();
            _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            _iv = new byte[16]; // IV fixo para simplicidade; considere um IV dinâmico e envie junto com a mensagem.
        }

        public string Encriptar(string mensagem)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);

            sw.Write(mensagem);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }
    }
}