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

    private void Update()
    {
        if (curhealth == 0)
        {
            GameEnd();
        }
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
    private void GameEnd()
    {
        if (gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }

        else if (gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.Invoke("GameClear", 4f);
        }
    }
}
