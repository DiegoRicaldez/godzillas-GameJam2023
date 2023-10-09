using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCollider : MonoBehaviour
{
    public GameObject parent;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnDestroy()
	{
		Destroy(parent);
	}
}
