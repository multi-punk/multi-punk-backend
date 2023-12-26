using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Database.Enums;

namespace Infrastructure.Database.Tables;

[Table("ApiKeys",Schema = "public")]
public class ApiKey
{
    [Key]
    public string Key { get; set; }
    [Required]
    public string OwnerId { get; set; }
    public ApiKeyTypes Type { get; set; }
}
