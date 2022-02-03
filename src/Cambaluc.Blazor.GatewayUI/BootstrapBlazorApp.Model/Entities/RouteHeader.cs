using System;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazorApp.Model.Entity
{
    public class RouteHeader
    {
        [Key]
     
        public int Id { get; set; }
        /// <summary>
        /// Name of the header to look for.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A collection of acceptable header values used during routing. Only one value must match.
        /// The list must not be empty unless using <see cref="HeaderMatchMode.Exists"/>.
        /// </summary>
        public string Values { get; set; }

        /// <summary>
        /// Specifies how header values should be compared (e.g. exact matches Vs. by prefix).
        /// Defaults to <see cref="HeaderMatchMode.ExactHeader"/>.
        /// </summary>
        public HeaderMatchMode Mode { get; set; }

        /// <summary>
        /// Specifies whether header value comparisons should ignore case.
        /// When <c>true</c>, <see cref="StringComparison.Ordinal" /> is used.
        /// When <c>false</c>, <see cref="StringComparison.OrdinalIgnoreCase" /> is used.
        /// Defaults to <c>false</c>.
        /// </summary>
        public bool IsCaseSensitive { get; set; }

        public int ProxyMatchId { get; set; }
        public virtual ProxyMatch ProxyMatch { get; set; }
    }

    public enum HeaderMatchMode
    {
        /// <summary>
        /// The header must match in its entirety, subject to case sensitivity settings.
        /// Only single headers are supported. If there are multiple headers with the same name then the match fails.
        /// </summary>
        ExactHeader,
        /// <summary>
        /// The header must match by prefix, subject to case sensitivity settings.
        /// Only single headers are supported. If there are multiple headers with the same name then the match fails.
        /// </summary>
        HeaderPrefix,
        /// <summary>
        /// The header must match by contains, subject to case sensitivity settings.
        /// Only single headers are supported. If there are multiple headers with the same name then the match fails.
        /// </summary>
        Contains,
        /// <summary>
        /// The header name must exist and the value must be non-empty and not match, subject to case sensitivity settings.
        /// If there are multiple values then it needs to not contain ANY of the values
        /// Only single headers are supported. If there are multiple headers with the same name then the match fails.
        /// </summary>
        NotContains,
        /// <summary>
        /// The header must exist and contain any non-empty value.
        /// </summary>
        Exists,
    }
}
