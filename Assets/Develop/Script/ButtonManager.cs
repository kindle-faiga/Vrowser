﻿using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    private bool isActive = false;

    private Vector3 defaultSize;

    void Start()
    {
        defaultSize = transform.localScale;

        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetButton()
    {
        if (!isActive)
        {
            isActive = true;

            //transform.localScale = defaultSize;
            iTween.ScaleTo(gameObject, defaultSize, 0.5f);
        }
    }

    public void ResetButton()
    {
        if (isActive)
        {
            isActive = false;

            //transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            iTween.ScaleTo(gameObject, new Vector3(0.01f, 0.01f, 0.01f), 0.5f);
        }
    }
}
