using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenade : MonoBehaviour
{
    [SerializeField] float detonationDelay;

    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() == null) StartCoroutine(Detonate());

    }

    IEnumerator Detonate()
    {
        yield return new WaitForSeconds(detonationDelay);
        GameObject exp = Instantiate(Resources.Load<GameObject>("Grenade_Explosion"), transform.position, transform.rotation);
        exp.GetComponent<Explosion>().damage = damage;
        Destroy(exp,2);
        Destroy(gameObject);
    }
}
