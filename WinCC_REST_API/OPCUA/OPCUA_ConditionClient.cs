using System;
using System.Diagnostics;
using System.Web.Http;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace.Standard;
using OpcLabs.EasyOpc.UA.AlarmsAndConditions;
using OpcLabs.EasyOpc.UA.Filtering;
using OpcLabs.EasyOpc.UA.OperationModel;
using System.Configuration;
using WinCC_REST_API.Global;
using Serilog;
using OpcLabs.BaseLib.Instrumentation;
using WinCC_REST_API.Controllers;
using WinCC_REST_API.Models;
using Newtonsoft.Json;

namespace WinCC_REST_API.OPCUA
{
    public class OPCUA_ConditionClient : ApiController
    {
        public void InitializeEventNotification()
        {
            UAEndpointDescriptor endpointDescriptor = Init._endpoint;

            Init._client.EventNotification += Client_EventNotification;

            Log.Information("Subscribing...{0}", UABaseEventObject.Operands.NodeId.ToString());
            Init._client.SubscribeEvent(
                endpointDescriptor,
                UAObjectIds.Server,
                1,
                new UAAttributeFieldCollection
                {
                    // Select specific fields using standard operand symbols
                    UABaseEventObject.Operands.EventId,
                    UABaseEventObject.Operands.EventType,
                    UABaseEventObject.Operands.LocalTime,
                    UABaseEventObject.Operands.Message,
                    UABaseEventObject.Operands.ReceiveTime,
                    UABaseEventObject.Operands.Severity,
                    UABaseEventObject.Operands.SourceName,
                    UABaseEventObject.Operands.SourceNode,
                    UABaseEventObject.Operands.Time,
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/AGNR"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/ALARMCOUNT"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/APPLICATION"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/AckedState"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/ActiveState"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/BACKCOLOR"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/BIG_COUNTER"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/BLOCKINFO"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/BranchId"), //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/CLASSID"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/CLASSNAME"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/COMMENT"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/COMPUTER"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/COUNTER"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/CPUNR"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/ClientUserId"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/Comment"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/ConditionClassId"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/ConditionClassName"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/ConditionName"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/ConfirmedState"), //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/DURATION"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/EnabledState"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/EventId"), //von BaseEvent
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/EventType"), //von BaseEvent
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/FLAGS"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/FLASHCOLOR"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/FORECOLOR"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/HIDDEN_COUNT"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/INFOTEXT"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/InputNode"), //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/LOCKCOUNT"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/LOOPINALARM"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/LastSeverity"), //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/LocalTime"), //von BaseEvent
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/MODIFYSTATE"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/MaxTimeShelved"), //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/Message"),  //von BaseEvent
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/OS_EVENTID"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/OS_HIDDEN"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PARAMETER"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/Priority"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE01"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE02"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE03"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE04"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE05"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE06"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE07"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE08"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE09"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE10"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/QUITCOUNT"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/QUITSTATETEXT"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/Quality"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/ReceiveTime"), //von BaseEvent
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/Retain"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/STATETEXT"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/Severity"), //von BaseEvent
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/SourceName"), //von BaseEvent
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/SourceNode"), //von BaseEvent
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/SuppressedOrShelved"), //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/SuppressedState"), //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT01"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT02"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT03"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT04"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT05"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT06"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT07"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT08"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT09"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT10"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TYPEID"),
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/TYPENAME"),
                    //UAFilterElements.SimpleAttribute("ns=2;i=1", "/Time"), //von BaseEvent
                    UAFilterElements.SimpleAttribute("ns=2;i=1", "/USER"),
                }); ;
            Log.Information("Initializing Event subscription succeded");
        }

        static void Client_EventNotification(object sender, EasyUAEventNotificationEventArgs e)
        {
            DateTime dateTime = new DateTime();
            dateTime = e.EventData.BaseEvent.Time;
            Log.Information("Alarm Zeit WinCC: ;" + dateTime.ToString("hh:mm:ss.fff"));
            Log.Information("Alarm Start Web API: ;" + DateTime.UtcNow.ToString("hh:mm:ss.fff"));

            if (e.EventData == null)
            {
                Log.Warning(e.ToString());
                return;
            }


            foreach (var item in e.EventData.FieldResults.Values)
            {
                if (item.Exception != null)
                {
                    Log.Error(item.Exception.Message);
                }
                if (item.Value == null)
                {
                    item.SetValue("N.A.");
                }
            }

            Alarm alarm = new Alarm
            {
                AGNR = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/AGNR")].Value.ToString(),
                ALARMCOUNT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/ALARMCOUNT")].Value.ToString(),
                APPLICATION = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/APPLICATION")].Value.ToString(),
                AckedState = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/AckedState")].Value.ToString(),
                ActiveState = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/ActiveState")].Value.ToString(),
                BACKCOLOR = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/BACKCOLOR")].Value.ToString(),
                BIG_COUNTER = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/BIG_COUNTER")].Value.ToString(),
                BLOCKINFO = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/BLOCKINFO")].Value.ToString(),
                //BranchId =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                CLASSID = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/CLASSID")].Value.ToString(),
                CLASSNAME = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/CLASSNAME")].Value.ToString(),
                COMMENT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/COMMENT")].Value.ToString(),
                COMPUTER = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/COMPUTER")].Value.ToString(),
                COUNTER = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/COUNTER")].Value.ToString(),
                CPUNR = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/CPUNR")].Value.ToString(),
                ClientUserId = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/ClientUserId")].Value.ToString(),
                Comment = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/Comment")].Value.ToString(),
                ConditionClassId = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/ConditionClassId")].Value.ToString(),
                ConditionClassName = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/ConditionClassName")].Value.ToString(),
                ConditionName = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/ConditionName")].Value.ToString(),
                //ConfirmedState =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                DURATION = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/DURATION")].Value.ToString(),
                EnabledState = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/EnabledState")].Value.ToString(),
                EventId = System.Text.Encoding.Unicode.GetString(e.EventData.BaseEvent.EventId),
                EventType = e.EventData.BaseEvent.EventType.ToString(),
                FLAGS = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/FLAGS")].Value.ToString(),
                FLASHCOLOR = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/FLASHCOLOR")].Value.ToString(),
                FORECOLOR = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/FORECOLOR")].Value.ToString(),
                HIDDEN_COUNT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/HIDDEN_COUNT")].Value.ToString(),
                INFOTEXT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/INFOTEXT")].Value.ToString(),
                //InputNode =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/InputNode"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                LOCKCOUNT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/LOCKCOUNT")].Value.ToString(),
                LOOPINALARM = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/LOOPINALARM")].Value.ToString(),
                //LastSeverity =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/LastSeverity"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                LocalTime = e.EventData.BaseEvent.LocalTime.ToString(),
                MODIFYSTATE = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/MODIFYSTATE")].Value.ToString(),
                //MaxTimeShelved =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/MaxTimeShelved"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                Message = e.EventData.BaseEvent.Message.ToString(),
                OS_EVENTID = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/OS_EVENTID")].Value.ToString(),
                OS_HIDDEN = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/OS_HIDDEN")].Value.ToString(),
                PARAMETER = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PARAMETER")].Value.ToString(),
                Priority = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/Priority")].Value.ToString(),
                PROCESSVALUE01 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE01")].Value.ToString(),
                PROCESSVALUE02 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE02")].Value.ToString(),
                PROCESSVALUE03 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE03")].Value.ToString(),
                PROCESSVALUE04 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE04")].Value.ToString(),
                PROCESSVALUE05 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE05")].Value.ToString(),
                PROCESSVALUE06 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE06")].Value.ToString(),
                PROCESSVALUE07 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE07")].Value.ToString(),
                PROCESSVALUE08 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE08")].Value.ToString(),
                PROCESSVALUE09 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE09")].Value.ToString(),
                PROCESSVALUE10 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/PROCESSVALUE10")].Value.ToString(),
                QUITCOUNT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/QUITCOUNT")].Value.ToString(),
                QUITSTATETEXT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/QUITSTATETEXT")].Value.ToString(),
                Quality = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/Quality")].Value.ToString(),
                ReceiveTime = e.EventData.BaseEvent.ReceiveTime.ToString(),
                Retain = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/Retain")].Value.ToString(),
                STATETEXT = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/STATETEXT")].Value.ToString(),
                Severity = e.EventData.BaseEvent.Severity.ToString(),
                SourceName = e.EventData.BaseEvent.SourceName.ToString(),
                SourceNode = e.EventData.BaseEvent.SourceNode.ToString(),
                //SuppressedOrShelved =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/SuppressedOrShelved"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                //SuppressedState =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/SuppressedState"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                TEXT01 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT01")].Value.ToString(),
                TEXT02 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT02")].Value.ToString(),
                TEXT03 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT03")].Value.ToString(),
                TEXT04 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT04")].Value.ToString(),
                TEXT05 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT05")].Value.ToString(),
                TEXT06 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT06")].Value.ToString(),
                TEXT07 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT07")].Value.ToString(),
                TEXT08 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT08")].Value.ToString(),
                TEXT09 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT09")].Value.ToString(),
                TEXT10 = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TEXT10")].Value.ToString(),
                TYPEID = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TYPEID")].Value.ToString(),
                TYPENAME = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/TYPENAME")].Value.ToString(),
                Time = e.EventData.BaseEvent.Time.ToString("r"),
                USER = e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1", "/USER")].Value.ToString()
            };
            

            Log.Information("NEUER EVENT:\r\n***********************************" + JsonConvert.SerializeObject(alarm));


            if (Init._filterEventType == null)
            {
                return;
            }
            if (Init._filterConditionType == null)
            {
                return;
            }

            if (alarm.EventType.EndsWith("ns=2;i=53") && Init._filterEventType == "true")
            {
                return;//Events gefiltert
            }
            else if (alarm.EventType.EndsWith("ns=2;i=1") && Init._filterConditionType == "true")
            {
                return;//Condition gefiltert
            }
            else //Der Event enspricht keinem Filter und wird nun überprüft, ob zu Liste hinzugefügt oder entfernt werden soll
            {
                if (alarm.AckedState == "Unacknowledged" && alarm.ActiveState == "Active")//keep
                {
                    alarm.State = ((int)Meldestatus.MSG_STATE_COME).ToString();
                }
                else if (alarm.AckedState == "Acknowledged" && alarm.ActiveState == "Active")//keep
                {
                    alarm.State = ((int)Meldestatus.MSG_STATE_QUIT).ToString();
                }
                else if (alarm.AckedState == "Unacknowledged" && alarm.ActiveState == "Inactive")//keep
                {
                    alarm.State = ((int)Meldestatus.MSG_STATE_GO).ToString();
                }
                else //dispose
                {
                    alarm.State = ((int)Meldestatus.MSG_STATE_GONE).ToString();
                }
            }

            if (alarm.State != ((int)Meldestatus.MSG_STATE_GONE).ToString())
            {
                try
                {
                    Init._alarms.Add(
                    alarm.ConditionName, //Alarmnummer als ID für Dictionary
                    alarm
                    );
                    Log.Information("Event mit Nummer: <" + alarm.ConditionName + "> in Liste aufgenommen");
                }
                catch (ArgumentException)
                {
                    Init._alarms[alarm.ConditionName] = alarm;
                    Log.Information("Event mit Nummer <" + alarm.ConditionName + "> bereits vorhanden, aktualisierung aufgrund Zustandsänderung");
                }

            }
            else
            {
                Init._alarms.Remove(alarm.ConditionName);
                Log.Information("Event mit Nummer <" + alarm.ConditionName + "> aus Liste entfernt (gegangen)");
            }
            Log.Information("Alarm Verarbeitet Web API: ;" + DateTime.UtcNow.ToString("hh:mm:ss.fff"));
            if (Init._websocketTransmit == null)
            {
                return;
            }

            if (Init._websocketTransmit == "true")
            {
                WSHandler.AlarmNotifier(JsonConvert.SerializeObject(Init._alarms.Values));
            }
        }

        public void InitializeLogServer()
        {
            Init._client.ServerConditionChanged += ServerConditionChanged;
        }

        private static void ServerConditionChanged(object sender, EasyUAServerConditionChangedEventArgs e)
        {
            if (e.Connected == true)
            {
                Log.Information("OPC UA Server Verbunden");
                Init._stateIO = true;
            }
            else
            {
                if (e.ErrorMessage != null)
                {
                    Log.Error("OPC UA Server nicht verbunden");
                    Init._stateIO = false;
                }
            }
        }

        public void InitializeLogs()
        {
            EasyUAClient.LogEntry += EasyUAClientOnLogEntry;
        }

        private static void EasyUAClientOnLogEntry(object sender, LogEntryEventArgs logEntryEventArgs)
        {
            string debugMode = ConfigurationManager.AppSettings["DebugMode"];

            if (debugMode == null)
            {
                return;
            }
            else if (debugMode == "true")
            {
                switch (logEntryEventArgs.EntryType.ToString())
                {
                    case "Information":
                        Log.Information(logEntryEventArgs.Message);
                        break;
                    case "Warning":
                        Log.Warning(logEntryEventArgs.Message);
                        break;
                    default:
                        Log.Debug(logEntryEventArgs.EntryType + ": " + logEntryEventArgs.Message);
                        break;
                }
            }
            else
            {
                return;
            }
        }
    }
}