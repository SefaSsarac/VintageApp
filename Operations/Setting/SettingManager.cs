using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageApp.Data.Entities;
using VintageApp.Data.Repositories;
using VintageApp.Data.UnitOfWork;

namespace VintageApp.Business.Operations.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        public bool GetMaintenanceState()
        {
            var maintenanceState = _settingRepository.GetById(1).MaintenanceMode;

            return maintenanceState;
        }

        public async Task ToggleMaintenance()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenanceMode = !setting.MaintenanceMode;

            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Bakım durumu güncellenirken bir hatayla karşılaşıldı");
            }

        }
    }
}   
