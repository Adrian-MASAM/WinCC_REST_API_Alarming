using Newtonsoft.Json;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinCC_REST_API.Models
{
    public class Alarm //ns=2i=1
    {
        public string AGNR { get; set; }
        public string ALARMCOUNT { get; set; }
        public string APPLICATION { get; set; }
        public string AckedState { get; set; }
        public string ActiveState { get; set; }
        public string BACKCOLOR { get; set; }
        public string BIG_COUNTER { get; set; }
        public string BLOCKINFO { get; set; }
        //public string BranchId { get; set; } //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
        public string CLASSID { get; set; }
        public string CLASSNAME { get; set; }
        public string COMMENT { get; set; }
        public string COMPUTER { get; set; }
        public string COUNTER { get; set; }
        public string CPUNR { get; set; }
        public string ClientUserId { get; set; }
        public string Comment { get; set; }
        public string ConditionClassId { get; set; }
        public string ConditionClassName { get; set; }
        public string ConditionName { get; set; }
        //public string ConfirmedState { get; set; } //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
        public string DURATION { get; set; }
        public string EnabledState { get; set; }
        public string EventId { get; set; }
        public string EventType { get; set; }
        public string FLAGS { get; set; }
        public string FLASHCOLOR { get; set; }
        public string FORECOLOR { get; set; }
        public string HIDDEN_COUNT { get; set; }
        public string INFOTEXT { get; set; }
        //public string InputNode { get; set; } //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
        public string LOCKCOUNT { get; set; }
        public string LOOPINALARM { get; set; }
        //public string LastSeverity { get; set; } //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
        public string LocalTime { get; set; }
        public string MODIFYSTATE { get; set; }
        //public string MaxTimeShelved { get; set; } //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
        public string Message { get; set; }
        public string OS_EVENTID { get; set; }
        public string OS_HIDDEN { get; set; }
        public string PARAMETER { get; set; }
        public string Priority { get; set; }
        public string PROCESSVALUE01 { get; set; }
        public string PROCESSVALUE02 { get; set; }
        public string PROCESSVALUE03 { get; set; }
        public string PROCESSVALUE04 { get; set; }
        public string PROCESSVALUE05 { get; set; }
        public string PROCESSVALUE06 { get; set; }
        public string PROCESSVALUE07 { get; set; }
        public string PROCESSVALUE08 { get; set; }
        public string PROCESSVALUE09 { get; set; }
        public string PROCESSVALUE10 { get; set; }
        public string QUITCOUNT { get; set; }
        public string QUITSTATETEXT { get; set; }
        public string Quality { get; set; }
        public string ReceiveTime { get; set; }
        public string Retain { get; set; }
        public string State { get; set; } //Hilfselement (nicht aus OPC UA)
        public string STATETEXT { get; set; }
        public string Severity { get; set; }
        public string SourceName { get; set; }
        public string SourceNode { get; set; }
        //public string SuppressedOrShelved { get; set; } //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
        //public string SuppressedState { get; set; } //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
        public string TEXT01 { get; set; }
        public string TEXT02 { get; set; }
        public string TEXT03 { get; set; }
        public string TEXT04 { get; set; }
        public string TEXT05 { get; set; }
        public string TEXT06 { get; set; }
        public string TEXT07 { get; set; }
        public string TEXT08 { get; set; }
        public string TEXT09 { get; set; }
        public string TEXT10 { get; set; }
        public string TYPEID { get; set; }
        public string TYPENAME { get; set; }
        public string Time { get; set; }
        public string USER { get; set; }
        public string Timediff { get; set; }
    }

    public class Alarm_example : IExamplesProvider //ns=2i=1
    {
        public object GetExamples()
        {
            return new Alarm
            {
                AGNR = "0",
                ALARMCOUNT = "3",
                APPLICATION = "MyApplication",
                AckedState = "Unacknowledged",
                ActiveState = "Active",
                BACKCOLOR = "64255",
                BIG_COUNTER = "N.A.",
                BLOCKINFO = "N.A.",
                //BranchId =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                CLASSID = "2",
                CLASSNAME = "Technik",
                COMMENT = "Wichtiger Alarm, Pikett auffordern",
                COMPUTER = "ThinkPadP51",
                COUNTER = "1",
                CPUNR = "0",
                ClientUserId = "N.A.",
                Comment = "Wichtiger Alarm, Pikett auffordern",
                ConditionClassId = "ProcessConditionClassType",
                ConditionClassName = "ProcessConditionClassType",
                ConditionName = "1",
                //ConfirmedState =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210
                DURATION = "0",
                EnabledState = "True",
                EventId = "@LOCALMACHINE::localhost::1....",
                EventType = "nsu=urn:Siemens/UA/Types/WinCC/ ;ns=2;i=1",
                FLAGS = "3276817",
                FLASHCOLOR = "64255",
                FORECOLOR = "0",
                HIDDEN_COUNT = "N.A.",
                INFOTEXT = "Information zum Alarm",
                //InputNode =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/InputNode"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                LOCKCOUNT = "0",
                LOOPINALARM = "0",
                //LastSeverity =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/LastSeverity"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                LocalTime = "00:00:00",
                MODIFYSTATE = "65537",
                //MaxTimeShelved =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/MaxTimeShelved"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                Message = "Meldung P1",
                OS_EVENTID = "N.A.",
                OS_HIDDEN = "N.A.",
                PARAMETER = "64",
                Priority = "0",
                PROCESSVALUE01 = "25 °C",
                PROCESSVALUE02 = "200 Pa",
                PROCESSVALUE03 = "50 Hz",
                PROCESSVALUE04 = "200 N",
                PROCESSVALUE05 = "200 kJ",
                PROCESSVALUE06 = "300 W",
                PROCESSVALUE07 = "230 V",
                PROCESSVALUE08 = "0.5 s",
                PROCESSVALUE09 = "3 m",
                PROCESSVALUE10 = "7 kg",
                QUITCOUNT = "1",
                QUITSTATETEXT = "+",
                Quality = "Good",
                ReceiveTime = "12.08.2021 10:48:29",
                Retain = "True",
                STATETEXT = "+",
                Severity = "1",
                SourceName = "@LOCALMACHINE::localhost::",
                SourceNode = "",
                //SuppressedOrShelved =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/SuppressedOrShelved"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                //SuppressedState =  //vom OPC UA-Server nicht unterstützt Systemhandbuch, 02/2017, A5E40840067-AA S.210//e.EventData.FieldResults[UAFilterElements.SimpleAttribute("ns=2;i=1","/SuppressedState"),//vom OPC UA-Server nicht unterstützt Systemhandbuch,02/2017,A5E40840067-AA S.210
                TEXT01 = "Meldung P1",
                TEXT02 = "Gebäude 1",
                TEXT03 = "Stockwerk 5",
                TEXT04 = "Raum 125 4",
                TEXT05 = "SAP-Nr. 234512",
                TEXT06 = "Interventionsanweisung 175",
                TEXT07 = "Ansprechpartner Elektro",
                TEXT08 = "Telefonnummer 0791234567",
                TEXT09 = "GMP",
                TEXT10 = "Dringend",
                TYPEID = "17",
                TYPENAME = "P1",
                Time = "Thu, 12 Aug 2021 10:48:07 GMT",
                USER = "Hans-Peter"
            };
        }
    }

    enum Meldestatus
    {
        MSG_STATE_COME = 0x00000001,   // Meldung gekommen
        MSG_STATE_GO = 0x00000002,   // Meldung gegangen
        MSG_STATE_QUIT = 0x00000003,   // Meldung quittiert
        MSG_STATE_GONE = 0x00000007,   // Meldung gegangen und quittiert
    }
}