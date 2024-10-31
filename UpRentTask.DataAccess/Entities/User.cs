using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UpRentTask.DataAccess.Entities;

[Table("User")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string Username { get; set; } = null!;

    public int CreatedByUserId { get; set; }

    public int? ModifiedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    public bool Locked { get; set; }

    public bool Visible { get; set; }

    public int Version { get; set; }

    [ForeignKey("CreatedByUserId")]
    [InverseProperty("InverseCreatedByUser")]
    public virtual User CreatedByUser { get; set; } = null!;

    [InverseProperty("CreatedByUser")]
    public virtual ICollection<User> InverseCreatedByUser { get; set; } = new List<User>();

    [InverseProperty("ModifiedByUser")]
    public virtual ICollection<User> InverseModifiedByUser { get; set; } = new List<User>();

    [ForeignKey("ModifiedByUserId")]
    [InverseProperty("InverseModifiedByUser")]
    public virtual User? ModifiedByUser { get; set; }

    [InverseProperty("CreatedByUser")]
    public virtual ICollection<Role> RoleCreatedByUsers { get; set; } = new List<Role>();

    [InverseProperty("ModifiedByUser")]
    public virtual ICollection<Role> RoleModifiedByUsers { get; set; } = new List<Role>();

    [InverseProperty("CreatedByUser")]
    public virtual ICollection<UserRole> UserRoleCreatedByUsers { get; set; } = new List<UserRole>();

    [InverseProperty("ModifiedByUser")]
    public virtual ICollection<UserRole> UserRoleModifiedByUsers { get; set; } = new List<UserRole>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();
}
