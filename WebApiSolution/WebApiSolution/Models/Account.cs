using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSolution.Models;

[Table(nameof(Account))]
public class Account
{
    [Key]
    public int Id { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public DateTime CreatedAt { get; set; }
}