using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menutheme : MonoBehaviour
{
	[Header("Sounds")]
	public AudioClip Music; 

    void Start()
    {
        AudioManager.instance.SetMusic(Music);
	}

    void Update()
    {
        
    }
}
