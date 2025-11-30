using System.ComponentModel.DataAnnotations;

namespace EdgeView.Infrastructure.Entities;

public class CacheEntry
{
    [Key]
    public string CacheKey { get; set; } = default!;
    public string JsonValue { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
