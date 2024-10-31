namespace UpRentTask.DataAccess.Services;

public class RoleService : IRoleService
{
    private readonly AppDbContext _context;
    
    public RoleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoleModel>> GetAll()
    {
        var result = await _context.Roles
            .AsNoTracking()
            .ToListAsync();

        var roles = result
            .Select(MapToRoleModel)
            .ToList();

        return roles;
    }

    private RoleModel MapToRoleModel(Role role)
    {
        return new RoleModel()
        {
            Id = role.RoleId,
            Name = role.RoleName,
        };
    }
}