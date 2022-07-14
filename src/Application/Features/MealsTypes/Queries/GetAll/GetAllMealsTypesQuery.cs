using AutoMapper;
using ErpDashboard.Application.Features.MealsTypes.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICurrentUserService _userService;

        public GetAllMealsTypesQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper, ICurrentUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<Result<List<MealsTypesResponse>>> Handle(GetAllMealsTypesQuery query, CancellationToken cancellationToken)
        {
            
            var MealsTypesList = await _unitOfWork.Repository<TbMealsType>().GetAllAsync()/*(c=>c.ComId == _userService.CompanyID)*/;
            var MappedMealsTypes = _mapper.Map<List<MealsTypesResponse>>(MealsTypesList);
            return await Result<List<MealsTypesResponse>>.SuccessAsync(MappedMealsTypes);


        }
    }
}
