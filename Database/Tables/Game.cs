using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiApi.Database.Tables;

[Table("Games",Schema = "public")]
public class Game
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public int MinPlayersCount { get; set; }
    public int MaxPlayersCount { get; set; }
}
