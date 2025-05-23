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
        public static string UpdatePassword = "usp_UpdatePassword";
        public static string UpdateEmailPassword = "usp_UpdateEmailPassword";


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
        


        public static string AddBilling = "usp_AddBilling";
        public static string ListBilling = "CALL usp_GetAllBillings()";
        public static string GetBillingByID = "usp_GetBillingById";
        public static string GetFullBillingByID = "GetFullBillingById";
        public static string UpdateBilling = "usp_UpdateBilling";
        public static string DeleteBilling = "usp_DeleteBilling";
        public static string GetBillingsByCompany = "GetFullBillingByCompany";
        public static string GetBillingsByClient = "GetFullBillingByClient";

        public static string AddRepair = "usp_AddRepair";
        public static string UpdateRepair = "usp_updateRepair";
        public static string ListRepair = "CALL usp_GetAllRepairs()";
        public static string GetRepairById = "usp_getRepairById";
        public static string DeleteRepair = "usp_DeleteRepair";
        public static string GetRepairByCompany = "usp_GetRepairByCompany";
        public static string GetRepairByClient = "usp_getRepairByClient";
        public static string GetRepairByVehicle = "usp_GetRepairsByVehicle";
        public static string UpdateDepaureDate = "usp_UpdateDepartureDate";


    }

    
        
}