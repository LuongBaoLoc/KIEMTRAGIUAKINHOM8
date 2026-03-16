using System.ComponentModel.DataAnnotations;

namespace KTGKNhom8.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}