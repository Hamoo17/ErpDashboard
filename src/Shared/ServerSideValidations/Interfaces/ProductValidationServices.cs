namespace ErpDashboard.Shared.ServerSideValidations.Interfaces
{
    public interface ProductValidationServices
    {
        Task<bool> isNameExist(string name);
    }
}
