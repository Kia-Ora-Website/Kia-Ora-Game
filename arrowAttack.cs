using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andrehard.CharacterStats;
public class arrowAttack : MonoBehaviour
{
    public float damage;
    public GameObject target;
    public GameObject arrowPrefab;
    public float respawnTime = 1.0f;

    public GameObject player;
    public bool targetSet;
    public string targetType;
    public float velocity = 5;
    public bool stopProjectile;
    public Transform myChildObject;
    public float distance;

    // Update is called once per frame
    void spawnArrow()
    {
        /*GameObject temp = (GameObject.FindGameObjectsWithTag("spawn_att")[0]);
        Debug.Log(temp);
        Debug.Log(distance);
        distance = Vector3.Distance(player.transform.position, transform.position);
        */
        GameObject a = Instantiate(arrowPrefab) as GameObject;
    }
    IEnumerator arrowCond()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnArrow();
        }
    }
    void Update()
    {
            if (target)
            {
                if (target == null)
                {
                    Destroy(gameObject);
                }
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);

                if (!stopProjectile)
                {
                    myChildObject.parent = null;
                    if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
                    {
                        if (targetType == "Mignon")
                        {
                            target.GetComponent<Character>().Health.BaseValue -= damage;
                            stopProjectile = true;
                            Destroy(gameObject);
                            spawnArrow();
                            stopProjectile = false;
                        }
                    }
                }

            }
    }
}
