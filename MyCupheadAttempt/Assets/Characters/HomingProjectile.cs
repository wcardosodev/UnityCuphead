using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile {

    Transform target;

    float rotationSpeed = 200f;

	void Start () {
        target = FindObjectOfType<PlayerMovement>().transform;
	}

	void FixedUpdate () {

        Vector2 direction = target.position - transform.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed;

		rb.velocity = transform.up * 5f;
	}
}
