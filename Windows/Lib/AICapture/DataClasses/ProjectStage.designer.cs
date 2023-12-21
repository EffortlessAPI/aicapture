using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Extensions;
using AIC.Lib.DataClasses;

namespace AIC.Lib.DataClasses
{
    public partial class ProjectStage
    {
        private void InitPoco()
        {
        }

        partial void AfterGet();
        partial void BeforeInsert();
        partial void AfterInsert();
        partial void BeforeUpdate();
        partial void AfterUpdate();




        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectStageId")]
        public String ProjectStageId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EffortlessApiProjects 2")]
        [RemoteIsCollection]
        public String[] EffortlessApiProjects2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "VSPAlias")]
        public String VSPAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsEC2")]
        [RemoteIsCollection]
        public Nullable<Boolean> IsEC2 { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ActiveVersion")]
        [RemoteIsCollection]
        public String ActiveVersion { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Alias")]
        public String Alias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Attachments")]
        public AirtableAttachment[] Attachments { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AwsId")]
        public String AwsId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AwsRegion")]
        public String AwsRegion { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CpuUnits")]
        public String CpuUnits { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomAlias")]
        public String CustomAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomAwsId")]
        public String CustomAwsId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomAwsRegion")]
        public String CustomAwsRegion { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomCpuUnits")]
        public Nullable<Int32> CustomCpuUnits { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomDesiredCount")]
        public Nullable<Int32> CustomDesiredCount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomMaxMemory")]
        public String CustomMaxMemory { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomProfile")]
        public String CustomProfile { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomRabbitMQHostUrl")]
        public String CustomRabbitMQHostUrl { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomSecurityGroup")]
        public String CustomSecurityGroup { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomSubnet")]
        public String CustomSubnet { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomTaskDefinitionJsonLocation")]
        public String CustomTaskDefinitionJsonLocation { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CustomTaskExecutionRole")]
        public String CustomTaskExecutionRole { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "DesiredCount")]
        public String DesiredCount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "HeadStageForProjects")]
        [RemoteIsCollection]
        public String[] HeadStageForProjects { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "MaxMemory")]
        public String MaxMemory { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Name")]
        public String Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Notes")]
        public String Notes { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OverrideRabbitMQEndpoint")]
        [RemoteIsCollection]
        public String OverrideRabbitMQEndpoint { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "OverrideSassyMQEndpoint")]
        [RemoteIsCollection]
        public String OverrideSassyMQEndpoint { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Profile")]
        public String Profile { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Project")]
        [RemoteIsCollection]
        public String Project { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectAlias")]
        [RemoteIsCollection]
        public String ProjectAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectEmails")]
        [RemoteIsCollection]
        public String ProjectEmails { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectId")]
        [RemoteIsCollection]
        public Nullable<Int32> ProjectId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ProjectVersionMismatch")]
        public String ProjectVersionMismatch { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RabbitMQHostUrl")]
        public String RabbitMQHostUrl { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SecurityGroup")]
        public String SecurityGroup { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SPVAlias")]
        public String SPVAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Stage")]
        public String Stage { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "StageAlias")]
        public String StageAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "Subnet")]
        public String Subnet { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskAwsId")]
        [RemoteIsCollection]
        public String TaskAwsId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskAwsRegion")]
        [RemoteIsCollection]
        public String TaskAwsRegion { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskClusterName")]
        [RemoteIsCollection]
        public String TaskClusterName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskCpuUnits")]
        [RemoteIsCollection]
        public String TaskCpuUnits { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskDefinitionJsonLocation")]
        public String TaskDefinitionJsonLocation { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskDesiredCount")]
        [RemoteIsCollection]
        public String TaskDesiredCount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskExecutionRole")]
        public String TaskExecutionRole { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskMaxMemory")]
        [RemoteIsCollection]
        public String TaskMaxMemory { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskProfile")]
        [RemoteIsCollection]
        public String TaskProfile { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskRabbitMQAdminUsername")]
        [RemoteIsCollection]
        public String TaskRabbitMQAdminUsername { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskRabbitMQHostUrl")]
        [RemoteIsCollection]
        public String TaskRabbitMQHostUrl { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskSecurityGroup")]
        [RemoteIsCollection]
        public String TaskSecurityGroup { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskSubnet")]
        [RemoteIsCollection]
        public String TaskSubnet { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskTaskDefinitionJsonLocation")]
        [RemoteIsCollection]
        public String TaskTaskDefinitionJsonLocation { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TaskTaskExecutionRole")]
        [RemoteIsCollection]
        public String TaskTaskExecutionRole { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "TemplateTaskEndpoint")]
        [RemoteIsCollection]
        public String TemplateTaskEndpoint { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "VersionNumber")]
        [RemoteIsCollection]
        public String VersionNumber { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "VersionProject")]
        [RemoteIsCollection]
        public String VersionProject { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "VPSAlias")]
        public String VPSAlias { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EffortlessApiProjects")]
        public String EffortlessApiProjects { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "EnableDeveloperDiscount")]
        public Nullable<Boolean> EnableDeveloperDiscount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SubscriptionId")]
        public String SubscriptionId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "MonthlySubscription")]
        public Nullable<decimal> MonthlySubscription { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SubscriptionStartDate")]
        public Nullable<DateTime> SubscriptionStartDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SubscriptionEndDate")]
        public Nullable<DateTime> SubscriptionEndDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "CreatedOn")]
        public Nullable<DateTime> CreatedOn { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "UpdatedOn")]
        public Nullable<DateTime> UpdatedOn { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "BasePrice")]
        public Nullable<decimal> BasePrice { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SuggestedMonthlySubscription")]
        public Nullable<decimal> SuggestedMonthlySubscription { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "StageNumber")]
        public Nullable<Int32> StageNumber { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsDeleted")]
        public Nullable<Boolean> IsDeleted { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "ActiveMonthlySubscription")]
        public Nullable<decimal> ActiveMonthlySubscription { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "AccountDiscount")]
        [RemoteIsCollection]
        public Nullable<decimal> AccountDiscount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "SuggestedMonthlySubscriptionWithAccountDiscount")]
        public Nullable<decimal> SuggestedMonthlySubscriptionWithAccountDiscount { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "RecId")]
        public String RecId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "HeadLexiconTerms")]
        [RemoteIsCollection]
        public String HeadLexiconTerms { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, PropertyName = "IsTestProject")]
        [RemoteIsCollection]
        public Nullable<Int32> IsTestProject { get; set; }







    }
}
