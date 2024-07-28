using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models;
public class Category
{
	#region Variables

	[Key] public int ID { get; set; }
	[Required] public string Name { get; set; }

	[DisplayName("Display Order")]
	[Range(1, 50, ErrorMessage = "Display Order msust be between 1 and 50")]
	public int DisplayOrder { get; set; }

	public DateTime CreatedDateTime { get; set; } = DateTime.Now;

	#endregion
}
