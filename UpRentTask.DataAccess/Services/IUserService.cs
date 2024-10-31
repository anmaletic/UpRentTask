using UprentTask.Library.Models;

namespace UpRentTask.DataAccess.Services;

public interface IUserService
{
    Task<UserModel?> GetById(int id);
    Task<List<UserModel>> GetAll();
}