namespace IQueueData.Entities;

public class SwitchRequest : BaseEntity
{
    public Guid RecordId { get; set; }
    public Record? Record { get; set; }
    
    public Guid SwitchWithRecordId { get; set; }
    public Record? SwitchWithRecord { get; set; }

    public bool? IsAccepted { get; set; } = null;
}