using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazorApp.Model.Entity
{
    public class SessionAffinityOptionSetting : KeyValueEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
