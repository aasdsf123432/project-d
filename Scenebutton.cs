using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [Tooltip("클릭했을 때 이동할 씬 이름 (Build Settings에 등록된 이름과 정확히 일치해야 함)")]
    [SerializeField] private string targetSceneName;

    // 스프라이트 클릭 시 호출됨 (Collider2D 필요)
    private void OnMouseDown()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogWarning("이동할 씬 이름이 비어있습니다!");
            return;
        }

        SceneManager.LoadScene(targetSceneName);
    }
}