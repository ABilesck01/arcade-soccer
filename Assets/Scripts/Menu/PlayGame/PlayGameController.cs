using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayGameController : MonoBehaviour
{
    [SerializeField] private List<GameType> gameTypes = new List<GameType>();
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI gameTypeLabel;
    [SerializeField] private GameObject splitSettings;
    [SerializeField] private GameObject onlineSettings;
    [Header("Split")]
    [SerializeField] private GameObject playerTwo;
    [Header("Online")]
    [SerializeField] private GameObject hostModal;
    [SerializeField] private GameObject enterModal;
    [SerializeField] private TMP_InputField roomInput;
    [SerializeField] private TextMeshProUGUI onlineLabel;
    [SerializeField] private CreateAndJoinRooms createAndJoinRooms;

    private int gameTypeIndex = 0;

    private bool isHosting = true;
    private bool isOnline = false;

    private void Start()
    {
        gameTypeLabel.text = gameTypes[gameTypeIndex].name;
    }

    public void btnNextGameType()
    {
        if (gameTypeIndex >= (gameTypes.Count - 1)) return;

        gameTypeIndex++;

        disableAllSubSettings();

        if (gameTypes[gameTypeIndex].canPlayOnline)
        {
            onlineSettings.SetActive(true);
            isHosting = true;
            PlayerPrefs.SetInt("twoPlayers", 0);
            playerTwo.SetActive(false);
            createAndJoinRooms.isHost = true;
        }
        else if (gameTypes[gameTypeIndex].canSplitController)
        {
            splitSettings.SetActive(true);
            createAndJoinRooms.isHost = false;
        }
        else
        {
            createAndJoinRooms.isHost = false;
            PlayerPrefs.SetInt("twoPlayers", 0);
            playerTwo.SetActive(false);
        }

        gameTypeLabel.text = gameTypes[gameTypeIndex].name;
    }

    public void btnPrevGameType()
    {
        if (gameTypeIndex <= 0) return;

        gameTypeIndex--;

        disableAllSubSettings();

        if(gameTypes[gameTypeIndex].canPlayOnline)
        {
            onlineSettings.SetActive(true);
            isHosting = true; 
            PlayerPrefs.SetInt("twoPlayers", 0);
            playerTwo.SetActive(false);
        }
        else if (gameTypes[gameTypeIndex].canSplitController)
        {
            splitSettings.SetActive(true);
            createAndJoinRooms.isHost = false;
        }
        else
        {
            createAndJoinRooms.isHost = false;
            PlayerPrefs.SetInt("twoPlayers", 0);
            playerTwo.SetActive(false);
        }
    }

    private void disableAllSubSettings()
    {
        splitSettings.SetActive(false);
        onlineSettings.SetActive(false);
    }

    public void SplitControls()
    {
        PlayerPrefs.SetInt("twoPlayers", 1);
        playerTwo.SetActive(true);
    }

    public void btnHostEnterOnline()
    {
        isHosting = !isHosting;
        if(isHosting)
        {
            hostModal.SetActive(true);
            enterModal.SetActive(false);
            onlineLabel.text = "Host";
        }
        else
        {
            hostModal.SetActive(false);
            enterModal.SetActive(true);
            onlineLabel.text = "Host";
            roomInput.Select();
            roomInput.ActivateInputField();
        }
    }

    public void Play()
    {
        if(gameTypes[gameTypeIndex].canPlayOnline)
        {
            createAndJoinRooms.OnlinePlay();
        }
        else
        {
            SceneManager.LoadScene(gameTypes[gameTypeIndex].name);
        }
    }

}

[System.Serializable]
public struct GameType
{
    public string name;
    public bool canPlayOnline;
    public bool canSplitController;
}
