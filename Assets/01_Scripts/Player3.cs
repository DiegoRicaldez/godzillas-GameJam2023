using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : PlayerBase
{
	public float Force = 40f;
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


				Vector3 v = SpecialSpawnPoint.TransformPoint(Vector3.zero);
				v = v - transform.position;
				v = new Vector3(v.x *2, v.y, v.z * 2);
				rb.AddForce(v * Force, ForceMode.Impulse);
				SpawnedSpecial = true;
			}
		}
	}
	#endregion
}
