using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Slot[] slots;

    private Vector3 _target;
    private ItemInfo carryingItem;

    private int _counter = 0;

    private Dictionary<int, Slot> slotDictionary;

    private void Awake()
    {
        instance = this;
        Utils.InitResources();
    }

    private void Start()
    {
        slotDictionary = new Dictionary<int, Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].id = i;
            slotDictionary.Add(i, slots[i]);
        }
    }

    //handle user input
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRayCast();
        }

        if (Input.GetMouseButton(0) && carryingItem)
        {
            OnItemSelected();
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Drop item
            SendRayCast();
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

    void SendRayCast()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        //we hit something
        if (hit.collider != null)
        {
            //we are grabbing the item in a full slot
            var slot = hit.transform.GetComponent<Slot>();
            if (slot.state == SlotState.Full && carryingItem == null)
            {
                var itemGO = (GameObject)Instantiate(Resources.Load("Prefabs/ItemDummy"));
                itemGO.transform.position = slot.transform.position;
                itemGO.transform.localScale = Vector3.one * 2;

                carryingItem = itemGO.GetComponent<ItemInfo>();
                carryingItem.InitDummy(slot.id, slot.currentItem.id);

                slot.ItemGrabbed();
            }
            //we are dropping an item to empty slot
            else if (slot.state == SlotState.Empty && carryingItem != null)
            {
                slot.CreateItem(carryingItem.itemId);
                Destroy(carryingItem.gameObject);
            }

            //we are dropping to full
            else if (slot.state == SlotState.Full && carryingItem != null)
            {
                //check item in the slot
                if (slot.currentItem.id == carryingItem.itemId && carryingItem.itemId + 2 < 6)
                {
                    print("merged");
                    OnItemMergedWithTarget(slot.id);
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
        _target.z = 0;
        var delta = 10 * Time.deltaTime;

        delta *= Vector3.Distance(transform.position, _target);
        carryingItem.transform.position = Vector3.MoveTowards(carryingItem.transform.position, _target, delta);
    }

    void OnItemMergedWithTarget(int targetSlotId)
    {

        var slot = GetSlotById(targetSlotId);
        Destroy(slot.currentItem.gameObject);

        slot.CreateItem(carryingItem.itemId + 2);
        Destroy(carryingItem.gameObject);
        _counter--;

    }

    void OnItemCarryFail()
    {
        var slot = GetSlotById(carryingItem.slotId);
        slot.CreateItem(carryingItem.itemId);
        Destroy(carryingItem.gameObject);
    }

    public void PlaceRandomArcher()
    {
        if (AllSlotsOccupied())
        {
            Debug.Log("No empty slot available!");
            return;
        }

        var rand = UnityEngine.Random.Range(32, slots.Length);
        var slot = GetSlotById(rand);

        while (slot.state == SlotState.Full)
        {
            rand = UnityEngine.Random.Range(32, slots.Length);
            slot = GetSlotById(rand);
        }
        _counter++;
        slot.CreateItem(0);

    }
    public void PlaceRandomMelee()
    {
        if (AllSlotsOccupied())
        {
            Debug.Log("No empty slot available!");
            return;
        }

        var rand = UnityEngine.Random.Range(32, slots.Length);
        var slot = GetSlotById(rand);

        while (slot.state == SlotState.Full)
        {
            rand = UnityEngine.Random.Range(32, slots.Length);
            slot = GetSlotById(rand);
        }
        _counter++;
        slot.CreateItem(1);

    }

    bool AllSlotsOccupied()
    {
        foreach (var slot in slots)
        {
            if (slot.state == SlotState.Full && slots.Length / 2 == _counter)
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