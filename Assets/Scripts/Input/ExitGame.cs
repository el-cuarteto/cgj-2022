using System.Collections;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    [SerializeField]
    private string _cancelName = "Cancel";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(_cancelName))
        {
            Application.Quit();
        }
    }
}