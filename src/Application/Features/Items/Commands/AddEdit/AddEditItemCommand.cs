using AutoMapper;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace ErpDashboard.Application.Features.Items.Commands.AddEdit
{
    public class AddEditItemCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string ItemEnName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ItemType { get; set; }
        public int? ItemUnit1 { get; set; }
        public int? ItemUnit2 { get; set; }
        public int? ItemUnit3 { get; set; }
        public int? MainUnit { get; set; }
        [Required]
        public int MainUnitId { get; set; }
        public decimal? UnitRate1 { get; set; } // default by 1
        public decimal? UnitRate2 { get; set; }
        public decimal? UnitRate3 { get; set; }

    }
    internal class AddEditItemCommandHandler : IRequestHandler<AddEditItemCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly ICustomIUnitOfWork<int> _customIUnitOfWork;
        private readonly ICurrentUserService _userService;
        private readonly IStringLocalizer<AddEditItemCommandHandler> _localizer;


        public AddEditItemCommandHandler(IMapper mapper, ICustomIUnitOfWork<int> customIUnitOfWork, ICurrentUserService userService, IStringLocalizer<AddEditItemCommandHandler> localizer)
        {
            _mapper = mapper;
            _customIUnitOfWork = customIUnitOfWork;
            _userService = userService;
            _localizer = localizer;
        }
        public async Task<Result<int>> Handle(AddEditItemCommand command, CancellationToken cancellationToken)
        {
            var CompanyId = _userService.CompanyID;
            if (!CompanyId.HasValue)
            {
                return await Result<int>.FailAsync(_localizer["NO COMPANY WITH THIS ID"]);
            }
            var ItemNameExist = await _customIUnitOfWork.Repository<TbItem>().Entities.AnyAsync(c => c.ItemEnName == command.ItemEnName);
            if (!ItemNameExist)
            {
                if (command.Id == 0)
                {
                    try
                    {
                        var item = _mapper.Map<TbItem>(command);
                        item.ComId = CompanyId;
                        await _customIUnitOfWork.Repository<TbItem>().AddAsync(item);
                        await _customIUnitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(item.Id, _localizer["Item Added"]);
                    }
                    catch (DbUpdateException ex)
                    {
                        var t = ex.InnerException.Message;
                        throw;
                    }
                }
                else
                {
                    var item = await _customIUnitOfWork.Repository<TbItem>().GetByIdAsync(command.Id);
                    if (item != null)
                    {
                        item.ItemEnName = command.ItemEnName ?? item.ItemEnName;
                        await _customIUnitOfWork.Repository<TbItem>().UpdateAsync(item, command.Id);
                        await _customIUnitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(item.Id, _localizer["Item Updated"]);
                    }
                    else
                    {
                        return await Result<int>.FailAsync(_localizer["Item Not Found"]);
                    }
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Item Name Exist"]);
            }
        }
    }
}
