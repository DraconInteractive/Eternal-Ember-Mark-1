using UnityEngine;
using System.Collections;

public class HorseMountTriger : MonoBehaviour
{

    public bool LeftSide;
    HorseController HorseC;
    Rider RFPS;

    // Use this for initialization
    void Start()
    {
        HorseC = GetComponentInParent<HorseController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!HorseC.Mounted)
        {
            if (other.GetComponent<Rider>())
            {
                RFPS = other.GetComponent<Rider>();
                RFPS.Can_Mount = true;
                RFPS.findHorse(HorseC);

                if (LeftSide) RFPS.Mountedside = true;
                else RFPS.Mountedside = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (RFPS != null)
        {
            if (RFPS == other.GetComponent<Rider>())
            {
                RFPS = other.GetComponent<Rider>();

                if (RFPS.Can_Mount)
                {
                    RFPS.Can_Mount = false;
                }
            }
        }
    }
}
