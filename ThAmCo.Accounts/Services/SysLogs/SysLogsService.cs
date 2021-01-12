using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models.SysLogs;

namespace ThAmCo.Accounts.Services.SysLogs
{
    public class SysLogsService : ISysLogsService
    {
        public readonly HttpClient _client;

        public SysLogsService(HttpClient client)
        {
            _client = client;
        }

        public async Task AddLog(SysLogDto sysLog)
        {
            var content = new ObjectContent(typeof(SysLogDto), sysLog, new JsonMediaTypeFormatter());

            var result = await _client.PutAsync("logs", content);
        }
    }
}
