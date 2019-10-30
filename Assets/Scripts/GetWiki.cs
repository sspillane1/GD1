using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;

public class GetWiki : MonoBehaviour {
    public Texture2D yourTexture2D;
    public string title;
    public string imgUrl;


    void Start()
    {
        findEnemy();
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                string req = webRequest.downloadHandler.text;
                Debug.Log(pages[page] + ":\nReceived: " + req);
                int titlePos = req.IndexOf("title") + 8;
                int end = req.Substring(titlePos).IndexOf('"');
                title = req.Substring(titlePos, end);
                Debug.Log(title);
            }
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://en.wikipedia.org/w/api.php?action=query&titles="+title+"&prop=pageimages&format=json&pithumbsize=100"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                string req = webRequest.downloadHandler.text;
                Debug.Log("https://en.wikipedia.org/w/api.php?action=query&titles=" + title + "&prop=pageimages&format=json&pithumbsize=1000");
                Debug.Log(pages[page] + ":\nRece: " + req);
                int titlePos = req.IndexOf("source") + 9;
                int end = req.Substring(titlePos).IndexOf('"');
                imgUrl = req.Substring(titlePos, end);
                Debug.Log(imgUrl);
                if (imgUrl == "omplete")
                {
                    findEnemy();
                }
            }
        }
        using (WWW www = new WWW(imgUrl))
        {
            yield return www;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Transform pos = GetComponent<Transform>();
            Texture2D webTex = www.texture;
            TextureScale.Bilinear(webTex, 300, 300);
            Vector3 worldPos=Camera.main.ScreenToWorldPoint(new Vector3(webTex.width, webTex.height, 0));
            renderer.sprite = Sprite.Create(webTex, new Rect(0, 0, webTex.width, webTex.height), new Vector2(0.5f, 0.5f), 100.0f);
            renderer.color = new Color32(255, 0, 0, 100);
        }
    }

    void findEnemy() {
        // A correct website page.
        StartCoroutine(GetRequest("https://en.wikipedia.org/w/api.php?format=json&action=query&generator=random&grnnamespace=0&prop=revisions|images&rvprop=content&grnlimit=1"));
    }

}


 
