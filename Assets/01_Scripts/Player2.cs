using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerBase
{
    void Start()
    {
        StartMethod();
        if (animatiorContainer != null)
            bodyAnim = animatiorContainer.GetComponent<Animator>();
    }


    void Update()
    {
        if (!isDead)
        {
            Move();
            Rotate();

            Attack();

            CheckSpecialAttack();
            SpecialAttack();
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

    void CheckSpecialAttack()
    {
        if (isUsingSpecialAttack)
        {
            if (SpecialAttactTimer < SpecialAttackCooldown)
            {
                SpecialAttactTimer += Time.deltaTime;
            }
            else
            {
                SpecialAttactTimer = 0f;
                canUseSpecialAttack = true;
                isUsingSpecialAttack = false;
            }
        }
    }
    void SpecialAttack()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isUsingSpecialAttack && !isAttacking)
            {
                if (SpecialAttactTimer < SpecialAttackCooldown)
                {
                    SpecialAttactTimer += Time.deltaTime;
                }
                else
                {
                    SpecialAttactTimer = 0f;

                    if (canUseSpecialAttack)
                    {
                        canUseSpecialAttack = false;
                        isUsingSpecialAttack = true;

                        //ataque? anim?
                    }
                }
            }
        }
    }
    #endregion
}
