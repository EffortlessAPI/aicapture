﻿/*************************************************
Initially Generated by SSoT.me - 2017
    EJ Alexandra - An odxml42 Tool
    This file WILL NOT be overwritten once changes are made.
*************************************************/
using System;
using System.ComponentModel;

namespace AIC.Lib.DataClasses
{

    /// <summary>
    /// No table description.
    /// </summary>
    public partial class ProjectStage
    {
        public ProjectStage()
        {
            this.InitPoco();
        }

        public override String ToString()
        {
            return String.Format("ProjectStage: {0}", this.Name);
        }

    }
}