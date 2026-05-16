using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed = 7f;

    private void Update()
    {
        if (!SpawnManager.Instance.IsGameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

    }
}
