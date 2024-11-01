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
            .Include(u => u.UserRoleUsers)
            .ThenInclude(u => u.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == id);

        return result is null ? null : MapToUserModel(result);
    }
    
    public async Task<List<UserModel>> GetAll()
    {
        var result = await _context.Users
            .Include(u => u.CreatedByUser)
            .Include(u => u.ModifiedByUser)
            .Include(u => u.UserRoleUsers)
            .ThenInclude(u => u.Role)
            .AsNoTracking()
            .ToListAsync();

        var users = result
            .Select(MapToUserModel)
            .ToList();

        return users;
    }
    
    public async Task<bool> Add(UserModel user, int userId)
    {
        var newUser = new User()
        {
            Username = user.Username,
            CreatedByUserId = userId,
            Visible = true,
            UserRoleUsers = user.Roles.Select(role => new UserRole()
            {
                RoleId = role.Id,
                CreatedByUserId = userId,
                Visible = true
            }).ToList() 
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int deleteId, int modifyId )
    {
        var userResult = await _context.Users
            .Where(x => x.UserId == deleteId && x.Visible)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Visible, false)
                    .SetProperty(p => p.ModifiedDate, DateTime.Now)
                    .SetProperty(p => p.ModifiedByUserId, modifyId)
                );

        var roleResult = await _context.UserRoles
            .Where(x => x.UserId == deleteId && x.Visible)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Visible, false)
                    .SetProperty(p => p.ModifiedDate, DateTime.Now)
                    .SetProperty(p => p.ModifiedByUserId, modifyId)
                );
        
        return userResult > 0;
    }
    
    public async Task<bool> Update(UserModel updatedUser, int modifyId)
    {
        var existingUser = await _context.Users
            .Include(u => u.UserRoleUsers)
            .FirstOrDefaultAsync(u => u.UserId == updatedUser.UserId);

        if (existingUser is null) return false;
        
        existingUser.Username = updatedUser.Username;
        existingUser.ModifiedByUserId = modifyId;
        existingUser.ModifiedDate = DateTime.Now;
        
        foreach (var role in existingUser.UserRoleUsers)
        {
            if (updatedUser.Roles.All(r => r.Id != role.RoleId))
            {
                role.Visible = false;
                role.ModifiedDate = DateTime.Now;
                role.ModifiedByUserId = modifyId;
            }
        }
        
        var newRoles = updatedUser.Roles
            .Where(r => existingUser.UserRoleUsers.All(x => x.RoleId != r.Id))
            .Select(r => new UserRole
            {
                RoleId = r.Id,
                CreatedByUserId = modifyId,
                Visible = true
            }).ToList();

        foreach (var role in newRoles)
        {
            existingUser.UserRoleUsers.Add(role);
        }
        
        _context.Update(existingUser);
        await _context.SaveChangesAsync();
        
        return true;
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
                },
            Roles = user.UserRoleUsers.Select(x => new RoleModel()
            {
                Id = x.Role.RoleId,
                Name = x.Role.RoleName,
                IsSelected = true
            }).ToList()
        };
    }

}