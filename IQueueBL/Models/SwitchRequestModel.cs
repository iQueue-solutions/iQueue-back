namespace IQueueBL.Models;

public class SwitchRequestModel
{
    public Guid Id { get; set; }
    
    public Guid RecordId { get; set; }
    public RecordModel? Record { get; set; }
    
    public Guid SwitchWithRecordId { get; set; }
    public RecordModel? SwitchWithRecord { get; set; }

    public bool? IsAccepted { get; set; } = null;
}