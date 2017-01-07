using UnityEngine;
using System.Collections;

public class BlockGetterChild : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            GetComponentInParent<BlockGetter>().SetBlock(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Block")
        {
            GetComponentInParent<BlockGetter>().ResetBlock(other.gameObject);
        }
    }
}
