using BookingManagement.Database;
using BookingManagement.Interfaces;
using BookingManagement.Models;
using BookingManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement.Services;

public class TicketService : ITicketService
{
    private readonly BookingManagementDbContext _context;

    public TicketService(BookingManagementDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetTicketsAsync()
    {
        return await _context.Tickets
            .Where(t => t.Status == TicketStatus.Pending || t.Status == TicketStatus.Processing)
            .ToListAsync();

    }

    public async Task<Ticket?> GetNextTicketAsync()
    {
        var ticket = await _context.Tickets
        .Where(t => t.Status == TicketStatus.Pending)
        .OrderByDescending(t => t.Priority)
        .ThenBy(t => t.CreationTime)
        .FirstOrDefaultAsync();

        if (ticket != null)
        {
            ticket.Status = TicketStatus.Processing;
            await _context.SaveChangesAsync();
        }

        return ticket;
    }

    public async Task AddTicketAsync(int customerId, TicketPriority priority)
    {
        var ticket = new Ticket
        {
            CustomerId = customerId,
            Priority = priority
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task CompleteTicketAsync(int ticketId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);

        if (ticket != null && ticket.Status == TicketStatus.Processing)
        {
            ticket.Status = TicketStatus.Completed;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Ticket>> GetCompletedTicketsAsync()
    {
        return await _context.Tickets
            .Where(t => t.Status == TicketStatus.Completed)
            .ToListAsync();
    }

    public async Task DeleteTicketAsync(int ticketId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);

        if (ticket is not null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
