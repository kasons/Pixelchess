using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUnit : UnitClass
{
    public GameObject arrowPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = 25;
        attack = 4;
        attackRange = 10f;
        attackSpeed = 1f;
        movementSpeed = 1.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    public override void Attack(GameObject targetedEnemy)
    {
        if (enemies.Contains(targetedEnemy))
        {
            FindObjectOfType<AudioManager>().Play("archer_attack");
            SpawnArrow(targetedEnemy);
        }
    }

    private void SpawnArrow(GameObject targetedEnemy)
    {
        GameObject arrowGO = (GameObject)Instantiate(arrowPrefab, new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z), Quaternion.identity);
        Arrow arrow = arrowGO.GetComponent<Arrow>();

        if (arrow != null)
        {
            arrow.Create(gameObject, targetedEnemy, attack);
        }
    }
}
