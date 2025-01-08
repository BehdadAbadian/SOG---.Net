using Notification.Domain.Email;
using Notification.Domain.Share;

namespace Notification.Infrastructure.Repository;

public class EmailRepository : IEmailRepository
{
    public Task CreateAsync(Email email)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Email>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Email>> GetByEmailAddressAsync(string Email)
    {
        throw new NotImplementedException();
    }

    public Task<Email> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Email>> GetByStatusAsync(Status status)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Email email)
    {
        throw new NotImplementedException();
    }
}
