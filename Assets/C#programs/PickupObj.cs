using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObj : MonoBehaviour
{
    [SerializeField] private GameObject CollectUI;
    [SerializeField] private GameObject ThisItem;
    //[SerializeField] Items.Type item;
    [SerializeField] Items item;

    private bool Action;
    public bool isCollect;

    // Start is called before the first frame update
    void Start()
    {
        isCollect = false;
        Action = false;
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            CollectUI.SetActive(true);
            Action = true;
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        CollectUI.SetActive(false);
        Action = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Action == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //ItemSetActive(false)
                ItemBox.instance.SetItem(item);
                ThisItem.SetActive(false);
                isCollect = true;
            }

        }
        //CollectÇ≥ÇÍÇΩÇÁUIÇè¡Ç∑
        if (isCollect)
        {
            CollectUI.SetActive(false);
        }
    }
}