using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float projectileSpeed = 4f;
    [SerializeField] int projectileDamage = 10;
    [SerializeField] float destroyTime = 5f;

    private void Start()
    {
        StartCoroutine(DestroyProjectile());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.layer != collision.gameObject.layer)
        {
            print(collision.gameObject.name);
            if(collision.GetComponent<CharacterHealth>() != null)
            {
                collision.GetComponent<CharacterHealth>().TakeDamage(projectileDamage);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }

    public int ProjectileDamage
    {
        get { return projectileDamage; }
    }
}
