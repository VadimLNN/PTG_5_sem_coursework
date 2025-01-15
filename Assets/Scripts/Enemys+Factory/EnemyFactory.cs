using UnityEngine;

public abstract  class EnemyFactory : MonoBehaviour
{
    public abstract IEnemy getEnemy();
    public abstract IEnemy getEnemy(Transform crowd);
}