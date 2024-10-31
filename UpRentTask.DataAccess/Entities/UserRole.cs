using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UpRentTask.DataAccess.Entities;

[Table("UserRole")]
public partial class UserRole
{
    [Key]
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    [StringLength(2000)]
    public string? Comment { get; set; }

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
    [InverseProperty("UserRoleCreatedByUsers")]
    public virtual User CreatedByUser { get; set; } = null!;

    [ForeignKey("ModifiedByUserId")]
    [InverseProperty("UserRoleModifiedByUsers")]
    public virtual User? ModifiedByUser { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserRoles")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserRoleUsers")]
    public virtual User User { get; set; } = null!;
}
