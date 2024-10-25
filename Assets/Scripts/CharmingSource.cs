using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmingSource : MonoBehaviour
{
    public AI enemy;

    public float charmingTime = 3f;
    public float charmTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Charming();
        }
    }

    void Charming()
    {
		StartCoroutine(enemy.Charmed(transform, charmingTime));
    }
}
