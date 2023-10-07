using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiApi.Database.Tables;

[Table("Permissions",Schema = "public")]
public class Permission
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
}
