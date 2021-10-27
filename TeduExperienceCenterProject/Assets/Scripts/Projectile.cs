using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.collider.gameObject.GetComponent<Enemy>() != null)
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }

            if (!hit.collider.gameObject.name.Equals("Player") && !hit.collider.gameObject.name.Equals("rover"))
            {
                Destroy(gameObject);
            }
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }

        if (!collision.gameObject.name.Equals("Player") && !collision.gameObject.name.Equals("rover"))
        {
            Destroy(gameObject);
        }
    }
}
