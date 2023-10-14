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

        transform.rotation = Quaternion.Euler(CameraAngle, 0, 0);
        updatePlayer();
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
		else
		{
			updatePlayer();
			Debug.Log("no hay player en la camara");
		}
	}

	public void updatePlayer()
	{
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null) player = obj.GetComponent<PlayerBase>();
	}

}
