using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float speed;
    [SerializeField] float jumpforce;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject hitdownpos;
    [SerializeField] private GameObject hituppos;
    [SerializeField] private BossPos bosspos;
    private Rigidbody2D rigid;
    public int h;
    private bool isjump;

    [Header("HP")]
    public Health health;

    [Header("Make")]
    public float minTrans;
    public float maxTrans;
    public int makeCount;

    [Header("brick")]
    public GameObject[] bricks;

    [Header("Red")]
    [SerializeField] private GameObject red;
    private float spawnpoint;
    private List<float> spawnpoints;
    private Vector2 position;

    [Header("ETC")]
    [SerializeField] private GameObject warning;


    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }
    public void Start()
    {
        spawnpoints = new List<float>();
        makeCount = 4;
        h = 1;
    }

    public void Update()
    {
        DrawRay();
        //if (warning || isjump)
        //{
        //    return;
        //}
        Move();
    }
    private void DrawRay()
    {
        RaycastHit2D hitup = Physics2D.Raycast(hituppos.transform.position, Vector2.up, 0.5f, layerMask);
        RaycastHit2D hitdown = Physics2D.Raycast(hitdownpos.transform.position, Vector2.down, 0.5f, layerMask);

        Debug.DrawRay(hituppos.transform.position, Vector2.up, Color.red, 0.5f);
        Debug.DrawRay(hitdownpos.transform.position, Vector2.down, Color.blue, 0.5f);

        if (hitup)
        {
            Debug.Log("충돌up");
            hitup.collider.isTrigger = true;
        }

        if (hitdown)
        {
            Debug.Log("충돌down");
            hitdown.collider.isTrigger = false;
        }
    }
    private void Move()
    {
        Vector2 currentVelocity = rigid.velocity;

        switch (h)
        {
            case 0: // 가만히있기
                currentVelocity.x = 0;
                break;
            case 1: // 오른쪽
                currentVelocity.x = speed; // 현재 속도에 speed를 더합니다.
                break;
            case 2: // 왼쪽
                currentVelocity.x = -speed; // 현재 속도에서 speed를 뺍니다.
                break;
        }
        rigid.velocity = currentVelocity;
    }
    public void Jump()
    {
        Debug.Log("Jump");
        rigid.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        Invoke("Stomp", 3f);
    }
    public void Stomp()
    {
        for (int i = 0; i < makeCount; i++)
        {
            MakeRed();
        }
        Invoke("Makebrick", 1.5f);
    }
    public void Makebrick()
    {
        for (int i = 0; i < makeCount; i++)
        {
            GameObject obj;
            if (i == makeCount - 1)
            {
                obj = Instantiate(bricks[1]);
            }

            else
            {
                obj = Instantiate(bricks[0]);
            }
            obj.transform.position = new Vector2(spawnpoints[i], 5);
        }
    }
    public void MakeRed()
    {
        while (true)
        {
            spawnpoint = Random.Range(minTrans, maxTrans);
            // 같은 스폰 포인트가 있는지 검사
            if (!spawnpoints.Contains(spawnpoint))
            {
                spawnpoints.Add(spawnpoint);
                position = new Vector2(spawnpoint, 0);
                var gameobject = Instantiate(red, position, Quaternion.identity);
                break;
            }
        }
    }
    public void Damage()
    {
        health.curhealth--;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall" && !isjump)
        {
            //순간 멈추게 만들기
            h = 0;
            Debug.Log("h: 0");

            //스프링발판이면 점프하게 만들기
            if (other.gameObject.GetComponent<BossPos>().isspring)
            {
                Jump();
            }

            //2초 지연시키기
            StartCoroutine("Delay", 2f);

            //방향 설정하기
            h = other.gameObject.GetComponent<BossPos>().posnum;
            Debug.Log("h:" + h);
        }
    }
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
    }
}