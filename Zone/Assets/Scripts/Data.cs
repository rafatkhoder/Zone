using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Data : MonoBehaviour
{
    public static Data Instance;
    public int count;
    private void Awake()
    {
        Instance = this;

    }
   
    public IEnumerator GetText()
    {
        WWWForm form = new WWWForm();

        if (PlayerPrefs.HasKey(GameString.Access))
            Manger.Instance.count = PlayerPrefs.GetInt(GameString.Access);

        Manger.Instance.count++;
        PlayerPrefs.SetInt(GameString.Access, Manger.Instance.count);
        form.AddField(GameString.Access, Manger.Instance.count);

        using (UnityWebRequest request = UnityWebRequest.Post("https://jsonplaceholder.typicode.com/posts", form))
        {
            // call API
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                Debug.Log("Success");
                int count = PlayerPrefs.GetInt(GameString.Access);
                UiGame.Instance.GetCountText(count);
                

                


            }


        }

          
    }
}
