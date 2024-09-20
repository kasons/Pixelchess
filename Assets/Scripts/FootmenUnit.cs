using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmenUnit : UnitClass
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = 75;
        attack = 6;
        attackRange = 0.03f;
        attackSpeed = 1f;
        movementSpeed = 0.01f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
