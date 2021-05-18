using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hp : MonoBehaviour
{

    public int hp;
    public Slider slider;

    public Text scoreValue;

    public int PlayerScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0){
            Invoke(nameof(Killed), .5f);
        }

        slider.value = hp;
    }

    public void TakeDmg(int dmg){
        hp -= dmg;
    }

    private void Killed(){
        SceneManager.LoadScene(2);
    }

    public void gainScore(int score){
        PlayerScore += score;
        scoreValue.text = PlayerScore.ToString();
    }
}
