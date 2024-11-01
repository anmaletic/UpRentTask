namespace UpRentTask.DataAccess.Services;

public interface IUserService
{
    Task<UserModel?> GetById(int id);
    Task<List<UserModel>> GetAll();
    Task<bool> Add(UserModel user, int userId);
    Task<bool> Delete(int deleteId, int modifyId);
}