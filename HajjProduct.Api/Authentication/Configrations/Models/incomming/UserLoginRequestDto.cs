using System.ComponentModel.DataAnnotations;

namespace HajjProduct.Api.Authentication.Configrations.Models.incomming;

public class UserLoginRequestDto
{
    [Required]
    public string UserName { get; set; }
    [Required] 
    public string Password { get; set; }
}
