using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FoodApp.Components;
using FoodApp.Data;

namespace FoodApp.API
{
    public static class APIConnector
    {
        // Gets the data for a single product from our database using our API
        public async static Task<ProductData> GetProductDataAsync(string name)
        {
            // Create a new HttpClient and get the data from our API
            HttpClient client = new HttpClient();
            string url = Data.Data.shopAPI + "get_product&name=" + name;

            // Get the response from the API
            HttpResponseMessage response = await client.GetAsync(url);

            // If the response is successful, return the product data
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON data from the response
                string json = await response.Content.ReadAsStringAsync();
                ProductData product = JsonSerializer.Deserialize<ProductData>(json);
                return product;
            }
            else
            {
                //TODO: Handle the error
                return null;
            }
        }

        // Gets all the products from our database using our API
        public async static Task<List<ProductData>> GetAllProductsAsync()
        {
            // Create a new HttpClient and get the data from our API
            HttpClient client = new HttpClient();
            string url = Data.Data.shopAPI + "get_all_products"; 

            // Get the response from the API
            HttpResponseMessage response = await client.GetAsync(url);

            // If the response is successful, return the product data
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON data from the response
                string json = await response.Content.ReadAsStringAsync();
                Data.Data.products = JsonSerializer.Deserialize<List<ProductData>>(json);
                return Data.Data.products;
            }
            else
            {
                //TODO: Handle the error
                return null;
            }
        }

        // Registers a new account using our API
        public async static Task<string> RegisterAccount(string email, string firstName, string lastName, string telephone, string address, string postcode, string password)
        {
            try
            {
                // Create a new HttpClient and post the data to our API
                string apiUrl = Data.Data.shopAPI + "register_account";

                // Create a new HttpClient and post the data to our API
                using (HttpClient client = new HttpClient())
                {
                    // Create a dictionary of the form data
                    var formData = new Dictionary<string, string>
                    {
                        { "email", email },
                        { "FirstName", firstName },
                        { "LastName", lastName },
                        { "telephone", telephone },
                        { "address", address },
                        { "postcode", postcode },
                        { "password", password }
                    };

                    // Convert the form data into a FormUrlEncodedContent object and create a POST request
                    var content = new FormUrlEncodedContent(formData);
                    var response = await client.PostAsync(apiUrl, content);

                    // If the response is successful, return the response content
                    if (response.IsSuccessStatusCode)
                    {
                        // Get the response content
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        // Get the error content
                        string errorContent = await response.Content.ReadAsStringAsync();
                        return "Registration request failed. Error: " + errorContent + await content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return "Registration request failed. Exception: " + ex.Message;
            }
        }

        public async static Task<string> LoginAccount(string email, string password)
        {
            try
            {
                string apiUrl = Data.Data.shopAPI + "login_account";

                using (HttpClient client = new HttpClient())
                {
                    var formData = new Dictionary<string, string>
                    {
                        { "email", email },
                        { "password", password }
                    };

                    var content = new FormUrlEncodedContent(formData);

                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        return "Login request failed. Error: " + errorContent + await content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return "Login request failed. Exception: " + ex.Message;
            }
        }

        public async static Task<List<NewsItemView>> GetNews()
        {
            string apiUrl = Data.Data.shopAPI + "get_news";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<NewsItemView> newsList = JsonSerializer.Deserialize<List<NewsItemView>>(responseContent);
                    return newsList;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    // Handle the error as needed
                    throw new Exception("Get news request failed. Error: " + errorContent);
                }
            }
        }

        public async static Task<string> GetUserData(string email)
        {
            try
            {
                string apiUrl = Data.Data.shopAPI + "get_user_data&email=" + email;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
                        json.TryGetValue("FirstName", out Data.Data.firstName);
                        json.TryGetValue("LastName", out Data.Data.lastName);
                        json.TryGetValue("Address", out Data.Data.address);
                        json.TryGetValue("Telephone", out Data.Data.phone);
                        json.TryGetValue("Email", out Data.Data.email);
                        return Data.Data.address;
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        return "Get user data request failed. Error: " + errorContent;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Get user data request failed. Exception: " + ex.Message;
            }
        }


    }
}
