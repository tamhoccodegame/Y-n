using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    public bool isFull = false;
    private SpriteRenderer waterBucketRenderer;
    private SpriteRenderer fullWaterBucketRenderer;
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
            GetComponent<SpriteRenderer>().sprite = fullWaterBucketRenderer.sprite;
        }
    }

    public void EmptyBucket()
    {
        if (isFull)
        {
            isFull = false;
            GetComponent<SpriteRenderer>().sprite = waterBucketRenderer.sprite;
        }
    }
}
