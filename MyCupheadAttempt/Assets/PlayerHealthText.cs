using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthText : MonoBehaviour {

    Text healthText;

	void Start () {
        healthText = GetComponent<Text>();
	}

	void Update () {

        try
        {
            healthText.text = FindObjectOfType<PlayerAttack>().GetComponent<CharacterHealth>().Health.ToString();
        }
        catch (System.NullReferenceException)
        {
            healthText.text = "0";
        }
	}
}
