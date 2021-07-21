
using System.IO;
using UnityEngine;
using Aki.Common;
using System.Reflection;

public class SPTLoader
{
    private static void Main(string[] args)
    {
        string folder = "";
        // Search mods folder for module.dlls
        foreach (var m in Directory.GetFiles(@"user\mods\", "module.dll", SearchOption.AllDirectories))
        {
            try {
                
                var name = AssemblyName.GetAssemblyName(m);
                Log.Info(name.ToString());
                // Check if the assembly name is our own
                // TODO: this stops us from having seperate release builds in different folders for now.
                if (name.Name == "KcY-TarkovMulti")
                {
                    // Get mod folder name
                    folder = m.Split('\\')[2];
                    break;
                }
            } catch { }
        }
        if (folder == "")
        {
            Log.Error("Unable to find own module.dll under 'user\\mods\\'. Stopping loader.");
            return;
        }

        Log.Info("Hello from modloader");
        var loader = new BaseLoader { mainObject = new GameObject("kcy-loader") };
        GameObject.DontDestroyOnLoad(loader.mainObject);

        loader.modOptions = Json.Deserialize<ModOptions[]>(File.ReadAllText(@"user\mods\"+folder+@"\loader.json"));
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

