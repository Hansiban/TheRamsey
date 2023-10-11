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
    private Rigidbody2D rigid;
    public int h;
    private bool wasjump;

    [Header("HP")]
    public Health health;
    public bool onDie;

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

        DrawRay();
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
            case 0: // �������ֱ�
                currentVelocity.x = 0;
                break;
            case 1: // ������
                currentVelocity.x = speed;
                break;
            case 2: // ����
                currentVelocity.x = -speed;
                break;
        }
        rigid.velocity = currentVelocity;
    }
    IEnumerator Jump()
    {
        //1�� ��ٷȴ� �ٱ�
        yield return new WaitForSeconds(1f);
        rigid.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);

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
        StartCoroutine("Jump");
    }
    public void MakeRed()
    {
        while (true)
        {
            spawnpoint = Random.Range(minTrans, maxTrans);
            // ���� ���� ����Ʈ�� �ִ��� �˻�
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
    public void OnDie()
    {
        onDie = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            //���� ���߰� �����
            h = 0;
            Debug.Log("h: 0");
            //1�� ���� �� ���⼳��
            StartCoroutine("Delay", other);

            //�����������̸� �����ϰ� �����
            if (other.gameObject.GetComponent<BossPos>().isspring)
            {
                StartCoroutine("Jump");
            }
            if (other.gameObject.GetComponent<BossPos>().isreset)
            {
                //���� �ʱ�ȭ
                wasjump = false;
            }
        }
    }
    IEnumerator Delay(Collider2D other)
    {
        yield return new WaitForSeconds(1f);
        h = other.gameObject.GetComponent<BossPos>().posnum;
        Debug.Log("h:" + h);
    }
}