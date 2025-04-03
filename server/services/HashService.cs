using System.Security.Cryptography;
using System.Text;

namespace server.services
{
    public class HashService : IHashService
    {
        public string hash(string pass) =>
            Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass)));

        // {
        // using (SHA256 sha = SHA256.Create())
        // {
        //     byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
        //     return Convert.ToBase64String(bytes);
        // }
        // }



    }
}