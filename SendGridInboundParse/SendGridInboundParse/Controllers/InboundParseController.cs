using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SendGridInboundParse.Controllers
{
    public class InboundParseController : ApiController
    {
        [HttpPost]
        // POST api/<controller>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<HttpResponseMessage> Post()
        {
            try
            {
                string root = HttpContext.Current.Server.MapPath("~/App_Data");
                var provider = new MultipartFormDataStreamProvider(root);

                StringBuilder sb = new StringBuilder(); // Holds the response body

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                //Bind to incoming email model
                Models.InboundParseModel _em = new Models.InboundParseModel
                {
                    to = provider.FormData.GetValues("to").SingleOrDefault(),
                    from = provider.FormData.GetValues("from").SingleOrDefault(),
                    subject = provider.FormData.GetValues("subject").SingleOrDefault(),
                    html = provider.FormData.GetValues("html").SingleOrDefault(),
                    sender_ip = provider.FormData.GetValues("sender_ip").SingleOrDefault(),
                    headers = provider.FormData.GetValues("headers").SingleOrDefault(),
                    dkim = provider.FormData.GetValues("dkim").SingleOrDefault(),
                    text = provider.FormData.GetValues("text").SingleOrDefault(),
                    SPF = provider.FormData.GetValues("spf").SingleOrDefault(),
                    attachments = provider.FormData.GetValues("attachments").SingleOrDefault(),
                    envelope = provider.FormData.GetValues("envelope").SingleOrDefault(),
                    charsets = provider.FormData.GetValues("charsets").SingleOrDefault()
                };
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
            {
                throw;
            }
        }
    }
}
