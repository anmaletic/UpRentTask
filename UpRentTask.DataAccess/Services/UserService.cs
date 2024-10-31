namespace UpRentTask.DataAccess.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel?> GetById(int id)
    {
        var result = await _context.Users
            .Include(u => u.CreatedByUser)
            .Include(u => u.ModifiedByUser)
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (result is null) return null;
        
        var user = new UserModel()
        {
            UserId = result.UserId,
            Username = result.Username,
            CreatedDate = result.CreatedDate,
            ModifiedDate = result.ModifiedDate,
            CreatedBy = new UserModel()
            {
                UserId = result.CreatedByUser.UserId,
                Username = result.CreatedByUser.Username,
                CreatedDate = result.CreatedByUser.CreatedDate,
                ModifiedDate = result.CreatedByUser.ModifiedDate
            },
            ModifiedBy = result.ModifiedByUser is null
                ? null
                : new UserModel()
                {
                    UserId = result.ModifiedByUser.UserId,
                    Username = result.ModifiedByUser.Username,
                    CreatedDate = result.ModifiedByUser.CreatedDate,
                    ModifiedDate = result.ModifiedByUser.ModifiedDate
                }
        };

        return user;
    }

    public async Task<List<UserModel>> GetAll()
    {
        var result = await _context.Users
            .Include(u => u.CreatedByUser)
            .Include(u => u.ModifiedByUser)
            .ToListAsync();

        var users = result.Select(u => new UserModel()
        {
            UserId = u.UserId,
            Username = u.Username,
            CreatedDate = u.CreatedDate,
            ModifiedDate = u.ModifiedDate,
            CreatedBy = new UserModel()
            {
                UserId = u.CreatedByUser.UserId,
                Username = u.CreatedByUser.Username,
                CreatedDate = u.CreatedByUser.CreatedDate,
                ModifiedDate = u.CreatedByUser.ModifiedDate
            },
            ModifiedBy = u.ModifiedByUser is null
                ? null
                : new UserModel()
                {
                    UserId = u.ModifiedByUser.UserId,
                    Username = u.ModifiedByUser.Username,
                    CreatedDate = u.ModifiedByUser.CreatedDate,
                    ModifiedDate = u.ModifiedByUser.ModifiedDate
                }
        }).ToList();

        return users;
    }
}