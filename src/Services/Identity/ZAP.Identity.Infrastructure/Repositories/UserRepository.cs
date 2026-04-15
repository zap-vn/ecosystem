using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZAP.Identity.Application.Common.Interfaces;
using ZAP.Identity.Infrastructure.Data;

namespace ZAP.Identity.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;

    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<dynamic> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.email == email)
            ?? (dynamic)null!;
    }

    public async Task<dynamic> GetByPhoneAsync(string phone)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.phone_number == phone)
            ?? (dynamic)null!;
    }
}
