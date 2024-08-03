namespace _2ch.WebApi.Middlewares
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Cookies.TryGetValue("UserId", out var userId))
            {
                userId = Guid.NewGuid().ToString();
                context.Response.Cookies.Append("UserId", userId, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            }

            await _next(context);
        }
    }
}
