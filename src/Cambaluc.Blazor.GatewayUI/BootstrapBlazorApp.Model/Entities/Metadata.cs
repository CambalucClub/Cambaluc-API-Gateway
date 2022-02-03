using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazorApp.Model.Entity
{
    public class Metadata : KeyValueEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
