using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Utilities;
using MySqlX.XDevAPI;

namespace MotorSolutionNet.Services
{
    public class VehicleService
    {
        private readonly PaginationHelper _vehiclePagination;
        private readonly VehicleData _vehicleData;

        public VehicleService()
        {
            _vehiclePagination = new PaginationHelper();
            _vehicleData = new VehicleData();
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            bool isValid = ConfigManager.IsObjectValid(vehicle);
            if (!isValid)
            {
                throw new Exception("El vehiculo tiene campos Vacios");

            }
            return _vehicleData.AddVehicle(vehicle);
        }

        public bool UpdateVehicle(VehicleDTO vehicle)
        {
            bool isValid = ConfigManager.IsObjectValid(vehicle);
            if (!isValid)
            {
                throw new Exception("El vehiculo tiene campos Vacios");

            }
            return _vehicleData.UpdateVehicle(vehicle);
        }
        public bool DeleteVehicle(int vehicleId)
        { return _vehicleData.DeleteVehicle(vehicleId); 
        }

        public Object GetVehiclesByCompany(int companyCode, int pageIndex, int pageSize)
        {
            var vehicles = _vehicleData.GetVehiclesByCompany(companyCode);
            return _vehiclePagination.Paginate(vehicles, pageIndex, pageSize);

        }
        public Object GetVehiclesByClient(int clientId, int pageIndex, int pageSize)
        {
            var vehicles = _vehicleData.GetVehiclesByClient(clientId);
            return _vehiclePagination.Paginate(vehicles, pageIndex, pageSize);

        }
        public Vehicle GetVehicle(int id_vehicle)
        {
            return _vehicleData.GetVehicle(id_vehicle);
        }
        public Vehicle GetVehicleValidation(int? id_vehicle = null, int? companyCode = null, string plate = null)
        {
            return _vehicleData.GetVehicleValidation(id_vehicle, companyCode, plate);
        }
    }
}