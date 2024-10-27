using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSwitcher : MonoBehaviour
{
    public float backGroundY;
    public float middleGroundY;

    public Vector3 playerMiddleGroundScale;
    public Vector3 playerBackGroundScale;

    public SpriteRenderer playerOrderLayer;
	public CinemachineVirtualCamera virtualCamera;
	Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            playerOrderLayer.sortingOrder = -3;
            virtualCamera.m_Lens.OrthographicSize = 6f;
			int middlegroundLayer = LayerMask.NameToLayer("MiddleGround");
			cam.cullingMask = ~(1 << middlegroundLayer);
            transform.localScale = playerBackGroundScale;
            transform.position = new Vector3(transform.position.x, backGroundY, -10);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerOrderLayer.sortingOrder = 0;
			virtualCamera.m_Lens.OrthographicSize = 7.5f;
			cam.cullingMask = -1;
			transform.localScale = playerMiddleGroundScale;
			transform.position = new Vector3(transform.position.x, middleGroundY, -10);
		}

	}
}
