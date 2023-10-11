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
    //warning이 사라지면 health활성화
    public void ActHealth()
    {
        if (!warning)
        {
            health.SetActive(true);
        }
    }
}
