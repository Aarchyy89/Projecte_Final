using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala_Escopeta : MonoBehaviour
{
    private Transform pirate_target;

    public float speed = 70f;

    public void Seek(Transform _target)
    {
        pirate_target = _target;
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        if (pirate_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = pirate_target.position - transform.position;
        float distance_This = speed * Time.deltaTime;

        if (dir.magnitude <= distance_This)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distance_This, Space.World);
    }

    public void HitTarget()
    {
        Pirate.instance.TAKE_damage();
        Destroy(gameObject);
    }
}
