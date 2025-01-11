using Notification.Domain.Share;

namespace Notification.Domain.Email;

public interface IEmailRepository
{
    public Task<bool> Exits(long id);
    public Task CreateAsync(Email email);
    public void Update(Email email);
    public void Delete(Email email);
    public Task<List<Email>> GetAllAsync();
    public Task<Email> GetByIdAsync(long id);
    public Task<List<Email>> GetByEmailAddressAsync(string Email);
    public Task<List<Email>> GetByStatusAsync(Status status);
}
