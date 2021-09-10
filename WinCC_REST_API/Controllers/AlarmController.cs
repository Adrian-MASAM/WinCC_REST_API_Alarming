using Microsoft.Web.WebSockets;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WinCC_REST_API.Global;
using WinCC_REST_API.Models;

namespace WinCC_REST_API.Controllers
{
    public class AlarmController : ApiController
    {
        readonly HttpContext currentContext = HttpContext.Current;
        //Endpointadresse: http://localhost/api/alarm (GET)
        /// <summary>
        /// Alle aktuellen Alarme
        /// </summary>
        /// <remarks>
        /// Mit einem websocket Request können auf diesem Endpoint die Daten bidirektional empfangen werden. Es wird dasselbe Datenmodel verwendet, wie bei einem
        /// HTTP-GET-Request - im JSON Format.
        /// </remarks>
        /// <returns></returns>
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(Alarm_example))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(IEnumerable<Error>))]
        public HttpResponseMessage Get()
        {
            if (Init._stateIO == false)
            {
                return Request.CreateResponse<Error>(HttpStatusCode.InternalServerError, new Error { errorValue = "Verbindung zu OPC UA Alarms & Conditions Server nicht hergestellt" });
            }
            else if (currentContext.IsWebSocketRequest || currentContext.IsWebSocketRequestUpgrading)
            {
                //var user = currentContext.Request.Cookies["username"].Value;
                //user
                currentContext.AcceptWebSocketRequest(new WSHandler());
                Log.Information("Neuer Websocket Request: " + currentContext.Server.MachineName + currentContext.Timestamp.ToString());
                return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            }
            else
            {
                try
                {
                    return Request.CreateResponse<IEnumerable<Alarm>>(HttpStatusCode.OK, Init._alarms.Values);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }

        }
    }
    public class WSHandler : WebSocketHandler
    {
        private static readonly WebSocketCollection webSocketColl = new WebSocketCollection();

        public WSHandler()
        {
            base.OnOpen();
        }

        public override void OnOpen()
        {
            webSocketColl.Add(this);
            webSocketColl.Broadcast(JsonConvert.SerializeObject(Init._alarms.Values));
        }

        public override void OnClose()
        {
            webSocketColl.Remove(this);
        }

        public static void AlarmNotifier(string alarmlist)
        {
            if (webSocketColl != null)
            {
                try
                {
                    webSocketColl.Broadcast(alarmlist);
                }
                catch (ObjectDisposedException e)
                {
                    Log.Error(e.Message);
                }
                catch (InvalidOperationException e)
                {
                    Log.Error(e.Message);
                }
            }
            else
            {
                Log.Information("No websocket Client connected");
            }
        }

    }
}
