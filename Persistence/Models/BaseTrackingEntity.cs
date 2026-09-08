namespace groceries_helper.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class BaseTrackingEntity
{
    public int Id { get; private set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; }
}

public class BaseTrackingEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseTrackingEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasIndex(x => new { x.IsActive, x.Id });
        builder.HasQueryFilter(x => x.IsActive);
        builder.HasIndex(x => x.CreatedAt).IsDescending();
    }
}