using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Application.Models;
using ErpDashboard.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDashboard.Application.Features.Recipe.Commabds.Delete.DeleteRecipeFromItem
{
    public class DeleteRecipeFromItemCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }
    internal class DeleteRecipeFromItemHandler : IRequestHandler<DeleteRecipeFromItemCommand, Result<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteRecipeFromItemHandler> _localizer;
        public DeleteRecipeFromItemHandler(ICustomIUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteRecipeFromItemHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public Task<Result<int>> Handle(DeleteRecipeFromItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var RecipeExistInMeal = _unitOfWork.Repository<TbMeal>().Entities.Any(x => x.TbMealsLines.Any(c=>c.));
        }
    }
}
