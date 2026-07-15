using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    [Tooltip("체크하면 다음 방향, 해제하면 이전 방향")]
    [SerializeField] private bool isNext = true;

    // 화살표 클릭 시 호출됨 (Collider2D 필요)
    private void OnMouseDown()
    {
        if (CameraPanController.Instance == null)
        {
            Debug.LogWarning("CameraPanController가 씬에 없습니다!");
            return;
        }

        if (isNext)
        {
            CameraPanController.Instance.MoveNext();
        }
        else
        {
            CameraPanController.Instance.MovePrevious();
        }
    }
}
//isNext는 해제하면 이전방 <<
//해제 안하면 다음방       >>