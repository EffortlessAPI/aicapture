using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using dc = AIC.Lib.DataClasses;
using System.Collections.Generic;


namespace AIC.SassyMQ.Lib
{
    public partial class StandardPayload
    {
        public string RoutingKey { get; set; }
        
        private StandardPayload(SMQActorBase actor, string content, bool final)
        {
            this.PayloadId = Guid.NewGuid().ToString();

            this.__Actor = actor;
            if (!ReferenceEquals(this.__Actor, null))
            {
                this.SenderId = actor.SenderId.ToString();
                this.SenderName = actor.SenderName;
                this.AccessToken = actor.AccessToken;
            }
            else
            {
                this.SenderId = Guid.NewGuid().ToString();
                this.SenderName = "Unnamed Actor";
                this.AccessToken = null;
            }

            this.Content = content;
        }

        // 31 odxml properties
        
        public String HelpfulPromptId { get; set; }
        
        public dc.HelpfulPrompt HelpfulPrompt { get; set; }
        
        public List<dc.HelpfulPrompt> HelpfulPrompts { get; set; }
        
        public String AIModelId { get; set; }
        
        public dc.AIModel AIModel { get; set; }
        
        public List<dc.AIModel> AIModels { get; set; }
        
        public String SharedDirectoryId { get; set; }
        
        public dc.SharedDirectory SharedDirectory { get; set; }
        
        public List<dc.SharedDirectory> SharedDirectories { get; set; }
        
        public String AICDirectoryId { get; set; }
        
        public dc.AICDirectory AICDirectory { get; set; }
        
        public List<dc.AICDirectory> AICDirectories { get; set; }
        
        public String RateLimitId { get; set; }
        
        public dc.RateLimit RateLimit { get; set; }
        
        public List<dc.RateLimit> RateLimits { get; set; }
        
        public String ContextSkillId { get; set; }
        
        public dc.ContextSkill ContextSkill { get; set; }
        
        public List<dc.ContextSkill> ContextSkills { get; set; }
        
        public String AppUserId { get; set; }
        
        public dc.AppUser AppUser { get; set; }
        
        public List<dc.AppUser> AppUsers { get; set; }
        
        public String AICWorkspaceId { get; set; }
        
        public dc.AICWorkspace AICWorkspace { get; set; }
        
        public List<dc.AICWorkspace> AICWorkspaces { get; set; }
        
        public String RepoId { get; set; }
        
        public dc.Repo Repo { get; set; }
        
        public List<dc.Repo> Repos { get; set; }
        
        public String AICContextCategoryId { get; set; }
        
        public dc.AICContextCategory AICContextCategory { get; set; }
        
        public List<dc.AICContextCategory> AICContextCategories { get; set; }
        
        public String AICPlanId { get; set; }
        
        public dc.AICPlan AICPlan { get; set; }
        
        public List<dc.AICPlan> AICPlans { get; set; }
        
        public String AICContextOutputId { get; set; }
        
        public dc.AICContextOutput AICContextOutput { get; set; }
        
        public List<dc.AICContextOutput> AICContextOutputs { get; set; }
        
        public String AICModelPricingId { get; set; }
        
        public dc.AICModelPricing AICModelPricing { get; set; }
        
        public List<dc.AICModelPricing> AICModelPricings { get; set; }
        
        public String AICAgentId { get; set; }
        
        public dc.AICAgent AICAgent { get; set; }
        
        public List<dc.AICAgent> AICAgents { get; set; }
        
        public String LanguageId { get; set; }
        
        public dc.Language Language { get; set; }
        
        public List<dc.Language> Languages { get; set; }
        
        public String AppUserUsageId { get; set; }
        
        public dc.AppUserUsage AppUserUsage { get; set; }
        
        public List<dc.AppUserUsage> AppUserUsages { get; set; }
        
        public String CripIntegrationId { get; set; }
        
        public dc.CripIntegration CripIntegration { get; set; }
        
        public List<dc.CripIntegration> CripIntegrations { get; set; }
        
        public String AIProviderId { get; set; }
        
        public dc.AIProvider AIProvider { get; set; }
        
        public List<dc.AIProvider> AIProviders { get; set; }
        
        public String AICSkillId { get; set; }
        
        public dc.AICSkill AICSkill { get; set; }
        
        public List<dc.AICSkill> AICSkills { get; set; }
        
        public String AICContextId { get; set; }
        
        public dc.AICContext AICContext { get; set; }
        
        public List<dc.AICContext> AICContexts { get; set; }
        
        public String FriendRequestId { get; set; }
        
        public dc.FriendRequest FriendRequest { get; set; }
        
        public List<dc.FriendRequest> FriendRequests { get; set; }
        
        public String AICConversationId { get; set; }
        
        public dc.AICConversation AICConversation { get; set; }
        
        public List<dc.AICConversation> AICConversations { get; set; }
        
        public String EntityId { get; set; }
        
        public dc.Entity Entity { get; set; }
        
        public List<dc.Entity> Entities { get; set; }
        
        public String AICSkillVersionId { get; set; }
        
        public dc.AICSkillVersion AICSkillVersion { get; set; }
        
        public List<dc.AICSkillVersion> AICSkillVersions { get; set; }
        
        public String AICMessageId { get; set; }
        
        public dc.AICMessage AICMessage { get; set; }
        
        public List<dc.AICMessage> AICMessages { get; set; }
        
        public String AICContextOutputTypeId { get; set; }
        
        public dc.AICContextOutputType AICContextOutputType { get; set; }
        
        public List<dc.AICContextOutputType> AICContextOutputTypes { get; set; }
        
        public String AICContextTypeId { get; set; }
        
        public dc.AICContextType AICContextType { get; set; }
        
        public List<dc.AICContextType> AICContextTypes { get; set; }
        
        public String RepoBranchId { get; set; }
        
        public dc.RepoBranch RepoBranch { get; set; }
        
        public List<dc.RepoBranch> RepoBranches { get; set; }
        
        public String AICProjectId { get; set; }
        
        public dc.AICProject AICProject { get; set; }
        
        public List<dc.AICProject> AICProjects { get; set; }
        
        public String AICFileId { get; set; }
        
        public dc.AICFile AICFile { get; set; }
        
        public List<dc.AICFile> AICFiles { get; set; }
        
        public String AICSkillStepId { get; set; }
        
        public dc.AICSkillStep AICSkillStep { get; set; }
        
        public List<dc.AICSkillStep> AICSkillSteps { get; set; }

        public List<dc.EffortlessAPIProject> EffortlessAPIProjects { get; set; }
        
        public List<dc.ProjectStage> ProjectStages { get; set; }
        
        
        public String ToJSON() 
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        private void HandleReplyTo(object sender, PayloadEventArgs e)
        {
            if (e.Payload.IsHandled && e.BasicDeliverEventArgs.BasicProperties.CorrelationId == this.PayloadId)
            {
                this.ReplyPayload = e.Payload;
                this.ReplyBDEA = e.BasicDeliverEventArgs;
                this.ReplyRecieved = true;
            }
        }

       
        public Task WaitForReply(PayloadHandler payloadHandler = null, PayloadHandler timeoutHandler = null, int waitTimeout = StandardPayload.DEFAULT_TIMEOUT)
        {
            var actor = this.__Actor;
            if (ReferenceEquals(actor, null)) throw new Exception("Can't handle response if payload.Actor is null");
            else
            {
                actor.ReplyTo += this.HandleReplyTo;
                var waitTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (waitTimeout > 0 && !ReferenceEquals(payloadHandler, null))
                        {

                            this.TimedOutWaiting = false;
                            var startedAt = DateTime.Now;

                            while (!this.ReplyRecieved && !this.TimedOutWaiting && DateTime.Now < startedAt.AddSeconds(waitTimeout))
                            {
                                Thread.Sleep(100);
                            }

                            if (!this.ReplyRecieved) this.TimedOutWaiting = true;

                            var errorMessageReceived = !ReferenceEquals(this.ReplyPayload, null) && !String.IsNullOrEmpty(this.ReplyPayload.ErrorMessage);

                            if (this.ReplyRecieved && (!errorMessageReceived || ReferenceEquals(timeoutHandler, null)))
                            {
                                this.ReplyPayload.__Actor = actor;
                                payloadHandler(this.ReplyPayload, this.ReplyBDEA);
                            }
                            else if (!ReferenceEquals(timeoutHandler, null)) timeoutHandler(this.ReplyPayload, default(BasicDeliverEventArgs));
                        }

                    }
                    finally
                    {
                        actor.ReplyTo -= this.HandleReplyTo;
                    }
                });
                return waitTask;
            }
        }
    }
}