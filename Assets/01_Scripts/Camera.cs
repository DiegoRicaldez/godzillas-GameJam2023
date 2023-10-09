using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera : MonoBehaviour
{
	PlayerBase player;
	public float distanceY;
	public float distanceZ;
	public float CameraAngle = 50f;

	void Start()
	{
		updatePlayer();
		CameraConfigure(0);
	}

	// Update is called once per frame
	void Update()
	{
		Move();
	}

	void Move()
	{
		if (player != null)
		{
			float x = player.transform.position.x;
			float z = player.transform.position.z - distanceZ;

			if (transform.position.x != x || transform.position.z != z)
			{
				transform.position = new Vector3(x, distanceY, z);
			}
		}
	}

	public void updatePlayer()
	{
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null) player = obj.GetComponent<PlayerBase>();
	}
	public void updatePlayer(PlayerBase p)
	{
		player = p;
	}

	public void CameraConfigure(int lvl)
	{
		transform.rotation = Quaternion.Euler(CameraAngle, 0, 0);
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null) player = obj.GetComponent<PlayerBase>();

		switch (lvl)
		{
			case 0:
				distanceY = 7f;
				distanceZ = 6f;
				break;
			case 1:
				distanceY = 11f;
				distanceZ = 10f;
				break;
			case 2:
				distanceY = 13f;
				distanceZ = 12f;
				break;
			case 3:
				distanceY = 16f;
				distanceZ = 15f;
				break;
			case 4:
				distanceY = 16f;
				distanceZ = 20f;
				break;
			case 5:
				distanceY = 20f;
				distanceZ = 25f;
				break;
		}
	}
}
