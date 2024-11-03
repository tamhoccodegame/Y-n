using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MNG_RapidButtonPressing : MonoBehaviour
{
    public Slider slider;
    public float decreaseSpeed;
    public float incrementSpeed;

    public Transform obstacle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value -= Time.deltaTime * decreaseSpeed;
        slider.value = Mathf.Clamp01(slider.value); 

        if (Input.GetKeyDown(KeyCode.E))
        {
            slider.value += incrementSpeed;
            obstacle.position += new Vector3(0.01f, 0, 0);
        }

        if(slider.value >= 1f)
        {
            GameManager.instance.SetIsControllable(true);
            gameObject.SetActive(false);
        }
    }
}
