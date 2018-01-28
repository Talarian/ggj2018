using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventShutdown : MonoBehaviour
{
    public EventSystem eventSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            ShutdownController controller = FindObjectOfType<ShutdownController>();
            if (controller != null)
            {
                controller.Shutdown();
            }
        }
    }
}
