using UnityEngine;
using System.Collections;

namespace Leap.Unity
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Material skybox;
        [SerializeField]
        private Material defaultSkybox;
        [SerializeField]
        private BlockManager blockManager;
        [SerializeField]
        private GameObject buttonHand;
        [SerializeField]
        private ButtonManager buttonManager;

        private bool isChange = false;

        void Awake()
        {
            RenderSettings.skybox = defaultSkybox;
        }

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

        public void SetSkyBox()
        {
            if (isChange)
            {
                RenderSettings.skybox = defaultSkybox;

                isChange = false;
            }
            else
            {
                RenderSettings.skybox = skybox;

                isChange = true;
            }
        }

        void Update()
        {
            /*
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
            */
        }
    }
}