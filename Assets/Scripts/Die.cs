using UnityEngine;

public class Die : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
    }
        void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy both enemies if they collide
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            GameObject.Find("GOCanvas").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
        
    
}
