using System;
using System.Collections.Generic;
using UnityEngine;
using Aki.Common;
public class BaseMod : MonoBehaviour
{
    public Dictionary<string, string> options;

    virtual public void Init(Dictionary<string, string> options) => this.options = options;
}

public class BaseLoader
{
    public ModOptions[] modOptions;
    public GameObject mainObject;
    public List<BaseMod> mods = new List<BaseMod>();
    public BaseLog log;

    public void LoadMods()
    {
        foreach (ModOptions modOption in this.modOptions)
        {
            if (modOption.enabled)
            {
                log.Info("Loading mod: " + modOption.name);
                LoadMod(modOption);
            }
        }
    }

    void LoadMod(ModOptions modOption)
    {
        log.Info("Finding mod: " + modOption.name);
        Type t = Type.GetType("Mods." + modOption.name);
        if (t == null)
        {
            log.Error("Unable to find mod: " + modOption.name);
        }
        log.Info("Adding mod to object: " + modOption.name);

        BaseMod mod = mainObject.AddComponent(t) as BaseMod;
        mod.Init(modOption.options);
        mods.Add(mod);
    }
}

public class ModOptions
{
    public string name;
    public bool enabled = true;
    public Dictionary<string, string> options = new Dictionary<string, string>();
}

abstract public class BaseLog
{
    abstract public void Info(string msg);
    abstract public void Debug(string msg);
    abstract public void Warn(string msg);
    abstract public void Error(string msg);
}