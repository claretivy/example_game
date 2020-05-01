using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public GameObject menu;
    public int UpgradeCost;
    public int earn;

    public int level;

    public GameObject ex;

    private checkDrop sc;
    private MouseOver sc2;
    // Start is called before the first frame update
    void Start()
    {
        sc = menu.GetComponent<checkDrop>();
        sc2 = transform.parent.GetComponent<MouseOver>();
    }

    // Update is called once per frame
    void Update()
    {
        check_Upgrade();
    }

    //checks if money is enough for upgrading and upgrades if clicked by the user
    void check_Upgrade()
    {
        sc2.up_txt.GetComponent<TextMesh>().text = UpgradeCost.ToString();
        if (sc.Score >= UpgradeCost)
        {
            ex.SetActive(true);
            sc2.up_txt.GetComponent<TextMesh>().color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                if (sc2.over && level<=3)
                {
                    sc.Score -= UpgradeCost;
                    UpgradeCost += 50;
                    level += 1;
                    if(transform.name == "House Big")
                    {
                        earn += 10;
                    }
                    else if (transform.name == "House Medium")
                    {
                        earn += 5;
                    }
                    else if (transform.name == "bunker")
                    {
                        earn += 2;
                    }
                }
            }
        }
        else if (sc.Score < UpgradeCost)
        {
            sc2.up_txt.GetComponent<TextMesh>().color = Color.red;
            ex.SetActive(false);
        }
    }
}
