using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    BoxCollider trigger;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
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
