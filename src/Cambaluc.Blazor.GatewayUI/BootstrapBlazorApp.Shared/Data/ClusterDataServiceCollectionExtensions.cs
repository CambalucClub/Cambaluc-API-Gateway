using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using BootstrapBlazorApp.Model.Entity;

namespace BootstrapBlazorApp.Shared.Data
{

    /// <summary>
    /// BootstrapBlazor 服务扩展类
    /// </summary>
    public static class ClusterDataServiceCollectionExtensions
    {
        /// <summary>
        /// 增加 PetaPoco 数据库操作服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddClusterDataService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDataService<>), typeof(ClusterDataService<>));
            return services;
        }
    }

    /// <summary>
    /// 演示网站示例数据注入服务实现类
    /// </summary>
    internal class ClusterDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
    {
        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<TModel>, string, SortOrder, IEnumerable<TModel>>> SortLambdaCache = new();

        [NotNull]
        private List<TModel>? Items { get; set; }

        private IConfiguration _configuration { get; set; }

        private HttpClient _httpclient { get; set; }

        public ClusterDataService(IConfiguration configuration, HttpClient httpclient)
        {
            _configuration = configuration;
            _httpclient = httpclient;
            var baseUri = _configuration["BackendUrl"];
            _httpclient.BaseAddress = new Uri(baseUri);
        }
        /// <summary>
        /// 查询操作方法
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions options)
        {
            if (typeof(TModel).Name == "ClusterDto")
            {
                List<ClusterDto> dtos = new List<ClusterDto>();
                var items = _httpclient.GetFromJsonAsync<IEnumerable<ClusterDto>>("Cluster").GetAwaiter().GetResult();
                if (items == null || items.ToArray().Count() == 0)
                {
                    ClusterDto dto = new ClusterDto();
                    dto.Id = "cessa";
                    dto.LoadBalancingPolicy = "ah";
                    dtos.Add(dto);
                }
                else
                {
                    dtos.AddRange(items.ToArray());
                }

                var isSearched = false;
                // 过滤
                var isFiltered = false;
                if (options.Filters.Any())
                {
                    isFiltered = true;
                }
                // 排序
                var isSorted = false;
                return Task.FromResult(new QueryData<TModel>()
                {
                    Items = dtos.Cast<TModel>().ToList(),
                    TotalCount = dtos.Count,
                    IsFiltered = isFiltered,
                    IsSorted = isSorted,
                    IsSearch = isSearched
                });
            }
            else
            {
                List<ProxyRoute> dtos = new List<ProxyRoute>();
               var items = _httpclient.GetFromJsonAsync<IEnumerable<ProxyRoute>>("ProxyRoute").GetAwaiter().GetResult();
                if (items == null || items.ToArray().Count() == 0)
                {
                   
                }
                else
                {
                    dtos.AddRange(items.ToArray());
                }

                var isSearched = false;
                // 过滤
                var isFiltered = false;
                if (options.Filters.Any())
                {
                    isFiltered = true;
                }
                // 排序
                var isSorted = false;
                return Task.FromResult(new QueryData<TModel>()
                {
                    Items = dtos.Cast<TModel>().ToList(),
                    TotalCount = dtos.Count,
                    IsFiltered = isFiltered,
                    IsSorted = isSorted,
                    IsSearch = isSearched
                });
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Task<bool> SaveAsync(TModel model, ItemChangedType changedType)
        {
            var ret = false;
            if (model is ClusterDto dto)
            {
                var res = _httpclient.PostAsJsonAsync<ClusterDto>("Cluster", dto).GetAwaiter().GetResult();

            }
            return Task.FromResult(ret);
        }

        public override Task<bool> DeleteAsync(IEnumerable<TModel> models)
        {

            return base.DeleteAsync(models);
        }
    }
}

