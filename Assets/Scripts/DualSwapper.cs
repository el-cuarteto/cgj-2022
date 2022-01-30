using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualSwapper : MonoBehaviour
{
    [SerializeField]
    private DualSerializedObject _mix;

    [SerializeField]
    private float _seconds = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DualMixer.ChangeMix(_mix, _seconds);
        }
    }
}
