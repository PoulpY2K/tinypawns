using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    
    private GameManager _gm;
    
    private void Awake()
    {
        _gm = GameManager.Instance;
    }
    
    private void Update()
    {
        //damageText.text = $"Score: {_gm.ScoreManager.Score}";
    }
    
    
}
