using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSquareBoss : MonoBehaviour
{

    int playerLayer = 9;

    CharacterHealth health;

    [SerializeField] GameObject[] projectile;
    [SerializeField] GameObject firePoint;
    [SerializeField] int fireCount;

    [SerializeField] float waitTimeForBurstAttackOne = 3f;
    [SerializeField] float waitTimeBetweenShots = 2f;
    bool attack1Started = false;
    bool attack2Started = false;
    bool attack3Started = false;

    [SerializeField] Vector3[] positions;

    enum Phase { Phase1, Phase2, Phase3 };

    Phase phase = Phase.Phase1;

    /// <summary>
    /// At what health percentages will the boss use each attack??
    /// </summary>

    private void Start()
    {
        health = GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        float healthPercent = health.HealthAsPercent;
        print(health.HealthAsPercent);

        if (healthPercent <= .4f)
        {
            if (!attack3Started)
            {
                attack3Started = true;
                StopAllCoroutines();
                StartCoroutine(MoveAndAttack(positions[2], "Attack3", 2f));
                //StartCoroutine(Attack3());
            }
        }

        if (healthPercent <= .7f)
        {
            if (!attack2Started)
            {     
                attack2Started = true;
                StopAllCoroutines();
                StartCoroutine(MoveAndAttack(positions[1], "Attack2", 2f));
                //StartCoroutine(Attack2());
            }
        }

        if (healthPercent <= 1f)
        {
            if (!attack1Started)
            {
                attack1Started = true;
                StartCoroutine(MoveAndAttack(positions[0],"Attack1", 0));
                //StartCoroutine(Attack1());
            }
        }
    }

    IEnumerator Attack1()
    {
        GameObject proj = null;
        for (int i = 0; i < fireCount; i++)
        {
            proj = Instantiate(projectile[0], firePoint.transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = Vector2.left * proj.GetComponent<Projectile>().ProjectileSpeed;
            proj.layer = gameObject.layer;

            //As health gets lower increase the rate at which the attack is fired

            yield return new WaitForSeconds(waitTimeBetweenShots);
        }

        yield return new WaitForSeconds(waitTimeForBurstAttackOne);
        StartCoroutine(Attack1());
    }

    IEnumerator Attack2()
    {
        GameObject proj = null;
        //float projectileSpeed = 12f;
        for (int i = 0; i < fireCount; i++)
        {
            proj = Instantiate(projectile[1], new Vector2(Random.Range(-9, 11), 9.5f), Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = Vector2.down * proj.GetComponent<Projectile>().ProjectileSpeed;
            //proj.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
            proj.layer = gameObject.layer;

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Attack2());
    }

    IEnumerator Attack3()
    {
        yield return new WaitForSeconds(2f);
    }

    IEnumerator MoveAndAttack(Vector3 position, string coroutineToStart, float time = 0f)
    {

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        collider.enabled = false;
        sprite.enabled = false;

        if (transform.position != position)
        {
            transform.position = position;
        }

        yield return new WaitForSeconds(time);

        collider.enabled = true;
        sprite.enabled = true;

        StartCoroutine(coroutineToStart);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If colliding with the player
        if (collision.gameObject.layer == playerLayer && collision.gameObject.tag == "Player")
        {
            collision.GetComponent<CharacterHealth>().TakeDamage();
        }
    }
}
