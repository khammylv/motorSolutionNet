using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MotorSolutionNet.Models;
using MotorSolutionNet.Services;
using MotorSolutionNet.Utilities;

namespace MotorSolutionNet.Data
{
    public class RepairData
    {
        private readonly ConectionDB _connection;
        private readonly Mapping _repairMapping;
        public RepairData() {
            _connection = new ConectionDB();
            _repairMapping = new Mapping(); 
        }

       
        public bool AddRepair(Repairs repair)
        {
            var parameters = _repairMapping.ToSqlParameters(repair);
            return _connection.ExecuteProcedure(ConfigurationVar.AddRepair, parameters);
        }
        public bool UpdateRepair(Repairs repair)
        {
            var parameters = _repairMapping.ToSqlParameters(repair);
            return _connection.ExecuteProcedure(ConfigurationVar.UpdateRepair, parameters);
        }

        public bool DeleteRepair(int repairId) {
            var parametersObject = new
            {
                RepairId = repairId,
            };

            var parameters = _repairMapping.ToSqlParameters(parametersObject);
            return _connection.ExecuteProcedure(ConfigurationVar.DeleteRepair, parameters);
        }

        
        public Repairs GetRepair(int repairId)
        {
            var parametersObject = new
            {
                RepairId = repairId,
            };

            var parameters = _repairMapping.ToSqlParameters(parametersObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetRepairById, parameters);
            return table?.Rows.Count > 0 ? _repairMapping.MapToEntity<Repairs>(table.Rows[0]) : null;
        }

        public bool UpdateDateRepair(int repairId)
        {
            var parametersObject = new
            {
                RepairId = repairId,
              
            };
            var parameters = _repairMapping.ToSqlParameters(parametersObject);
            return _connection.ExecuteProcedure(ConfigurationVar.UpdateRepair, parameters);
        }

       
        public List<Object> GetRepairsByCompanyCode(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode,
            };

            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetRepairByCompany, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _repairMapping.GenericMapping(row))
                            .ToList();
            }

            return new List<Object>();

        }
        public List<Object> GetRepairsByClientId(int clientId)
        {
            var parameterObject = new
            {
                ClientId = clientId,
            };

            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetRepairByClient, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _repairMapping.GenericMapping(row))
                            .ToList();
            }

            return new List<Object>();

        }
        public List<Object> GetRepairsByVehicle(int vehicleId)
        {
            var parameterObject = new
            {
                VehicleId = vehicleId,
            };

            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVar.GetRepairByVehicle, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _repairMapping.GenericMapping(row))
                            .ToList();
            }

            return new List<Object>();

        }

        public bool UpdateDepartureDate(int repairId)
        {
            var parameterObject = new
            {
                RepairId = repairId,
                
            };
            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVar.UpdateDepaureDate, parameters);
        }
    }

}