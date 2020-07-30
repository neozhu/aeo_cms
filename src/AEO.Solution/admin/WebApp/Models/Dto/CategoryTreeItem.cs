using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Dto
{
  public class CategoryTreeItem
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string EName { get; set; }
    public string Icon { get; set; }
    public string state { get; set; }
    public IEnumerable<CategoryTreeItem> children { get; set; }
  }
  public class ComboTreeItem {
    public int id { get; set; }
    public string text { get; set; }
    public IEnumerable<ComboTreeItem> children { get; set; }
   }
}