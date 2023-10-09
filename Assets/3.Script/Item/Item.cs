using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Itemset
{
    cotton = 0,
    boltnut,
}

public class Item : MonoBehaviour
{
    Rigidbody2D rigid;
    public Itemset itemset;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

    }

    private void cotton()
    {
    
    }

}
