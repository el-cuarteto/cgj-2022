using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualSwapper : MonoBehaviour
{
    [SerializeField]
    private DualSerializedObject _mix;

    [SerializeField]
    private bool isMainMix = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isMainMix)
            {
                DualController.ChangeDualMix(_mix);
            }
            else
            {
                DualController.ChangeMainMix(_mix);
            }
        }
    }
}
