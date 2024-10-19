using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GunAim2D : MonoBehaviour
{
	public Transform gun;  // Vị trí của cây súng
	public Transform firePoint;
	public GameObject bulletPrefab;
	private SpriteRenderer spriteRenderer;
	private Vector3 direction;
	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	void Update()
	{
		AimGun();
		Shoot();
	}

	void AimGun()
	{
		// Lấy vị trí chuột trong thế giới 2D
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0f;  // Đặt giá trị z bằng 0 để giữ trong không gian 2D

		// Tính hướng từ súng đến chuột
		direction = mousePosition - gun.position;

		// Tính toán góc xoay bằng cách sử dụng Atan2
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// Xoay súng theo góc tính được
		gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		if (angle > 180f) angle -= 180f;
		if (angle < -180f) angle += 180f;

		if (angle < 90 && angle > -90) // Nửa bên phải
		{
			spriteRenderer.flipY = false;
		}
		else if (angle > 90 && angle < 270) // Nửa bên trái
		{
			spriteRenderer.flipY = true;
		}

	}

	void Shoot()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Rigidbody2D bulletRb = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
			bulletRb.velocity = direction * 15;
		}
	}
}
