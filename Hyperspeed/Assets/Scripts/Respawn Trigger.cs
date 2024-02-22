using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnTrigger : MonoBehaviour
{
    BoxCollider trigger;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        trigger = GetComponent<BoxCollider>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
