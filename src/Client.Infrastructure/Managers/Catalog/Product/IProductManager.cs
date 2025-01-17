﻿using ReturneeManager.Application.Features.Products.Commands.AddEdit;
using ReturneeManager.Application.Features.Products.Queries.GetAllPaged;
using ReturneeManager.Application.Requests.Catalog;
using ReturneeManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace ReturneeManager.Client.Infrastructure.Managers.Catalog.Product
{
    public interface IProductManager : IManager
    {
        Task<PaginatedResult<GetAllPagedProductsResponse>> GetProductsAsync(GetAllPagedProductsRequest request);

        Task<IResult<string>> GetProductImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditProductCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}