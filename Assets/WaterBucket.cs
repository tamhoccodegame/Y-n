using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    public bool isFull = false;
    public Sprite waterBucketRenderer;
    public Sprite fullWaterBucketRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUpBucket(Transform holdingPoint)
    {
        transform.position = holdingPoint.position;
        transform.SetParent(holdingPoint);
    }

    public void FillBucket()
    {
        if(!isFull)
        {
            isFull = true;
            GetComponent<SpriteRenderer>().sprite = fullWaterBucketRenderer;
            GameManager.instance.PlayAudio("Muc_Nuoc");
        }
    }

    public void EmptyBucket()
    {
        if (isFull)
        {
            isFull = false;
            GetComponent<SpriteRenderer>().sprite = waterBucketRenderer;
			GameManager.instance.PlayAudio("Dap_Lua");
		}
	}
}
