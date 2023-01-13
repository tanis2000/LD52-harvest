using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace App.Editor.Build
{
    public class GameBuilder
    {
                private static readonly bool IsDebugBuild = false;

        [MenuItem("Builds/All platforms")]
        private static void BuildAllPlatforms()
        {
            BuildWindowsGame();
            BuildOsxGame();
            BuildLinuxGame();
        }

        [MenuItem("Builds/Windows Build")]
        private static void BuildWindowsGame()
        {
            BuildGame($"/Windows/{Application.productName}.exe", BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("Builds/MacOS Build")]
        private static void BuildOsxGame()
        {
            BuildGame($"/MacOS/{Application.productName}.app", BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
        }

        [MenuItem("Builds/Linux Build")]
        private static void BuildLinuxGame()
        {
            BuildGame($"/Linux/{Application.productName}.x86_64", BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
        }

        private static void BuildGame(string versionPath, BuildTargetGroup buildTargetGroup, BuildTarget buildTarget)
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

            buildPlayerOptions.locationPathName = Application.dataPath + $"/../Builds{versionPath}";
            buildPlayerOptions.targetGroup = buildTargetGroup;
            buildPlayerOptions.target = buildTarget;

            List<string> scenes = new List<string>();

            foreach (EditorBuildSettingsScene editorBuildSettingsScene in EditorBuildSettings.scenes)
            {
                scenes.Add(editorBuildSettingsScene.path);
            }

            buildPlayerOptions.scenes = scenes.ToArray();

            if (IsDebugBuild)
            {
                buildPlayerOptions.options = BuildOptions.Development | BuildOptions.ShowBuiltPlayer | BuildOptions.AllowDebugging;
            }

            EditorUserBuildSettings.SwitchActiveBuildTarget(buildPlayerOptions.targetGroup, buildPlayerOptions.target);

            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

            Debug.Log(buildReport.summary);
        }
    }
}