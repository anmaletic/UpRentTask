namespace UpRentTask.Library.Models;

public class UserModel
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public DateTime CreatedDate { get; set; }
    public UserModel CreatedBy { get; set; } = null!;
    public DateTime? ModifiedDate { get; set; }
    public UserModel? ModifiedBy { get; set; }
}