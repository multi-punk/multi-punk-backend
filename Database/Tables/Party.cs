using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiApi.Database.Tables;

[Table("Party",Schema = "public")]
public class Party
{
    [Key]
    public string Id { get; set; }
    public string? OwnerId { get; set; }
    public List<string>? Participants { get; set; }
}
