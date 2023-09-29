using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_api.Database.Tables;

[Table("Party",Schema = "public")]
public class Party
{
    [Key]
    public string Id { get; set; }
    public string? OwnerId { get; set; }
    public string? Participants { get; set; }
}
