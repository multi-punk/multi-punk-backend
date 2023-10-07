using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiApi.Database.Tables;

[Table("Roles",Schema = "public")]
public class Role
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
}
