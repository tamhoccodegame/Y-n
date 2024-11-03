using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RawImage img;
    public float x, y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// Lấy giá trị uvRect hiện tại
		Rect uvRect = img.uvRect;

		// Cộng thêm offset vào vị trí hiện tại
		uvRect.position += new Vector2(x, y) * Time.deltaTime;

		// Giới hạn vị trí uvRect trong khoảng 0 - 1 để lặp lại
		uvRect.x = uvRect.x % 1;
		uvRect.y = uvRect.y % 1;

		// Gán lại uvRect cho RawImage
		img.uvRect = uvRect;
	}
}
