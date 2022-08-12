using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameController gameController;

    public GameObject MainMenu;
    public GameObject MergeMenu;
    public GameObject SetActiveSlots;

    //Merge menu buttons
    public Button BuyArcher;
    public Button BuyMelee;
    public Button BackButton;

    //Main menu buttons
    public Button FightButton;
    public Button MergeButton;
    public Button QuitButton;

    private int sceneToContinue;


    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

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
            }
            else
            {
                SetActiveSlots.SetActive(true);
            }
        }
    }

    void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        if (sceneToContinue !=0 )
        {
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

}
