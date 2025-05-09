using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Utilities
{
    public class ConfigurationVar
    {
        public static string ConectionDB = "server=127.0.0.1;port=3306;database=motor_solution;user=root;password=;";

        public static string AddUser = "usp_AddUser";
        public static string ListUser = "CALL usp_getAllUsers()";
        public static string GetUser = "usp_getUser";
        public static string GetUserById = "usp_getUserById";
        public static string UpdateUser = "usp_updateUser";
        public static string DeleteUser = "usp_deleteUser";
        public static string UserEnumRole = "CALL usp_getRole()";
        public static string GetUsersByCompany = "usp_GetUsersWithCompanyByCompanyCode";

        public static string AddCompany = "usp_AddCompany";
        public static string ListCompany = "CALL usp_GetAllCompanies()";
        public static string GetCompany = "usp_Getcompany";
        public static string GetCompanByID = "usp_GetCompanyByCode";
        public static string DeleteCompany = "usp_DeleteCompany";
        public static string UpdateCompany = "usp_UpdateCompany";


        public static string AddClient = "usp_AddClient";
        public static string ListClient = "CALL usp_GetAllClients()";
        public static string GetClientValidation = "usp_GetClient";
        public static string GetClient = "usp_GetClientById";
        public static string UpdateClient = "usp_UpdateClient";
        public static string DeleteClient = "usp_DeleteClient";
        public static string GetClientByCompany = "usp_GetClientsWithCompanyCode";

        public static string AddVehicle = "usp_AddVehicle ";
        public static string ListVehicle = "CALL usp_GetAllVehicles()";
        public static string GetVehicle = "usp_GetVehicle";
        public static string GetVehicleByID = "usp_GetVehicleById";
        public static string UpdateVehicle = "usp_UpdateVehicle";
        public static string DeleteVehicle = "usp_DeleteVehicle";
        public static string GetVehicleByClient = "usp_GetVehicleWithCompanyByClient";
        public static string GetVehicleByCompany = "usp_GetVehicleWithCompanyByCompanyCode";
        public static string UpdateDepaureDate = "usp_UpdateDepartureDate";

    }
}