using BrowserBarcodePrinting.Helpers;
using BrowserBarcodePrinting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BrowserBarcodePrinting.Controllers
{
    [RoutePrefix("api/print")]
    public class PrintController : ApiController
    {
        [HttpPost]
        [Route("printdoc")]
        public async Task<IHttpActionResult> NoxBelgeKaydet([FromBody]ModelPrintInfo printInfo)
        {
            ModelPrintResult _result = new ModelPrintResult();
            try
            {
                RawPrinterHelper.SendStringToPrinter(printInfo.PrinterName, printInfo.PrintCommand);
                _result.IsSuccessfull = true;
            }
            catch (Exception e)
            {
                _result.IsSuccessfull = false;
                _result.ErrorMessage = e.Message;
            }
            return Ok(_result);
        }
    }
}
