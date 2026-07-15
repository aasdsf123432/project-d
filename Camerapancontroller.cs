using UnityEngine;
using System.Collections;

public class CameraPanController : MonoBehaviour
{
    public static CameraPanController Instance;

    [Tooltip("이동할 위치들. 순서대로 배치 (0번 = 알 방, 1번 = 도감 방 등)")]
    [SerializeField] private Transform[] viewPoints;

    [Tooltip("카메라 이동 속도")]
    [SerializeField] private float moveSpeed = 5f;

    private int currentIndex = 0;
    private bool isMoving = false;

    private void Awake()
    {
        Instance = this;
    }

    public void MoveNext()
    {
        if (isMoving) return;

        if (currentIndex < viewPoints.Length - 1)
        {
            currentIndex++;
            StartCoroutine(MoveToPoint(viewPoints[currentIndex].position));
        }
    }

    public void MovePrevious()
    {
        if (isMoving) return;

        if (currentIndex > 0)
        {
            currentIndex--;
            StartCoroutine(MoveToPoint(viewPoints[currentIndex].position));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPosition)
    {
        isMoving = true;

        // 카메라의 Z값은 유지하고 X, Y만 이동
        Vector3 target = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}