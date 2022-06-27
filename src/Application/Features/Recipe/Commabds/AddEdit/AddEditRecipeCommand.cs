using ErpDashboard.Application.Features.Recipe.Queries.Dto;
using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace ErpDashboard.Application.Features.Recipe.Commabds.AddEdit
{
    public class AddEditRecipeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int ComplexItemId { get; set; }
        public decimal QtyNeeded { get; set; }
        public int MainUnit { get; set; }

        public List<ItemComponentDetailResponse> itemComponentDetailResponse { get; set; }
    }
    internal class AddEditRecipeCommandHandler : IRequestHandler<AddEditRecipeCommand, Result<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IStringLocalizer<AddEditRecipeCommand> _localizer;
        public AddEditRecipeCommandHandler(ICustomIUnitOfWork<int> unitOfWork, ICurrentUserService currentUser, IStringLocalizer<AddEditRecipeCommand> localizer)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditRecipeCommand command, CancellationToken cancellationToken)
        {
            var CompanyID = _currentUser.CompanyID;
            if (!CompanyID.HasValue)
            {
                return await Result<int>.FailAsync(_localizer["NO COMPANY WITH THIS ID"]);
            }
            if (command.Id == 0)
            {
                var ItemRecipe = new TbItemComponentsHdr();
                ItemRecipe.ComplexItem = command.ComplexItemId;
                ItemRecipe.QtyNeeded = command.QtyNeeded;
                ItemRecipe.MainUnit = command.MainUnit.ToString();

                await _unitOfWork.Repository<TbItemComponentsHdr>().AddAsync(ItemRecipe);
                await _unitOfWork.Commit(cancellationToken);

                var HdrId = ItemRecipe.Id;
                foreach (var item in command.itemComponentDetailResponse)
                {
                    item.ItemComHdr = HdrId;
                }

                return await Result<int>.SuccessAsync(_localizer["Recipy Added"]);
            }
            else
            {
                var ItemRecipe = await _unitOfWork.Repository<TbItemComponentsHdr>().GetByIdAsync(command.Id);
                if (ItemRecipe != null)
                {
                    ItemRecipe.ComplexItem = command.ComplexItemId;
                    ItemRecipe.QtyNeeded = command.QtyNeeded;
                    ItemRecipe.MainUnit = command.MainUnit.ToString();

                    await _unitOfWork.Repository<TbItemComponentsHdr>().UpdateAsync(ItemRecipe, command.Id);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(_localizer["Recipy Updated"]);

                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Recipy Not Found"]);
                }
            }
        }
    }

}
