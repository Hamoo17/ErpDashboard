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

namespace ErpDashboard.Application.Features.MealsTypes.Queries.GetById
{
    public class GetMealsTypesByIdQuery : IRequest<Result<MealsTypesResponse>>
    {
        public int Id { get; set; }

        public GetMealsTypesByIdQuery()
        {
        }
    }
    internal class GetMealsTypesByIdQueryHandler : IRequestHandler<GetMealsTypesByIdQuery, Result<MealsTypesResponse>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetMealsTypesByIdQueryHandler(ICustomIUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<MealsTypesResponse>> Handle(GetMealsTypesByIdQuery request, CancellationToken cancellationToken)
        {
            var MealsTypes = await _unitOfWork.Repository<TbMealsType>().GetByIdAsync(request.Id);
            var MapedTypes = _mapper.Map<MealsTypesResponse>(MealsTypes);
            return await Result<MealsTypesResponse>.SuccessAsync(MapedTypes);

        }
    }
}
