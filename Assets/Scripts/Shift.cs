using UnityEngine;
using System.Collections;
public class Shift : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SelfD());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SelfD() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
