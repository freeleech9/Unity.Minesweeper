using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Square"))
                {
                    Square square = hit.collider.GetComponent<Square>();
                    gameManager.revealSquare(square.x, square.y);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Square"))
                {
                    Square square = hit.collider.GetComponent<Square>();
                    gameManager.flagSquare(square.x, square.y);
                }
            }
        }
    }
}
