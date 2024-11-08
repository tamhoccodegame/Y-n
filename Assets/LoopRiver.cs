using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRiver : MonoBehaviour
{
    public float speed;
    Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(Time.time * speed, 0);
        GetComponent<SpriteRenderer>().material.mainTextureOffset = offset;
    }
}
