using BootstrapBlazor.Components;
using BootstrapBlazorApp.Model.Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class RouteList
    {
        private List<ProxyRoute> list = new List<ProxyRoute>();
        [NotNull]
        [Inject]
        private  HttpClient _httpclient { get; set; }
        [NotNull]
        [Inject]
        private IConfiguration _configuration { get; set; }
 
        public  IEnumerable<ProxyRoute> GetRouteList()
        {
            // var response = httpclient.GetAsync("ProxyRoute").GetAwaiter().GetResult();
            return    _httpclient.GetFromJsonAsync<IEnumerable<ProxyRoute>>("ProxyRoute").GetAwaiter().GetResult();
        }
        protected override async Task OnInitializedAsync()
        {

            
            //var baseUri = _configuration["BackendUrl"];
            //_httpclient.BaseAddress = new Uri(baseUri);
            ////GetRouteList()
            //list =  GetRouteList().ToList();
            await base.OnInitializedAsync();
        }


        /// <summary>
        /// 获得 默认数据集合
        /// </summary>
        private readonly IEnumerable<SelectedItem> MethodList = new SelectedItem[]
        {
            new SelectedItem ("GET","GET"),
            new SelectedItem ("HEAD","HEAD"),
            new SelectedItem ("POST","POST"),
            new SelectedItem ("PUT","PUT"),
            new SelectedItem ("DELETE","DELETE"),
            new SelectedItem ("OPTIONS","OPTIONS"),
            new SelectedItem ("PPATCHUT","PATCH"),
        };
    }
}
