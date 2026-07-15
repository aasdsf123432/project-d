using UnityEngine;
using System.Collections.Generic;

public class PokedexManager : MonoBehaviour
{
    public static PokedexManager Instance;

    [System.Serializable]
    public struct PokedexEntry
    {
        public string creatureName;   // 크리처 이름 (EggController랑 이름 반드시 일치시켜야 함)
        public Sprite sprite;         // 발견했을 때 보여줄 이미지
    }

    [Header("도감에 등록될 크리처 전체 목록")]
    [SerializeField] private List<PokedexEntry> allCreatures = new List<PokedexEntry>();

    // 발견한 크리처 이름들을 저장해두는 목록
    private HashSet<string> discoveredCreatures = new HashSet<string>();

    private const string SavePrefix = "Discovered_";

    private void Awake()
    {
        // 싱글톤 세팅: 이미 있으면 새로 생긴 걸 파괴, 없으면 이걸 유지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadDiscoveredData();
    }

    // 크리처 발견 처리 (부화했을 때 EggController에서 이 함수 호출)
    public void Discover(string creatureName)
    {
        if (discoveredCreatures.Contains(creatureName))
        {
            return; // 이미 발견한 크리처면 아무것도 안 함
        }

        discoveredCreatures.Add(creatureName);
        PlayerPrefs.SetInt(SavePrefix + creatureName, 1);
        PlayerPrefs.Save();

        Debug.Log($"도감 등록: {creatureName}");
    }

    public bool IsDiscovered(string creatureName)
    {
        return discoveredCreatures.Contains(creatureName);
    }

    public List<PokedexEntry> GetAllCreatures()
    {
        return allCreatures;
    }

    // 게임 시작할 때 저장된 발견 기록 불러오기
    private void LoadDiscoveredData()
    {
        foreach (var entry in allCreatures)
        {
            if (PlayerPrefs.GetInt(SavePrefix + entry.creatureName, 0) == 1)
            {
                discoveredCreatures.Add(entry.creatureName);
            }
        }
    }
}