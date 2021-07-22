using OpcLabs.BaseLib.ComponentModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AlarmsAndConditions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using WinCC_REST_API.Models;

namespace WinCC_REST_API.Global
{
    public class Init
    {
        public static readonly EasyUAClient _client = new EasyUAClient();
        public static readonly IEasyUAAlarmsAndConditionsClient _alarmsAndConditionsClient = Init._client.AsAlarmsAndConditionsClient();
        public static Dictionary<string, Alarm> _alarms = new Dictionary<string, Alarm>();
        public static string _endpoint { get; set; }
        public static string _deployedIP { get; set; }
        public static bool _stateIO;
        


        public void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithHttpRequestClientHostIP()
                .Enrich.WithHttpRequestRawUrl()
                .Enrich.WithHttpRequestType()
                .WriteTo.File(@"C:\Log_Master\logging.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Logging initialized");

            #region Lizenzierung (Aus Code-Examples OPC-Labs)
            // Register a license that is we embed as a managed resource in this program.

            // The first two arguments should always be "QuickOPC" and "Multipurpose".
            // The third argument determines the assembly where the license resides.
            // The fourth argument is the namespace-qualified name of the managed resource.
            LicensingManagement.Instance.RegisterManagedResource("QuickOPC", "Multipurpose",
                Assembly.GetExecutingAssembly(),
                "WinCC_REST_API.Global.DEC-PO-208016.bin");

            // Instantiate the client object, obtain the serial number from the license info, and display the serial number.
            long serialNumber = (uint)_client.LicenseInfo["Multipurpose.SerialNumber"];
            Log.Information("SerialNumber: {0}", serialNumber);

            #endregion

            if (ConfigurationManager.AppSettings["deployedIP"] != null)
            {
                Init._deployedIP = ConfigurationManager.AppSettings["deployedIP"];
                Debug.WriteLine("deployedIP: {0}", Init._deployedIP);
            }
            else
            {
                Debug.WriteLine("deployedIP not valid");
            }
            if (ConfigurationManager.AppSettings["endpointDescriptor"] != null)
            {
                Init._endpoint = ConfigurationManager.AppSettings["endpointDescriptor"];
                Debug.WriteLine("Endpoint: {0}", Init._endpoint);
            }
            else
            {
                Debug.WriteLine("Endpoint not valid");
            }
            /*
            _alarms.Add("1234",
            new Alarmlist()
            {
                Time = DateTime.Now.ToString(),
                ConditionName = "1234",
                Message = "Hallo zusammen"
                
            });
            */
        }
    }
}