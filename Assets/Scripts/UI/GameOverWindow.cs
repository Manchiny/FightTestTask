using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameTimeText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Dropdown _searchSystemsDropdawn;

    private List<EnemySearchSystem> _searchSystems;
    public void Init(float gameTime, List<EnemySearchSystem> searchSystems, 
                                     Type currentSearchSystem, Action onRestartButtonClick)
    {
        _searchSystems = searchSystems;
        _gameTimeText.text = $"Game time: {gameTime.ToString()} seconds";

        List<Dropdown.OptionData> dropDawnOptions = new List<Dropdown.OptionData>();
        int currentSearchSystemId = 0;
        for (int i = 0; i < searchSystems.Count; i++)
        {
            var system = searchSystems[i];        
            var data = new Dropdown.OptionData();
            data.text = system.Name;
            dropDawnOptions.Add(data);

            if (system.GetType() == currentSearchSystem)
                currentSearchSystemId = i;
        }
        _searchSystemsDropdawn.ClearOptions();
        _searchSystemsDropdawn.AddOptions(dropDawnOptions);
        _searchSystemsDropdawn.value = currentSearchSystemId;
        _searchSystemsDropdawn.RefreshShownValue();
        _searchSystemsDropdawn.onValueChanged.AddListener(value => Game.Instance.SetEnemySearchSystem(_searchSystems[value].GetType()));

        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(onRestartButtonClick.Invoke);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        Destroy(gameObject);
    }
}
