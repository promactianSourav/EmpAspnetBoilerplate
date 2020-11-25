using System.ComponentModel.DataAnnotations;

namespace EmpAspnetBoilerplate.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}