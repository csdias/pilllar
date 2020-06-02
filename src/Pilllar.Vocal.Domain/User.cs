using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pilllar.Vocal.Domain
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo nome deve conter no máximo 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo Email deve conter no máximo 50 caracteres")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "O campo Senha é obrigatório")]
        //[MinLength(8, ErrorMessage = "O campo Senha deve conter no mínimo 8 caracteres")]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}
