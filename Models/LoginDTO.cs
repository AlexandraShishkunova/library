using System.ComponentModel.DataAnnotations.Schema;

namespace libraryWeb.Models
{
    [Table("register")]

    public class LoginDTO
    { 
       public int Id { get; set; }
       public string Username { get; set; }
       public string Password { get; set; }
       
    }
}
