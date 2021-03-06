﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingMonster : EnemyMovement
{
    private float CoolDownBefore;
    [SerializeField]
    private float CoolDown = 1f;

    protected override void Start()
    {
        CoolDownBefore = CoolDown;
        base.Start();
    }

    protected override void UpdateEnemy()
    {
        CoolDownBefore = Mathf.Max(CoolDownBefore - Time.deltaTime, 0);
        if (CoolDownBefore == 0)
        {
            Attack();
            CoolDownBefore = CoolDown;
        }
        base.UpdateEnemy();
    }

    protected virtual void Attack() {    }
}
