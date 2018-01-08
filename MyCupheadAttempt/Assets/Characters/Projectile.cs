using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour {

    protected Rigidbody2D rb;

    [SerializeField] float projectileSpeed = 4f;
    [SerializeField] int projectileDamage = 10;
    [SerializeField] float destroyTime = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
