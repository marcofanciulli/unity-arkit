#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

class UnityBuildingScript
{

    static string[] SCENES = FindEnabledEditorScenes();
    static string APP_NAME = "UnityBuild";
    static BuildOptions OPTIONS = BuildOptions.None;

    [MenuItem("xcode/Build iPhone")]
    static void PerformIPhoneBuild()
    {
        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; ++i)
        {
            if (args[i].ToLower() == "-is_development")
                if (args[i + 1].ToLower() == "true")
                {
                    OPTIONS = BuildOptions.Development | BuildOptions.AllowDebugging;
                }
        }

        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            string target_dir = APP_NAME;
            GenericBuild(SCENES,  "iOS_Build/" + target_dir, BuildTarget.iOS, OPTIONS);
        }
        else
        {
            throw new Exception("You'r can't run build for on this platform. Build can be runned only on Mac OS X Machine.");
        }
    }

    [MenuItem("xcode/Build Android")]
    static void PerformAndroidBuild()
    {
        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; ++i)
        {
            if (args[i].ToLower() == "-is_development")
                if (args[i + 1].ToLower() == "true")
                {
                    OPTIONS = BuildOptions.Development | BuildOptions.AllowDebugging;
                }
        }

		if (Application.platform == RuntimePlatform.Android)
        {
            string target_dir = APP_NAME;
			GenericBuild(SCENES, "Android_Build/" + target_dir, BuildTarget.Android, OPTIONS);
        }
        else
        {
            throw new Exception("You'r can't run build for on this platform. Build can be runned only on Mac OS X Machine.");
        }
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
                continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(target_dir));

        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
        string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
        if (res.Length > 0)
        {
            throw new Exception("BuildPlayer failure: " + res);
        }
        Debug.Log("Build complete");
    }
}

#endif
