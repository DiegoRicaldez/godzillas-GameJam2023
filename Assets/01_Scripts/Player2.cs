using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerBase
{
    public int acidAttacks = 5;
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

			ReloadSpecial();
			UseSpecialAttack();
            SpecialAttackDuration();
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

    void ReloadSpecial()
    {
		if (!canUseSpecialAttack && !isUsingSpecialAttack)
        {
			if (CooldownTimer < SpecialAttackCooldown)
			{
				CooldownTimer += Time.deltaTime;
				if (SpecialBarUI != null) SpecialBarUI.fillAmount = CooldownTimer / SpecialAttackCooldown;
			}
			else
			{
				if (SpecialBarUI != null) SpecialBarUI.fillAmount = 1f;
				CooldownTimer = 0f;
                canUseSpecialAttack = true;
				SpawnedSpecial = false;
			}
		}
	}
    void UseSpecialAttack()
    {
		if (canUseSpecialAttack && !isUsingSpecialAttack && Input.GetKeyDown(KeyCode.R))
		{
			canUseSpecialAttack = false;
			isUsingSpecialAttack = true;
			if (bodyAnim != null) bodyAnim.SetTrigger("SpecialAttack");
		}
    }
    void SpecialAttackDuration()
    {
        if (isUsingSpecialAttack)
        {
			if (SpecialAttactTimer < SpecialAttactDuration)
			{
				SpecialAttactTimer += Time.deltaTime;
			}
			else
			{
				SpecialAttactTimer = 0f;

				isUsingSpecialAttack = false;
			}

			if (!SpawnedSpecial && SpecialAttactTimer >= TimeToSpawnSpecialAttack)
			{
				for (int i = 0; i < acidAttacks; i++)
                {
					Instantiate(SpecialPrefab, SpecialSpawnPoint.position, SpecialSpawnPoint.rotation);
				}
				SpawnedSpecial = true;
			}
		}
    }
    #endregion
}
