using ErpDashboard.Application.Interfaces.Repositories;
using ErpDashboard.Shared.Wrapper;
using MediatR;

namespace ErpDashboard.Application.Features.Companies.AddEdit
{
    public class DeleteCompanyCommand : IRequest<IResult<int>>
    {

    }
    internal class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, IResult<int>>
    {
        private readonly ICustomIUnitOfWork<int> _unitOfWork;

        public DeleteCompanyCommandHandler(ICustomIUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult<int>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
