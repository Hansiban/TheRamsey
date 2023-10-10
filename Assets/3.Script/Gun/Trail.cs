using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public GameObject trailprefabs;
    public GameObject Bulletprefabs;
    public Transform cottonspawn;
    private GameObject player;
    private PlayerControll playerControll;

    public float speed;
    private float h;
    private int count;

    private void Awake()
    {
        cottonspawn = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        playerControll = player.GetComponent<PlayerControll>();
    }

    private void Start()
    {
        h = playerControll.h;
        if (h == 1)
        {
            //h가 1이라면 rotation변경
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        count = 0;
    }

    private void Update()
    {
        Move();
    }
    public void Make()
    {
        //최대 6개 생성
        if (count < 7)
        {
            Debug.Log("trail 생성");
            GameObject trail = Instantiate(trailprefabs, cottonspawn.position, Quaternion.identity);
            count++;
        }
        else
        {
            Debug.Log("카운트 초과");
        }
    }

    private void Move()
    {
        //이동시키기
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //removeTail이랑 trigger하면 삭제
        if (other.name == "removeTail")
        {
            Destroy(gameObject);
        }
    }
}
