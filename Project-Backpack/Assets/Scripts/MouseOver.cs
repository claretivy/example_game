using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{

    private bool owned;
    private bool occupied;

    public GameObject txt;
    public GameObject up_txt;
    public GameObject re_text;

    public GameObject ex_point;

    public bool over;

    public GameObject menu;

    private checkDrop my_script;

    private GameObject dragged;


    void OnMouseOver()
    {
        over = true;
    }

    void OnMouseExit()
    {
        over = false;
    }

    void Start()
    {
        my_script = menu.GetComponent<checkDrop>();
        occupied = false;
        owned = false;
        up_txt.SetActive(false);
    }

    //function that checks the cost and occupied or owned , then builds houses , adds these objects to checkDrop scripts array and destroys the current dragged object
    void BuildHouse()
    {
        if (over && !occupied && owned && my_script.dragged != null)
        {
            if (my_script.dragged.name == "House Big(Clone)")
            {
                if (my_script.Score >= 100)
                {
                    occupied = true;
                    dragged = Instantiate(my_script.dragged, new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z - 1f), transform.parent.transform.rotation);
                    dragged.transform.SetParent(transform);
                    dragged.transform.localScale = new Vector3(0.3f, 0.1f, 0.3f);
                    dragged.name = "House Big";
                    Upgrade temp = dragged.AddComponent<Upgrade>();
                    //set up the script variables
                    temp.UpgradeCost = 150;
                    temp.earn = 25;
                    temp.menu = menu;
                    temp.level = 1;
                    my_script.Score -= 100;
                    temp.ex = Instantiate(ex_point, new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z - 1f), transform.parent.transform.rotation);
                    temp.ex.transform.SetParent(dragged.transform);
                    up_txt.SetActive(true);
                }
                else
                {
                    re_text.GetComponent<Text>().text = "Not enough money!";
                    StartCoroutine(ShowAndHide(re_text, 1.0f)); // 1 second
                    Destroy(my_script.dragged);
                }
            }
            else if (my_script.dragged.name == "House Medium(Clone)")
            {
                if (my_script.Score >= 50)
                {
                    occupied = true;
                    dragged = Instantiate(my_script.dragged, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 1f), transform.parent.transform.rotation);
                    dragged.transform.SetParent(transform);
                    dragged.transform.localScale = new Vector3(0.3f, 0.15f, 0.3f);
                    dragged.name = "House Medium";
                    Upgrade temp = dragged.AddComponent<Upgrade>();
                    //set up the script variables
                    temp.UpgradeCost = 100;
                    temp.earn = 15;
                    temp.menu = menu;
                    temp.level = 1;
                    my_script.Score -= 50;
                    temp.ex = Instantiate(ex_point, new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z - 1f), transform.parent.transform.rotation);
                    temp.ex.transform.SetParent(dragged.transform);
                    up_txt.SetActive(true);
                }
                else
                {
                    re_text.GetComponent<Text>().text = "Not enough money!";
                    StartCoroutine(ShowAndHide(re_text, 1.0f)); // 1 second
                    Destroy(my_script.dragged);
                }
            }
            else if (my_script.dragged.name == "Bunker(Clone)")
            {
                if (my_script.Score >= 25) {
                    occupied = true;
                    dragged = Instantiate(my_script.dragged, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 1f), transform.parent.transform.rotation);
                    dragged.transform.SetParent(transform);
                    dragged.transform.localScale = new Vector3(0.3f, 0.15f, 0.3f);
                    dragged.name = "Bunker";
                    Upgrade temp = dragged.AddComponent<Upgrade>();
                    //set up the script variables
                    temp.UpgradeCost = 50;
                    temp.earn = 5;
                    temp.level = 1;
                    my_script.Score -= 25;
                    temp.menu = menu;
                    temp.ex = Instantiate(ex_point, new Vector3(transform.position.x+1f, transform.position.y + 1f, transform.position.z - 1f), transform.parent.transform.rotation);
                    temp.ex.transform.SetParent(dragged.transform);
                    up_txt.SetActive(true);
                }
                else
                {
                    re_text.GetComponent<Text>().text = "Not enough money!";
                    StartCoroutine(ShowAndHide(re_text, 1.0f)); // 1 second
                    Destroy(my_script.dragged);
                }
            }
            Destroy(dragged.GetComponent<DragObject>());
            dragged.SetActive(true);
            my_script.houses.Add(dragged);
            Destroy(my_script.dragged);
        }
        else if (over && occupied && my_script.dragged !=null)
        {
            re_text.GetComponent<Text>().text = "This place is occupied!";
            StartCoroutine(ShowAndHide(re_text, 1.0f)); // 1 second
            Destroy(my_script.dragged);
        }
        else
        {
            Destroy(my_script.dragged);
        }
    }

    IEnumerator ShowAndHide(GameObject go, float delay)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }

    //function to unlock the tile if its not owned and money is enough
    void BuyTile()
    {
        if (over && !occupied && !owned)
        {
            if (my_script.Score >= 25)
            {
                my_script.Score -= 25;
                owned = true;
                txt.GetComponent<TextMesh>().text = "Build here!";
            }
            else
            {
                re_text.GetComponent<Text>().text = "Not enough money!";
                StartCoroutine(ShowAndHide(re_text, 1.0f)); // 1 second
            }
        }
    }

    void Update()
    {
        //to see all the objects which is behind the cursor
        Ray ray = Camera.main.ViewportPointToRay(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
            if (hit.transform.name == transform.name)
            {
                over = true;
            }
        if (!owned)
        {
            txt.GetComponent<TextMesh>().text = "Buy Here!";
        }
        else if(owned && occupied)
        {
            Upgrade temp = dragged.GetComponent<Upgrade>();
            if (temp.level == 4)
            {
                txt.GetComponent<TextMesh>().text = "Level: MAX" + "\n" + "Income: " + temp.earn.ToString();
            }
            else
            {
                txt.GetComponent<TextMesh>().text = "Level: " + temp.level.ToString() + "\n" + "Income: " + temp.earn.ToString();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            BuildHouse();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            BuyTile();
        }
    }

}
