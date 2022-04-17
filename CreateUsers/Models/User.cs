using System.ComponentModel.DataAnnotations;

namespace CreateUsers.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}