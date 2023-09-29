using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_api.Database.Tables;

[Table("Users",Schema = "public")]
public class User
{
    [Key]
    public string Id { get; set; }
    public string? RoleId { get; set; }
    public string? PartyId { get; set; }
}
