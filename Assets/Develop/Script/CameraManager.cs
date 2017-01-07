using UnityEngine;
using System.Collections;

namespace Leap.Unity
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private BlockManager blockManager;

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
    }
}