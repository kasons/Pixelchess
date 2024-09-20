using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject source;
    private GameObject target;
    private int attack;

    public float speed = 0.1f;

    /*
     * Assign variables to instance of arrow prefab
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

        transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y + 0.01f, target.transform.position.z));
        //Vector3 dir = target.transform.position - transform.position;
        Vector3 dir = new Vector3(target.transform.position.x, target.transform.position.y + 0.01f, target.transform.position.z) - transform.position;
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
        Destroy(gameObject);
    }
}
