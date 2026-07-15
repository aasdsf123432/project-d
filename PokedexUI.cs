using UnityEngine;
using UnityEngine.UI;

public class PokedexUI : MonoBehaviour
{
    [Header("슬롯이 생성될 부모 (Grid Layout Group 붙어있어야 함)")]
    [SerializeField] private Transform slotParent;

    [Header("슬롯 프리팹 (Image 컴포넌트 있는 UI 오브젝트)")]
    [SerializeField] private GameObject slotPrefab;

    [Header("미발견 크리처 대신 보여줄 이미지 (물음표 실루엣 등)")]
    [SerializeField] private Sprite lockedSprite;

    // 도감 화면 열릴 때마다 최신 상태로 다시 그림
    private void OnEnable()
    {
        RefreshPokedex();
    }

    public void RefreshPokedex()
    {
        // 기존에 생성된 슬롯 전부 삭제 (다시 그리기 위해)
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        if (PokedexManager.Instance == null)
        {
            Debug.LogWarning("PokedexManager가 씬에 없습니다!");
            return;
        }

        var allCreatures = PokedexManager.Instance.GetAllCreatures();

        foreach (var entry in allCreatures)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            Image slotImage = slot.GetComponent<Image>();

            bool discovered = PokedexManager.Instance.IsDiscovered(entry.creatureName);

            if (discovered)
            {
                slotImage.sprite = entry.sprite;
                slotImage.color = Color.white;
            }
            else
            {
                slotImage.sprite = lockedSprite;
                slotImage.color = Color.white;
            }
        }
    }
}