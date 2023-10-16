using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4 : PlayerBase
{
	void Start()
	{
		StartMethod();
		if (animatiorContainer != null)
			bodyAnim = animatiorContainer.GetComponent<Animator>();
		isAttacking = true;
	}

	void Update()
	{
		if (!isDead)
		{
			Move();
			Rotate();

			ReloadSpecial();
			UseSpecialAttack();
			SpecialAttackDuration();
		}

		DeadAnim();
	}

	#region Attacks
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
			AudioManager.instance.PlaySFX(AttackSound);
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
				Instantiate(SpecialPrefab, SpecialSpawnPoint.position, SpecialSpawnPoint.rotation);
				SpawnedSpecial = true;
			}
		}
	}
	#endregion
}
