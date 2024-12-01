using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int _healthpoints;
    [SerializeField] private TextMeshProUGUI _healthUI;

    private void Awake()
    {
        _healthpoints = 50;
        _healthUI.text = "HP : " + _healthpoints.ToString();
    }

    public bool TakeHit()
    {
        _healthpoints -= 1;
        bool isDead = _healthpoints <= 0;
        _healthUI.text = "HP : " + _healthpoints.ToString();

        if(isDead) Death();
        return isDead;
    }

    private void Death()
    {
        Destroy(transform.parent.gameObject);
    }
}
