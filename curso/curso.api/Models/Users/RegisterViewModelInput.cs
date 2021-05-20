using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Users
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage="O Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O Password é obrigatório")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        public string Email { get; set; }
    }
}