using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _countMoneyText;

    [SerializeField]
    private TMP_Text _countHealthText;

    [SerializeField]
    private TMP_Text _countPowerText;

    [SerializeField]
    private TMP_Text _countCrimeText;

    [SerializeField]
    private TMP_Text _countPowerEnemyText;

    [SerializeField]
    private Button _addMoneyButton;

    [SerializeField]
    private Button _minusMoneyButton;

    [SerializeField]
    private Button _addHealthButton;

    [SerializeField]
    private Button _minusHealthButton;

    [SerializeField]
    private Button _addPowerButton;

    [SerializeField]
    private Button _minusPowerButton;
    
    [SerializeField]
    private Button _addCrimeButton;

    [SerializeField]
    private Button _minusCrimeButton;

    [SerializeField]
    private Button _fightButton;

    [SerializeField]
    private Button _passButton;

    private Enemy _enemy;

    private Money _playerMoney;
    private Health _playerHealth;
    private Power _playerPower;
    private Crime _playerCrime;

    private UiObserver _uiObserver;

    private void Start()
    {
        SubscribeWindow();

        _playerHealth = new Health();
        _playerMoney = new Money();
        _playerPower = new Power();
        _playerCrime = new Crime();

        _enemy = new Enemy("Bird");
        _enemy.OnUpdate += OnEnemyUpdate;
        SubscribeEnemy(_enemy);

        _uiObserver = new UiObserver(_countHealthText, _countPowerText, _countMoneyText, _countCrimeText, _passButton);
        SubscribeEnemy(_uiObserver);
    }

    private void OnEnemyUpdate(Enemy obj)
    {
        _countPowerEnemyText.text = $"Enemy Power : {obj.Power}";
    }

    private void SubscribeEnemy(IEnemy enemy)
    {
        _playerMoney.Subscribe(enemy);
        _playerHealth.Subscribe(enemy);
        _playerPower.Subscribe(enemy);
        _playerCrime.Subscribe(enemy);
    }
    
    private void UnsubscribeEnemy(IEnemy enemy)
    {
        _playerMoney.Unsubscribe(enemy);
        _playerHealth.Unsubscribe(enemy);
        _playerPower.Unsubscribe(enemy);
        _playerCrime.Unsubscribe(enemy);
    }

    private void SubscribeWindow()
    {
        _addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _addPowerButton.onClick.AddListener(() => ChangePower(true));
        _minusPowerButton.onClick.AddListener(() => ChangePower(false));
        
        _addCrimeButton.onClick.AddListener(() => ChangeCrime(true));
        _minusCrimeButton.onClick.AddListener(() => ChangeCrime(false));

        _fightButton.onClick.AddListener(Fight);

        _passButton.onClick.AddListener(Pass);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();

        _passButton.onClick.RemoveAllListeners();
        UnsubscribeEnemy(_enemy);
    }

    private void Fight()
    {
        var result = _playerPower.Power > _enemy.Power ? "You Win" : "You lose";
        Debug.Log($"Fight comlete result = {result}");
    }

    private void Pass()
    {
        Debug.Log($"You pass peacefully");
    }

    private void ChangePower(bool isAddCount)
    {
        _playerPower.Power += isAddCount ? 1 : -1;
    }

    private void ChangeHealth(bool isAddCount)
    {
        _playerHealth.Health += isAddCount ? 1 : -1;
    }

    private void ChangeMoney(bool isAddCount)
    {
        _playerMoney.Money += isAddCount ? 1 : -1;
    }

    private void ChangeCrime(bool isAddCount)
    {
        _playerCrime.Crime += isAddCount ? 1 : -1;

    }
}
