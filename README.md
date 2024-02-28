# Food App
Along with the [Food Website](https://github.com/CusYaBasic/FoodSite), I wanted to make an app for mobile that ran along site for the more modern people who choose apps over a web browser.
Funnily enough this app was made way before the site counterpart and was more of a showcase/prototype to show someone I used to work for that I could make them a app and save them money on their just-eat orders via saving money on the commision they take.
However I ended up getting a new job and the project became abandoned, althought I wanted to keep working on it I never quite got the time due to working lengthy hours at my other job.

---

### DISCLAIMER: 
* ~~There was a Config.php that was meant to go along with this that i ended up losing stupidly.. so none of the backend features work such as the login and register account.~~ FOUND THE APP API; Put app_api.php on your webhost and change the database details to yours.
* There was also minor issues with the login entrybox for the user name and for all entrys on the registerbox.
* Lastly the app failed to launch using windows and only launched on android, no idea if it worked for IOS as I don't have a IOS device to try it on

Take a look at [Food Website](https://github.com/CusYaBasic/FoodSite), you can use the code from there to link the site and app up together ~~and fix the login with a few edits to this apps code~~, it's something I might do at a later date if I ever get time.

---

### Features:
* Full Login and register (broken, missing config.php)
* New Tab with news components and pages
* Login page
* Register page
* Several hidden tabs once you've logged in which are unaccessible

---

## Media:

### Login:
<img src="https://github.com/CusYaBasic/FoodApp/assets/86253238/44789284-7886-47b4-83f5-8a42c34df947" width="300">

### News Page:
<img src="https://github.com/CusYaBasic/FoodApp/assets/86253238/2c1b2411-dd80-4e62-bd4a-1bbf0a794feb" width="300">

### Register Page:
<img src="https://github.com/CusYaBasic/FoodApp/assets/86253238/a699c5e0-dfe1-45eb-98cf-5a7b09441df9" width="300">

### News Tab:
<img src="https://github.com/CusYaBasic/FoodApp/assets/86253238/859a8a38-8ecf-41a1-a01b-7f93781cdb3d" width="300">


---

### MySQL:

```
CREATE TABLE `News` (
  `Title` varchar(100) NOT NULL,
  `Description` varchar(5000) NOT NULL,
  `NewsImage` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Products` (
  `Name` text NOT NULL,
  `Description` text NOT NULL,
  `Price` float NOT NULL,
  `Icon` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Users` (
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `Telephone` text NOT NULL,
  `Address` text NOT NULL,
  `Email` text NOT NULL,
  `Password` text NOT NULL,
  `Admin` tinyint(1) NOT NULL,
  `Cart` json NOT NULL,
  `Postcode` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

```
