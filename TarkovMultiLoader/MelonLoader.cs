using System.IO;
using Newtonsoft.Json;
using UnityEngine;

using MelonLoader;
[assembly: MelonInfo(typeof(TarkovMod), "TarkovModLoader", "0.0.1", "KcY")]
[assembly: MelonGame("Escape From Tarkov", "Battlestate Games")]
public class TarkovMod : MelonMod
{
    public override void OnApplicationStart()
    {
        MelonLogger.Msg("Hello from modloader");

        var loader = new BaseLoader { mainObject = new GameObject("kcy-loader") };
        GameObject.DontDestroyOnLoad(loader.mainObject);

        loader.modOptions = JsonConvert.DeserializeObject<ModOptions[]>(File.ReadAllText(@"Mods\loader.json"));

        loader.LoadMods();
    }
}

public class MelonLog : BaseLog
{
    public override void Debug(string msg) => MelonLogger.Msg(msg);

    public override void Error(string msg) => MelonLogger.Error(msg);

    public override void Info(string msg) => MelonLogger.Msg(msg);

    public override void Warn(string msg) => MelonLogger.Warning(msg);
}