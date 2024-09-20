using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject source;
    private GameObject target;
    private int attack;

    public float speed = 0.1f;

    /*
     * Assign variables to instance of fireball prefab
     * 
     * @param _source GameObject that calls method
     * @param _target GameObject that _source is targeting
     * @param _attack Integer of damage projectile does
     */
    public void Create(GameObject _source, GameObject _target, int _attack)
    {
        source = _source;
        target = _target;
        attack = _attack;
    }

    private void Update()
    {
        // If enemy unit is destroyed, then destroy arrow
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = new Vector3(target.transform.position.x, target.transform.position.y + 0.02f, target.transform.position.z) - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;   // Distance arrow moves each frame

        // If true, then arrow has reached target
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        target.transform.SendMessage("TakeDamage", attack);

        // Find Colliders near target
        Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, 0.05f);

        // Loop through array of Collider to find viable targets to take splash damage 
        foreach (var hitCollider in hitColliders)
        {
            // If hitCollider is original target, Untagged, or friendly unit, continue to next iteration
            if (hitCollider.gameObject == target || hitCollider.gameObject.CompareTag("Untagged") || hitCollider.gameObject.CompareTag(source.tag))
            {
                continue;
            }
            hitCollider.gameObject.SendMessage("TakeDamage", attack * 0.5f);
        }
        Destroy(gameObject);
    }
}
