using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MultiApi.Auth;

namespace MultiApi.Database.Tables;

[Table("ApiKeys",Schema = "public")]
public class ApiKey
{
    [Key]
    public string Key { get; set; }
    [Required]
    public string ownerId { get; set; }
    public ApiKeyTypes type { get; set; }
}
