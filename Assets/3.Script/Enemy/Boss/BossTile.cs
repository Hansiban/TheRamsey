using UnityEngine;
public class BossTile : MonoBehaviour
{
    [Header("Damage")]
    //[SerializeField] Boss boss;
    private Boss boss;
    private Rigidbody2D rigid;
    [SerializeField] float attackforce;

    [Header("ETC")]
    public bool canDamage; //Ư���� ���� Ȯ��
    private Animator animator;
    private PlayerControll player;
    [SerializeField]private AudioSource sound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>();
        sound = GetComponent<AudioSource>();
    }

    private void Damage()
    {
        boss.Damage();
        animator.SetBool("destroy", true);
        sound.Play();
    }

    public void Attack()
    {
        if (canDamage)   
        {
            Debug.Log("���� ����");
            rigid.AddForce(Vector2.up * attackforce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //�Ϲ� �� : �÷��̾�� ������, ���������� ����
        if (!canDamage)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                animator.SetBool("destroy", true);
            }

            else if (col.gameObject.CompareTag("Player"))
            {
                player.Damage();
                sound.Play();
            }
        }

        //Ư�� �� : ������ ������
        else if (col.gameObject.CompareTag("Enemy"))
        {
            Damage();
            sound.Play();
        }
    }
}
