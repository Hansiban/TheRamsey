using UnityEngine;

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

    [Header("Animation")]
    private PlayerEffect playerEffect;
    public bool haveGun;
    private Animator animator;

    [Header("Bullet")]
    private Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Cotton cotton;
    public Trail trail;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthManager = GetComponent<Health>();
        trail = GetComponentInChildren<Trail>();

        haveGun = true;
        bulletSpawnPoint = this.transform.Find("CottonSpawn");
        //playerEffect = GetComponentInChildren<PlayerEffect>();
        isGrounded = true;
    }

    void Update()
    {
        Move();
        Ani();

        //점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(animator.GetBool("isSit"));
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
        if (Input.GetKeyDown(KeyCode.Z) && haveGun)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            //trail.Make();
        }
    }

    private void Move()
    {
        //Move
        h = Input.GetAxisRaw("Horizontal");
        _rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (_rigidbody.velocity.x > maxSpeed) //오른쪽 최대속도 제한
        {
            _rigidbody.velocity = new Vector2(maxSpeed, _rigidbody.velocity.y);
        }
        else if (_rigidbody.velocity.x < maxSpeed * (-1)) //왼쪽 최대속도 제한
        {
            _rigidbody.velocity = new Vector2(maxSpeed * (-1), _rigidbody.velocity.y);
        }

        //Ani
        animator.SetFloat("Horizontal", h);
    }

    public void Jump(bool isSit)
    {
        //Jump
        if (isSit)
        {
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

    public void Damage()
    {
        Debug.Log("뚜까맞음");
        healthManager.curhealth--;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            animator.SetBool("isJump", !isGrounded);
        }
        if (col.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    private void HaveGun()
    {
        haveGun = true;
        //총 아이템 만들어 연동하기 1009
    }
}
