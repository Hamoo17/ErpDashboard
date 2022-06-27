using ErpDashboard.Shared.Settings;
using ErpDashboard.Shared.Wrapper;

namespace ErpDashboard.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}