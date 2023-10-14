using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerBase
{
    
    void Start()
    {
		StartMethod();
		if (animatiorContainer != null )
			bodyAnim = animatiorContainer.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
		{
			Move();
			Rotate();

			Attack();

			// no tiene ataque especial
		}

		DeadAnim();
    }

	#region Attacks
	void Attack()
	{
		if (!isUsingSpecialAttack)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				isAttacking = true;
			}
			else if (Input.GetKeyUp(KeyCode.Space))
			{
				isAttacking = false;
			}
		}
		else
		{
			isAttacking = false;
		}
	}
	#endregion

}
