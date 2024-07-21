using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton instance
    public Text cellCountText; // Reference to the UI Text element

    void Awake()
    {
        // Ensure only one instance of UIManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCellCount(int count)
    {
        cellCountText.text = "Cells Added: " + count; // Update the UI text
    }
}