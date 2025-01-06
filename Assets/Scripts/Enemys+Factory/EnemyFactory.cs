using UnityEngine;

public abstract  class EnemyFactory : MonoBehaviour
{
    public abstract IEnemy getEnemy();
}