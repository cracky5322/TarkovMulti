#if FREECAM
using System.Collections.Generic;
using UnityEngine;

namespace Mods
{
    public class Freecam : BaseMod
    {
        GameObject camObj;
        ExtendedFlycam flycam;
        Camera cam;
        Camera backupCam;
        GameObject _input = null;
        GameObject input { get
            {
                if (_input == null) _input = GameObject.Find("___Input");
                return _input;
            }
        }
        override public void Init(Dictionary<string, string> options)
        {
            base.Init(options);
            if (!options.ContainsKey("key"))
            {
                options["key"] = "m";
            }

            camObj = new GameObject("NoclipCam");
            camObj.transform.position = new Vector3(0, 1, 0);
            GameObject.DontDestroyOnLoad(camObj);
            cam = camObj.AddComponent<Camera>();
            cam.tag = "MainCamera";
            cam.enabled = false;
            flycam = camObj.AddComponent<ExtendedFlycam>();
        }
        void Update()
        {
            if (Utils.InGame && Input.GetKeyDown(options["key"]))
            {
                if (!cam.enabled)
                {
                    backupCam = Camera.main;
                    backupCam.enabled = false;
                    cam.enabled = true;
                    input?.SetActive(false);

                } else
                {
                    backupCam.enabled = true;
                    cam.enabled = false;
                    input?.SetActive(true);
                }
                flycam.enabled = cam.enabled;
            }
        }
    }
}
#endif