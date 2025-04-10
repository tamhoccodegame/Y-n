using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yen_DuaGheController : MonoBehaviour
{
    Rigidbody2D rb;
    public float horizontalMoveSpeed;
    public float verticalMoveSpeed;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.HideUI();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(horizontalInput * horizontalMoveSpeed, verticalInput * verticalMoveSpeed);
    }
}
