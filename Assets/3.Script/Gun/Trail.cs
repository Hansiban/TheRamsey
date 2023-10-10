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
            //h�� 1�̶�� rotation����
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
        //�ִ� 6�� ����
        if (count < 7)
        {
            Debug.Log("trail ����");
            GameObject trail = Instantiate(trailprefabs, cottonspawn.position, Quaternion.identity);
            count++;
        }
        else
        {
            Debug.Log("ī��Ʈ �ʰ�");
        }
    }

    private void Move()
    {
        //�̵���Ű��
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //removeTail�̶� trigger�ϸ� ����
        if (other.name == "removeTail")
        {
            Destroy(gameObject);
        }
    }
}
