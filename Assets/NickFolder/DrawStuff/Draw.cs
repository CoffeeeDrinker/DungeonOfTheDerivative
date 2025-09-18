using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] GameObject drawThing;
    [SerializeField] GameObject eraseThing;
    [SerializeField] Color defaultColor;
    [SerializeField] double defaultSize;
    [SerializeField] double defaultEraserSize;
    [SerializeField] GameObject holder;
    private int currOrder;
    private bool drawing;

    // Start is called before the first frame update
    void Start()
    {
        currOrder = 0;
        drawThing.transform.localScale = new Vector2((float)defaultSize, (float)defaultSize);
        eraseThing.transform.localScale = new Vector2((float)defaultEraserSize, (float)defaultEraserSize);
        ChangeColor(defaultColor);
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
            drawObject.transform.parent = holder.transform;
            drawObject.transform.GetComponent<Renderer>().sortingOrder = currOrder;
        } else if(Input.GetMouseButton(1)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if(drawing)
            {
                drawing = false;
                currOrder++;
            }

            GameObject eraseObject = Instantiate(eraseThing, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
            eraseObject.transform.parent = holder.transform;
            eraseObject.transform.GetComponent<Renderer>().sortingOrder = currOrder;
            
        }   
    }

    public void ChangeColor(Color color) {
        drawThing.GetComponent<SpriteRenderer>().color = color;
        currOrder++;
    }

    public void ChangeDrawSize(int size)
    {
        drawThing.transform.localScale = new Vector2((float)defaultSize * size, (float)defaultSize * size);
    }

    public void ChangeEraseSize(int size)
    {
        eraseThing.transform.localScale = new Vector2((float)defaultSize * size, (float)defaultSize * size);
    }
}
