﻿//using ErpDashboard.Application.Features.Products.Commands.AddEdit;
using ErpDashboard.Application.Features.Products.Queries.GetAllPaged;
using ErpDashboard.Application.Requests.Catalog;
using ErpDashboard.Shared.ServerSideValidations;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
        Task<bool> IsExist(string name);
    }
}