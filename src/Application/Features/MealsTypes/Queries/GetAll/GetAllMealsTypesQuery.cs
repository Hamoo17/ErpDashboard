using AutoMapper;
using ErpDashboard.Application.Features.MealsTypes.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.MealsTypes.Queries.GetAll
{
    public class GetAllMealsTypesQuery : IRequest<Result<List<MealsTypesResponse>>>
    {
        public GetAllMealsTypesQuery()
        {

        }
    }
    internal class GetAllMealsTypesQueryHandler : IRequestHandler<GetAllMealsTypesQuery, Result<List<MealsTypesResponse>>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMealsTypesQueryHandler(ICustomIUnitOfWork<int> unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<MealsTypesResponse>>> Handle(GetAllMealsTypesQuery query, CancellationToken cancellationToken)
        {
            var MealsTypesList = await _unitOfWork.Repository<TbMealsType>().GetAllAsync();
            var MappedMealsTypes = _mapper.Map<List<MealsTypesResponse>>(MealsTypesList);
            return await Result<List<MealsTypesResponse>>.SuccessAsync(MappedMealsTypes);


        }
    }
}
