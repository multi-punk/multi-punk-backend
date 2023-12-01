using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiApi.Database.Tables;

[Table("Statistic",Schema = "public")]
public class Statistic
{
    [Key]
    public string UserId { get; set; }
    public string Game { get; set; }
    public int score { get; set; }
}
