using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    [Header("UI介面")]
    public GameObject hackMark;
    [Header("玩家操作中")]
    public bool isControlling;

    void Update()
    {
        Transfer();
    }

    void Transfer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Robot")
            {
                hackMark.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    isControlling = false;
                    hackMark.SetActive(false);
                    hit.transform.parent.gameObject.GetComponent<Controllable>().isControlling = true;
                }
            }
            else
            {
                hackMark.SetActive(false);
            }
        }
        else
        {
            hackMark.SetActive(false);
        }
    }
}
