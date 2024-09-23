using Microsoft.EntityFrameworkCore;
using Wisr.Customer.Api.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "customer:read-personal-details",
        policy => policy.RequireAssertion(context => true)
    );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerDbContext>(
    options => options.UseInMemoryDatabase(databaseName: "Customer")
);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await services.GetRequiredService<CustomerDbContext>().Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
