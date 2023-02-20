namespace FastDeliveryApi.Repositories.Interfaces;

public interface IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}