namespace Middlewares
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Registering services
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<FactoryMiddleware>(); // registering FactoryMiddleware as per scope (see line 66)
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Default Middlewares
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Custom Middelwares

            // Inline

            // Use -> used for chaining middlewares
            // next request delegate is passed with httpcontext, the next middleware is called using next();
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("1st Custom Middleware.\n");
                await next();
                await context.Response.WriteAsync("1st Custom Middleware.\n");
            });

            // Map --> used for handling specific request endpoints
            app.Map("/custom", handler =>
            {
                handler.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("3rd Custom Middleware.\n");
                    await next();
                    await context.Response.WriteAsync("3rd Custom Middleware.\n");
                });

                handler.Run(async context =>
                {
                    await context.Response.WriteAsync("4th Custom Middleware.\n");
                });
            });

            //Defined
            // Conventional based, created at the start of the application, acts like singleton
            app.UseMiddleware<ConventionalMiddleware>();
            //app.UseConventionalMiddleware(); can do this because of extension method in the middleware.

            // Factory based, strongly typed and any dependencies can be injected directly in contructor
            app.UseMiddleware<FactoryMiddleware>(); // need to register service for this

            // Run --> Used as termial middleware
            // only http context is passed, this is the last middleware in a request pipeline
            // nothing after this will execute
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("2nd Custom Middleware.\n");
            });


            app.Run();
        }
    }
}