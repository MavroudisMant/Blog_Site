using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "char(6)")]
        public string Type { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(300)]
        public string Password { get; set; }
        //[Required]
        [MaxLength(128)]
        public byte[]? PasswordSalt { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Blog>? Blogs { get; set; }

    }
}
