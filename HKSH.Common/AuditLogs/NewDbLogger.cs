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
            _dbContext.SavingChanges += DbContext_SavingChanges;
            _dbContext.SavedChanges += DbContext_SavedChanges;
            Console.WriteLine("NewDbLogger 追加事件成功");
        }

        /// <summary>
        /// Handles the SavingChanges event of the DbContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SavingChangesEventArgs"/> instance containing the event data.</param>
        private void DbContext_SavingChanges(object sender, SavingChangesEventArgs e)
        {
            Console.WriteLine("NewDbLogger DbContext_SavingChanges");

            EntityEntry[] entityEntries = _dbContext.ChangeTracker.Entries().Where(a => a.State == EntityState.Modified || a.State == EntityState.Deleted || a.State == EntityState.Added).ToArray();
            var serializeSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            foreach (EntityEntry item in entityEntries)
            {
                if (item.Entity is not IAuditLog)
                {
                    continue;
                }

                Console.WriteLine("NewDbLogger DbContext_SavingChanges 当前保存后的模型：" + JsonConvert.SerializeObject(item.Entity, serializeSettings));
            }
        }

        /// <summary>
        /// Handles the SavedChanges event of the DbContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SavedChangesEventArgs"/> instance containing the event data.</param>
        private void DbContext_SavedChanges(object sender, SavedChangesEventArgs e)
        {
            Console.WriteLine("NewDbLogger DbContext_SavedChanges");

            EntityEntry[] entityEntries = _dbContext.ChangeTracker.Entries().Where(a => a.State == EntityState.Modified || a.State == EntityState.Deleted || a.State == EntityState.Added).ToArray();
            var serializeSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            foreach (EntityEntry item in entityEntries)
            {
                if (item.Entity is not IAuditLog)
                {
                    continue;
                }

                Console.WriteLine("NewDbLogger DbContext_SavedChanges 当前保存后的模型：" + JsonConvert.SerializeObject(item.Entity, serializeSettings));
            }
        }
    }
}
