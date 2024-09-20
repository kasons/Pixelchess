using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MageUnit : UnitClass
{
    public GameObject fireballPrefab;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = 25;
        attack = 4;
        attackRange = 1f;
        attackSpeed = 2f;
        movementSpeed = 0.1f;
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
            FindObjectOfType<AudioManager>().Play("mage_attack");
            SpawnFireball(targetedEnemy);
        }
    }

    private void SpawnFireball(GameObject targetedEnemy)
    {
        GameObject fireballGO = (GameObject)Instantiate(fireballPrefab, new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z), Quaternion.identity);
        Fireball fireball = fireballGO.GetComponent<Fireball>();

        if (fireball != null)
        {
            fireball.Create(gameObject, targetedEnemy, attack);
        }
    }
}
