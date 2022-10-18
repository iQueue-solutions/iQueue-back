namespace IQueueData.Entities
{
    /// <summary>
    /// This class represents BaseEntity object.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique identificator.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// BaseEntity constructor.
        /// </summary>
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
