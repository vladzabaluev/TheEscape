using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
	protected new void Start()
	{

	}

	protected override void Die()
    {
        Destroy(gameObject);
    }
}
