using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionCrowd : MonoBehaviour
{
    // ссылки на игрока
    public GameObject player;

    // список прихвостней и их мест за игроком 
    List<Transform> minions = new List<Transform>();
    List<Vector3> places = new List<Vector3>();

    // позиция игрока 
    Vector3 playerPos;

    // кол-во приспешников
    int crowdCount;

    // ссылка на текст для отображения кол-ва приспешников в строю
    public Text minionsInRank;
    int inRank;

    void Start()
    {
        
    }

    void Update()
    {
        // позиция чуть позади игрока 
        playerPos = player.transform.position - player.transform.forward*0.7f;
        
        // переменная для отслеживания изменений колличества приспешников
        int newCrowdCount = transform.childCount;

        // перепись миньёнов в списке при изменении их колличества
        if (newCrowdCount != crowdCount)
        {
            minions.Clear();
            for(int i = 0; i < transform.childCount; i++)
                minions.Add(transform.GetChild(i));
        }
        
        // число миньёнов
        crowdCount = minions.Count;
        
        // параметры смещения мест вниз и в сторону 
        float indentDown = 0.4f;
        float indentRight = 0.42f;

        // параметры и алгоритм построения
        int previousRow = 0;
        int currentRow = 0;
        int currentMinions = 0;
        
        places.Clear();

        while (currentMinions < crowdCount)
        {
            currentRow = previousRow + 1;
            
            if (currentRow + currentMinions <= crowdCount)
                for (float j = -(currentRow - 1 ) / 2f; j <= (currentRow - 1) / 2f; j++)
                    places.Add(playerPos - player.transform.forward * indentDown * currentRow
                                                    + player.transform.right * indentRight * j);
            else
            {
                currentRow = crowdCount - currentMinions;
                for (float j = -(currentRow - 1) / 2f; j <= (currentRow - 1) / 2f; j++)
                    places.Add(playerPos - player.transform.forward * indentDown * (previousRow + 1)
                                                    + player.transform.right * indentRight * j);
            }
            currentMinions += currentRow;
            previousRow = currentRow;
        }

        // расстановка по местам и подсчёт прихвостней 
        inRank = 0;
        for (int i = 0; i < crowdCount; i++)
        {
            if (minions[i].GetComponent<MinionScr>().GetIsOnAssignment() == false)
            {
                minions[i].GetComponent<MinionScr>().FollowMaster(places[i]);
                inRank++;
            }
        }

        minionsInRank.text = $"{inRank}/{crowdCount}";
    }

    public void GoForward()
    {
        bool minionSent = false;
        for (int i = 0; i < crowdCount; i++)
        {
            if (minions[i].GetComponent<MinionScr>().GetIsOnAssignment() == false)
            {
                // вычисление точки перед игроком и приказ посылающий миньёна вперёд
                Vector3 point = player.transform.position + player.transform.forward * 15;
                
                minions[i].GetComponent<MinionScr>().FollowOrder(point);

                minionSent = true;
            }

            if (minionSent == true)
                break;
        }
    }

    public void GoBackAll()
    {
        for (int i = 0; i < crowdCount; i++)
        {
           minions[i].GetComponent<MinionScr>().FollowMaster(places[i]);
           minions[i].GetComponent<MinionScr>().stopFollowOrder();
        }
    }

    public void GoBackOne()
    {
        bool minionSentBack = false;
        for (int i = 0; i < crowdCount; i++)
        {
            if (minions[i].GetComponent<MinionScr>().GetIsOnAssignment() == true)
            {
                minions[i].GetComponent<MinionScr>().FollowMaster(places[i]);
                minionSentBack = true;
            }

            if (minionSentBack == true)
                break;
        }

    }

    private void OnDrawGizmos()
    {
        playerPos = player.transform.position - player.transform.forward*0.7f;
        float indentDown = 0.4f;
        float indentRight = 0.42f;

        int previousRow = 0;
        int currentRow = 0;
        int currentMinions = 0;

        while (currentMinions < crowdCount)
        {
            currentRow = previousRow + 1;
            
            if (currentRow + currentMinions <= crowdCount)
                for (float j = -(currentRow - 1 ) / 2f; j <= (currentRow - 1) / 2f; j++)
                    Gizmos.DrawWireSphere(playerPos - player.transform.forward * indentDown * currentRow
                                                    + player.transform.right * indentRight * j, 0.2f);
            else
            {
                currentRow = crowdCount - currentMinions;
                for (float j = -(currentRow - 1) / 2f; j <= (currentRow - 1) / 2f; j++)
                    Gizmos.DrawWireSphere(playerPos - player.transform.forward * indentDown * (previousRow+1)
                                                    + player.transform.right * indentRight * j, 0.2f);
            }
            currentMinions += currentRow;
            previousRow = currentRow;
        }
    }
}
