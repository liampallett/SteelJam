using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FocusGauge : MonoBehaviour
{


    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;
    [SerializeField] bool showValue;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<TMP_Text>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value!=0){
            slider.value-=2f * Time.deltaTime;
        }



        if (showValue){
            int x=0;
            x=(int)slider.value;
            
            text.SetText("Focus Gauge: "+x+" %");
           
        }
        else {
            int y=0;
            y=(int)slider.value;
            text.SetText("Focus Gauge :"+y+" %");

        }


        if (slider.value<=50 && slider.value>0){
            int x=0;
            x=(int)slider.value;
            text.SetText("Lock In. "+ "Focus Gauge at: "+x+" %");
        }


        if (slider.value==0){
            text.SetText("Lost Focus "+"Should have Locked In");

        }
    
        
    }

}

