using AutoMapper;
using ErpDashboard.Application.Features.Items.Quaries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Items.Quaries.GetById
{
    public class GetItemByIdQuery : IRequest<Result<GetItemResponse>>
    {
        public int Id { get; set; }

    }
    internal class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, Result<GetItemResponse>>
    {
        private readonly ICustomIUnitOfWork<int> _customIUnitOfWork;
        private readonly IMapper _mapper;

        public GetItemByIdHandler(ICustomIUnitOfWork<int> customIUnitOfWork, IMapper mapper)
        {
            _customIUnitOfWork = customIUnitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<GetItemResponse>> Handle(GetItemByIdQuery query, CancellationToken cancellationToken)
        {
            var item =  await _customIUnitOfWork.Repository<TbItem>().GetByIdAsync(query.Id);
            var ItemMapper = _mapper.Map<GetItemResponse>(item);
            return await Result<GetItemResponse>.SuccessAsync(ItemMapper);

        }
    }
}
