using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models.SysLogs;

namespace ThAmCo.Accounts.Services.SysLogs
{
    public class MockSysLogsService : ISysLogsService
    {
        public static List<SysLogDto> SysLogs = new List<SysLogDto>();

        public async Task AddLog(SysLogDto sysLog)
        {
            SysLogs.Add(sysLog);
        }
    }
}
