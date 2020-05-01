using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 initial;

    private float z_coord;

    public GameObject parent;

    public GameObject newObj;

    private checkDrop myParent;

    void Start()
    {
        myParent = transform.parent.GetComponent<checkDrop>();
    }

    void OnMouseDown()
    {
        z_coord = Camera.main.WorldToScreenPoint(transform.position).z;

        initial = transform.position - GetMousePosition();

        newObj = Instantiate(gameObject, transform.position, parent.transform.rotation);
        newObj.transform.SetParent(parent.transform);
        newObj.transform.localScale = new Vector3(transform.localScale.x - 0.01f, transform.localScale.y - 0.15f, transform.localScale.z - 0.05f);
        myParent.dragged = newObj;
    }

    void OnMouseDrag()
    {
        newObj.transform.position = GetMousePosition() + initial;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mouse = Input.mousePosition;

        mouse.z = z_coord;
        return Camera.main.ScreenToWorldPoint(mouse);
    }

    void OnMouseUp()
    {
        newObj.SetActive(false);
    }
}
