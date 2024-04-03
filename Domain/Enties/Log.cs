namespace Domain.Entities
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Log : BaseEntity<int>
  {
    public string? Username { get; set; }
    public string Description { get; set; }
  }
}
