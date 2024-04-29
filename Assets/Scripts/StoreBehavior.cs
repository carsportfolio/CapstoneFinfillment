using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class StoreBehavior : MonoBehaviour
{
    [System.Serializable]
    public class StoreItem
    {
        public string itemName;
        public int price;
        public string description;
        public Boolean isBoughtYet;

        //can add an image reference

        public StoreItem(string name, int price, string description, Boolean TorF)
        {
            this.itemName = name;
            this.price = price;
            this.description = description;
            this.isBoughtYet = TorF;
        }
    }

    [SerializeField] private List<StoreItem> storeItems = new List<StoreItem>(); // list of items + their info
    [SerializeField] private GameObject buttonPrefab; // prefab of the button to instantiate
    [SerializeField] private int numberOfButtons = 5; // number of buttons to create
    [SerializeField] private float buttonSpacing = 30f; // spacing between buttons
    [SerializeField] private GameObject groupGameObject; // StoreItems
    private SceneSystem sceneFunctions;
    public StoreItem currentItem; // holds the reference to the current storeitem being manipulated
    
    void Start()
    {
        sceneFunctions = FindObjectOfType<SceneSystem>(); // better than just having an empty variable

        // make your items
        StoreItem item1 = new StoreItem("Item 1", 10, "Description of Item 1", false);
        storeItems.Add(item1);

        StoreItem item2 = new StoreItem("Item 2", 20, "Description of Item 2", false);
        storeItems.Add(item2);

        StoreItem item3 = new StoreItem("Item 3", 30, "Description of Item 3", false);
        storeItems.Add(item3);

        StoreItem item4 = new StoreItem("Item 4", 10, "Description of Item 4", false);
        storeItems.Add(item4);

        StoreItem item5 = new StoreItem("Item 5", 20, "Description of Item 5", false);
        storeItems.Add(item5);

        PopulateButtons();
    }

    public void PopulateButtons()
    {
        if (groupGameObject == null)
        {
            Debug.LogError("Group GameObject reference is not set.");
            return;
        }

        foreach (Transform child in groupGameObject.transform)
        {
            Destroy(child.gameObject);
        }

        float totalHeight = numberOfButtons * (buttonPrefab.GetComponent<RectTransform>().sizeDelta.y + buttonSpacing);
        Vector3 startPos = new Vector3(0f, totalHeight / 2f, 0f);

        groupGameObject.SetActive(true); // activate temporarily to assign buttons

        for (int i = 0; i < storeItems.Count; i++)
        {
            StoreItem item = storeItems[i];
            Debug.Log(storeItems[i].itemName);

            if (item.isBoughtYet)
            {
                continue; //skips them
            }

            GameObject buttonItem = Instantiate(buttonPrefab, groupGameObject.transform);
            buttonItem.transform.localPosition = startPos - new Vector3(0f, i * (buttonPrefab.GetComponent<RectTransform>().sizeDelta.y + buttonSpacing), 0f);

            TextMeshProUGUI buttonText = buttonItem.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "S" + (i + 1);

            Button buttonComponent = buttonItem.GetComponent<Button>();

            if (buttonComponent != null) //shouldnt be
            {
                int buttonIndex = i; //current index that has been triggered
                buttonComponent.onClick.AddListener(() => OnButtonClick(buttonIndex));
            }
        }

        //groupGameObject.SetActive(false);
    }

    void OnButtonClick(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < storeItems.Count)
        {
            StoreItem selectedItem = storeItems[buttonIndex];
            if (selectedItem.isBoughtYet)
            {
                Debug.Log("Item has already been bought: " + selectedItem.itemName);
            }
            else
            {
                sceneFunctions.InfoPopUpStoreButton(selectedItem, selectedItem.itemName, selectedItem.price, selectedItem.description);
            }
        }
        else
        {
            Debug.LogError("Invalid button index.");
        }
    }
}
