using System;
using ThAmCo.Accounts.Enums;

namespace ThAmCo.Accounts.Models.SysLogs
{
    public class SysLogDto
    {
        public Guid Id { get; set; }
        public string ComponentName { get; set; }
        public string Details { get; set; }
        public string Role { get; set; }
        public DateTime Date { get; set; }
        public AlertTypeEnum AlertType { get; set; }
    }
}
