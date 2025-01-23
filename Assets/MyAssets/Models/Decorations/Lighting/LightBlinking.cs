using UnityEngine;

public class example : MonoBehaviour
{
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = Mathf.Sin(Time.time*10)+1.5f;
    }
}