using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    public static API Instance;

    public string Address = "bd9b27fa7827dcb1406f5c83b98c02ba1417d52a6ccfbdf4ef952c2dc9162500";

    private void Start()
    {
        Instance = this;
    }

    public void SignAndSendCommitment(string commitmentHext)
    {
        if (!GameManager.Instance.EnableCommitments)
            return;

        this.StartCoroutine(this.Enumerator_SignAndSendCommitment(commitmentHext));
    }

    public IEnumerator Enumerator_SignAndSendCommitment(string commitmentHext)
    {
        string signedCommitment = this.Sign(commitmentHext);

        UIManager.Instance.DebugText.text = "Commitment signed! " + signedCommitment;

        Debug.Log("Commitment signed!");

        //TODO send it to chain

        yield break;
    }

    private IEnumerator Balance()
    {
        string signMethod = "http://localhost:8087/api/balance?address=" + this.Address;

        using (UnityWebRequest www = UnityWebRequest.Get(signMethod))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string answer = www.downloadHandler.text;

                Debug.Log("Balance: " + answer);
            }
        }
    }

    private string Sign(string hex)
    {
        var signMethod = "http://localhost:8087/api/sign";

        string postData = "{ 'app': 'smthHere', 'bytes': '" + hex + "' }";
        postData = postData.Replace("'", "\"");

        WebRequest request = WebRequest.Create(signMethod);
        request.Method = "POST";
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        request.ContentType = "application/json";
        request.ContentLength = byteArray.Length;
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        string responseFromServer = null;

        try
        {
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Commitment NOT signed! " + e.ToString());
            UIManager.Instance.DebugText.text = "Commitment NOT signed! " + e.ToString();
        }

        string signedData = responseFromServer;

        signedData = signedData.Replace("{\r\n  \"signedData\" : \"", "");
        signedData = signedData.Replace("\"\r\n}", "");

        return signedData;
    }

    private IEnumerator Sign()
    {
        string signMethod = "http://localhost:8087/api/sign";

        var form = new WWWForm();
        form.AddField("app", "MyAppName");
        form.AddField("bytes", "12345678");

        Dictionary<string, string> d = new Dictionary<string, string>();
        d.Add("app", "qwe");

        using (UnityWebRequest www = UnityWebRequest.Post(signMethod, d))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }


        }
    }

}
