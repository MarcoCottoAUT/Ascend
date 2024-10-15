using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    // Update is called once per frame
    bool PlatformTriggered = false;
    BoxCollider2D LastPlatformTouched;
    void Update()
    {
        RaycastHit2D PlatformDetection = Physics2D.Raycast(transform.position, Vector2.up * 2);

        if (PlatformDetection)
        {
            if (PlatformDetection.transform.gameObject.layer == 8)
            {
                if (!PlatformTriggered)
                {
                    PlatformTriggered = true;
                    LastPlatformTouched = PlatformDetection.transform.gameObject.GetComponent<BoxCollider2D>();
                    LastPlatformTouched.isTrigger = true;
                }
            } else
            {
                if (PlatformTriggered)
                {
                    PlatformTriggered = false;
                    LastPlatformTouched.isTrigger = false;

                    LastPlatformTouched = null;
                }
            }
        } else
        {
            if (PlatformTriggered)
            {
                PlatformTriggered = false;
                LastPlatformTouched.isTrigger = false;

                LastPlatformTouched = null;
            }
        }
    }
}
