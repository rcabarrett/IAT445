using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConventionsAndSyntax : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position.x);

        if (transform.position.y <= 5f)
        {
            Debug.Log("I'm about to hit the ground");
            
            // This is how you comment
            
            /* this is how we do multiple lines of code
             * it automatically puts the next * on the line for me
             * */

        }
        
    }

    /* this is how you comment out code
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
