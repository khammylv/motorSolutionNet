using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorSolutionNet.Services;
using MotorSolutionNet.Models;
using System.Data;
using MotorSolutionNet.Utilities;
using MotorSolutionNet.DTO;

namespace MotorSolutionNet.Data
{
    public class VehicleData
    {
        private readonly ConectionDB _connection;
        private readonly Mapping _vehicleMapping;

        public VehicleData()
        {
            _connection = new ConectionDB();
            _vehicleMapping = new Mapping();
        }

        public List<Vehicle> ListVehicles()
        {
            DataTable table = _connection.ExecuteQuery(ConfigurationVarVehicle.ListVehicle);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _vehicleMapping.MapToEntity<Vehicle>(row))
                            .ToList();

            }

            return new List<Vehicle>();
        }
        public Vehicle GetVehicleValidation(int? id_vehicle = null, int? companyCode = null, string plate = null)
        {
            var parameterObject = new
            {
                VehicleId = id_vehicle,
                Plate = plate,
                CompanyCode = companyCode
            };
            var parameters = _vehicleMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarVehicle.GetVehicle, parameters);
            return table?.Rows.Count > 0 ? _vehicleMapping.MapToEntity<Vehicle>(table.Rows[0]) : null;
        }
        public Vehicle GetVehicle(int id_vehicle)
        {
            var parameterObject = new
            {
                VehicleId = id_vehicle,

            };
            var parameters = _vehicleMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarVehicle.GetVehicleByID, parameters);

            return table?.Rows.Count > 0 ? _vehicleMapping.MapToEntity<Vehicle>(table.Rows[0]) : null;
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            var parameters = _vehicleMapping.ToSqlParameters(vehicle);
            return _connection.ExecuteProcedure(ConfigurationVarVehicle.AddVehicle, parameters);
        }

        public bool UpdateVehicle(VehicleDTO vehicle)
        {
            var parameters = _vehicleMapping.ToSqlParameters(vehicle);
            return _connection.ExecuteProcedure(ConfigurationVarVehicle.UpdateVehicle, parameters);
        }

        public bool DeleteVehicle(int vehicleId)
        {
            var parameterObject = new
            {
                VehicleId = vehicleId,
            };
            var parameters = _vehicleMapping.ToSqlParameters(parameterObject);
            return _connection.ExecuteProcedure(ConfigurationVarVehicle.DeleteVehicle, parameters);
        }

        public List<Object> GetVehiclesByCompany(int companyCode)
        {
            var parameterObject = new
            {
                CompanyCode = companyCode,
            };
            var parameters = _vehicleMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarVehicle.GetVehicleByCompany, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _vehicleMapping.GenericMapping(row))
                            .ToList();
            }

            return new List<Object>();
        }

        public List<Object> GetVehiclesByClient(int clientId)
        {
            var parameterObject = new
            {
                ClientId = clientId,
            };
            var parameters = _vehicleMapping.ToSqlParameters(parameterObject);
            DataTable table = _connection.ExecuteProcedureQuery(ConfigurationVarVehicle.GetVehicleByClient, parameters);
            if (table?.Rows.Count > 0)
            {
                return table.AsEnumerable()
                            .Select(row => _vehicleMapping.GenericMapping(row))
                            .ToList();
            }

            return new List<Object>();
        }

       
    }
}