using System.Threading.Tasks;
using KTGKNhom8.Configuration.Dto;

namespace KTGKNhom8.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
