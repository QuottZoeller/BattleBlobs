using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int aliveBlobCount;
    private Color winningBlobColor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        aliveBlobCount = GameObject.FindGameObjectsWithTag("Blob").Length;
    }

    public void BlobDied()
    {
        aliveBlobCount--;

        if (aliveBlobCount == 1)
        {
            DetermineWinner();
            SceneManager.LoadScene("Win Screen");
        }
    }

    private void DetermineWinner()
    {
        GameObject[] blobs = GameObject.FindGameObjectsWithTag("Blob");
        foreach (GameObject blob in blobs)
        {
            if (blob.activeSelf)
            {
                SpriteRenderer blobRenderer = blob.GetComponent<SpriteRenderer>();
                if (blobRenderer != null)
                {
                    winningBlobColor = blobRenderer.color;
                    return;
                }
            }
        }

        Debug.LogError("No winning blob found!");
    }

    public Color GetWinningBlobColor()
    {
        return winningBlobColor;
    }
}
