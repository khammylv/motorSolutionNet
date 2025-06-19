using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MotorSolutionNet.Utilities
{
    public class ConfigManager
    {
        public static string GetConfigValue(string key)
        {
            var rawValue = ConfigurationManager.AppSettings[key];
            
            if (rawValue != null && rawValue.StartsWith("env:"))
            {
                var envKey = rawValue.Substring(4);
                return Environment.GetEnvironmentVariable(envKey);
            }
            return rawValue;
        }
        public static bool IsObjectValid<T>(T obj)
        {
            if (obj == null) return false;

            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);

                //System.Diagnostics.Debug.WriteLine("Prop: " + prop);
                //System.Diagnostics.Debug.WriteLine("Value: " + value);
                if (prop.PropertyType == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace(value as string))
                        return false;
                }


                if (prop.PropertyType == typeof(int))
                {
                    if ((int)value <= 0)
                        return false;
                }
                if (prop.PropertyType == typeof(Decimal))
                {
                    if ((int)value <= 0)
                        return false;
                }


            }

            return true;
        }
    }
}