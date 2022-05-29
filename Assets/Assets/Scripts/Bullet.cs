using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);//Quaternion.identity);
        Destroy(effect, .2f);
        Destroy(gameObject);
    }
}
