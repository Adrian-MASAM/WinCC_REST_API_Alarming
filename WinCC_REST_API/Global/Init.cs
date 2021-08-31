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
        public static string _filterEventType { get; set; }
        public static string _filterConditionType { get; set; }
        public static string _websocketTransmit { get; set; }
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

            if (ConfigurationManager.AppSettings["websocketTransmit"] != null)
            {
                Init._websocketTransmit = ConfigurationManager.AppSettings["websocketTransmit"];
                Log.Information("websocketTransmit: {0}", Init._websocketTransmit);
            }
            else
            {
                Log.Error("websocketTransmit not valid");
            }
            if (ConfigurationManager.AppSettings["endpointDescriptor"] != null)
            {
                Init._endpoint = ConfigurationManager.AppSettings["endpointDescriptor"];
                Log.Information("Endpoint: {0}", Init._endpoint);
            }
            else
            {
                Log.Error("Endpoint not valid");
            }
            if (ConfigurationManager.AppSettings["filterEventType"] != null)
            {
                Init._endpoint = ConfigurationManager.AppSettings["filterEventType"];
                Log.Information("filterEventType: {0}", Init._filterEventType);
            }
            else
            {
                Log.Error("filterEventType not valid");
            }
            if (ConfigurationManager.AppSettings["filterConditionType"] != null)
            {
                Init._endpoint = ConfigurationManager.AppSettings["filterConditionType"];
                Log.Information("filterConditionType: {0}", Init._filterConditionType);
            }
            else
            {
                Log.Error("filterConditionType not valid");
            }
        }
    }
}