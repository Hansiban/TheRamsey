using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.tag == "Player")
        {
            Debug.Log("Player");
            GameManager.Instance.GameClear();
        }
    }
}
