namespace Domain.Common;

public record BaseEntitySoftDelete<T> : BaseEntity where T : class
{
    public bool IsDeleted { get; set; } = false;

    public virtual void Update(T entity)
    {
        throw new NotImplementedException();
    }
}