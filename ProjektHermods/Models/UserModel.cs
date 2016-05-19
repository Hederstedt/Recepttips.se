using System.ComponentModel.DataAnnotations;

namespace ProjektHermods.Models
{
    public class UserModel
    {
        [Key]
        public int userId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}