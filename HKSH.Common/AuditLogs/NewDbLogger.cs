using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace HKSH.Common.AuditLogs
{
    /// <summary>
    /// NewDbLogger
    /// </summary>
    public class NewDbLogger
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewDbLogger"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public NewDbLogger(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Enables the database log.
        /// </summary>
        public void EnableDbLog()
        {
            _dbContext.SavedChanges += DbContext_SavedChanges;
        }

        /// <summary>
        /// Handles the SavedChanges event of the DbContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SavedChangesEventArgs"/> instance containing the event data.</param>
        public void DbContext_SavedChanges(object sender, SavedChangesEventArgs e)
        {
            EntityEntry[] entityEntries = _dbContext.ChangeTracker.Entries().Where(a => a.State == EntityState.Modified || a.State == EntityState.Deleted || a.State == EntityState.Added).ToArray();
            var serializeSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            foreach (EntityEntry item in entityEntries)
            {
                if (item.Entity is not IAuditLog)
                {
                    continue;
                }

                Console.WriteLine("NewDbLogger 当前保存后的模型：" + JsonConvert.SerializeObject(item.Entity, serializeSettings));
            }
        }
    }
}
