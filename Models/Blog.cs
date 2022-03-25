using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Body { get; set; } = string.Empty;
        [Required]
        public User Author { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        [Required]
        public int Upvotes { get; set; } = 0;
    }
}
