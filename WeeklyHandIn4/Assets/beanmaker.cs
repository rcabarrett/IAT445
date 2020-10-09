using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beanmaker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject angrybean;
    public GameObject dispenser;
    public float respawn_time = 1.0f;

    void Start()
    {
        StartCoroutine(beandispensing());
    }
    private void spawnbeans()
    {
        GameObject a = Instantiate(angrybean) as GameObject;
        a.transform.position = new Vector3(dispenser.transform.position.x, dispenser.transform.position.y, dispenser.transform.position.z);

    }
    IEnumerator beandispensing()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawn_time);
            spawnbeans();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
