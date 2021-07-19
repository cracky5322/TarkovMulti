#if EXTRACT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using Comfort.Common;

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
            if (Utils.inGame && Input.GetKeyDown(options["key"]))
            {
                Player localPlayer = Utils.localPlayer;
                if (localPlayer != null)
                {
                    // Get an extract
                    var ex = Utils.gameWorld.ExfiltrationController.ExfiltrationPoints[0];
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