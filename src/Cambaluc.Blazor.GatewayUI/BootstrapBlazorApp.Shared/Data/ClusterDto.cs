using BootstrapBlazor.Components;
using BootstrapBlazorApp.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazorApp.Shared.Data
{
    public class ClusterDto
    {

        /// <summary>
        /// The Id for this cluster. This needs to be globally unique.
        /// </summary>
        [Display(Name = "名称(id)全局唯一")]
        public string Id { get; set; }

        /// <summary>
        /// Load balancing policy.
        /// </summary>
        [Display(Name = "负载均衡策略")]
        public string LoadBalancingPolicy { get; set; }

        /// <summary>
        /// Session affinity options.
        /// </summary>
        public virtual SessionAffinityConfig SessionAffinity { get; set; } = new SessionAffinityConfig();

        /// <summary>
        /// Health checking options.
        /// </summary>
        public virtual HealthCheckOptions HealthCheck { get; set; } = new HealthCheckOptions();

        /// <summary>
        /// Options of an HTTP client that is used to call this cluster.
        /// </summary>
        public virtual HttpClientConfig HttpClient { get; set; } = new HttpClientConfig();

        /// <summary>
        /// Options of an outgoing HTTP request.
        /// </summary>
        public virtual ForwarderRequest HttpRequest { get; set; } = new ForwarderRequest();

        /// <summary>
        /// The set of destinations associated with this cluster.
        /// </summary>
       
        public virtual List<Destination> Destinations { get; set; } = new List<Destination>();

        /// <summary>
        /// Arbitrary key-value pairs that further describe this cluster.
        /// </summary>
        public virtual List<Metadata> Metadata { get; set; } = new List<Metadata>();

        public virtual List<ProxyRoute> ProxyRoutes { get; set; } = new List<ProxyRoute>();
    }
}
