using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;
using AIC.Lib.DataClasses;

namespace AIC.Lib.DataClasses
{
    public partial class EffortlessAPIProject
    {
        private void InitPoco()
        {
        }

        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();




        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EffortlessAPIProjectId")]
        public String EffortlessAPIProjectId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ModifiedOn")]
        public Nullable<DateTime> ModifiedOn { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Account")]
        [RemoteIsCollection]
        public String Account { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountName")]
        [RemoteIsCollection]
        public String AccountName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AdminRole")]
        public String AdminRole { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AirtableId")]
        public String AirtableId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Alias")]
        public String Alias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AllowAirtableWhere")]
        public Nullable<Boolean> AllowAirtableWhere { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AutoAssignedID")]
        public Nullable<Int32> AutoAssignedID { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AutoNumber")]
        public String AutoNumber { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ConnectionString")]
        public String ConnectionString { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DefaultDesiredAlias")]
        public String DefaultDesiredAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DesiredAlias")]
        public String DesiredAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EffortlessApiService")]
        [RemoteIsCollection]
        public String EffortlessApiService { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EmailColumnName")]
        public String EmailColumnName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EntitiesTable")]
        public String EntitiesTable { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "GuestRole")]
        public String GuestRole { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "HeadStage")]
        [RemoteIsCollection]
        public String HeadStage { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "HeadVersion")]
        [RemoteIsCollection]
        public String HeadVersion { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsLexiconSecret")]
        public Nullable<Boolean> IsLexiconSecret { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsSchemaSecret")]
        public Nullable<Boolean> IsSchemaSecret { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LowerAirtableId")]
        public String LowerAirtableId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OwnerEmail")]
        [RemoteIsCollection]
        public String OwnerEmail { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProductionStage")]
        [RemoteIsCollection]
        public String ProductionStage { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectBuilds")]
        public String ProjectBuilds { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectConnections")]
        [RemoteIsCollection]
        public String[] ProjectConnections { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectEmailList")]
        public String ProjectEmailList { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectEmails")]
        [RemoteIsCollection]
        public String[] ProjectEmails { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectLexiconTerms")]
        [RemoteIsCollection]
        public String ProjectLexiconTerms { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectRequestSummary")]
        [RemoteIsCollection]
        public String[] ProjectRequestSummary { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectStage")]
        public String ProjectStage { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectStages")]
        [RemoteIsCollection]
        public String[] ProjectStages { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectType")]
        public String ProjectType { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectVersions")]
        [RemoteIsCollection]
        public String[] ProjectVersions { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RemoteEndpoints")]
        [RemoteIsCollection]
        public String[] RemoteEndpoints { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RoleColumnName")]
        public String RoleColumnName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RolesTable")]
        public String RolesTable { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RowKey")]
        public String RowKey { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ServiceLogo")]
        [RemoteIsCollection]
        public AirtableAttachment[] ServiceLogo { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UseDebugDataStorage")]
        public Nullable<Boolean> UseDebugDataStorage { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UserRole")]
        public String UserRole { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UserTable")]
        public String UserTable { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EffortlessAPIServiceName")]
        [RemoteIsCollection]
        public String EffortlessAPIServiceName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "LastPublished")]
        public Nullable<DateTime> LastPublished { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RECORD_ID")]
        public String RECORD_ID { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "HeadStageIsEC2")]
        [RemoteIsCollection]
        public Nullable<Boolean> HeadStageIsEC2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SubscriptionTotals")]
        [RemoteIsCollection]
        public Nullable<decimal> SubscriptionTotals { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountDiscount")]
        [RemoteIsCollection]
        public Nullable<decimal> AccountDiscount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomMaxPages")]
        public Nullable<Int32> CustomMaxPages { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountDefaultMaxPages")]
        [RemoteIsCollection]
        public Nullable<Int32> AccountDefaultMaxPages { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "MaxPages")]
        public Nullable<Int32> MaxPages { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Description")]
        public String Description { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DeploymentMode")]
        public String DeploymentMode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsTestProject")]
        public Nullable<Int32> IsTestProject { get; set; }







    }
}
