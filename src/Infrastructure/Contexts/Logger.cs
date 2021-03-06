using ErpDashboard.Application.Enums;
using ErpDashboard.Application.Interfaces.Services;
using ErpDashboard.Application.Models;
using ErpDashboard.Domain.Contracts;
using ErpDashboard.Shared.CustomAttribute;
using ErpDashboard.Shared.Wrapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Data;

namespace ErpDashboard.Infrastructure.Contexts
{
    public class Logger : DbContext
    {
        private protected ICurrentUserService _currentUser;
        public Logger(DbContextOptions<ERBSYSTEMContext> options, ICurrentUserService currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }
        public virtual DbSet<TbSystemLogger> TbSystemLoggers { get; set; }
        public List<AuditEntryy> OnBeforeSaveChanges(int? userid, int? comId)
        {

            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntryy>();
            var ChangedData = ChangeTracker.Entries();
            foreach (var entry in ChangedData)
            {
                if (entry.Entity is TbSystemLogger || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntryy(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userid ?? 0;
                auditEntry.CompanyID = comId ?? 0;

                auditEntries.Add(auditEntry);

                var EntityProperties = entry.Entity.GetType().GetProperties();

				if (entry.State == EntityState.Added)
				{
                    SetCompanyIdentity(entry);
                }
                foreach (var property in entry.Properties)
                {

                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;

                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && property.OriginalValue?.Equals(property.CurrentValue) == false)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                TbSystemLoggers.Add(auditEntry.ToLog());
            }
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }
        private void SetCompanyIdentity(EntityEntry entry) 
        {
            string CompanyIdFieldName = "";
               var propertiesWithAttribute = entry.Entity.GetType().GetProperties()
                .Select(pi => new { Property = pi, Attribute = pi.GetCustomAttributes(typeof(CompanyIdAttribute), true).FirstOrDefault() as CompanyIdAttribute })
                .Where(x => x.Attribute != null)
                .ToList();
			if (propertiesWithAttribute.Count > 0)
			{
               CompanyIdFieldName = entry.Context.Model.FindEntityType(entry.Entity.GetType()).GetProperty(propertiesWithAttribute[0].Property.Name).GetColumnName();
                propertiesWithAttribute[0].Property.SetValue(entry.Entity, _currentUser.CompanyID);
            }
            var propertiesWithAttributeCompanyIdentity = entry.Entity.GetType().GetProperties()
                 .Select(pi => new { Property = pi, Attribute = pi.GetCustomAttributes(typeof(CompanyIdentityAttribute), true).FirstOrDefault() as CompanyIdentityAttribute })
                 .Where(x => x.Attribute != null)
                 .ToList();
			if (propertiesWithAttributeCompanyIdentity.Count > 0)
			{
               var property = propertiesWithAttributeCompanyIdentity[0].Property;
                    var propName = entry.Context.Model.FindEntityType(entry.Entity.GetType()).GetProperty(property.Name).GetColumnName();
                var tableName = entry.Context.Model.FindEntityType(entry.Entity.GetType()).GetTableName();
                    SqlParameter[] @params =
                        {
                                 new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output}
                            };
                    var CompanyCondition = string.IsNullOrEmpty(CompanyIdFieldName) ? string.Empty : $"WHERE {CompanyIdFieldName}={_currentUser.CompanyID}";
                    var qry = $"set @returnVal =(SELECT ISNULL(MAX({propName}),0) FROM {tableName} {CompanyCondition})";
                    var count = entry.Context.Database.ExecuteSqlRaw(qry, @params);
                    var result = @params[0].Value ?? 0;
                    property.SetValue(entry.Entity, (int)result + 1);
                
            }
        }
        public virtual async Task<int> SaveChangesAsync(int? userId = null, int? commId = null, CancellationToken cancellationToken = new())
        {
            var auditEntries = OnBeforeSaveChanges(_currentUser.SystemUserId, _currentUser.CompanyID);
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChanges(auditEntries, cancellationToken);
            return result;
        }

        private Task OnAfterSaveChanges(List<AuditEntryy> auditEntries, CancellationToken cancellationToken = new CancellationToken())
        {


            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {

                foreach (var prop in auditEntry.TemporaryProperties)
                {

                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }

                }
                TbSystemLoggers.Add(auditEntry.ToLog());
            }
            return SaveChangesAsync(cancellationToken);
        }
    }
    public class AuditEntryy
    {
        public AuditEntryy(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public int UserId { get; set; }
        public int CompanyID { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public TbSystemLogger ToLog()
        {
            var audit = new TbSystemLogger();
            audit.UserId = UserId;
            audit.CompanyId = CompanyID;
            audit.Action = AuditType.ToString();
            audit.TableName = TableName;
            audit.CreatedOn = DateTime.Now;
            audit.ActionDate = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValue = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValue = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.PropertyName = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
