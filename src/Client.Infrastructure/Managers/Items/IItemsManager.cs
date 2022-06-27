using ErpDashboard.Application.Features.Items.Commands.AddEdit;
using ErpDashboard.Application.Features.Items.Quaries.Dto;
using ErpDashboard.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Client.Infrastructure.Managers.Items
{
	public interface IItemsManager : IManager
	{
		public Task<IResult<List<GetItemResponse>>> GetAll();
		public Task<IResult<int>> SaveAsync(AddEditItemCommand Command);
		public Task<IResult<int>> DeleteAsync(int id);
	}
}
