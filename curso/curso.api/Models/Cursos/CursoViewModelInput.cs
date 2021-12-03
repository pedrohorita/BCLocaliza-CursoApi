using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Cursos
{
    public class CursoViewModelInput
    {
        [Required(ErrorMessage = "O nome do curso é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O descricao do curso é obrigatória")]
        public string Descricao { get; set; }
    }
}
