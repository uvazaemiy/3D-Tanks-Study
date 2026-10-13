using UnityEngine;

public class ObjectCyclicMove : MonoBehaviour
{
    public Transform targetPoint;
    public float speed = 2f;
    public bool useLocalPosition = false;

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        startPosition = useLocalPosition ? transform.localPosition : transform.position;

        if (targetPoint != null)
        {
            endPosition = useLocalPosition ? targetPoint.localPosition : targetPoint.position;
        }
        else
        {
            Debug.LogWarning("Не указана целевая точка (Target Point)! Объект будет двигаться вверх по умолчанию.");
            endPosition = startPosition + Vector3.up * 3f;
        }
    }

    void Update()
    {
        float progress = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, progress);

        if (useLocalPosition)
        {
            transform.localPosition = currentPos;
        }
        else
        {
            transform.position = currentPos;
        }
    }
}