using UnityEngine;
using TMPro;

public class Square : MonoBehaviour
{
    public int x, y;
    public bool isMine = false;
    public bool isRevealed = false;
    public bool isFlagged = false;
    public int neighboringMines;

    public GameObject cube;
    public Material revealedMaterial;
    public Material mineMaterial;
    public Material flagMaterial;
    public Material hiddenMaterial;

    public TMP_Text textMeshPro;
    //public Color[] numberColors;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TMP_Text>();
    }

    public void reveal()
    {
        isRevealed = true;
        if (isMine)
        {
            cube.GetComponent<Renderer>().material = mineMaterial;
        }
        else
        {
            cube.GetComponent<Renderer>().material = revealedMaterial;
            
            if (neighboringMines > 0)
            {
                textMeshPro.text = neighboringMines.ToString();
                //textMeshPro.color = numberColors[neighboringMines - 1];
            }
        }
    }

    public void flag()
    {
        if (!isRevealed)
        {
            isFlagged = !isFlagged;
            if (isFlagged)
            {
                cube.GetComponent<Renderer>().material = flagMaterial;
            }
            else
            {
                cube.GetComponent<Renderer>().material = hiddenMaterial;
            }
        }
    }
}
