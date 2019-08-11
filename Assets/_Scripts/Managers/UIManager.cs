using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject closeBtn;

    private bool inventoryOpen;
    // Start is called before the first frame update
    void Start()
    {
        inventoryOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp("b")) {
            if (inventoryOpen)
            {
                Inventory.gameObject.SetActive(false);
                inventoryOpen = false;
            }
            else {
                Inventory.gameObject.SetActive(true);
                inventoryOpen = true;
            }
            
        }


    }



    void RemoveInventoryItem() {





    }
}
