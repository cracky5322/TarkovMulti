#if NIGHTVISION
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Mods
{
    public class Nightvision : BaseMod
    {
        override public void Init(Dictionary<string, string> options)
        {
            base.Init(options);
            if (!options.ContainsKey("key"))
            {
                options["key"] = "k";
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(options["key"]))
            {
                var component = Camera.main.GetComponent<BSG.CameraEffects.NightVision>();
                component?.StartSwitch(!component.On);
            }
        }
    }
}
#endif