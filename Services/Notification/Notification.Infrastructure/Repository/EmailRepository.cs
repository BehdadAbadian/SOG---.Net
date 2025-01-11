using Microsoft.EntityFrameworkCore;
using Notification.Domain.Email;
using Notification.Domain.Share;
using Notification.Infrastructure.Database;

namespace Notification.Infrastructure.Repository;

public class EmailRepository : IEmailRepository
{
    private readonly NotificationContext _context;

    public EmailRepository(NotificationContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Email email)
    {
        await _context.Emails.AddAsync(email);
    }

    public void Delete(Email email)
    {
        _context.Emails.Remove(email);
    }

    public async Task<bool> Exits(long id)
    {
        return await _context.Emails.AnyAsync(x => x.Id == id);
    }

    public async Task<List<Email>> GetAllAsync()
    {
        return await _context.Emails.AsNoTracking().ToListAsync();
    }

    public async Task<List<Email>> GetByEmailAddressAsync(string Email)
    {
        return await _context.Emails.AsNoTracking().Where(x => x.EmailAddress == Email).ToListAsync();
    }

    public async Task<Email> GetByIdAsync(long id)
    {
        
        return await _context.Emails.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Email>> GetByStatusAsync(Status status)
    {
        return await _context.Emails.Where(x => x.EmailStatus == status).ToListAsync();
    }

    public void Update(Email email)
    {
        _context.Emails.Update(email);
    }
}
