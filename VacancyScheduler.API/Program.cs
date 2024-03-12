using Quartz;
using VacancyScheduler.API.VacancyScheduleJobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/***********Quartz.NET*********/
IConfigurationSection quartzConfiguration = builder.Configuration.GetSection("Quartz"); // Quartz узел конфигурации
builder.Services.AddQuartz(q =>
{
    var vacancyImportJobKey = new JobKey("VacancyImportJob");
    q.AddJob<VacancyImportJob>(opts => opts.WithIdentity(vacancyImportJobKey));

    q.AddTrigger(opts => opts
        .ForJob(vacancyImportJobKey)
        .WithIdentity("VacancyImportJob-trigger")
        .WithCronSchedule("0 * * ? * *")            //This Cron interval can be described as "run every minute" (when second is zero)
    );

    var analyticsBuilderJobKey = new JobKey("AnalyticsBuilderJob");
    q.AddJob<AnalyticsBuilderJob>(opts => opts.WithIdentity(analyticsBuilderJobKey));

    q.AddTrigger(opts => opts
        .ForJob(analyticsBuilderJobKey)
        .WithIdentity("AnalyticsBuilderJob-trigger")
        .WithCronSchedule("0 * * ? * *")            //This Cron interval can be described as "run every minute" (when second is zero)
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.Run();
