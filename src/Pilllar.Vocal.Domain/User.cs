using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pilllar.Vocal.Domain
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome � obrigat�rio")]
        [StringLength(50, ErrorMessage = "O campo nome deve conter no m�ximo 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Email � obrigat�rio")]
        [StringLength(50, ErrorMessage = "O campo Email deve conter no m�ximo 50 caracteres")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "O campo Senha � obrigat�rio")]
        //[MinLength(8, ErrorMessage = "O campo Senha deve conter no m�nimo 8 caracteres")]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}
