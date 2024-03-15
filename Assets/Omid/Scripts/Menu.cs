using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public GameObject selectionImage;

    private Button currentButton;

    private void Start()
    {
        currentButton = playButton;
        UpdateSelectionImage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentButton = exitButton;
            UpdateSelectionImage();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentButton = playButton;
            UpdateSelectionImage();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentButton == playButton)
            {
                LoadScene(1); 
            }
            else if (currentButton == exitButton)
            {
                ExitGame();
            }
        }
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void UpdateSelectionImage()
    {
        RectTransform buttonTransform = currentButton.GetComponent<RectTransform>();
        selectionImage.transform.position = new Vector2(500f, buttonTransform.position.y - 20f);
    }
}

