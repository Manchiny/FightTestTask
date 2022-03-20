using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _windowsHolder;
    [SerializeField] private GameOverWindow _gameOverWindowPrefab;

    public void ShowGameOverWindow(float gameTime, List<EnemySearchSystem> searchSystems,
                                     Type currentSearchSystem, Action onRestart)
    {
        var window = Instantiate(_gameOverWindowPrefab, _windowsHolder);
        window.Init(gameTime, searchSystems, currentSearchSystem, onRestart);
    }
}
