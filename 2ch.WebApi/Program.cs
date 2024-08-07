using _2ch.Application.DbConnection;
using _2ch.Application.Interfaces;
using _2ch.Application.Repositories;
using _2ch.Application.Services;
using _2ch.Persistence;
using _2ch.Persistence.Migrations;
using _2ch.Persistence.Repositories;
using _2ch.WebApi.Middlewares;
using Minio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMinioClient>(sp =>
    new MinioClient()
        .WithEndpoint("minio:9000")
        .WithCredentials("jPGyFNyA12IccwqKId06", "gKO5TS9M4i0lSoVLCrse2krvXy9Xqa55OMXNrBFQ")
        .Build());

builder.Services.AddScoped<IThreadRepository, ThreadRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IAnonymousUserRepository, AnonymousUserRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IThreadService, ThreadService>();
builder.Services.AddScoped<IAnonymousUserService, AnonymousUserService>();
builder.Services.AddSingleton<RedisCacheService>();

builder.Services.AddScoped<IDbConnectionFactory>(sp =>
                new NpgsqlConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();

builder.Services.AddScoped<IMigrationService, MigrationService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<UserIdMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var migrationService = services.GetRequiredService<IMigrationService>();
    migrationService.RunMigrations();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Urls.Add("http://*:90");

app.Run();
