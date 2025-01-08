using Notification.Domain.Share;

namespace Notification.Domain.Email;

public interface IEmailRepository
{
    public Task CreateAsync(Email email);
    public Task UpdateAsync(Email email);
    public Task DeleteAsync(long id);
    public Task<List<Email>> GetAllAsync();
    public Task<Email> GetByIdAsync(long id);
    public Task<List<Email>> GetByEmailAddressAsync(string Email);
    public Task<List<Email>> GetByStatusAsync(Status status);
}
