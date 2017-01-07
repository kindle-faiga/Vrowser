using UnityEngine;
using System.Collections;

public class BlockGetter : MonoBehaviour
{
    [SerializeField]
    private GameObject block;

    public GameObject GetBlock()
    {
        return block;
    }

    public void SetBlock(GameObject _block)
    {
        _block.GetComponent<BlockState>().ChangeMaterialRed();
        block = _block;
    }

    public void ResetBlock(GameObject _block)
    {
        _block.GetComponent<BlockState>().ResetMaterial();
        block = null;
    }
}
