using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType Type = WeaponType.pistol;
    public GameObject bulletPrefab;

    public int Damage = 1;
    public float Speed = 6f;
    public float ReloadTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (Type != WeaponType.shotgun)
        {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = b.GetComponent<Bullet>();
            bullet.Damage = Damage;
            bullet.Speed = Speed;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Bullet bullet = b.GetComponent<Bullet>();
                bullet.Damage = Damage;
                bullet.Speed = Speed;

                bullet.AlterRotation();
            }
        }
    }
}

public enum WeaponType
{
	pistol,
	rifle,
	shotgun,
	bazooka,
	machineGun
}
