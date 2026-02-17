using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;
    public int currentLevel = 1;
    
    private bool _endGame;

    void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }
    
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (!_endGame && player != null)
        {
            ShowEndGame();
        }
    }

    void ShowEndGame()
    {
        _endGame = true;
        endGameObjects.ForEach(i => i.SetActive(true));

        foreach (var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            SaveManager.Instance.SaveLastLevel(currentLevel);
        }
    }
}
