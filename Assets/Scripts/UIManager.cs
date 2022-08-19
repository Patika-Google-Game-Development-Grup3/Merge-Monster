using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]private GameController gameController;

    public GameObject MainMenu;
    public GameObject MergeMenu;
    public GameObject SetActiveSlots;
    public GameObject LevelMenu;

    //Merge menu buttons
    public Button BuyArcher;
    public Button BuyMelee;
    public Button BackButton;

    public TextMeshProUGUI ArcherCost;
    public TextMeshProUGUI MeleeCost;
    //Main menu buttons
    public Button FightButton;
    public Button MergeButton;
    public Button QuitButton;
    [SerializeField] private CharacterPropertiesSO[] _character;
    private LevelDataSO _leveData;
    private SettingsController _settings;

    public TextMeshProUGUI goldPoint;
    
    private float gold;

    private int sceneToContinue;

    public float Gold { get => gold; set => gold = value; }



    private void Start()
    {
        _leveData = FindObjectOfType<LevelDataSO>();
        _settings = FindObjectOfType<SettingsController>();

        //Merge menu buttons interaction 
        BuyArcher.onClick.AddListener(() => { gameController.PlaceRandomArcher(); });
        BuyMelee.onClick.AddListener(() => { gameController.PlaceRandomMelee(); });
        BackButton.onClick.AddListener(() => { ChangeMenu(MergeMenu, MainMenu); });
        
        
        //Main menu bottons interaction
        FightButton.onClick.AddListener(() => { ContinueGame(); ChangeMenu(MainMenu,LevelMenu);});
        MergeButton.onClick.AddListener(() => { ChangeMenu(MainMenu, MergeMenu); });
        QuitButton.onClick.AddListener(() => { Application.Quit(); });
        
        SetGold();

    }


    void ChangeMenu(GameObject activeMenu, GameObject setMenu)
    {
        
        if (setMenu != null && activeMenu)
        {
            activeMenu.SetActive(false);
            setMenu.SetActive(true);
            if (!MergeMenu.activeInHierarchy)
            {
                SetActiveSlots.SetActive(false);
                var slots = gameController.slots;
               
                _settings._userSettingsSO.UserSettings.itemId = new List<int>();
                _settings._userSettingsSO.UserSettings.slotId = new List<int>();
                _settings._userSettingsSO.UserSettings.totalGold = Gold;
                foreach (var slot in slots)
                {
                    if (slot.currentItem == null)
                    {
                        continue;
                    }
                    var itemId = slot.currentItem.id;
                    var slotId = slot.id;
                    _settings._userSettingsSO.UserSettings.itemId.Add(itemId);
                    _settings._userSettingsSO.UserSettings.slotId.Add(slotId);
                    if (itemId % 2 == 0)
                        _settings._userSettingsSO.UserSettings.archerCounter = gameController.ArcherCost;
                    else _settings._userSettingsSO.UserSettings.meleeCounter = gameController.MeleeCost;
                    
                }
                _settings.SaveSettings();
            }
            else
            {
                SetActiveSlots.SetActive(true);
            }
        }
    }

    void ContinueGame()
    {
        sceneToContinue = 0; //PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue !=0 )
        {
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            SceneManager.LoadScene(1);
            
        }
    }

    void SetGold()
    {
        Gold = _settings._userSettingsSO.UserSettings.totalGold;
        goldPoint.text = Gold.ToString();

        CurrentCost();

    }

    public float UpdateGold(float amount)
    {
        Gold += amount;
        goldPoint.text = Gold.ToString();
        
        return Gold;
    }

    public float ItemPrice (float amount)
    {
        
        Gold -= amount;
        goldPoint.text = Gold.ToString();
        CurrentCost();
        
        return Gold;
    }

    void CurrentCost()
    {
        ArcherCost.text = " "+ (_character[0].ArcherCost + 10);
        MeleeCost.text = " "+ (_character[1].MeleeCost + 10);
    }
}
