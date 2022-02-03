using BootstrapBlazorApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazorApp.Model.Entities
{
    public class RouteQueryParameter
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the query parameter to look for.
        /// This field is case insensitive and required.
        /// </summary>
        public string Name { get; init; } = default!;

        /// <summary>
        /// A collection of acceptable query parameter values used during routing.
        /// </summary>
        public string? Values { get; init; }

        /// <summary>
        /// Specifies how query parameter values should be compared (e.g. exact matches Vs. contains).
        /// Defaults to <see cref="QueryParameterMatchMode.Exact"/>.
        /// </summary>
        public QueryParameterMatchMode Mode { get; init; }

        /// <summary>
        /// Specifies whether query parameter value comparisons should ignore case.
        /// When <c>true</c>, <see cref="StringComparison.Ordinal" /> is used.
        /// When <c>false</c>, <see cref="StringComparison.OrdinalIgnoreCase" /> is used.
        /// Defaults to <c>false</c>.
        /// </summary>
        public bool IsCaseSensitive { get; init; }
        public int ProxyMatchId { get; set; }
        public virtual ProxyMatch ProxyMatch { get; set; }
    }

    public enum QueryParameterMatchMode
    {
        /// <summary>
        /// Query string must match in its entirety,
        /// Subject to case sensitivity settings.
        /// Only single query parameter name supported. If there are multiple query parameters with the same name then the match fails.
        /// </summary>
        Exact,

        /// <summary>
        /// Query string key must be present and substring must match for each of the respective query string values.
        /// Subject to case sensitivity settings.
        /// Only single query parameter name supported. If there are multiple query parameters with the same name then the match fails.
        /// </summary>
        Contains,

        /// <summary>
        /// Query string key must be present and value must not match for each of the respective query string values.
        /// Subject to case sensitivity settings.
        /// If there are multiple values then it needs to not contain ANY of the values 
        /// Only single query parameter name supported. If there are multiple query parameters with the same name then the match fails.
        /// </summary>
        NotContains,

        /// <summary>
        /// Query string key must be present and prefix must match for each of the respective query string values.
        /// Subject to case sensitivity settings.
        /// Only single query parameter name supported. If there are multiple query parameters with the same name then the match fails.
        /// </summary>
        Prefix,

        /// <summary>
        /// Query string key must exist and contain any non-empty value.
        /// </summary>
        Exists
    }
}
