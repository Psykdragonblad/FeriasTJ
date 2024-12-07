using FeriasTJBase.Application.Interface;
using System.Security.Cryptography;
using System.Text;

namespace FeriasTJBase.Infra.Security
{
    public class DescriptografiaService : IDescriptografiaService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public DescriptografiaService()
        {
            string key = Environment.GetEnvironmentVariable("keyferiastj"); // Criar variável de ambiente com esse nome e o valor pode ser qualquer coisa
            // Gera o key e o IV (vector de inicialização)
            using var sha256 = SHA256.Create();
            _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            _iv = new byte[16]; // IV de 128 bits (16 bytes)
        }       

        public string Descriptar(string mensagem)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(mensagem));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
