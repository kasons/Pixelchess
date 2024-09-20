using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUnit : UnitClass
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = 100;
        attack = 2;
        attackRange = 0.03f;    
        attackSpeed = 2f;
        movementSpeed = 0.01f;

        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
