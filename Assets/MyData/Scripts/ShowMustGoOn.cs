using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMustGoOn : MonoBehaviour
{
    public float perdeHizi = 5f;

    public void perdeAcilma()
    {
        Vector3 position = this.transform.position;
        while (position.x < 9.61f)
        {
            position.x++;
            this.transform.position = position;
        }
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        Vector3 position = this.transform.position;
        yield return new WaitForSeconds(5);
        while (position.x > 4.92f)
        {
            position.x = position.x - 0.1f;
            this.transform.position = position;
        }
    }
}
