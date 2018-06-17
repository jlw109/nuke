// Copyright Matthias Koch, Sebastian Karasek 2018.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;

namespace Nuke.Common.Tools.VsWhere
{
    public partial class VsWhereTasks
    {
        /// <summary>
        /// Settings to get the path to the directory of the latest installation.
        /// </summary>
        public static VsWhereSettings DefaultVsWhereGetInstallationPathSettings => new VsWhereSettings()
            .SetProducts("*")
            .SetProperty("installationPath")
            .EnableLatest()
            .EnableUTF8();

        /// <summary>
        /// Settings to get the path to the directory of the latest VC installation.
        /// </summary>
        public static VsWhereSettings DefaultVsWhereGetVCInstallationPathSettings => DefaultVsWhereGetInstallationPathSettings
            .SetRequires("Microsoft.VisualStudio.Component.VC.Tools.x86.x64");

        /// <summary>
        /// Settings to get the path to the directory of the latest MSBuild installation. 
        /// </summary>
        public static VsWhereSettings DefaultVsWhereGetMSBuildInstallationPathSettings => DefaultVsWhereGetInstallationPathSettings
            .SetRequires("Microsoft.Component.MSBuild");

        /// <summary>
        /// Settings to get the path to the directory of the latest MSBuild installation where the .NET Core workload is available.
        /// </summary>
        public static VsWhereSettings DefaultVsWhereGetMSBuildINetCoreInstallationPathSettings => DefaultVsWhereGetInstallationPathSettings
            .SetRequires("Microsoft.Component.MSBuild")
            .SetRequires("Microsoft.Net.Core.Component.SDK");

        /// <summary>
        /// Settings to get the path to the directory of the latest VSTest installation.
        /// </summary>
        public static VsWhereSettings DefaultVsWhereGetVsTestInstallationPathSettings => DefaultVsWhereGetInstallationPathSettings
            .SetRequires("Microsoft.VisualStudio.Workload.ManagedDesktop", "Microsoft.VisualStudio.Workload.Web")
            .EnableRequiresAny();

        private static string GetResult(IProcess process, VsWhereSettings toolSettings, ProcessSettings processSettings)
        {
            return process.HasOutput ? process.Output.Where(x => x.Type == OutputType.Std).Select(x => x.Text).JoinNewLine() : string.Empty;
        }

        private static IProcess StartProcess(VsWhereSettings toolSettings, [CanBeNull] ProcessSettings processSettings)
        {
            processSettings = processSettings ?? new ProcessSettings();
            return ProcessTasks.StartProcess(toolSettings, processSettings.EnableRedirectOutput()).NotNull();
        }
    }
}

