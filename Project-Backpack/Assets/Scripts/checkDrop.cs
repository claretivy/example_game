using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class checkDrop : MonoBehaviour
{
    public GameObject dragged = null;
    public GameObject text;

    public int Score;

    public List<GameObject> houses = new List<GameObject>();

    void Start()
    {
        Score = 100;
        text.GetComponent<Text>().text = "Coin: "+Score.ToString();
        StartCoroutine(ScoreUp());
    }

    void Update()
    {
        text.GetComponent<Text>().text = "Coin: " + Score.ToString();
    }

    //calculates the score every 2 seconds and updates it according to income
    IEnumerator ScoreUp()
    {
        while (true)
        {
            if (houses.Count != 0)
            {
                foreach (GameObject house in houses)
                {
                    Upgrade sc = house.GetComponent<Upgrade>();
                    Score += sc.earn;
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
