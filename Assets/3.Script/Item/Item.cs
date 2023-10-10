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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
        }
    }

    private void cotton()
    {
        //플레이어 애니메이션
        //아이템 획득 UI
        //플레이어 haveGun활성화
        //z누르면 총쏠수있다는 UI
    }

    private void boltnut()
    {

    }

}
