﻿namespace IQueueBL.Models;

/// <summary>
/// Record in queue model.
/// </summary>
public class RecordModel
{
    /// <summary>
    /// Model id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User In queue Id.
    /// </summary>
    public Guid ParticipantId { get; set; }

    /// <summary>
    /// Number of laboratory work.
    /// </summary>
    public string? LabNumber { get; set; }

    /// <summary>
    /// Index number in queue.
    /// </summary>
    public int Index { get; set; }
    
}