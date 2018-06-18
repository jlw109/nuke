// Copyright Matthias Koch, Sebastian Karasek 2018.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.MSBuildLocator
{
    public static class Program
    {
        private const string c_msBuildComponent = "Microsoft.Component.MSBuild";
        private const string c_netCoreComponent = "Microsoft.Net.Core.Component.SDK";
        private const string c_vsWhereExecutableName = "vswhere.exe";

        private static string s_vsWherePath;

        [STAThread]
        public static void Main(string[] args)
        {
            string msBuildPath;
            if (IsUnixOperatingSystem())
            {
                msBuildPath = new[]
                              {
                                  "/usr/bin/msbuild",
                                  "/Library/Frameworks/Mono.framework/Versions/Current/Commands/msbuild"
                              }.FirstOrDefault(File.Exists);
                Console.WriteLine(msBuildPath);
                return;
            }

            if (args.Length == 0)
                throw new ArgumentException($"path to '{c_vsWhereExecutableName}' must be passed as first argument.");

            s_vsWherePath = args[0];
            if (!s_vsWherePath.EndsWith(c_vsWhereExecutableName))
                throw new ArgumentException($"the path '{s_vsWherePath}' is not a valid path to {c_vsWhereExecutableName}.");

            if (!File.Exists(s_vsWherePath))
                throw new ArgumentException($"unable to find {c_vsWhereExecutableName} at {s_vsWherePath}.");

            msBuildPath = TryGetMsBuildPath(new[] { c_msBuildComponent, c_netCoreComponent }) //MsBuild with netcore
                          ?? TryGetMsBuildPath(new[] { c_msBuildComponent }) //MsBuild
                          ?? TryGetMsBuildPath(requires: null, products: null, legacy: true); //Legacy MsBuild versions

            Console.WriteLine(msBuildPath);
        }

        [CanBeNull]
        private static string GetLatestVersionText([CanBeNull] IReadOnlyCollection<string> requires, [CanBeNull] string products, bool legacy = false)
        {
            var requiresArg = requires == null || requires.Count == 0
                ? string.Empty
                : $" -requires {requires.Aggregate(string.Empty, (x, y) => $"{x} {y}").TrimStart()}";
            var productsArg = products == null ? string.Empty : $" -products {products}";
            var arguments = $"-latest -format text {requiresArg}{productsArg}{(legacy ? " -legacy" : string.Empty)}";

            var info = new ProcessStartInfo(s_vsWherePath, arguments)
                       {
                           RedirectStandardOutput = true,
                           RedirectStandardError = true,
                           CreateNoWindow = true,
                           UseShellExecute = false
                       };
            var process = new Process { StartInfo = info };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                Console.WriteLine(error);
                Console.WriteLine(output);
                return null;
            }

            return output;
        }

        private static bool TryParseVersionInformation([CanBeNull] string versionText, out string installationPath, out string installationVersion)
        {
            installationPath = null;
            installationVersion = null;
            var versionLines = versionText?.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (versionLines == null)
                return false;

            string GetValue(IEnumerable<string> lines, string identifier)
            {
                var line = lines.Single(x => x.StartsWith(identifier));
                var value = line.Substring(identifier.Length).TrimStart(':', ' ');
                return value;
            }

            installationPath = GetValue(versionLines, "installationPath");
            installationVersion = GetValue(versionLines, "installationVersion");

            return installationPath != null && installationVersion != null;
        }

        [CanBeNull]
        private static string TryGetMsBuildPath(
            [CanBeNull] IReadOnlyCollection<string> requires,
            [CanBeNull] string products = "*",
            bool legacy = false)
        {
            var text = GetLatestVersionText(requires, products, legacy);
            if (!TryParseVersionInformation(text, out var installationPath, out var installationVersion))
                return null;

            var binaryDirectory = Path.Combine(installationPath, "MSBuild", $"{installationVersion.Split('.')[0]}.0", "Bin");
            binaryDirectory = Environment.Is64BitOperatingSystem ? Path.Combine(binaryDirectory, "amd64") : binaryDirectory;
            var msBuildPath = Path.Combine(binaryDirectory, "MSBuild.exe");

            return File.Exists(msBuildPath) ? msBuildPath : null;
        }

        private static bool IsUnixOperatingSystem()
        {
            var platform = (int) Environment.OSVersion.Platform;
            return platform > 5 || platform == 4;
        }
    }
}
