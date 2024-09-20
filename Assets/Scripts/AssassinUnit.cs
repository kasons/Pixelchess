using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinUnit : UnitClass
{
    private short teleported = 0;       // 0=falase, 1=in process, 2=true
    public bool addMyList = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = 25;
        attack = 8;
        attackRange = 0.03f;
        attackSpeed = 2f;
        movementSpeed = 0.01f;
    }

    protected override void Update()
    {
        // Update enemy-related info
        if (GameMaster.instance.selectPhase == false)
        {
            if (addMyList)
            {
                enemies.AddRange(GameObject.FindGameObjectsWithTag(enemyTag));
                addMyList = false;
                addList = false;
            }

            // If not teleported, begin teleporting Assassin
            if (teleported == 0)
            {
                Teleport();
                teleported = 1;
            }
        }
        
        // Only attack if teleport is finished
        if (teleported == 2)
        {
            base.Update();
        }
    }

    private void Teleport()
    {
        enemyTargeted = false;
        GameObject furthest = GetFurthestEnemy();
        StartCoroutine(Teleport(furthest));
    }

    // Find and return furthest target
    private GameObject GetFurthestEnemy()
    {
        GameObject furthest = null;
        float furthestDistanceSq = Mathf.NegativeInfinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 distToTarget = potentialTarget.transform.position - currentPos;
            float distToTargetSq = distToTarget.sqrMagnitude;

            if (distToTargetSq > furthestDistanceSq)
            {
                furthest = potentialTarget;
                furthestDistanceSq = distToTargetSq;
            }
        }

        return furthest;
    }

    IEnumerator Teleport(GameObject furthest)
    {
        yield return new WaitForSeconds(3);
        if (transform.position.x > furthest.transform.position.x)
        {
            transform.position = new Vector3(furthest.transform.position.x - 0.03f, furthest.transform.position.y, furthest.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(furthest.transform.position.x + 0.03f, furthest.transform.position.y, furthest.transform.position.z);
        }
        transform.RotateAround(transform.position, transform.up, 180f);
        teleported = 2;

    }
}
