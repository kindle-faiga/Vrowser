using UnityEngine;
using Leap.Unity;
using System.Collections;

public class InputButton : MonoBehaviour
{
    public BlockManager blockManager;

    public void OnClick()
    {
        Debug.Log("Button click!");
        blockManager.ChangeCubeScaling();
    }
}
