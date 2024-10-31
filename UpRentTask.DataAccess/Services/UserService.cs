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

        return result is null ? null : MapToUserModel(result);
    }
    
    public async Task<List<UserModel>> GetAll()
    {
        var result = await _context.Users
            .Include(u => u.CreatedByUser)
            .Include(u => u.ModifiedByUser)
            .AsNoTracking()
            .ToListAsync();

        var users = result
            .Select(MapToUserModel)
            .ToList();

        return users;
    }

    private UserModel MapToUserModel(User user)
    {
        return new UserModel()
        {
            UserId = user.UserId,
            Username = user.Username,
            CreatedDate = user.CreatedDate,
            ModifiedDate = user.ModifiedDate,
            CreatedBy = new UserModel()
            {
                UserId = user.CreatedByUser.UserId,
                Username = user.CreatedByUser.Username,
                CreatedDate = user.CreatedByUser.CreatedDate,
                ModifiedDate = user.CreatedByUser.ModifiedDate
            },
            ModifiedBy = user.ModifiedByUser is null
                ? null
                : new UserModel()
                {
                    UserId = user.ModifiedByUser.UserId,
                    Username = user.ModifiedByUser.Username,
                    CreatedDate = user.ModifiedByUser.CreatedDate,
                    ModifiedDate = user.ModifiedByUser.ModifiedDate
                }
        };
    }
    
    public async Task<bool> Delete(int deleteId, int modifyId )
    {
        var result = await _context.Users
            .Where(x => x.UserId == deleteId && x.Visible)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Visible, false)
                    .SetProperty(p => p.ModifiedDate, DateTime.Now)
                    .SetProperty(p => p.ModifiedByUserId, modifyId)
                );
        
        return result > 0;
    }
}