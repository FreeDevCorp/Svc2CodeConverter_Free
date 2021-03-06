﻿using System.Configuration;
using System.Linq;

namespace Svc2CodeConverter.CodeBuilder
{
    public static class GlobalConfig
    {
        public static readonly string SitLogin = ConfigurationManager.AppSettings["basic_user"] ?? "sit";
        public static readonly string SitPassword = ConfigurationManager.AppSettings["basic_password"] ?? "rZ_GG72XS^Vf55ZW";
        private static readonly string CurrentPlatform = ConfigurationManager.AppSettings["current_platform"];
        private static readonly string ServicePoint = ConfigurationManager.AppSettings[$"service_{CurrentPlatform}"];
        private static readonly string ServicePointAddress = ConfigurationManager.AppSettings[$"servicepoint_{((CurrentPlatform != "file") ? "uri" : "patch")}"];
        private static string[] _result;

        public static string[] EndPointAddress
        {
            get
            {
                if (null != _result) return _result;
                var mass = System.Text.RegularExpressions.Regex.Replace(
                    ServicePointAddress,
                    @"[\s\r\n]+",
                    string.Empty).Split(',');

                _result = mass.Select(
                    x => $"{ServicePoint}{((CurrentPlatform != "file") ? @"/" : string.Empty)}{x}{((CurrentPlatform != "file") ? "?wsdl" : string.Empty)}").ToArray();
                return _result;
            }

        }

        public static readonly string ContractsDestinationPath =
            ConfigurationManager.AppSettings["contracts_destination_path"] ?? @"C:\INTEGRATION\Svc2CodeCvtResult";

        public static readonly string DtosDestinationPath = ConfigurationManager.AppSettings["dtos_destination_path"] ?? @"C:\INTEGRATION\Svc2CodeCvtResult\Dtos";


    }
}
