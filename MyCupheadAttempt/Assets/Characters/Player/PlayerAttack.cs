using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] GameObject projectileToFire;
    [SerializeField] GameObject[] firePoint;
    [SerializeField] int weaponDamage = 10;

    float lastAttackTime = 0f;
    [SerializeField] float secondsBetweenAttacks = .3f;

    int counter = 0;

    void Update () {
        lastAttackTime += Time.deltaTime;

        GetComponent<PlayerMovement>().CanMove = !Input.GetKey(KeyCode.LeftShift);
        
        if (Input.GetKey(KeyCode.K))
        {
            //Fire Projectile straight ahead
            if (lastAttackTime >= secondsBetweenAttacks)
            {
                lastAttackTime = 0f;

                GameObject projectile = Instantiate(projectileToFire, firePoint[counter].transform.position, Quaternion.identity);
                projectile.layer = gameObject.layer;

                if (GetComponent<PlayerMovement>().FacingLeft)
                {
                    projectile.GetComponent<Rigidbody2D>().velocity = -Vector2.right * projectile.GetComponent<Projectile>().ProjectileSpeed;
                }
                else
                {
                    projectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * projectile.GetComponent<Projectile>().ProjectileSpeed;
                }

                counter++;

                if(counter == firePoint.Length)
                {
                    counter = 0;
                }
            }
        }
	}
}
