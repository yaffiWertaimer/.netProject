using Newtonsoft.Json;

namespace Subscriber.WebApi.Config
{
    public class Middleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<Middleware> _looger;
        public Middleware(RequestDelegate next, ILogger<Middleware> looger)
        {
            this.next = next;
            _looger = looger;
        }
      
        public async Task Invoke(HttpContext context )
        {
            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");

                _looger.LogDebug($"start cell api {context.Request.Path} start in {time} ");
                await next(context);

                string time2 = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");

                _looger.LogInformation($"end  cell api {context.Request.Path} start in {time2} ");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
