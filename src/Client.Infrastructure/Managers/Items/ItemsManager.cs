using ErpDashboard.Application.Features.Items.Commands.AddEdit;
using ErpDashboard.Application.Features.Items.Quaries.Dto;
using ErpDashboard.Shared.Wrapper;
using ErpDashboard.Client.Infrastructure.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDashboard.Client.Infrastructure.Extensions;
using System.Net.Http.Json;

namespace ErpDashboard.Client.Infrastructure.Managers.Items
{
	public class ItemsManager : IItemsManager
	{
		private readonly HttpClient _httpClient;

		public ItemsManager(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IResult<int>> DeleteAsync(int id)
		{
			var Response = await _httpClient.DeleteAsync(ItemsEndpointa.DeleteAsync(id));
			return await Response.ToResult<int>();
		}

		public async Task<IResult<List<GetItemResponse>>> GetAll()
		{
			var Response = await _httpClient.GetAsync(ItemsEndpointa.GetAll);
			return await Response.ToResult<List<GetItemResponse>>();
		}

		public async Task<IResult<int>> SaveAsync(AddEditItemCommand Command)
		{
			var Response = await _httpClient.PostAsJsonAsync(ItemsEndpointa.SaveAsync,Command);
			return await Response.ToResult<int>();

		}
	}
}
