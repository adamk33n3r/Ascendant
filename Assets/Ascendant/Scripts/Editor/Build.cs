using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Build {
    private const string APP_NAME = "Ascendant";

    [MenuItem("My Tools/Windows Build")]
    public static void BuildGame() {
        BuildProcess(false);
    }

    [MenuItem("My Tools/Windows Build and Run")]
    public static void BuildAndRunGame() {
        BuildProcess(true);
    }

    public static void CloudBuild(string path) {
        Console.WriteLine("!!!!! THIS IS CLOUD BUILD METHOD !!!!!");
        Console.WriteLine(MethodBase.GetCurrentMethod().ToString());
        Console.WriteLine("Replacing path");
        Console.WriteLine(path);
        string replacedPath = path.Replace(".exe", "_Data");
        Console.WriteLine(replacedPath);
        Console.WriteLine("Combining path");
        string combinedPath = Path.Combine(replacedPath, "Ascendant/Cards");
        Console.WriteLine(combinedPath);
        Console.WriteLine("Calling ReplaceDirectory");
        DirectoryInfo currentDir = new DirectoryInfo(".");
        Console.WriteLine(currentDir.FullName);
        string dirs = "";
        foreach (DirectoryInfo directoryInfo in currentDir.GetDirectories()) {
            dirs += directoryInfo.Name + " ";
        }
        Console.WriteLine(dirs);
        Type type = typeof(FileUtil);
        MethodInfo replaceDir = type.GetMethod("ReplaceDirectory");
        ParameterInfo[] pars = replaceDir.GetParameters();
        foreach (ParameterInfo parameterInfo in pars) {
            Console.WriteLine(parameterInfo.Name + ": " + parameterInfo.ParameterType);
        }
        Console.WriteLine(replaceDir.ToString());
        FileUtil.ReplaceDirectory("Assets/Ascendant/Cards", combinedPath);
        Console.WriteLine("After Function call");
    }

    private static void BuildProcess(bool run) {
        // Clear log
        Assembly assembly = Assembly.GetAssembly(typeof(ActiveEditorTracker));
        Type type = assembly.GetType("UnityEditorInternal.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);

        // Get scenes
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        string[] enabledScenes = scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray();

        // Build game
        const string path = "Build";
        string appPath = Path.Combine(path, APP_NAME + ".exe");
        BuildPipeline.BuildPlayer(enabledScenes, appPath, BuildTarget.StandaloneWindows, BuildOptions.Development);

        // Copy card data over
        string dest = Path.Combine(path, APP_NAME + "_Data/Ascendant/Cards");
        DirectoryInfo destInfo = new DirectoryInfo(dest);
        if (!destInfo.Exists) {
            destInfo.Create();
        }
        FileUtil.ReplaceDirectory("Assets/Ascendant/Cards", dest);

        // Zip up files
        Process zip = new Process {
            StartInfo = {
                FileName = "python",
                Arguments = @"-c ""
import os, zipfile, time
os.chdir('Build')
dirName = time.strftime('%m-%d-%Y')
print('Zipping up files...')
os.makedirs(dirName, exist_ok=True)
zipf = zipfile.ZipFile(os.path.join(dirName, 'Ascendant {0}.zip'.format(time.strftime('%Y-%m-%dT%H-%M-%S'))), 'w', zipfile.ZIP_DEFLATED)
zipf.write('Ascendant.exe')
for root, dirs, files in os.walk('Ascendant_Data'):
    for file in files:
        if not file.endswith('.meta'):
            zipf.write(os.path.join(root, file))
zipf.close()
"""
            }
        };
        zip.Start();

        if (run) {
            // Start game
            Process proc = new Process {StartInfo = {FileName = appPath}};
            proc.Start();
        }
    }
}
