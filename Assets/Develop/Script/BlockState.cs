using UnityEngine;
using System.Linq;
using System.Collections;

public class BlockState : MonoBehaviour
{
    [SerializeField]
    private Material lightMaterial;
    [SerializeField]
    private Material greenMaterial;
    private Material defaultMaterial;
    private bool isGrap = false;
    private float maxSize;
    private float minSize;
    private Vector3 defaultSize;
    private Vector3 defaultPos;

    void Start ()
    {
        defaultMaterial = GetComponent<Renderer>().material;
        maxSize = transform.localScale.x * 3.0f;
        minSize = transform.localScale.x * 0.5f;
        defaultPos = transform.position;
        defaultSize = transform.localScale;
	}
	
	void Update ()
    {
        	
	}

    public void ChangeMaterialRed()
    {
        isGrap = true;
        GetComponent<Renderer>().material = lightMaterial;
    }

    public void ChangeMaterialGreen()
    {
        if (!isGrap)
        {
            GetComponent<Renderer>().material = greenMaterial;
        }
    }

    public void ResetMaterialGreen()
    {
        if (!isGrap)
        {
            GetComponent<Renderer>().material = defaultMaterial;
        }
    }

    public void ResetMaterial()
    {
        isGrap = false;
        GetComponent<Renderer>().material = defaultMaterial;
    }

    public void ChangeScale(float scaleRange, bool isCube)
    {
        Vector3 scale = transform.localScale;
        float[] scales = { scale.x, scale.y }; 

        if (scaleRange < 0)
        {
            if(minSize < scales.Min())
            {
                if (isCube)
                {
                    transform.localScale = new Vector3(scale.x + scaleRange, scale.y + scaleRange, scale.z + scaleRange);
                }
                else
                {
                    transform.localScale = new Vector3(scale.x + scaleRange, scale.y + scaleRange, scale.z);
                }
            }
        }
        else
        {
            if (scales.Min() < maxSize)
            {
                if (isCube)
                {
                    transform.localScale = new Vector3(scale.x + scaleRange, scale.y + scaleRange, scale.z + scaleRange);
                }
                else
                {
                    transform.localScale = new Vector3(scale.x + scaleRange, scale.y + scaleRange, scale.z);
                }
            }
        }
    }

    public void useRotartion()
    {

    }

    public void usePosition()
    {

    }

    public void resetPhysics()
    {

    }
}
