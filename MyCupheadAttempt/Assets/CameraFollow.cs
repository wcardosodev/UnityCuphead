using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;

    [SerializeField] bool lockedCamera = false;

    [SerializeField] float xMin;
    [SerializeField] float xMax;

    [SerializeField] float yMin;
    [SerializeField] float yMax;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerMovement>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (!lockedCamera)
            {
                //transform.position = new Vector3(player.transform.position.x, Mathf.Clamp(player.transform.position.y, yMin, yMax), transform.position.z);
                transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, xMin, xMax), Mathf.Clamp(player.transform.position.y, yMin, yMax), transform.position.z);
            }
        }
	}
}
