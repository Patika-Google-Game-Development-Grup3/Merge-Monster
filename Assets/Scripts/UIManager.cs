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

    public TextMeshProUGUI goldPoint;
    private float gold;

    private int sceneToContinue;

    public float Gold { get => gold; set => gold = value; }



    private void Start()
    {
        
        //Merge menu buttons interaction 
        BuyArcher.onClick.AddListener(() => { gameController.PlaceRandomArcher(); });
        BuyMelee.onClick.AddListener(() => { gameController.PlaceRandomMelee(); });
        BackButton.onClick.AddListener(() => { ChangeMenu(MergeMenu, MainMenu); });
        
        
        //Main menu bottons interaction
        FightButton.onClick.AddListener(() => { ContinueGame(); });
        MergeButton.onClick.AddListener(() => { ChangeMenu(MainMenu, MergeMenu); });
        QuitButton.onClick.AddListener(() => { Application.Quit(); });
        
        ArcherCost.text = "Archer Cost: "+ _character[0].ArcherCost.ToString();
        MeleeCost.text = "Melee Cost: "+ _character[1].MeleeCost.ToString();
        
        UpdateGold(5000);
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
                var settings = SettingsController.Instance;
                settings._userSettingsSO.UserSettings.itemId = new List<int>();
                settings._userSettingsSO.UserSettings.slotId = new List<int>();

                foreach (var slot in slots)
                {
                    if (slot.currentItem == null)
                    {
                        continue;
                    }
                    var itemId = slot.currentItem.id;
                    var slotId = slot.id;
                    settings._userSettingsSO.UserSettings.itemId.Add(itemId);
                    settings._userSettingsSO.UserSettings.slotId.Add(slotId);
                    if (itemId == 0 || itemId == 2 || itemId == 4 ) settings._userSettingsSO.UserSettings.archerCounter++;
                    else settings._userSettingsSO.UserSettings.meleeCounter++;
                }
                settings.SaveSettings();
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

    public void UpdateGold(int amount)
    {
        Gold += amount;
        goldPoint.text = Gold.ToString();
    }

    public float ItemPrice (float amount)
    {
        ArcherCost.text = "Archer Cost: "+ (_character[0].ArcherCost + 10);
        MeleeCost.text = "Melee Cost: "+ (_character[1].MeleeCost + 10);
        Gold -= amount;
        goldPoint.text = Gold.ToString();

        return Gold;
    }

    
    

}
