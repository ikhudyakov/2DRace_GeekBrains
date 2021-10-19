using UnityEngine;

public class BuyGoldView : MonoBehaviour
{
    private PlayerData _model;

    public void Init(PlayerData model)
    {
        _model = model;
    }

    public void GetGold()
    {
        _model.Gold += 1000;
    }
}
