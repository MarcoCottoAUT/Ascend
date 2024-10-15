using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    public List<GameObject> Colliders = new List<GameObject>(); 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            for (int k = 0; k < collision.contacts.Length; k++)
            {
                if (Vector3.Angle(collision.contacts[k].normal, Vector2.up) <= 30)
                {
                    Colliders.Add(collision.transform.gameObject);
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            Colliders.Remove(collision.transform.gameObject);
        }
    }
}
