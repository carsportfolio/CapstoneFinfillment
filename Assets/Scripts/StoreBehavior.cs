using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class StoreBehavior : MonoBehaviour
{
    [System.Serializable]
    public class StoreItem
    {
        public string itemName;
        public int price;
        public string description;
        public bool isBoughtYet;
        public Vector3 cordsAfterBuying;

        public StoreItem(string name, int price, string description, bool TorF, Vector3 cords)
        {
            this.itemName = name;
            this.price = price;
            this.description = description;
            this.isBoughtYet = TorF;
            this.cordsAfterBuying = cords;
        }
    }

    [SerializeField] private List<StoreItem> storeItems = new List<StoreItem>();
    public StoreItem currentItem; // holds the reference to the current storeitem being manipulated
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private int numberOfButtons = 5;
    [SerializeField] private float buttonSpacing = 30f;
    [SerializeField] private GameObject groupGameObject;
    private SceneSystem sceneFunctions;
    private string filePath;

    [System.Serializable]
    private class SerializableList<T>
    {
        public List<T> list;

        public SerializableList(List<T> list)
        {
            this.list = list;
        }
    }

    void OnApplicationQuit()
    {
        SaveData();
        //ClearFile(); 
    }

    void Start()
    {
        filePath = Application.persistentDataPath + "/StoreData.json";

        if (!File.Exists(filePath))
        {
            Debug.Log("StoreData.json not found. Creating a new file.");

            // make your items
            StoreItem item1 = new StoreItem("Item 1", 10, "Description of Item 1", false, new Vector3(0,0,0));
            storeItems.Add(item1);

            StoreItem item2 = new StoreItem("Item 2", 20, "Description of Item 2", false, new Vector3(0,0,0));
            storeItems.Add(item2);

            StoreItem item3 = new StoreItem("Item 3", 30, "Description of Item 3", false, new Vector3(0,0,0));
            storeItems.Add(item3);

            StoreItem item4 = new StoreItem("Item 4", 10, "Description of Item 4", false, new Vector3(0,0,0));
            storeItems.Add(item4);

            StoreItem item5 = new StoreItem("Item 5", 20, "Description of Item 5", false, new Vector3(0,0,0));
            storeItems.Add(item5);

            //PlayerPrefs.SetInt("StoreInitialized", 1); // set the flag indicating initialization
            PopulateButtons();
            SaveData();
        }
        else
        {
            Debug.Log("StoreData.json found. Loading data.");
            LoadData();
            PopulateButtons();
        }

        sceneFunctions = FindObjectOfType<SceneSystem>(); // better than just having an empty variable
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

        groupGameObject.SetActive(true);

        for (int i = 0; i < storeItems.Count; i++)
        {
            StoreItem item = storeItems[i];

            if (item.isBoughtYet)
            {
                continue;
            }

            GameObject buttonItem = Instantiate(buttonPrefab, groupGameObject.transform);
            buttonItem.transform.localPosition = startPos - new Vector3(0f, i * (buttonPrefab.GetComponent<RectTransform>().sizeDelta.y + buttonSpacing), 0f);

            TextMeshProUGUI buttonText = buttonItem.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "S" + (i + 1);

            Button buttonComponent = buttonItem.GetComponent<Button>();

            if (buttonComponent != null)
            {
                int buttonIndex = i;
                buttonComponent.onClick.AddListener(() => OnButtonClick(buttonIndex));
            }
        }
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

    public void SaveData()
    {
        string jsonString = JsonUtility.ToJson(new SerializableList<StoreItem>(storeItems));
        File.WriteAllText(filePath, jsonString);
        Debug.Log("Data saved to " + filePath);
    }

    public void LoadData()
    {
        string jsonString = File.ReadAllText(filePath);
        SerializableList<StoreItem> data = JsonUtility.FromJson<SerializableList<StoreItem>>(jsonString);
        storeItems = data.list;
        Debug.Log("Data loaded from " + filePath);
    }

    void ClearFile()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("StoreData.json cleared.");
        }
    }
}
