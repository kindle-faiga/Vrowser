using UnityEngine;
using Leap.Unity;
using System.Collections;

public class InputButton : MonoBehaviour
{
    private CameraManager cameraManager;

    private BlockManager blockManager;

    private ButtonManager buttonManager;

    void Start()
    {
        blockManager = GameObject.Find("WebPageManager").GetComponent<BlockManager>();

        cameraManager = GameObject.Find("CenterEyeAnchor").GetComponent<CameraManager>();

        buttonManager = transform.parent.GetComponent<ButtonManager>();
    }

    public void OnClick()
    {
        if (buttonManager.IsActive())
        {
            Debug.Log("Button click!");
            blockManager.ResetBlocks();
        }
    }

    public void MagneticState()
    {
        if (buttonManager.IsActive())
        {
            Debug.Log("Button1 click!");
            blockManager.SetMagnetic();
        }
    }

    public void ChangeScaleState()
    {
        if (buttonManager.IsActive())
        {
            Debug.Log("Button2 click!");
            blockManager.SetChangeScale();
        }
    }

    public void ChangeSky()
    {
        if (buttonManager.IsActive())
        {
            Debug.Log("Button3 click!");
            cameraManager.SetSkyBox();
        }
    }
}
