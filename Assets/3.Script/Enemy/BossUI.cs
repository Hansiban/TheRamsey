using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public GameObject warning;
    public GameObject health;

    private void Update()
    {
        ActHealth();
    }
    //warning�� ������� healthȰ��ȭ
    public void ActHealth()
    {
        if (!warning)
        {
            health.SetActive(true);
        }
    }
}
