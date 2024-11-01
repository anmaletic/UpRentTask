namespace UpRentTask.Library.Models;

public interface ILoggedInUser
{
    int UserId { get; set; }
    string Username { get; set; }
    DateTime CreatedDate { get; set; }
    UserModel CreatedBy { get; set; }
    DateTime? ModifiedDate { get; set; }
    UserModel? ModifiedBy { get; set; }
    List<RoleModel> Roles { get; set; }
}