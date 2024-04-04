namespace Domain.Entities
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class BaseEntity<TID>
  {
    public TID Id { get; set; }
    public DateTime CreatedAt  { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
  }
}
