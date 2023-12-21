using System;
using System.Collections.Generic;

namespace AICapture.OST.Lib
{

    public class SeedRepository
    {
        public String Name { get; set; }
        public String ShortName { get; set; }
        public String DisplayName { get; set; }
        public String RepositoryUrl { get; set; }
        public string PrivateUrl { get; set; }
        public string[] SeedReplacementsText { get; set; }
        public String Notes { get; set; }
        public String AdditionalDeploymentCommands { get; set; }
        public bool InvokeNPMInstall { get; set; }
        public Int32 SortOrder { get; set; }
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return this.ShortName;
        }
    }
}
