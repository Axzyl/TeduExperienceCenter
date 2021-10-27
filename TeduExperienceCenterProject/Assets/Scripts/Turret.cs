using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] shootPoints;
    [SerializeField] float fireRate;
    [SerializeField] AudioClip fireClip;

    GameObject pointTo;

    public void StartShooting()
    {
        pointTo = new GameObject();
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        while(GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            pointTo.transform.LookAt(enemies[Random.Range(0,enemies.Length - 1)].transform);
            transform.LookAt(enemies[Random.Range(0, enemies.Length - 1)].transform);
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, pointTo.transform.eulerAngles.y, transform.eulerAngles.z);
            //transform.GetChild(0).eulerAngles = new Vector3(pointTo.transform.eulerAngles.x, transform.GetChild(0).eulerAngles.y, transform.GetChild(0).eulerAngles.z);
            for(int i = 0; i < shootPoints.Length; i++)
            {
                GameObject proj = Instantiate(projectile, shootPoints[i].position, Quaternion.Euler(shootPoints[i].eulerAngles + Vector3.right * 90));
                Destroy(proj,1);
            }

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 3000, LayerMask.GetMask("Enemy")))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(25);
            }

            GetComponent<AudioSource>().PlayOneShot(fireClip);
            yield return new WaitForSeconds(fireRate);
        }
    }
}
