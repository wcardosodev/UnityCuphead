using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour {

    [SerializeField] float movementSpeed;

    [SerializeField] Vector2 posA, posB;

    void Update () {
        transform.position = Vector3.Lerp(posA, posB, Mathf.PingPong(Time.time * movementSpeed, 1.0f));
    }
}
