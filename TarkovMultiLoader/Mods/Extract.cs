#if EXTRACT
using System.Collections.Generic;
using UnityEngine;
using EFT;

namespace Mods
{
    public class Extract : BaseMod
    {
        override public void Init(Dictionary<string, string> options) 
        {
            base.Init(options);
            if (!options.ContainsKey("key"))
            {
                options["key"] = "l";
            }
        }
        void Update()
        {
            if (Utils.InGame && Input.GetKeyDown(options["key"]))
            {
                Player localPlayer = Utils.LocalPlayer;
                if (localPlayer != null)
                {
                    // Get an extract
                    var ex = Utils.GameWorld.ExfiltrationController.ExfiltrationPoints[0];
                    // Set it to instant
                    ex.Settings.ExfiltrationTime = 0.0f;
                    // Extract
                    ex.Proceed(localPlayer);
                }
            }
        }
    }
}
#endif