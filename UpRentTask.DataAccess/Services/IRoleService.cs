namespace UpRentTask.DataAccess.Services;

public interface IRoleService
{
    Task<List<RoleModel>> GetAll();
}