using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public float aimTime = 1f;
    float timer = 0f;
    Transform player;

    public Transform WeaponPoint;
    public List<GameObject> WeaponsPrefabs; // 5
    
    Weapon weapon;
    bool isPlayerNear = false;
    public float DistanceToAttack = 5f;
    float AttackTime; // viene del arma
    float timer2 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        searchPlayer();
        selectWeapon();
    }

    void selectWeapon()
    {
        (int min, int max) usedWeapons;

        switch (Manager.instance.GameLevel)
        {
            case 1:
                usedWeapons = (0, 1);
                break;
            case 2:
                usedWeapons = (0, 2);
                break;
            case 3:
                usedWeapons = (1, 3);
                break;
            case 4:
                usedWeapons = (2, 3);
                break;
            case 5:
                usedWeapons = (3, 4);
                break;
            default:
                usedWeapons = (0,4); 
                break;
        }

        if (usedWeapons.max != 0)
        {
            int index = Random.Range(usedWeapons.min, usedWeapons.max + 1);

            GameObject g = Instantiate(WeaponsPrefabs[index], WeaponPoint.position, WeaponPoint.rotation);
            weapon = g.GetComponent<Weapon>();
            weapon.transform.parent = WeaponPoint;
            weapon.transform.localScale = WeaponPoint.localScale;
            AttackTime = weapon.ReloadTime;
        }
	}

    // Update is called once per frame
    void Update()
    {
        Rotate();

        // if (weapon != null) {
        CanAttack();
        Attack();
        // }
    }

    void Rotate()
    {
        if (timer < aimTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;

            if (player != null)
            {
				transform.LookAt(player);
                transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
			}
            else
            {
                searchPlayer();
            }
        }
    }
    void CanAttack()
    {
        if (player != null)
        {
			float d = Vector3.Distance(transform.position, player.position);
			if (d <= DistanceToAttack)
			{
				isPlayerNear = true;
			}
			else
			{
				isPlayerNear = false;
			}
		}
        else
        {
            isPlayerNear = false;
        }
    }
    void Attack()
    {
		if (isPlayerNear)
        {
            if (timer2 < AttackTime)
            {
                timer2 += Time.deltaTime;
            }
            else
            {
                if (isPlayerNear)
                {
                    timer2 = 0f;

                    if (player != null)
                    {
                        weapon.Shoot();
                    }
                }
            }
        }
		
	}

    void searchPlayer()
    {
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null) player = obj.GetComponent<PlayerBase>().transform;
	}
}
