using System.Collections;
using System.Collections.Generic;
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
    private Collider2D col;
    private Rigidbody2D rigid;
    public int h;
    private bool wasjump;

    [Header("HP")]
    public Health health;
    public bool onDie;
    [SerializeField] private GameObject howto;

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
    private Animator ani;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        ani = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }
    public void Start()
    {
        spawnpoints = new List<float>();
        makeCount = 4;
        h = 1;
    }
    public void FixedUpdate()
    {
        if (warning || onDie)
        {
            return;
        }
        if (health.curhealth == 0 && !onDie)
        {
            OnDie();
        }
        Move();
        DrawRay();
    }
    private void DrawRay()
    {
        RaycastHit2D hitup = Physics2D.Raycast(hituppos.transform.position, Vector2.up, 10f, layerMask);
        RaycastHit2D hitdown = Physics2D.Raycast(hitdownpos.transform.position, Vector2.down, 10f, layerMask);

        Debug.DrawRay(hituppos.transform.position, Vector2.up, Color.red, 10f);
        Debug.DrawRay(hitdownpos.transform.position, Vector2.down, Color.blue, 10f);

        if (hitup)
        {
            hitup.collider.isTrigger = true;
        }

        if (hitdown)
        {
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
                ani.SetBool("Run", false);
                break;
            case 1: // 오른쪽
                currentVelocity.x = speed;
                ani.SetBool("Run", true);
                break;
            case 2: // 왼쪽
                currentVelocity.x = -speed;
                ani.SetBool("Run", true);
                break;
        }
        rigid.velocity = currentVelocity;
    }
    IEnumerator Jump()
    {
        //1초 기다렸다 뛰기
        yield return new WaitForSeconds(1f);
        rigid.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        ani.SetBool("isJump", true);

        if (!wasjump)
        {
            Invoke("Stomp", 3f);
            wasjump = true;
        }

        else
        {
            Invoke("Fly", 1f);
        }
    }
    private void Fly()
    {
        transform.position = new Vector2(0, 3);
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
        StartCoroutine(Jump());
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
        if (howto)
        {
            Destroy(howto);
        }
    }
    public void OnDie()
    {
        onDie = true;
        ani.SetBool("Dead", true);
        rigid.AddForce(Vector2.up * jumpforce / 2, ForceMode2D.Impulse);
        col.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            //순간 멈추게 만들기
            h = 0;
            Debug.Log("h: 0");

            //1초 지연 후 방향설정
            StartCoroutine("Delay", other);

            //스프링발판이면 점프하게 만들기
            if (other.gameObject.GetComponent<BossPos>().isspring)
            {
                StartCoroutine("Jump");
            }
            if (other.gameObject.GetComponent<BossPos>().isreset)
            {
                //변수 초기화
                wasjump = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            ani.SetBool("isJump", false);
        }
    }
    IEnumerator Delay(Collider2D other)
    {
        yield return new WaitForSeconds(1f);
        h = other.gameObject.GetComponent<BossPos>().posnum;
        Debug.Log("h:" + h);
    }
}