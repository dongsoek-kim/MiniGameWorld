using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stack
{
    public class DestroyZone : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision");
            if (collision.gameObject.name.Equals("Rubble"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
