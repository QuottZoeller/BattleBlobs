using UnityEngine;
using UnityEngine.UI;

public class WinScreenText : MonoBehaviour
{
    public Text winText;

    void Start()
    {
        GameManager gameManager = GameManager.instance;
        if (gameManager != null)
        {
            Color winningColor = gameManager.GetWinningBlobColor();
            winText.text = "Winner: " + ColorUtility.ToHtmlStringRGBA(winningColor);
        }
        else
        {
            Debug.LogError("GameManager instance is not set!");
        }
    }
}
