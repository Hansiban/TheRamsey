using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int curhealth;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emtyHeart;

    private void Start()
    {
        //���� �ʱ�ȭ
        //curhealth = 3; //����ü��
        //numOfHearts = 3; //�ִ�ü��
    }
    private void Update()
    {
        //����ó�� - ����ü��> �ִ�ü���Ͻ� ����
        if (curhealth > numOfHearts)
        {
            curhealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < curhealth) //i�� ����ü�º��� ���� ���
            {
                hearts[i].sprite = fullHeart;//�� ��Ʈ
            }
            else
            {
                hearts[i].sprite = emtyHeart;//����� ��Ʈ
            }
            if (i < numOfHearts) //i�� �ִ�ü�º��� ���� ���
            {
                hearts[i].enabled = true; //Ȱ��ȭ
            }
            else
            {
                hearts[i].enabled = false; //��Ȱ��ȭ
            }
        }
    }
}
