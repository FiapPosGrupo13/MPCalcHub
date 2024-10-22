using MPCalcHub.Domain.Entities.Interfaces;

namespace MPCalcHub.Domain.Entities;

public abstract class BaseEntity : Identifier, IBaseEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public bool Removed { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }

    public virtual void PrepareToInsert(Guid createdBy)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public virtual void PrepareToUpdate(Guid createdBy)
    {
        UpdatedAt = DateTime.Now;
        UpdatedBy = Guid.NewGuid();
    }

    public virtual void PrepareToRemove(Guid createdBy)
    {
        Removed = true;
        RemovedAt = DateTime.Now;
        RemovedBy = Guid.NewGuid();
    }
}