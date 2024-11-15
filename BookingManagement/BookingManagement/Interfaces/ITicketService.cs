using BookingManagement.Models;
using BookingManagement.Models.Enums;

namespace BookingManagement.Interfaces;

public interface ITicketService
{
    Task AddTicketAsync(int customerId, TicketPriority priority);
    Task<Ticket?> GetNextTicketAsync();
    Task CompleteTicketAsync(int ticketId);
    Task<List<Ticket>> GetTicketsAsync();
    Task<List<Ticket>> GetCompletedTicketsAsync();
    Task DeleteTicketAsync(int ticketId);
}
