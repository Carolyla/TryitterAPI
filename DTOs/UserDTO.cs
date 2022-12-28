using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryitterApi.DTOs
{
    public class UserDTO
    {
        public string Email {get; set;}
        public string Password {get; set;}
        public string ConfirmPassword {get; set;}    
    }

}