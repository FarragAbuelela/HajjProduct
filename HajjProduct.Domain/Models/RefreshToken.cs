namespace HajjProduct.Domain.Models;

public class RefreshToken : BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; }
    public string UserId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public string JwtId { get; set; }
    public DateTime ExpiryDate { get; set; }
}