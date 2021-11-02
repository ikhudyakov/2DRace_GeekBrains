using TMPro;
using UnityEngine;

public class CurrencyWindow : MonoBehaviour
{
    private const string WoodKey = "Wood";
    private const string DiamondKey = "Diamond";

    public static CurrencyWindow Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _woodText;
    [SerializeField]
    private TextMeshProUGUI _diamondText;

    private int Wood
    {
        get => PlayerPrefs.GetInt(WoodKey);
        set => PlayerPrefs.SetInt(WoodKey, value);
    }

    private int Diamond
    {
        get => PlayerPrefs.GetInt(DiamondKey, 0);
        set => PlayerPrefs.SetInt(DiamondKey, value);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void AddWood(int count)
    {
        Wood += count;
        RefreshText();
    }

    public void AddDiamond(int count)
    {
        Diamond += count;
        RefreshText();
    }

    private void Start()
    {
        RefreshText();
    }

    private void RefreshText()
    {
        if(_diamondText != null)
            _diamondText.text = Diamond.ToString();
        if (_woodText != null)
            _woodText.text = Wood.ToString();
    }
}
