using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadHighScore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI highScoreDisplay;
    void Start()
    {
        highScoreDisplay.SetText(PlayerPrefs.GetInt("Highscore").ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
