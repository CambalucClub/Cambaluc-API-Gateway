﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazorApp.Model.Entity
{
    /// <summary>
    /// Describes a destination of a cluster.
    /// </summary>
    public class Destination
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        /// <summary>
        /// Address of this destination. E.g. <c>https://127.0.0.1:123/abcd1234/</c>.
        /// </summary>
        [Display(Name = "域名/ip")]
        public string Address { get; set; }

        /// <summary>
        /// Endpoint accepting active health check probes. E.g. <c>http://127.0.0.1:1234/</c>.
        /// </summary>
        [Display(Name = "Health地址")]
        public string Health { get; set; }
      
        public string ClusterId { get; set; }
        public virtual Cluster Cluster { get; set; }

        /// <summary>
        /// Arbitrary key-value pairs that further describe this destination.
        /// </summary>
        public virtual List<Metadata> Metadata { get; set; }
    }
}