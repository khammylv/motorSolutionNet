using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Utilities;

namespace MotorSolutionNet.Services
{
    public class RepairService
    {
        private readonly RepairData _repairData;
        private readonly PaginationHelper _repairPagination;
        public RepairService()
        {
            _repairData = new RepairData();
            _repairPagination = new PaginationHelper();
        }
        public bool AddRepair(RepairsDTO repair)
        {
            bool isValid = ConfigManager.IsObjectValid(repair);
            if (!isValid)
            {
                throw new Exception("La reparacion tiene campos Vacios");

            }
            return _repairData.AddRepair(repair);
        }
        public bool AddDetailRepair(RepairDetails repair)
        {
            bool isValid = ConfigManager.IsObjectValid(repair);
            if (!isValid)
            {
                throw new Exception("La reparacion tiene campos Vacios");

            }
            return _repairData.AddDetailRepair(repair);
        }
        public bool UpdateDetailRepair(RepairDetails repair)
        {
            bool isValid = ConfigManager.IsObjectValid(repair);
            if (!isValid)
            {
                throw new Exception("La reparacion tiene campos Vacios");
            }
            return _repairData.UpdateDetailRepair(repair);
        }
        public bool UpdateRepair(RepairsDTO repair)
        {
            bool isValid = ConfigManager.IsObjectValid(repair);
            if (!isValid)
            {
                throw new Exception("La reparacion tiene campos Vacios");
            }
            return _repairData.UpdateRepair(repair);
        }
        public bool DeleteRepair(int repairId)
        {
            return _repairData.DeleteRepair(repairId);
        }
        public bool DeleteRepairDetails(int repairDetailsId)
        {
            return _repairData.DeleteRepairDetails(repairDetailsId);
        }
        public Object GetRepair(int repairId)
        {
            return _repairData.GetRepair(repairId);
        }
        public bool UpdateDateRepair(int repairId)
        {
            return _repairData.UpdateDateRepair(repairId);
        }

        public Object GetRepairsByCompanyCode(int companyCode, int pageIndex, int pageSize)
        {
            var repairs = _repairData.GetRepairsByCompanyCode(companyCode);
            return _repairPagination.Paginate(repairs, pageIndex, pageSize);
        }
        public Object GetRepairsByClientId(int clientId, int pageIndex, int pageSize)
        {
            var repairs = _repairData.GetRepairsByClientId(clientId);
            return _repairPagination.Paginate(repairs, pageIndex, pageSize); 
        }
        public List<RepairDetails> GetRepairsDetails(int repairID)
        {
            return _repairData.GetRepairsDetails(repairID);
        }
        public Object GetRepairsByVehicle(int vehicleId, int pageIndex, int pageSize)
        {
            var repairs = _repairData.GetRepairsByVehicle(vehicleId);
            return _repairPagination.Paginate(repairs, pageIndex, pageSize);
        }
        public bool UpdateDepartureDate(int repairId)
        {
            return _repairData.UpdateDepartureDate(repairId);
        }
        public bool DoneRepair(RepairsDTO repair)
        {
            return _repairData.DoneRepair(repair);
        }
        public bool UpdateStatus(int repairId, string repairStatus)
        {
            return _repairData.UpdateStatus(repairId, repairStatus);
        }
        public RepairsDTO GetRepairDTO(int repairId)
        {
            return _repairData.GetRepairDTO(repairId);
        }
        public RepairDetails GetRepairDetailById(int repairDetailsId)
        { return _repairData.GetRepairDetailById(repairDetailsId); 
        }
        public RoleOptionsResult GetStatusOptions()
        { return _repairData.GetStatusOptions(); }

        public StatusSummaryResult GetRepairStatusSummary(int companyId)
        { return _repairData.GetRepairStatusSummary(companyId); }
    }
}