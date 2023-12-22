using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiApi.Database.Tables;

[Table("Users",Schema = "public")]
public class User
{
    [Key]
    public string Id { get; set; }
    public string XUId { get; set; }
    public string? RoleId { get; set; }
    public string[]? Permissions { get; set;}
}
