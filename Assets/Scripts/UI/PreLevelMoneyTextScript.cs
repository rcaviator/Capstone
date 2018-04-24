using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelMoneyTextScript : MonoBehaviour
{
    //text reference
    Text moneyText;

	// Use this for initialization
	void Awake()
    {
        UIManager.Instance.PreLevelMoneyText = this;
        moneyText = GetComponent<Text>();
        moneyText.text = GameManager.Instance.Score.ToString();
	}

    private void OnEnable()
    {
        moneyText.text = GameManager.Instance.Score.ToString();
    }


    public void SetMoneyText()
    {
        moneyText.text = GameManager.Instance.Score.ToString();
    }
}
