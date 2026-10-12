using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player1Controller;
    public PlayerController player2Controller;
    public Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(
            (player1Controller.transform.position.x + player2Controller.transform.position.x) / 2,
            (player1Controller.transform.position.y + player2Controller.transform.position.y) / 2,
            mainCamera.transform.position.z
        );
        mainCamera.orthographicSize = Mathf.Max(
            5f,
            Vector2.Distance(player1Controller.transform.position, player2Controller.transform.position) / 2 + 2f
        );

    }
}
