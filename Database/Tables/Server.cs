using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MultiApi.Database.Tables;

[Table("Servers",Schema = "public")]
public class Server
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string GameId { get; set; }
    public string URL { get; set; }
    public int Port { get; set; }
    public bool IsInUse { get; set; }
}
