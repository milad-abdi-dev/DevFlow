using DevFlow.Modules.Workspaces.Presentation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(WorkspacesPresentationAssembly).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(static type =>
        type.FullName?.Replace('+', '.') ?? type.Name);
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "DevFlow API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFlow API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
