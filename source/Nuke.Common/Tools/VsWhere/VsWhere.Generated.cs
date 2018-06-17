// Copyright Matthias Koch, Sebastian Karasek 2018.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

// Generated with Nuke.CodeGeneration, Version: Local.
// Generated from https://github.com/nuke-build/nuke/blob/master/build/specifications/VsWhere.json.

using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Tooling;
using Nuke.Common.Tools;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Nuke.Common.Tools.VsWhere
{
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class VsWhereTasks
    {
        /// <summary><p>Path to the VsWhere executable.</p></summary>
        public static string VsWherePath => ToolPathResolver.GetPackageExecutable("vswhere", "vswhere.exe");
        /// <summary><p>VsWhere is designed to be a redistributable, single-file executable that can be used in build or deployment scripts to find where Visual Studio - or other products in the Visual Studio family - is located.</p></summary>
        public static IEnumerable<string> VsWhere(string arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool redirectOutput = false, Func<string, string> outputFilter = null)
        {
            var process = ProcessTasks.StartProcess(VsWherePath, arguments, workingDirectory, environmentVariables, timeout, redirectOutput, outputFilter);
            process.AssertZeroExitCode();
            return process.HasOutput ? process.Output.Select(x => x.Text) : null;
        }
        static partial void PreProcess(VsWhereSettings toolSettings);
        static partial void PostProcess(VsWhereSettings toolSettings);
        /// <summary><p>VsWhere is designed to be a redistributable, single-file executable that can be used in build or deployment scripts to find where Visual Studio - or other products in the Visual Studio family - is located.</p><p>For more details, visit the <a href="https://github.com/Microsoft/vswhere">official website</a>.</p></summary>
        public static string VsWhere(Configure<VsWhereSettings> configurator = null, ProcessSettings processSettings = null)
        {
            var toolSettings = configurator.InvokeSafe(new VsWhereSettings());
            PreProcess(toolSettings);
            var process = StartProcess(toolSettings, processSettings);
            process.AssertZeroExitCode();
            PostProcess(toolSettings);
            return GetResult(process, toolSettings, processSettings);
        }
    }
    #region VsWhereSettings
    /// <summary><p>Used within <see cref="VsWhereTasks"/>.</p></summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class VsWhereSettings : ToolSettings
    {
        /// <summary><p>Path to the VsWhere executable.</p></summary>
        public override string ToolPath => base.ToolPath ?? VsWhereTasks.VsWherePath;
        /// <summary><p>Finds all instances even if they are incomplete and may not launch.</p></summary>
        public virtual bool? All { get; internal set; }
        /// <summary><p>Also searches prereleases. By default, only releases are searched.</p></summary>
        public virtual bool? Prerelease { get; internal set; }
        /// <summary><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        public virtual IReadOnlyList<string> Products => ProductsInternal.AsReadOnly();
        internal List<string> ProductsInternal { get; set; } = new List<string>();
        /// <summary><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        public virtual IReadOnlyList<string> Requires => RequiresInternal.AsReadOnly();
        internal List<string> RequiresInternal { get; set; } = new List<string>();
        /// <summary><p>Find instances with any one or more workload or components IDs passed to -requires.</p></summary>
        public virtual bool? RequiresAny { get; internal set; }
        /// <summary><p>A version range for instances to find. Example: [15.0,16.0) will find versions 15.*.</p></summary>
        public virtual string Version { get; internal set; }
        /// <summary><p> Return only the newest version and last installed.</p></summary>
        public virtual bool? Latest { get; internal set; }
        /// <summary><p>Also searches Visual Studio 2015 and older products. Information is limited.This option cannot be used with either -products or -requires.</p></summary>
        public virtual bool? Legacy { get; internal set; }
        /// <summary><p>Do not show logo information. Some formats of <see cref="Nuke.Common.Tools.VsWhere.Format"/> will not show a logo anyway.</p></summary>
        public virtual bool? NoLogo { get; internal set; }
        /// <summary><p>Use UTF-8 encoding (recommended for JSON).</p></summary>
        public virtual bool? UTF8 { get; internal set; }
        /// <summary><p>A version range for instances to find. Example: [15.0,16.0) will find versions 15.*.</p></summary>
        public virtual Format Format { get; internal set; }
        /// <summary><p>The name of a property to return. Defaults <see cref="Format"/> to <see cref="Nuke.Common.Tools.VsWhere.Format.value"/>. Use delimiters <em>'.'</em>, <em>'/'</em>, or <em>'_'</em> to separate object and property names. Example: <em>properties.nickname</em> will return the <em>nickname</em> property under <em>properties</em>.</p></summary>
        public virtual string Property { get; internal set; }
        protected override Arguments ConfigureArguments(Arguments arguments)
        {
            arguments
              .Add("-all", All)
              .Add("-prerelease", Prerelease)
              .Add("-products {value}", Products, separator: ' ')
              .Add("-requires {value}", Requires, separator: ' ')
              .Add("-requiresAny", RequiresAny)
              .Add("-version {value}", Version)
              .Add("-latest", Latest)
              .Add("-legacy", Legacy)
              .Add("-nologo", NoLogo)
              .Add("-utf8", UTF8)
              .Add("-format {value}", Format)
              .Add("-property {value}", Property);
            return base.ConfigureArguments(arguments);
        }
    }
    #endregion
    #region VsWhereSettingsExtensions
    /// <summary><p>Used within <see cref="VsWhereTasks"/>.</p></summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class VsWhereSettingsExtensions
    {
        #region All
        /// <summary><p><em>Sets <see cref="VsWhereSettings.All"/>.</em></p><p>Finds all instances even if they are incomplete and may not launch.</p></summary>
        [Pure]
        public static VsWhereSettings SetAll(this VsWhereSettings toolSettings, bool? all)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.All = all;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.All"/>.</em></p><p>Finds all instances even if they are incomplete and may not launch.</p></summary>
        [Pure]
        public static VsWhereSettings ResetAll(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.All = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="VsWhereSettings.All"/>.</em></p><p>Finds all instances even if they are incomplete and may not launch.</p></summary>
        [Pure]
        public static VsWhereSettings EnableAll(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.All = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="VsWhereSettings.All"/>.</em></p><p>Finds all instances even if they are incomplete and may not launch.</p></summary>
        [Pure]
        public static VsWhereSettings DisableAll(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.All = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="VsWhereSettings.All"/>.</em></p><p>Finds all instances even if they are incomplete and may not launch.</p></summary>
        [Pure]
        public static VsWhereSettings ToggleAll(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.All = !toolSettings.All;
            return toolSettings;
        }
        #endregion
        #region Prerelease
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Prerelease"/>.</em></p><p>Also searches prereleases. By default, only releases are searched.</p></summary>
        [Pure]
        public static VsWhereSettings SetPrerelease(this VsWhereSettings toolSettings, bool? prerelease)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Prerelease = prerelease;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.Prerelease"/>.</em></p><p>Also searches prereleases. By default, only releases are searched.</p></summary>
        [Pure]
        public static VsWhereSettings ResetPrerelease(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Prerelease = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="VsWhereSettings.Prerelease"/>.</em></p><p>Also searches prereleases. By default, only releases are searched.</p></summary>
        [Pure]
        public static VsWhereSettings EnablePrerelease(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Prerelease = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="VsWhereSettings.Prerelease"/>.</em></p><p>Also searches prereleases. By default, only releases are searched.</p></summary>
        [Pure]
        public static VsWhereSettings DisablePrerelease(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Prerelease = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="VsWhereSettings.Prerelease"/>.</em></p><p>Also searches prereleases. By default, only releases are searched.</p></summary>
        [Pure]
        public static VsWhereSettings TogglePrerelease(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Prerelease = !toolSettings.Prerelease;
            return toolSettings;
        }
        #endregion
        #region Products
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Products"/> to a new list.</em></p><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        [Pure]
        public static VsWhereSettings SetProducts(this VsWhereSettings toolSettings, params string[] products)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ProductsInternal = products.ToList();
            return toolSettings;
        }
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Products"/> to a new list.</em></p><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        [Pure]
        public static VsWhereSettings SetProducts(this VsWhereSettings toolSettings, IEnumerable<string> products)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ProductsInternal = products.ToList();
            return toolSettings;
        }
        /// <summary><p><em>Adds values to <see cref="VsWhereSettings.Products"/>.</em></p><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        [Pure]
        public static VsWhereSettings AddProducts(this VsWhereSettings toolSettings, params string[] products)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ProductsInternal.AddRange(products);
            return toolSettings;
        }
        /// <summary><p><em>Adds values to <see cref="VsWhereSettings.Products"/>.</em></p><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        [Pure]
        public static VsWhereSettings AddProducts(this VsWhereSettings toolSettings, IEnumerable<string> products)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ProductsInternal.AddRange(products);
            return toolSettings;
        }
        /// <summary><p><em>Clears <see cref="VsWhereSettings.Products"/>.</em></p><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        [Pure]
        public static VsWhereSettings ClearProducts(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ProductsInternal.Clear();
            return toolSettings;
        }
        /// <summary><p><em>Removes values from <see cref="VsWhereSettings.Products"/>.</em></p><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        [Pure]
        public static VsWhereSettings RemoveProducts(this VsWhereSettings toolSettings, params string[] products)
        {
            toolSettings = toolSettings.NewInstance();
            var hashSet = new HashSet<string>(products);
            toolSettings.ProductsInternal.RemoveAll(x => hashSet.Contains(x));
            return toolSettings;
        }
        /// <summary><p><em>Removes values from <see cref="VsWhereSettings.Products"/>.</em></p><p>One or more product IDs to find. Defaults to Community, Professional, and Enterprise. Specify <em>*</em> by itself to search all product instances installed.</p></summary>
        [Pure]
        public static VsWhereSettings RemoveProducts(this VsWhereSettings toolSettings, IEnumerable<string> products)
        {
            toolSettings = toolSettings.NewInstance();
            var hashSet = new HashSet<string>(products);
            toolSettings.ProductsInternal.RemoveAll(x => hashSet.Contains(x));
            return toolSettings;
        }
        #endregion
        #region Requires
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Requires"/> to a new list.</em></p><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        [Pure]
        public static VsWhereSettings SetRequires(this VsWhereSettings toolSettings, params string[] requires)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresInternal = requires.ToList();
            return toolSettings;
        }
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Requires"/> to a new list.</em></p><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        [Pure]
        public static VsWhereSettings SetRequires(this VsWhereSettings toolSettings, IEnumerable<string> requires)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresInternal = requires.ToList();
            return toolSettings;
        }
        /// <summary><p><em>Adds values to <see cref="VsWhereSettings.Requires"/>.</em></p><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        [Pure]
        public static VsWhereSettings AddRequires(this VsWhereSettings toolSettings, params string[] requires)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresInternal.AddRange(requires);
            return toolSettings;
        }
        /// <summary><p><em>Adds values to <see cref="VsWhereSettings.Requires"/>.</em></p><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        [Pure]
        public static VsWhereSettings AddRequires(this VsWhereSettings toolSettings, IEnumerable<string> requires)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresInternal.AddRange(requires);
            return toolSettings;
        }
        /// <summary><p><em>Clears <see cref="VsWhereSettings.Requires"/>.</em></p><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        [Pure]
        public static VsWhereSettings ClearRequires(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresInternal.Clear();
            return toolSettings;
        }
        /// <summary><p><em>Removes values from <see cref="VsWhereSettings.Requires"/>.</em></p><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        [Pure]
        public static VsWhereSettings RemoveRequires(this VsWhereSettings toolSettings, params string[] requires)
        {
            toolSettings = toolSettings.NewInstance();
            var hashSet = new HashSet<string>(requires);
            toolSettings.RequiresInternal.RemoveAll(x => hashSet.Contains(x));
            return toolSettings;
        }
        /// <summary><p><em>Removes values from <see cref="VsWhereSettings.Requires"/>.</em></p><p>One or more workload or component IDs required when finding instances. All specified IDs must be installed unless -requiresAny is specified. See <a href="https://aka.ms/vs/workloads"/> for a list of workload and component IDs.</p></summary>
        [Pure]
        public static VsWhereSettings RemoveRequires(this VsWhereSettings toolSettings, IEnumerable<string> requires)
        {
            toolSettings = toolSettings.NewInstance();
            var hashSet = new HashSet<string>(requires);
            toolSettings.RequiresInternal.RemoveAll(x => hashSet.Contains(x));
            return toolSettings;
        }
        #endregion
        #region RequiresAny
        /// <summary><p><em>Sets <see cref="VsWhereSettings.RequiresAny"/>.</em></p><p>Find instances with any one or more workload or components IDs passed to -requires.</p></summary>
        [Pure]
        public static VsWhereSettings SetRequiresAny(this VsWhereSettings toolSettings, bool? requiresAny)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresAny = requiresAny;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.RequiresAny"/>.</em></p><p>Find instances with any one or more workload or components IDs passed to -requires.</p></summary>
        [Pure]
        public static VsWhereSettings ResetRequiresAny(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresAny = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="VsWhereSettings.RequiresAny"/>.</em></p><p>Find instances with any one or more workload or components IDs passed to -requires.</p></summary>
        [Pure]
        public static VsWhereSettings EnableRequiresAny(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresAny = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="VsWhereSettings.RequiresAny"/>.</em></p><p>Find instances with any one or more workload or components IDs passed to -requires.</p></summary>
        [Pure]
        public static VsWhereSettings DisableRequiresAny(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresAny = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="VsWhereSettings.RequiresAny"/>.</em></p><p>Find instances with any one or more workload or components IDs passed to -requires.</p></summary>
        [Pure]
        public static VsWhereSettings ToggleRequiresAny(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.RequiresAny = !toolSettings.RequiresAny;
            return toolSettings;
        }
        #endregion
        #region Version
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Version"/>.</em></p><p>A version range for instances to find. Example: [15.0,16.0) will find versions 15.*.</p></summary>
        [Pure]
        public static VsWhereSettings SetVersion(this VsWhereSettings toolSettings, string version)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Version = version;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.Version"/>.</em></p><p>A version range for instances to find. Example: [15.0,16.0) will find versions 15.*.</p></summary>
        [Pure]
        public static VsWhereSettings ResetVersion(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Version = null;
            return toolSettings;
        }
        #endregion
        #region Latest
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Latest"/>.</em></p><p> Return only the newest version and last installed.</p></summary>
        [Pure]
        public static VsWhereSettings SetLatest(this VsWhereSettings toolSettings, bool? latest)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Latest = latest;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.Latest"/>.</em></p><p> Return only the newest version and last installed.</p></summary>
        [Pure]
        public static VsWhereSettings ResetLatest(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Latest = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="VsWhereSettings.Latest"/>.</em></p><p> Return only the newest version and last installed.</p></summary>
        [Pure]
        public static VsWhereSettings EnableLatest(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Latest = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="VsWhereSettings.Latest"/>.</em></p><p> Return only the newest version and last installed.</p></summary>
        [Pure]
        public static VsWhereSettings DisableLatest(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Latest = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="VsWhereSettings.Latest"/>.</em></p><p> Return only the newest version and last installed.</p></summary>
        [Pure]
        public static VsWhereSettings ToggleLatest(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Latest = !toolSettings.Latest;
            return toolSettings;
        }
        #endregion
        #region Legacy
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Legacy"/>.</em></p><p>Also searches Visual Studio 2015 and older products. Information is limited.This option cannot be used with either -products or -requires.</p></summary>
        [Pure]
        public static VsWhereSettings SetLegacy(this VsWhereSettings toolSettings, bool? legacy)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Legacy = legacy;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.Legacy"/>.</em></p><p>Also searches Visual Studio 2015 and older products. Information is limited.This option cannot be used with either -products or -requires.</p></summary>
        [Pure]
        public static VsWhereSettings ResetLegacy(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Legacy = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="VsWhereSettings.Legacy"/>.</em></p><p>Also searches Visual Studio 2015 and older products. Information is limited.This option cannot be used with either -products or -requires.</p></summary>
        [Pure]
        public static VsWhereSettings EnableLegacy(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Legacy = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="VsWhereSettings.Legacy"/>.</em></p><p>Also searches Visual Studio 2015 and older products. Information is limited.This option cannot be used with either -products or -requires.</p></summary>
        [Pure]
        public static VsWhereSettings DisableLegacy(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Legacy = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="VsWhereSettings.Legacy"/>.</em></p><p>Also searches Visual Studio 2015 and older products. Information is limited.This option cannot be used with either -products or -requires.</p></summary>
        [Pure]
        public static VsWhereSettings ToggleLegacy(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Legacy = !toolSettings.Legacy;
            return toolSettings;
        }
        #endregion
        #region NoLogo
        /// <summary><p><em>Sets <see cref="VsWhereSettings.NoLogo"/>.</em></p><p>Do not show logo information. Some formats of <see cref="Nuke.Common.Tools.VsWhere.Format"/> will not show a logo anyway.</p></summary>
        [Pure]
        public static VsWhereSettings SetNoLogo(this VsWhereSettings toolSettings, bool? noLogo)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.NoLogo = noLogo;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.NoLogo"/>.</em></p><p>Do not show logo information. Some formats of <see cref="Nuke.Common.Tools.VsWhere.Format"/> will not show a logo anyway.</p></summary>
        [Pure]
        public static VsWhereSettings ResetNoLogo(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.NoLogo = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="VsWhereSettings.NoLogo"/>.</em></p><p>Do not show logo information. Some formats of <see cref="Nuke.Common.Tools.VsWhere.Format"/> will not show a logo anyway.</p></summary>
        [Pure]
        public static VsWhereSettings EnableNoLogo(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.NoLogo = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="VsWhereSettings.NoLogo"/>.</em></p><p>Do not show logo information. Some formats of <see cref="Nuke.Common.Tools.VsWhere.Format"/> will not show a logo anyway.</p></summary>
        [Pure]
        public static VsWhereSettings DisableNoLogo(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.NoLogo = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="VsWhereSettings.NoLogo"/>.</em></p><p>Do not show logo information. Some formats of <see cref="Nuke.Common.Tools.VsWhere.Format"/> will not show a logo anyway.</p></summary>
        [Pure]
        public static VsWhereSettings ToggleNoLogo(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.NoLogo = !toolSettings.NoLogo;
            return toolSettings;
        }
        #endregion
        #region UTF8
        /// <summary><p><em>Sets <see cref="VsWhereSettings.UTF8"/>.</em></p><p>Use UTF-8 encoding (recommended for JSON).</p></summary>
        [Pure]
        public static VsWhereSettings SetUTF8(this VsWhereSettings toolSettings, bool? utf8)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.UTF8 = utf8;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.UTF8"/>.</em></p><p>Use UTF-8 encoding (recommended for JSON).</p></summary>
        [Pure]
        public static VsWhereSettings ResetUTF8(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.UTF8 = null;
            return toolSettings;
        }
        /// <summary><p><em>Enables <see cref="VsWhereSettings.UTF8"/>.</em></p><p>Use UTF-8 encoding (recommended for JSON).</p></summary>
        [Pure]
        public static VsWhereSettings EnableUTF8(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.UTF8 = true;
            return toolSettings;
        }
        /// <summary><p><em>Disables <see cref="VsWhereSettings.UTF8"/>.</em></p><p>Use UTF-8 encoding (recommended for JSON).</p></summary>
        [Pure]
        public static VsWhereSettings DisableUTF8(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.UTF8 = false;
            return toolSettings;
        }
        /// <summary><p><em>Toggles <see cref="VsWhereSettings.UTF8"/>.</em></p><p>Use UTF-8 encoding (recommended for JSON).</p></summary>
        [Pure]
        public static VsWhereSettings ToggleUTF8(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.UTF8 = !toolSettings.UTF8;
            return toolSettings;
        }
        #endregion
        #region Format
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Format"/>.</em></p><p>A version range for instances to find. Example: [15.0,16.0) will find versions 15.*.</p></summary>
        [Pure]
        public static VsWhereSettings SetFormat(this VsWhereSettings toolSettings, Format format)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Format = format;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.Format"/>.</em></p><p>A version range for instances to find. Example: [15.0,16.0) will find versions 15.*.</p></summary>
        [Pure]
        public static VsWhereSettings ResetFormat(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Format = null;
            return toolSettings;
        }
        #endregion
        #region Property
        /// <summary><p><em>Sets <see cref="VsWhereSettings.Property"/>.</em></p><p>The name of a property to return. Defaults <see cref="Format"/> to <see cref="Nuke.Common.Tools.VsWhere.Format.value"/>. Use delimiters <em>'.'</em>, <em>'/'</em>, or <em>'_'</em> to separate object and property names. Example: <em>properties.nickname</em> will return the <em>nickname</em> property under <em>properties</em>.</p></summary>
        [Pure]
        public static VsWhereSettings SetProperty(this VsWhereSettings toolSettings, string property)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Property = property;
            return toolSettings;
        }
        /// <summary><p><em>Resets <see cref="VsWhereSettings.Property"/>.</em></p><p>The name of a property to return. Defaults <see cref="Format"/> to <see cref="Nuke.Common.Tools.VsWhere.Format.value"/>. Use delimiters <em>'.'</em>, <em>'/'</em>, or <em>'_'</em> to separate object and property names. Example: <em>properties.nickname</em> will return the <em>nickname</em> property under <em>properties</em>.</p></summary>
        [Pure]
        public static VsWhereSettings ResetProperty(this VsWhereSettings toolSettings)
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Property = null;
            return toolSettings;
        }
        #endregion
    }
    #endregion
    #region Format
    /// <summary><p>Used within <see cref="VsWhereTasks"/>.</p></summary>
    [PublicAPI]
    [Serializable]
    public partial class Format : Enumeration
    {
        public static Format json = new Format { Value = "json" };
        public static Format text = new Format { Value = "text" };
        public static Format value = new Format { Value = "value" };
        public static Format xml = new Format { Value = "xml" };
    }
    #endregion
}
