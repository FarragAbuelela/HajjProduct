namespace HajjProduct.Domain.Models;

public class BaseEntity
{
    public Guid Id { get; set; }
    public int status { get; set; } = 1;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
