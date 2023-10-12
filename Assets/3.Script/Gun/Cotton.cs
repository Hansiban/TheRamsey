using UnityEngine;

public class Cotton : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float destroy = 5;
    [SerializeField] private GameObject trail;
    private GameObject player;
    private PlayerControll playerControll;
    private Animator bulletanimation;
    private bool isMove = true;
    private float h;

    private void Awake()
    {
        bulletanimation = GetComponentInChildren<Animator>();
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
    }
    private void Update()
    {
        //�ʹ� �ָ����� ����
        if (this.transform.position.x > destroy || this.transform.position.x < -destroy)
        {
            Destroy(this.gameObject);
        }

        //�̵�
        if (isMove)
        {
            CottonMove();
        }
    }
    private void CottonMove()
    {
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            isMove = false;
            bulletanimation.SetTrigger("Bloom");
            trail.SetActive(false);
            bulletanimation.SetBool("Keep", true);
        }

        if (other.tag == "Player")
        {
            bulletanimation.SetBool("Remove", true);
            playerControll.Jump(1.5f);
        }
    }
}
