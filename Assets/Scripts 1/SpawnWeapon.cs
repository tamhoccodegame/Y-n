using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Transform weaponHolder;
    // Start is called before the first frame update
    void Start()
    {
        GameObject currentWeapon = Instantiate(weaponPrefab, weaponHolder.position, weaponPrefab.transform.rotation);
		currentWeapon.transform.localPosition = Vector3.zero;
		currentWeapon.transform.localRotation = Quaternion.identity;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
