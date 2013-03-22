namespace Reader.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SignInViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(140)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(140)]
        public string Password { get; set; }
    }
}