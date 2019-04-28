

namespace SchoolLibrary.Models
{
    using System.Security.Cryptography;
    using SchoolLibrary.Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public User()
        {

        }
        public User(string name, string password)
        {
    
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
