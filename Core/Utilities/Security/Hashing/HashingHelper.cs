using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash, out byte[] passwordSalt) //Burası verdiğimiz bir password'un salt ve hash değerini oluşturmaya yarıyor 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // her kullanıcı için o an bir key oluşturur
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //string bir değeri byte'a dönüştürmek için bu kod
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) //kullanıcının gönderdiği password ile veritabanımızdaki password eşleşiyor mu bakıyoruz
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //HMACSHA512 burada bizden key istiyor o yüzden verdik
            {
                var computedHash = passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
