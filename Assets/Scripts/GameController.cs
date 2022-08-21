using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private UIManager _uiManager;
    

    private Vector3 _target;

    private ItemInfo carryingItem;

    private int _counter = 0;

    private int _archerCounter, _meleeCounter;
    public int ArcherCost { get; private set; }
    public int MeleeCost { get; private set; }


    public Dictionary<int, Slot> slotDictionary;

    public List<Slot> slots;

    [SerializeField] private CharacterPropertiesSO[] character;
    

    private void Awake()
    {
        instance = this;
        Utils.InitResources();
        
    }

    private void Start()
    {
        // getComponent uiManager
        _uiManager = FindObjectOfType<UIManager>();
        
        
        var settings = SettingsController.Instance;
        settings.LoadSettings();

        ArcherCost = settings._userSettingsSO.UserSettings.archerCounter;
       
        MeleeCost = settings._userSettingsSO.UserSettings.meleeCounter;

        _uiManager.Gold = settings._userSettingsSO.UserSettings.totalGold;



    }

    //handle user input
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRayCast(false);
        }

        if (Input.GetMouseButton(0) && carryingItem)
        {
            OnItemSelected();
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Drop item
            SendRayCast(true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlaceRandomArcher();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlaceRandomMelee();
        }
    }

    void SendRayCast(bool isMouseUp)
    {

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit);

        //we hit something
        if (hit.collider != null && hit.collider.CompareTag("Slot"))
        {
            //we are grabbing the item in a full slot
            var slot = hit.transform.GetComponent<Slot>();

            if (!isMouseUp && slot.state == SlotState.Full && carryingItem == null)
            {
                if (hit.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("2D"))
                {
                    var itemGO = (GameObject) Instantiate(Resources.Load("Prefabs/ItemDummy"));
                    itemGO.transform.position = slot.transform.position;
                    itemGO.transform.localScale = Vector3.one * 2;

                    carryingItem = itemGO.GetComponent<ItemInfo>();
                    carryingItem.InitDummy(slot.id, slot.currentItem.id);

                    slot.ItemGrabbed();
                }
                else if (hit.collider.gameObject.transform.GetChild(0).gameObject.CompareTag("3D"))
                {
                    var itemGO = (GameObject) Instantiate(Resources.Load("Prefabs/ItemDummy3D"));
                    itemGO.transform.position = slot.transform.position;
                    itemGO.transform.localScale = Vector3.one * 2;
                    
                    carryingItem = itemGO.GetComponent<ItemInfo>();

                    var modelObj = carryingItem.InitDummy3D(slot.currentItem.id, slot.id, slot.currentItem.type);
                    var model = Instantiate(modelObj, Vector3.zero, transform.rotation, itemGO.transform);
                    model.transform.localPosition = Vector3.zero;
                    model.transform.localRotation = Quaternion.identity;
                    model.transform.localScale = Vector3.one / 2;
                    
                    slot.ItemGrabbed();
                }
              
            }
            //we are dropping an item to empty slot
            else if (slot.state == SlotState.Empty && carryingItem != null)
            {
                if (carryingItem.visualRenderer!=null)
                {
                    slot.CreateItem(carryingItem.itemId);
                    Destroy(carryingItem.gameObject);
                }
                else if (carryingItem.meshFilter != null)
                {
                    slot.CreateItem3D(carryingItem.itemId);
                    Destroy(carryingItem.gameObject);
                }
               
            }

            //we are dropping to full
            else if (slot.state == SlotState.Full && carryingItem != null)
            {
                //check item in the slot
                if (slot.currentItem.id == carryingItem.itemId && carryingItem.itemId + 2 < 6)
                {
                    print("merged");
                    OnItemMergedWithTarget(slot.id);
                    if (slot.currentItem.id == 0 || slot.currentItem.id == 2) _archerCounter--;
                    else _meleeCounter--;
                }
                else
                {
                    OnItemCarryFail();
                }
            }

        }
        else
        {
            if (!carryingItem)
            {
                return;
            }

            OnItemCarryFail();
        }
    }


    void OnItemSelected()
    {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _target.Normalize();

        if (carryingItem.gameObject.CompareTag("2D"))
        {
            carryingItem.transform.position = GetPos2D(Input.mousePosition, 2);
        }
        else if (carryingItem.gameObject.CompareTag("3D"))
        {
            carryingItem.transform.position = GetPos3D(Input.mousePosition, 2);
        }

    }
    private Vector3 GetPos3D(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, z, 0));
        xy.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }

    public Vector3 GetPos2D(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, z, 0));
        xy.Raycast(ray, out float distance);
        return ray.GetPoint(distance);
    }

    void OnItemMergedWithTarget(int targetSlotId)
    {
        var slot = GetSlotById(targetSlotId);
        Destroy(slot.currentItem.gameObject);

        if (carryingItem.gameObject.CompareTag("2D"))
        {
            slot.CreateItem(carryingItem.itemId + 2);
            Destroy(carryingItem.gameObject);
            _counter--;
        }
        else if (carryingItem.gameObject.CompareTag("3D"))
        {
            slot.CreateItem3D(carryingItem.itemId + 2);
            Destroy(carryingItem.gameObject);
            _counter--;
        }
        
    }

    void OnItemCarryFail()
    {
        if (carryingItem.gameObject.CompareTag("2D"))
        {
            var slot = GetSlotById(carryingItem.slotId);
            slot.CreateItem(carryingItem.itemId);
            Destroy(carryingItem.gameObject);
        }
        else if (carryingItem.gameObject.CompareTag("3D"))
        {
            var slot = GetSlotById(carryingItem.slotId);
            slot.CreateItem3D(carryingItem.itemId);
            Destroy(carryingItem.gameObject);
        }
        
    }

    public void PlaceRandomArcher()
    {
        if (AllSlotsOccupied())
        {
            Debug.Log("No empty slot available!");
            return;
        }
        
        _archerCounter++;
       
        _counter = _archerCounter + _meleeCounter;

        if (_uiManager.Gold >= character[0].ArcherCost && _archerCounter < 33)
        {
            var rand = UnityEngine.Random.Range(0, 32);
            var slot = GetSlotById(rand);

            while (slot.state == SlotState.Full)
            {
                rand = UnityEngine.Random.Range(0, 32);
                slot = GetSlotById(rand);
            }

            ArcherCost++;
            character[0].ArcherCost = ArcherCost * 10;
            _uiManager.ItemPrice(character[0].ArcherCost);
            
            slot.CreateItem(0);
            Debug.Log("Archer cost " + character[0].ArcherCost);
        }
        else
        {
            Debug.Log("Not enough gold!");
            _archerCounter--;
            _counter--;
        }
    }
    public void PlaceRandomMelee()
    {
        if (AllSlotsOccupied())
        {
            Debug.Log("No empty slot available!");
            return;
        }

        _meleeCounter++;
        _counter = _meleeCounter + _archerCounter;

        if (_uiManager.Gold >= character[1].MeleeCost + 10 && _meleeCounter < 33)
        {
            var rand = UnityEngine.Random.Range(32, slots.Count);
            var slot = GetSlotById(rand);

            while (slot.state == SlotState.Full)
            {
                rand = UnityEngine.Random.Range(32, slots.Count);
                slot = GetSlotById(rand);
            }

            MeleeCost++;
            character[1].MeleeCost = MeleeCost * 10;
            _uiManager.ItemPrice(character[1].MeleeCost);
            
            slot.CreateItem(1);
            Debug.Log("Archer placed" + character[1].MeleeCost);
        }
        else
        {
            Debug.Log("Not enough gold!");

            _meleeCounter--;
            _counter--;
        }
    }

    bool AllSlotsOccupied()
    {
        foreach (var slot in slots)
        {
            if (slot.state == SlotState.Full && slots.Count / 2 == _counter)
            {
                // all slots are full
                return true;
            }
        }

        // not all slots are full
        return false;

    }

    Slot GetSlotById(int id)
    {
        return slotDictionary[id];
    }
}
