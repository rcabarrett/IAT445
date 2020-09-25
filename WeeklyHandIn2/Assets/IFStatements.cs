using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFStatements : MonoBehaviour
{
    string spherecolour;
    string red;
    string green;
    string blue;

    private void Start()
    {
        red = System.Convert.ToString(Color.red);
        green = System.Convert.ToString(Color.green);
        blue = System.Convert.ToString(Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            colourtest();

        spherecolour = System.Convert.ToString(GetComponent<Renderer>().material.color);

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    void colourtest()
    {
        if(spherecolour == red)
        {
            print("The Sphere is Red");
        }
        else if(spherecolour == green)
        {
            print("The Sphere is Green");
        }
        else if (spherecolour == blue)
        {
            print("The Sphere is blue");
        }
        else
        {
            print("The Sphere has no colour");
        }
    }
}
