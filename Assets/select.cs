using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select : MonoBehaviour
{
    public static GameObject selected;
    public LineRenderer line;
    void Start()
    {
    }

    void Update()
    { 
    }
    void OnMouseDown()
    {
        
        if(!selected)
        {
            selected=this.transform.gameObject;
        }
        else if(selected==this.transform.gameObject)
        {
            selected = null;
        }
        else if (selected!=this.transform.gameObject)
        {
            float radio = 2.5F; //ajustar a tamano del radio
            line = selected.gameObject.AddComponent<LineRenderer>();
            line.SetWidth(0.1F, 0.1F);
            line.SetVertexCount(2);
            Vector2 temp1 = selected.transform.position;
            Vector2 temp2 = this.transform.position;
            float dist = Vector2.Distance(temp1,temp2);
            float disty = Mathf.Abs(temp1.y-temp2.y);
            float distx = Mathf.Abs(temp1.x-temp2.x);
            print(dist);
            print(disty);
            print(distx);
            if (temp1.x>=temp2.x)
            {
                temp1.x=temp1.x-(distx/dist)*radio;
                temp2.x=temp2.x+(distx/dist)*radio;
            }
            else
            {
                temp1.x=temp1.x+(distx/dist)*radio;
                temp2.x=temp2.x-(distx/dist)*radio;
            }

            if (temp1.y>=temp2.y)
            {
                temp1.y=temp1.y-(disty/dist)*radio;
                temp2.y=temp2.y+(disty/dist)*radio;
            }
            else
            {
                temp1.y=temp1.y+(disty/dist)*radio;
                temp2.y=temp2.y-(disty/dist)*radio;
            }

            line.SetPosition(0, temp1);
            line.SetPosition(1, temp2);
            print(selected);
            print(this.transform.gameObject);
            selected=null;
        }
    }
}
