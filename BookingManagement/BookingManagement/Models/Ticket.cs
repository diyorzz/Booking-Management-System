using BookingManagement.Models.Enums;
namespace BookingManagement.Models;

public class Ticket
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.UtcNow.AddHours(5);
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Pending;
}
