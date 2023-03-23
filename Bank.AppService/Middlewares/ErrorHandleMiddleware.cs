using Bank.AppService.Wrappers;

namespace Bank.AppService.Middlewares
{
	public class ErrorHandleMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandleMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				var response = context.Response;
				response.ContentType = "application/json";

				var responseModel = new ResponseModel<string>() { Success = false, Message = ex?.Message };

				switch (ex)
				{
					
				}
}
