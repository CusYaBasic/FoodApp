using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodApp.Components;

namespace FoodApp.Data
{
    public static class Data
    {
        // - Shop Data - //

        // Name of the shop
        public static string shopName = "Simple Game Studios";
        // Description of the shop
        public static string ShopDescription = "Simple Game Studios is a small indie game development studio based in the UK. We make games for mobile, PC and console.";
        // Email address of the shop
        public static string shopEmail = "support@simplegamestudios.co.uk";
        // Address of the shop
        public static string shopAddress = "123 Fake Street, Fake Town, Fake City, Fake Country, FK1 1FA";
        // Phone number of the shop
        public static string shopPhone = "01234567890";
        // Website of the shop
        public static string shopWebsite = "https://simplegamestudios.co.uk";
        // API of the shop
        public static string shopAPI = "https://simplegamestudios.co.uk/app_api.php?action=";

        // - Product Data - //

        // List of all products from our database
        public static List<ProductData> products;
        

        // - User Data - //

        // Whether the user is logged in or not
        public static bool loggedIn = false;
        // First name of the user
        public static string firstName;
        // Last name of the user
        public static string lastName;
        // Address of the user
        public static string address;
        // Phone number of the user
        public static string phone;
        // Email address of the user
        public static string email;
        // Password of the user
        public static string password;
        // User's cart
        public static List<ProductData> cart = new List<ProductData>();

        // - News Data - //

        // List of all news articles from our database
        public static List<NewsItemView> news;

    }
}
