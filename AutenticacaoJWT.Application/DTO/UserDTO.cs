using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo de nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo de nome deve ter no máximo 50 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo de email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo de senha é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
