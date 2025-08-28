using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] GameObject drawThing;
    [SerializeField] GameObject eraseThing;
    private int currOrder;
    private bool drawing;

    // Start is called before the first frame update
    void Start()
    {
        currOrder = 0;
        drawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(!drawing)
            {
                drawing = true;
                currOrder++;
            }

            GameObject drawObject = Instantiate(drawThing, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
            drawObject.transform.GetComponent<Renderer>().sortingOrder = currOrder;
        } else if(Input.GetMouseButton(1)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if(drawing)
            {
                drawing = false;
                currOrder++;
            }

            GameObject eraseObject = Instantiate(eraseThing, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
            eraseObject.transform.GetComponent<Renderer>().sortingOrder = currOrder;
            
        }   
    }
}
