using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Aki.Common;

public class SPTLoader
{
    private static void Main(string[] args)
    {
        Log.Info("Hello from modloader");
        var loader = new BaseLoader { mainObject = new GameObject("kcy-loader") };
        GameObject.DontDestroyOnLoad(loader.mainObject);

        loader.modOptions = Json.Deserialize<ModOptions[]>(File.ReadAllText(@"user\mods\kcy-modloader\loader.json"));
        Log.Info("Modloader config: " + Json.Serialize(loader.modOptions));

        loader.log = new SPTLog();

        loader.LoadMods();
    }
}

public class SPTLog : BaseLog
{
    public override void Debug(string msg) => Log.Write(msg);

    public override void Error(string msg) => Log.Error(msg);

    public override void Info(string msg) => Log.Info(msg);

    public override void Warn(string msg) => Log.Warning(msg);
}

