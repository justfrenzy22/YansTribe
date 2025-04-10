using System.Security.Cryptography;
using System.Text;
using bll.interfaces;

namespace server.services
{
    public class HashService : IHashService
    {
        public string hash(string pass) =>
            Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass)));
    }
}