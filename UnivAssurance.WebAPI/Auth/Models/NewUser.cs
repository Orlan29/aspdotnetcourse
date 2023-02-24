
using System.ComponentModel.DataAnnotations;

namespace UnivAssurance.Auth.Models;

public class NewUser
{
    [Required(ErrorMessage = "Le nom est obligatoire")]
    public string UserName {get; set;}

    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    public string Password {get; set;}

    [Required(ErrorMessage = "L'email est obligatoire")]
    public string Email {get; set;}
}