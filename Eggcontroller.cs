using UnityEngine;

public class EggController : MonoBehaviour
{
    [System.Serializable]
    public struct HatchableCreature
    {
        public GameObject prefab;
        public string creatureName; // PokedexManager에 등록된 이름이랑 반드시 똑같이 적기
    }

    [Header("부화 조건")]
    [SerializeField] private int requiredClicks = 5; // 몇 번 클릭해야 부화하는지

    [Header("부화 결과")]
    [SerializeField] private HatchableCreature[] hatchableCreatures; // 여기서 나올 크리처 후보들 (랜덤으로 하나 선택됨)
    [SerializeField] private GameObject hatchEffect; // 부화할 때 터지는 이펙트 (파티클 등, 없으면 비워둬도 됨)

    [Header("사운드 (선택)")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hatchSound;

    private int currentClicks = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 알을 클릭했을 때 호출됨 (Collider2D 필요)
    private void OnMouseDown()
    {
        currentClicks++;

        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        Debug.Log($"알 클릭! ({currentClicks}/{requiredClicks})");

        if (currentClicks >= requiredClicks)
        {
            Hatch();
        }
    }

    private void Hatch()
    {
        // 이펙트 재생
        if (hatchEffect != null)
        {
            Instantiate(hatchEffect, transform.position, Quaternion.identity);
        }

        // 부화 사운드
        if (hatchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hatchSound);
        }

        // 크리처 후보 중 랜덤으로 하나 스폰
        if (hatchableCreatures != null && hatchableCreatures.Length > 0)
        {
            int randomIndex = Random.Range(0, hatchableCreatures.Length);
            HatchableCreature chosen = hatchableCreatures[randomIndex];

            Instantiate(chosen.prefab, transform.position, Quaternion.identity);

            // 도감에 발견 등록
            if (PokedexManager.Instance != null)
            {
                PokedexManager.Instance.Discover(chosen.creatureName);
            }
            else
            {
                Debug.LogWarning("PokedexManager가 씬에 없습니다!");
            }
        }
        else
        {
            Debug.LogWarning("부화할 크리처가 등록되어 있지 않습니다!");
        }

        // 알 오브젝트 제거
        Destroy(gameObject);
    }
}