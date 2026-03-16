using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using KTGKNhom8.Configuration.Dto;

namespace KTGKNhom8.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : KTGKNhom8AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
