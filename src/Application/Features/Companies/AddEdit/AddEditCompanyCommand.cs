using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.Companies.AddEdit
{
    public class AddEditCompanyCommand : IRequest<IResult<int>>
    {

    }
    internal class AddEditCompanyCommandHandler : IRequestHandler<AddEditCompanyCommand, IResult<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;

        public AddEditCompanyCommandHandler(ICustomIUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<int>> Handle(AddEditCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
