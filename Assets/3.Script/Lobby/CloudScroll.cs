using UnityEngine;

public class CloudScroll : MonoBehaviour
{
    [SerializeField] int speed;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
        if (rectTransform.anchoredPosition.x <= -254f)
        {
            Vector3 newPosition = new Vector3(256f, rectTransform.anchoredPosition.y, 0);
            rectTransform.anchoredPosition = newPosition;
        }
    }
}
