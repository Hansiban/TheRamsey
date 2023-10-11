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

        //����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(animator.GetBool("isSit"));
        }

        //���̱� ���
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("isSit", true);
        }
        else
        {
            animator.SetBool("isSit", false);
        }

        //�Ѿ� �����
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

        if (_rigidbody.velocity.x > maxSpeed) //������ �ִ�ӵ� ����
        {
            _rigidbody.velocity = new Vector2(maxSpeed, _rigidbody.velocity.y);
        }
        else if (_rigidbody.velocity.x < maxSpeed * (-1)) //���� �ִ�ӵ� ����
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
        //���̱� �ִϸ��̼�
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
        Debug.Log("�ѱ����");
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
        //�� ������ ����� �����ϱ� 1009
    }
}
