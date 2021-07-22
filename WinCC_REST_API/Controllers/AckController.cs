using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.AddressSpace;
using OpcLabs.EasyOpc.UA.OperationModel;
using WinCC_REST_API.Models;
using WinCC_REST_API.Global;
using Serilog;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using Swashbuckle.Examples;

namespace WinCC_REST_API.Controllers
{
    public class AckController : ApiController
    {
        //Endpointadresse: HTTP://localhost/api/ack (POST)
        /// <summary>
        /// Quittiert einen Alarm
        /// </summary>
        /// <remarks>
        /// Ein Alarm wird im WinCC quittiert. Anhand der übermittelten Meldenummer (ConditionName) sucht sich das Programm die dazugehörige EventID und übermittelt eine Quittieranforderung an den OPC UA Server
        /// </remarks>
        /// <returns></returns>
        [SwaggerRequestExample(typeof(Ack), typeof(Ack_example))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Model")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid data (ID)")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Alarm mit Nummer <23333> erfolgreich quittiert")]
        [SwaggerResponse(HttpStatusCode.Conflict, "Kein Alarm mit Nummer <23333> anstehend")]
        public HttpResponseMessage Post([FromBody] Ack ack)
        {
            UAEndpointDescriptor endpointDescriptor = Init._endpoint;
            UANodeId nodeId;
            byte[] eventId;

            if (!ModelState.IsValid)
            {
                Log.Error("api/ack: Ungültige Daten übermittelt bekommen!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Model");
            }
            else
            {
                if (ack.ConditionName == null)
                {
                    Log.Error("api/ack: Keine Id erhalten");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data (ID)");
                }
                Log.Information("Quittierung aufgerufen mit Nummer: " + ack.ConditionName);
                Alarm alarm;
                if (Init._alarms.TryGetValue(ack.ConditionName, out alarm))
                {
                    try
                    {
                        Init._alarmsAndConditionsClient.Acknowledge(
                            endpointDescriptor,
                            nodeId = new UANodeId("nsu=http://opcfoundation.org/UA/;i=2881"),
                            eventId = System.Text.Encoding.Unicode.GetBytes(alarm.EventId.ToString()),
                            "OPCUA");
                        Log.Information("Alarm mit Nummer <" + ack.ConditionName + "> erfolgreich quittiert");
                        return Request.CreateResponse(HttpStatusCode.Accepted, "Alarm mit Nummer <" + ack.ConditionName + "> erfolgreich quittiert");
                    }
                    catch (UAException uaException)
                    {
                        Log.Error(uaException.InnerException + " (Kein Alarm mit dieser ID anstehend)");
                        return Request.CreateResponse(HttpStatusCode.Conflict, uaException.InnerException + "; Kein Alarm mit Nummer <" + ack.ConditionName + "> anstehend");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, " Kein Alarm mit Nummer <" + ack.ConditionName + "> anstehend");
                }


            }
        }
    }
}
