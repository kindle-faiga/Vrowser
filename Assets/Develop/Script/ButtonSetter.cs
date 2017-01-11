using UnityEngine;
using System.Collections;

public class ButtonSetter : MonoBehaviour
{
    [SerializeField]
    private ButtonManager buttonManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UITarget")
        {
            Debug.Log("Hit");
            buttonManager.SetButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "UITarget")
        {
            Debug.Log("Release");
            buttonManager.ResetButton();
        }
    }
}
