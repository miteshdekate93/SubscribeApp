using Microsoft.EntityFrameworkCore;
using SubscribeApp.Data;
using SubscribeApp.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Services ────────────────────────────────────────────────────────────────

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000", "http://frontend")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "SubscribeApp API",
        Version = "v1",
        Description = "REST API for managing email subscribers."
    });
});

var app = builder.Build();

// ── Middleware ───────────────────────────────────────────────────────────────

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SubscribeApp API v1"));
}

app.UseCors("AllowFrontend");

// ── Auto-migrate on startup ──────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ── Endpoints ────────────────────────────────────────────────────────────────

var api = app.MapGroup("/api/subscribers").WithTags("Subscribers");

// GET /api/subscribers
api.MapGet("/", async (AppDbContext db) =>
{
    var subscribers = await db.Subscribers
        .OrderByDescending(s => s.CreatedAt)
        .ToListAsync();
    return Results.Ok(subscribers);
})
.WithName("GetSubscribers")
.WithSummary("Get all subscribers");

// POST /api/subscribers
api.MapPost("/", async (Subscriber subscriber, AppDbContext db) =>
{
    // Check for duplicate email
    var exists = await db.Subscribers.AnyAsync(s => s.Email == subscriber.Email);
    if (exists)
        return Results.Conflict(new { message = "A subscriber with this email already exists." });

    subscriber.CreatedAt = DateTime.UtcNow;
    db.Subscribers.Add(subscriber);
    await db.SaveChangesAsync();
    return Results.Created($"/api/subscribers/{subscriber.Id}", subscriber);
})
.WithName("CreateSubscriber")
.WithSummary("Add a new subscriber");

// DELETE /api/subscribers/{id}
api.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
{
    var subscriber = await db.Subscribers.FindAsync(id);
    if (subscriber is null)
        return Results.NotFound(new { message = $"Subscriber with id {id} not found." });

    db.Subscribers.Remove(subscriber);
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteSubscriber")
.WithSummary("Delete a subscriber by ID");

app.Run();
