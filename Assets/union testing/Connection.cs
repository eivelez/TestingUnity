using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Connection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject g1;
    public GameObject g2;
    public GameObject arrow;

    public bool switchVar = false;

    void Start()
    {
        if(PermitConnection(g1,g2))
        {
            g1.GetComponent<Nodo>().objectives.Add(g2);
            Connect(g1,g2);
            PointsAfterConnection(g1,g2);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePointsNumber(g1);
        UpdatePointsNumber(g2);
        if(switchVar)
        {
            DefinePowerFactors(g1);
            DefinePowerFactors(g2);
            AtackHealUnit(g1, g2);
            switchVar=false;
        }
        
    }

    void Connect(GameObject sender, GameObject objective) //generate the arrow gameObject, show it on the screen and add it to unions list on sender
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
        sender.GetComponent<Nodo>().unions.Add(arrowObject);
    }

    bool PermitConnection(GameObject sender, GameObject objective) //Bool, seems if the connection length is allowed or too far.
    {
        int points = sender.GetComponent<Nodo>().points;
        Vector2 posSender = sender.transform.position;
        Vector2 posObjective = objective.transform.position;
        float distTotal = Vector2.Distance(posSender,posObjective);
        if((int) (90f*distTotal/Camera.main.GetComponent<CameraSize>().camWidth)<=points)
        {
            Debug.Log("permited connection");
            return true;
        }
        else
        {
            Debug.Log("out of range");
            return false;
            //send out of range error message

        }
    }

    void PointsAfterConnection(GameObject sender, GameObject objective)// use only with values pre validated by PermitConnection, reduce sender points by stretching concept
    {
        Vector2 posSender = sender.transform.position;
        Vector2 posObjective = objective.transform.position;
        float distTotal = Vector2.Distance(posSender,posObjective);
        int points = sender.GetComponent<Nodo>().points;
        int finalPoints = points-(int) (90f*distTotal/Camera.main.GetComponent<CameraSize>().camWidth);
        sender.GetComponent<Nodo>().points = finalPoints;
    }

    void RecoverPointsFromConnectionCancel(GameObject sender, GameObject objective)// recover all the points (or less when over 100) when 
    {
        Vector2 posSender = sender.transform.position;
        Vector2 posObjective = objective.transform.position;
        float distTotal = Vector2.Distance(posSender,posObjective);
        int points = sender.GetComponent<Nodo>().points;
        int finalPoints = points+Mathf.Min(100,(int) (90f*distTotal/Camera.main.GetComponent<CameraSize>().camWidth));
        sender.GetComponent<Nodo>().points = finalPoints;
    }

    void DeleteConnection(GameObject sender, GameObject objective) //remove the arrow from unions and objectives lists, and scene
    {
        int index  = sender.GetComponent<Nodo>().objectives.IndexOf(objective);
        Destroy(sender.GetComponent<Nodo>().unions[index]);
        sender.GetComponent<Nodo>().unions[index]=null;
        sender.GetComponent<Nodo>().objectives[index]=null;
    }

    void DefinePowerFactors(GameObject unit) //this function should be executed when ending the turn before doing the healings/damages, after all connections and points adjustments are done
    {
        int points = unit.GetComponent<Nodo>().points;
        int unitType = unit.GetComponent<Nodo>().type;
        unit.GetComponent<Nodo>().healingFactor = (int)Mathf.Sqrt(points);
        unit.GetComponent<Nodo>().dmgFactor = (int)Mathf.Sqrt(points);
        if (unitType==1){unit.GetComponent<Nodo>().dmgFactor*=2;}
        else if (unitType==2){unit.GetComponent<Nodo>().healingFactor*=2;}
    }

    void AtackHealUnit(GameObject sender, GameObject objective) //remeber to DefinePowerFactors before atking/healing, this function autoconvert the unit owner when defeated
    {
        Nodo senderAttributes = sender.GetComponent<Nodo>();
        Nodo objectiveAttributes = objective.GetComponent<Nodo>();
        if (senderAttributes.owner==objectiveAttributes.owner)
        {
            objectiveAttributes.points = Mathf.Min(100,objectiveAttributes.points+senderAttributes.healingFactor);
        }
        else
        {
            objectiveAttributes.points -= senderAttributes.dmgFactor;
            if(objectiveAttributes.points<0)
            {
                objectiveAttributes.points*=-1;
                objectiveAttributes.owner=senderAttributes.owner;
            }
        }
    }

    void UpdatePointsNumber(GameObject unit)
    {
        int points = unit.GetComponent<Nodo>().points;
        unit.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text=points.ToString();
    }


}
