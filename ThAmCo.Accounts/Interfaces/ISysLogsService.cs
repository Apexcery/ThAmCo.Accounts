using System.Threading.Tasks;
using ThAmCo.Accounts.Models.SysLogs;

namespace ThAmCo.Accounts.Interfaces
{
    public interface ISysLogsService
    {
        public Task AddLog(SysLogDto sysLog);
    }
}
