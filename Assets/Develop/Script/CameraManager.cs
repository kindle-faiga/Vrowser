using UnityEngine;
using System.Collections;

namespace Leap.Unity
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private BlockManager blockManager;
        [SerializeField]
        private GameObject buttonHand;
        [SerializeField]
        private ButtonManager buttonManager;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Block")
            {
                Debug.Log(collider.gameObject.name);
                collider.gameObject.GetComponent<BlockState>().ChangeMaterialGreen();
                blockManager.ChangeTarget(collider.gameObject.GetComponent<BlockState>());
            }
        }

        void OnTriggerExit(Collider collider)
        {
            if (collider.tag == "Block")
            {              
                collider.gameObject.GetComponent<BlockState>().ResetMaterialGreen();
            }
        }

        void Update()
        {
            if(buttonHand.activeSelf)
            {
                float distance = Vector3.Distance(transform.position, buttonHand.transform.position);
                //Debug.Log(distance);

                if(distance < 0.4f && !blockManager.GetLeftHandPinch())
                {
                    buttonManager.SetButton();
                }
                else
                {
                    buttonManager.ResetButton();
                }
            }
        }
    }
}