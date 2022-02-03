using BootstrapBlazorApp.Shared.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class BlazorComponent1
    {
        [Inject] public HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
            await base.OnInitializedAsync();
        }




    }
}
