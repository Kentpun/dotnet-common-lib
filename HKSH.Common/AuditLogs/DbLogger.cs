using Microsoft.EntityFrameworkCore;

namespace HKSH.Common.AuditLogs;

/// <summary>
/// DbLogger
/// </summary>
public class DbLogger
{
    /// <summary>
    /// The database context
    /// </summary>
    private readonly DbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbLogger"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public DbLogger(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Enables the database log.
    /// </summary>
    public void EnableDbLog()
    {
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        _dbContext.SavingChanges += DbContext_SavingChanges;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        _dbContext.SavedChanges += DbContext_SavedChanges;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        _dbContext.SaveChangesFailed += DbContext_SaveChangesFailed;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
    }

    /// <summary>
    /// Handles the SavingChanges event of the DbContext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="SavingChangesEventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    private void DbContext_SavingChanges(object sender, SavingChangesEventArgs e)
    {
    }

    /// <summary>
    /// Handles the SavedChanges event of the DbContext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="SavedChangesEventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    private void DbContext_SavedChanges(object sender, SavedChangesEventArgs e)
    {
    }

    /// <summary>
    /// Handles the SaveChangesFailed event of the DbContext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="SaveChangesFailedEventArgs"/> instance containing the event data.</param>
    /// <returns></returns>
    private void DbContext_SaveChangesFailed(object sender, SaveChangesFailedEventArgs e)
    {
    }
}