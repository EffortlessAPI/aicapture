using AIC.SassyMQ.Lib;
using Newtonsoft.Json;
using AIC.Lib.DataClasses;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIClassLibrary.RoleHandlers
{
    public abstract class RoleHandlerBase
    {
        public abstract string Handle(string invoke, string data, string where, int maxPages = 5);

        protected string SerializePayload(StandardPayload reply)
        {
            return JsonConvert.SerializeObject(reply, Formatting.Indented, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public abstract void AddHelp(StringBuilder sb, string helpTerm);

        public abstract EffortlessAPIProject GetProjectByAlias(string eapiProjectAlias);

        public abstract ProjectStage GetProjectStageByName(EffortlessAPIProject eapiProject, string projectStageName);
    }

    public abstract class RoleHandlerBase<T> : RoleHandlerBase
        where T : SMQActorBase
    {
        private T _smqActor;

        public T SMQActor
        {
            get
            {
                if (_smqActor is null)
                {
                    _smqActor = Activator.CreateInstance(typeof(T), this.AMQPS) as T;
                    this.SMQActor.AccessToken = this.AccessToken;
                }
                return _smqActor;
            }
        }

        public RoleHandlerBase(string amqps, string accessToken)
        {
            this.AMQPS = amqps;
            this.AccessToken = accessToken;
        }

        public string AMQPS { get; }
        public string AccessToken { get; }
        public delegate Task handler(StandardPayload payload, PayloadHandler handler, PayloadHandler errors = null, int timeout = 30000);
        public handler GetEffortlessAPIProjectsHandler { get; set; }
        public handler GetProjectStagesHandler { get; set; }

        public override EffortlessAPIProject GetProjectByAlias(string defaultDesiredAlias)
        {
            var payload = this.SMQActor.CreatePayload();
            payload.AirtableWhere = string.Format($"FIND('{defaultDesiredAlias}',Alias)");
            var result = default(EffortlessAPIProject);
            this.GetEffortlessAPIProjectsHandler(payload, (reply, bdea) =>
            {
                if (reply.HasNoErrors(bdea) && !(reply.EffortlessAPIProjects is null))
                {
                    result = reply.EffortlessAPIProjects.FirstOrDefault();
                }
            }).Wait(30000);

            return result;
        }

        public override ProjectStage GetProjectStageByName(EffortlessAPIProject eapiProject, string projectStageName)
        {
            var payload = this.SMQActor.CreatePayload();
            payload.AirtableWhere = string.Format($"AND(Project='{eapiProject.RowKey}',Stage='{projectStageName}')");
            var result = default(ProjectStage);
            this.GetProjectStagesHandler(payload, (reply, bdea) =>
            {
                if (reply.HasNoErrors(bdea) && !(reply.ProjectStages is null))
                {
                    result = reply.ProjectStages.FirstOrDefault();
                }
            }).Wait(30000);

            return result;
        }

    }


}
