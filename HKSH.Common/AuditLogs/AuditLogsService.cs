using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HKSH.Common.AuditLogs;

/// <summary>
/// AuditLogService
/// </summary>
public class AuditLogsService
{
    /// <summary>
    /// The logs collection
    /// </summary>
    private readonly IMongoCollection<AuditLog> _logsCollection;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditLogsService"/> class.
    /// </summary>
    /// <param name="logDatabaseSettings">The log database settings.</param>
    public AuditLogsService(
        IOptions<AuditLogStoreDatabaseSettings> logDatabaseSettings)
    {
        var mongoClient = new MongoClient(logDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(logDatabaseSettings.Value.DatabaseName);
        _logsCollection = mongoDatabase.GetCollection<AuditLog>(logDatabaseSettings.Value.AuditLogsCollectionName);
    }

    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <returns></returns>
    public async Task<List<AuditLog>> GetAsync() => await _logsCollection.Find(_ => true).ToListAsync();

    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public async Task<AuditLog?> GetAsync(string id) => await _logsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    /// <summary>
    /// Creates the asynchronous.
    /// </summary>
    /// <param name="newLog">The new log.</param>
    public async Task CreateAsync(AuditLog newLog) => await _logsCollection.InsertOneAsync(newLog);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="updatedLog">The updated log.</param>
    public async Task UpdateAsync(string id, AuditLog updatedLog) => await _logsCollection.ReplaceOneAsync(x => x.Id == id, updatedLog);

    /// <summary>
    /// Removes the asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    public async Task RemoveAsync(string id) => await _logsCollection.DeleteOneAsync(x => x.Id == id);
}