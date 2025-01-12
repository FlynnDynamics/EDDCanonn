using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EDDCanonn
{
    public class CanonnDataHandler
    {
        // Fetches data from the Canonn endpoint
        public void FetchCanonnAsync(string fullUrl, Action<string> callback, Action<Exception> errorCallback = null)
        {
            StartTask(() =>
            {
                try
                {
                    string jsonResponse = PerformGetRequest(fullUrl);
                    callback?.Invoke(jsonResponse);
                }
                catch (Exception ex)
                {
                    errorCallback?.Invoke(ex);
                }
            });
        }

        // Pushes data to the Canonn endpoint
        public void PushCanonnAsync(string fullUrl, string jsonData, Action<bool> callback, Action<Exception> errorCallback = null)
        {
            StartTask(() =>
            {
                try
                {
                    string response = PerformPostRequest(fullUrl, jsonData, "application/json");
                    bool success = !string.IsNullOrEmpty(response);
                    callback?.Invoke(success);
                }
                catch (Exception ex)
                {
                    errorCallback?.Invoke(ex);
                }
            });
        }

        public List<Task> _tasks { get; }

        public CanonnDataHandler()
        {
            _tasks = new List<Task>();
        }

        private void StartTask(Action job)
        {
            Task task = Task.Run(() =>
            {
                try
                {
                    job.Invoke();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error in job execution: {ex.Message}");
                    throw;
                }
            });
            _tasks.Add(task);
        }

        // Performs a GET request to the specified endpoint
        private string PerformGetRequest(string fullUrl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullUrl);
                request.Method = "GET";
                request.Accept = "application/json";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.Error.WriteLine($"GET request failed. Status: {response.StatusCode}");
                        return null;
                    }

                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine($"Error performing GET request: {ex.Message}");
                throw;
            }
        }

        // Performs a POST request to the specified endpoint with JSON data
        private string PerformPostRequest(string fullUrl, string postData, string contentType)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullUrl);
                request.Method = "POST";
                request.ContentType = contentType;
                request.ContentLength = Encoding.UTF8.GetByteCount(postData);

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(postData);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
                    {
                        Console.Error.WriteLine($"POST request failed. Status: {response.StatusCode}");
                        return null;
                    }

                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine($"Error performing POST request: {ex.Message}");
                throw;
            }
        }
    }
}
