using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControll : MonoBehaviour
{
    [Header("Move")]
    public float h;
    private bool isGrounded;
    [SerializeField] private float maxSpeed;
    [SerializeField] private int jumpforce;
    private Rigidbody2D _rigidbody;

    [Header("Health")]
    public Health healthManager;
    private bool onDie;

    [Header("Animation")]
    private PlayerEffect playerEffect;
    public bool haveGun;
    private Animator animator;
    private Collider2D col;

    [Header("Bullet")]
    private Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Cotton cotton;
    public Trail trail;

    [Header("Boss")]
    [SerializeField] private LayerMask layer;
    [SerializeField] private BossTile bossTile;

    [Header("Potal")]
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _rigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        healthManager = GetComponent<Health>();
        trail = GetComponentInChildren<Trail>();
    }

    void Start()
    {
        haveGun = true;
        bulletSpawnPoint = this.transform.Find("CottonSpawn");
        isGrounded = true;
        onDie = false;
    }

    void Update()
    {
        if (onDie)
        {
            return;
        }

        Move();
        Ani();
        DrawCircle();

        //점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (bossTile)
            {
                bossTile.Attack();
            }
            else
            {
                Jump(animator.GetBool("isSit"));
            }
        }

        //숙이기 모션
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("isSit", true);
        }
        else
        {
            animator.SetBool("isSit", false);
        }

        //총알 만들기
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            //trail.Make();
        }
    }

    private void Move()
    {
        h = Input.GetAxisRaw("Horizontal");//Move
        animator.SetFloat("Horizontal", h);//Ani
        _rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (_rigidbody.velocity.x > maxSpeed)//오른쪽 최대속도 제한
        {
            _rigidbody.velocity = new Vector2(maxSpeed, _rigidbody.velocity.y);
        }
        else if (_rigidbody.velocity.x < maxSpeed * (-1))//왼쪽 최대속도 제한
        {
            _rigidbody.velocity = new Vector2(maxSpeed * (-1), _rigidbody.velocity.y);
        }
    }

    public void Jump(bool isSit)
    {
        //Jump
        if (isSit)
        {
            _rigidbody.velocity = new Vector2(transform.position.x, 0);
            _rigidbody.AddForce(Vector2.down * jumpforce, ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }
        isGrounded = false;

        //Ani
        animator.SetBool("isJump", true);
    }
    public void Jump(float jump)
    {
        //Jump
        _rigidbody.velocity = new Vector2(transform.position.x, 0);
        _rigidbody.AddForce(Vector2.up * jumpforce * jump, ForceMode2D.Impulse);
        isGrounded = false;

        //Ani
        animator.SetBool("isJump", true);
    }

    private void Ani()
    {
        //숙이기 애니메이션
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("isSit", true);
        }
        else
        {
            animator.SetBool("isSit", false);
        }
    }

    private void DrawCircle()
    {
        Collider2D obj = Physics2D.OverlapCircle(transform.position, 0.2f, layer);
        if (obj)
        {
            bossTile = obj.gameObject.GetComponent<BossTile>();
        }

        else
        {
            bossTile = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
        Gizmos.DrawWireSphere(position, 0.2f);
    }

    public void Damage()
    {
        Debug.Log("뚜까맞음");
        healthManager.curhealth--;

        if (healthManager.curhealth == 0)
        {
            OnDie();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!isGrounded && ( col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Obstacle")))
        {
            isGrounded = true;
            animator.SetBool("isJump", !isGrounded);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "J")
        {
            gameManager.GotoAcornVillage();
        }

        else if (other.gameObject.name == "B")
        {
            gameManager.GotoBoss();
        }
    }

    private void OnDie()
    {
        onDie = true;
        animator.SetTrigger("Dead");
        _rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        col.isTrigger = true;
    }

    private void HaveGun()
    {
        haveGun = true;
        //총 아이템 만들어 연동하기 1009
    }
}
