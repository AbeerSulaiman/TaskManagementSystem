using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data
{
    
    public class TaskDBContext: DbContext 
    {
        protected readonly IConfiguration Configuration;
        public TaskDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseLoggerFactory(LoggerFactory);
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        public TaskDBContext(): base() {}
        public DbSet<Member> Members { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<TodoTask> TodoTasks { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
    
}
