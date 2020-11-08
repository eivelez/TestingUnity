using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject g1;
    public GameObject g2;
    public GameObject arrow;

    void Start()
    {
        Connect(g1,g2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Connect(GameObject sender, GameObject objective)
    {
        Vector2 posSender = sender.transform.position;
        Vector2 posObjective = objective.transform.position;
        Vector2 initialPos = sender.GetComponent<CircleCollider2D>().ClosestPoint(posObjective);
        Vector2 finalPos = objective.GetComponent<CircleCollider2D>().ClosestPoint(posSender);
        float distX = Mathf.Abs(posSender.x-posObjective.x);
        float distY = Mathf.Abs(posSender.y-posObjective.y);
        float centerDistance = Vector2.Distance(posSender,posObjective);
        float colliderDistance = Vector2.Distance(initialPos,finalPos);
        float middleX = (posSender.x + posObjective.x) / 2f;
        float middleY = (posSender.y + posObjective.y) / 2f;
        float angle = Mathf.Atan(distY / distX) * 180 / Mathf.PI;
        if (posSender.x < posObjective.x && posSender.y >= posObjective.y){angle *= -1;}
        else if (posSender.x >= posObjective.x && posSender.y >= posObjective.y){angle += 180;}
        else if (posSender.x >= posObjective.x && posSender.y < posObjective.y){angle += (90 - angle) * 2;}
        GameObject arrowObject = Instantiate(arrow, new Vector3(middleX, middleY,0), Quaternion.identity);
        arrowObject.transform.Rotate(0, 0, angle - 90);
        arrowObject.transform.localScale = new Vector3(0.3f, 0.15f * colliderDistance, 1);
    }
}
