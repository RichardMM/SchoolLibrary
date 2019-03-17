
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using SchoolLibrary.Utilities;

namespace SchoolLibrary.Models
{
    class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public User()
        {

        }
        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }

        public bool ConfirmPassword(string testPassword)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                EncryptionTools obj = new EncryptionTools(md5Hash);
                return obj.VerifyMd5Hash(input:testPassword,hash:Password);


            }

        }

    }
}
