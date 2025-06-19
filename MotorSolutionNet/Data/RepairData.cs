using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MotorSolutionNet.DTO;
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

       
        public bool AddRepair(RepairsDTO repair)
        {
            var parameters = _repairMapping.ToSqlParameters(repair);
            return _connection.ExecuteProcedure(ConfigurationVarRepair.AddRepair, parameters);
        }
        public bool AddDetailRepair(RepairDetails repair)
        {
            var parameters = _repairMapping.ToSqlParameters(repair);
            return _connection.ExecuteProcedure(ConfigurationVarDetailRepair.AddRepairDetails, parameters);
        }
        public bool UpdateDetailRepair(RepairDetails repair)
        {
            var parameters = _repairMapping.ToSqlParameters(repair);
            return _connection.ExecuteProcedure(ConfigurationVarDetailRepair.UpdateRepairDetails, parameters);
        }
        public bool UpdateRepair(RepairsDTO repair)
        {
            var parameters = _repairMapping.ToSqlParameters(repair);
            return _connection.ExecuteProcedure(ConfigurationVarRepair.UpdateRepair, parameters);
        }

        public bool DeleteRepair(int repairId) {
            var parametersObject = new
            {
                RepairId = repairId,
            };

            var parameters = _repairMapping.ToSqlParameters(parametersObject);
            return _connection.ExecuteProcedure(ConfigurationVarRepair.DeleteRepair, parameters);
        }
        public bool DeleteRepairDetails(int repairDetailsId)
        {
            var parametersObject = new
            {
                RepairDetailsId = repairDetailsId,
            };

            var parameters = _repairMapping.ToSqlParameters(parametersObject);
            return _connection.ExecuteProcedure(ConfigurationVarDetailRepair.DeleteRepairDetails, parameters);
        }

        public Object GetRepair(int repairId)
        {
            var parametersObject = new
            {
                RepairId = repairId,
            };

            var parameters = _repairMapping.ToSqlParameters(parametersObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarRepair.GetRepairById, parameters);
            return table?.Rows.Count > 0 ? _repairMapping.GenericMapping(table.Rows[0]) : null;
        }

        public bool UpdateDateRepair(int repairId)
        {
            var parametersObject = new
            {
                RepairId = repairId,
              
            };
            var parameters = _repairMapping.ToSqlParameters(parametersObject);
            return _connection.ExecuteProcedure(ConfigurationVarRepair.UpdateRepair, parameters);
        }

       
        public List<Object> GetRepairsByCompanyCode(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode,
            };

            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarRepair.GetRepairByCompany, parameters);
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
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarRepair.GetRepairByClient, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _repairMapping.GenericMapping(row))
                            .ToList();
            }

            return new List<Object>();

        }
        public List<RepairDetails> GetRepairsDetails(int repairID)
        {
            var parameterObject = new
            {
                RepairId = repairID,
            };

            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarDetailRepair.GetByRepairID, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _repairMapping.MapToEntity<RepairDetails>(row))
                            .ToList();
            }

            return new List<RepairDetails>();

        }
        public List<Object> GetRepairsByVehicle(int vehicleId)
        {
            var parameterObject = new
            {
                VehicleId = vehicleId,
            };

            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarRepair.GetRepairByVehicle, parameters);
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
            return _connection.ExecuteProcedure(ConfigurationVarRepair.UpdateDepaureDate, parameters);
        }
        public bool DoneRepair(RepairsDTO repair)
        {
            var parameterObject = new
            {
                RepairId = repair.RepairId,
                Status = repair.Status

            };
            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVarRepair.DoneRepair, parameters);
        }
        public bool UpdateStatus(int repairId, string repairStatus)
        {
            var parameterObject = new
            {
                RepairId = repairId,
                RepairStatus = repairStatus

            };
            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVarRepair.UpdateStatus, parameters);
        }

        public RepairsDTO GetRepairDTO(int repairId)
        {
            var parameterObject = new
            {
                RepairId = repairId
            };
            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarRepair.GetrepairDTO, parameters);
            return table?.Rows.Count > 0 ? _repairMapping.MapToEntity<RepairsDTO>(table.Rows[0]) : null;
        }
        public RepairDetails GetRepairDetailById(int repairDetailsId)
        {
            var parameterObject = new
            {
                RepairDetailsId = repairDetailsId
            };
            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarDetailRepair.GetByID, parameters);
            return table?.Rows.Count > 0 ? _repairMapping.MapToEntity<RepairDetails>(table.Rows[0]) : null;
        }
        public RoleOptionsResult GetStatusOptions()
        {
            DataTable table = _connection.ExecuteQuery(ConfigurationVarRepair.GetEnumStatus);
            var result = new RoleOptionsResult { Roles = new Dictionary<string, string>() };
           

            if (table?.Rows.Count > 0)
            {
                /*  foreach (DataColumn col in table.Columns)
                  {
                      System.Diagnostics.Debug.WriteLine("column name:" + col.ColumnName); 
                  }*/
                string enumDefinition = table.Rows[0]["enum_definition"].ToString();
                string valuesString = enumDefinition.Substring(5, enumDefinition.Length - 6);
                string[] values = valuesString.Split(',');
                foreach (var val in values)
                {
                    string cleanValue = val.Trim('\'', ' ');
                    result.Roles[cleanValue] = cleanValue;
                }

            }
            return result;
        }

        public StatusSummaryResult GetRepairStatusSummary(int companyId)
        {
            var parameterObject = new
            {
                CompanyCode = companyId
            };
            var parameters = _repairMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarRepair.GetStatusSumary, parameters);

            var result = new StatusSummaryResult();

            if (table?.Rows.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("Aqui:");
                DataRow row = table.Rows[0];

                result.Status["completado"] = row["completado"] != DBNull.Value ? Convert.ToInt32(row["completado"]) : 0;
                result.Status["pendiente"] = row["pendiente"] != DBNull.Value ? Convert.ToInt32(row["pendiente"]) : 0;
                result.Status["en_proceso"] = row["en_proceso"] != DBNull.Value ? Convert.ToInt32(row["en_proceso"]) : 0;
            }
            else
            {
               
                result.Status["completado"] = 0;
                result.Status["pendiente"] = 0;
                result.Status["en_proceso"] = 0;
            }

            return result;
        }
    }

}