using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageApp.Business.Operations.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintenance();
        bool GetMaintenanceState();
    }
}
