using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityForce = 9.81f; // Сила гравитации
    private Vector3 velocity; // Скорость объекта
    [Range(0, 5f)]
    public float distToGround;

    private void Update()
    {
        // Применяем гравитацию
        ApplyGravity();

        // Обновляем позицию объекта
        transform.position += velocity * Time.deltaTime;
    }

    private void ApplyGravity()
    {
        // Увеличиваем скорость вниз
        velocity.y -= gravityForce * Time.deltaTime;

        // Проверяем столкновение с землей (например, y = 0)
        if (Physics.Raycast(transform.position, Vector3.down, distToGround))
            velocity.y = 0; // Обнуляем скорость при столкновении с землей
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distToGround, transform.position.z));
    }

}
