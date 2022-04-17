using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}