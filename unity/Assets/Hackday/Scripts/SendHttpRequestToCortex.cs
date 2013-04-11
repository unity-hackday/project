using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System;

public class SendHttpRequestToCortex : MonoBehaviour {

	const int DefaultTimeout = 2 * 60 * 1000;  // 2 minutes timeout 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//GameObject.Find().GetComponent
	}
	
	void OnGUI () {
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
		
		if(GUI.Button(new Rect(20,40,80,20), "Login")) {
			var httpResponse = SendRequst("http://10.10.121.110:8080/cortex/authentication/mobee/form","GET","");
			Debug.Log(httpResponse.StatusCode);
		}
	}
	
	/*
	 * requestType must be "POST", "GET", "PUT" or "DELETE"
	 */
	HttpWebResponse SendRequst(string url, string requestType, string json) {
		//Create Request
		var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		httpWebRequest.ContentType = "Application/json";
		httpWebRequest.Method = requestType;
		
		if(requestType.Equals("POST")){
			//Add JSON to Request body
			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{	
					streamWriter.Write(json);
					streamWriter.Flush();
					streamWriter.Close();
				}
		}
		
		//Get Response
		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		
		//RequestState myRequestState = new RequestState();  
		//myRequestState.request = httpWebRequest;
		
		//IAsyncResult result= (IAsyncResult) httpWebRequest.BeginGetResponse(new AsyncCallback(RespCallback),myRequestState);
		
		// this line implements the timeout, if there is a timeout, the callback fires and the request becomes aborted
		//ThreadPool.RegisterWaitForSingleObject (result.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), httpWebRequest, DefaultTimeout, true);
		
		// The response came in the allowed time. The work processing will happen in the  
		// callback function.
		//allDone.WaitOne();
		
		// Release the HttpWebResponse resource.
		//myRequestState.response.Close();
		
		return httpResponse;
	}

	/*
	public class RequestState
		{
		  // This class stores the State of the request. 
		  const int BUFFER_SIZE = 1024;
		  public StringBuilder requestData;
		  public byte[] BufferRead;
		  public HttpWebRequest request;
		  public HttpWebResponse response;
		  public Stream streamResponse;
		  public RequestState()
		  {
		    BufferRead = new byte[BUFFER_SIZE];
		    requestData = new StringBuilder("");
		    request = null;
		    streamResponse = null;
		  }
		}
	*/
}