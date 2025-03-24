namespace HajjProduct.Domain.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public DateTime DateOfBirth { get; set; }
}
