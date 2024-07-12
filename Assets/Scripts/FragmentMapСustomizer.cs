using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FragmentMapСustomizer : MonoBehaviour
{
    [SerializeField, Header("Игрок")] private int _health = 10;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _textProgress;
    [SerializeField] private TextMeshProUGUI _finalTextProgress;

    private int _score = 0;
    private int _currentIndex = 0;
    private float _step = -9f;
    private float _lastBlock = -81f;
    private int _progress = 0;


    [SerializeField] private GameObject _panelRestart;
    [SerializeField] private List<GameObject> _pices = new List<GameObject>();
    

    [SerializeField, Range(1, 100)] private int _percentageDifficulty = 15;

    private void Start()
    {
        UpdateHealthUI();
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if(_textProgress != null)
            _textProgress.text = "Progress :" + _score.ToString();
    }

    private void UpdateHealthUI()
    {
        if(_healthSlider != null)
            _healthSlider.value = (float)_health;

    }

    public void TakeDamage(int amount)
    {
        
        _health -= amount;


        if (_health <= 0)
        {
            _health = 0;
            
            PanelRestart();
        }

        UpdateHealthUI();
    }

    private void PanelRestart()
    {
        Time.timeScale = 0;
        _panelRestart.SetActive(true);
        _finalTextProgress.text = "Итого ты набрал " + _score.ToString() + " очков";

    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneName);
    }
    public void UpdateLevel()
    {

        _lastBlock += _step;
        Vector3 position = _pices[_currentIndex].transform.position;
        position.x = _lastBlock;
        _pices[_currentIndex].transform.position = position;

        _currentIndex++;
        _progress++;
        
        _score++;
        UpdateScoreText();

        if (_currentIndex >= _pices.Count)
        {
            _currentIndex = 0;

        }

    } 

}
