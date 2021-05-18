using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            string level = "Death";
            Application.LoadLevel(level);
        }
        else
        {
            Destroy(collision.gameObject);
            CollectableManager.instance.IncrementScore(1);
        }
    }
}
