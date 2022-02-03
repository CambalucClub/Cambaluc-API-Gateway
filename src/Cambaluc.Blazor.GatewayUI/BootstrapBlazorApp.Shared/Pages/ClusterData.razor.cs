using BootstrapBlazor.Components;
using BootstrapBlazorApp.Model.Entity;
using BootstrapBlazorApp.Shared.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;


namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class ClusterData : ComponentBase
    {

        private List<ClusterDto> dtos = new List<ClusterDto>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        [NotNull]
        private ValidateForm? ComplexForm { get; set; }
        [NotNull]
        private ClusterDto? ComplexModel { get; set; }

        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
        {
            new SelectedItem ("RoundRobin","RoundRobin"),
             new SelectedItem ("Random","Random"),
              new SelectedItem ("LeastRequests","LeastRequests"),
                 new SelectedItem ("PowerOfTwoChoices","PowerOfTwoChoices"),


        };
    }
}
