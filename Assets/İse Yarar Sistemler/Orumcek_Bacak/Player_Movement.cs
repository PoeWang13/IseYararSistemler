using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;
    private float verSpeed;
    private float horSpeed;
    private void Update()
    {
        verSpeed = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
        horSpeed = Input.GetAxisRaw("Horizotal") * Time.deltaTime * moveSpeed;
        transform.Translate(horSpeed, 0, verSpeed);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        }
    }
}