<?php

error_reporting(E_ALL);
ini_set('display_errors', 1);

// Replace these variables with your database credentials
$host = "";
$username = "";
$password = "";
$database = "";

// Create a connection to the database
$conn = new mysqli($host, $username, $password, $database);

// Check the connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

function getProductByName($conn, $searchName) {
    $query = "SELECT * FROM Products WHERE Name = ?";
    
    $stmt = $conn->prepare($query);
    $stmt->bind_param("s", $searchName);
    $stmt->execute();
    
    $result = $stmt->get_result();
    
    if ($result->num_rows > 0) {
        $data = $result->fetch_assoc();
        return $data;
    } else {
        return null;
    }
}

function getAllProducts($conn) {
    $query = "SELECT * FROM Products";
    $result = $conn->query($query);

    if ($result->num_rows > 0) {
        $products = array();

        while ($row = $result->fetch_assoc()) {
            $row['Price'] = (float)$row['Price'];
            $products[] = $row;
        }

        return $products;
    } else {
        return null;
    }
}

function registerAccount($conn, $email, $firstName, $lastName, $telephone, $address, $postcode, $password) {
    // Check if an account with the same email already exists
    $checkQuery = "SELECT * FROM Users WHERE Email = ?";
    $stmt = $conn->prepare($checkQuery);
    $stmt->bind_param("s", $email);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
        error_log("An account with this email already exists", 0);
        return "An account with this email already exists.";
    }

    // If no account exists, proceed with registration
    $insertQuery = "INSERT INTO Users (Email, FirstName, LastName, Telephone, Address, Postcode, Password) VALUES (?, ?, ?, ?, ?, ?, ?)";
    $stmt = $conn->prepare($insertQuery);
    $stmt->bind_param("sssssss", $email, $firstName, $lastName, $telephone, $address, $postcode, $password);

    if ($stmt->execute()) {
        error_log("Account registered successfully", 0);
        return "Account registered successfully.";
    } else {
        error_log("Registration failed", 0);
        return "Registration failed.";
    }
}

function loginAccount($conn, $email, $password) {
    $query = "SELECT * FROM Users WHERE Email = ?";

    $stmt = $conn->prepare($query);
    $stmt->bind_param("s", $email);
    $stmt->execute();

    $result = $stmt->get_result();

    if ($result->num_rows === 1) {
        $user = $result->fetch_assoc();

        // Check if the provided password matches the stored password
        if ($password === $user['Password']) {
            return "Login successful";
        }
    }

    return "Login failed. Invalid email or password.";
}

function getUserData($conn, $email) {
    $query = "SELECT Email, FirstName, LastName, Telephone, Address, Postcode FROM Users WHERE Email = ?";

    $stmt = $conn->prepare($query);
    $stmt->bind_param("s", $email);
    $stmt->execute();

    $result = $stmt->get_result();

    if ($result->num_rows === 1) {
        $userData = $result->fetch_assoc();

        // Remove any sensitive data you don't want to return to the client
        unset($userData['Cart']);
        unset($userData['Admin']);

        return $userData;
    }

    return null;
}

function getNews($conn) {
    $query = "SELECT * FROM News";

    $stmt = $conn->prepare($query);
    $stmt->execute();

    $result = $stmt->get_result();

    if ($result->num_rows > 0) {
		$news = array();

		while ($row = $result->fetch_assoc()) {
			$news[] = $row;
		}

	    return $news;
	} else {
	    return null;
	}
}

if (isset($_GET['action'])) {
    $action = $_GET['action'];
    
    if ($action === 'get_product') {
        // Get a product by name
        if (isset($_GET['name'])) {
            $searchName = $_GET['name'];
            $product = getProductByName($conn, $searchName);
            if ($product !== null) {
                echo json_encode($product);
            } else {
                echo "Product not found.";
            }
        } else {
            echo "Name parameter missing.";
        }
    } elseif ($action === 'get_all_products') {
        // Get all products
        $products = getAllProducts($conn);
        if ($products !== null) {
            echo json_encode($products);
        } else {
            echo "No products found.";
        }
     } elseif ($action === 'register_account') {
        // Register an account
        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $email = $_POST['email'] ?? '';
            $firstName = $_POST['FirstName'] ?? '';
            $lastName = $_POST['LastName'] ?? '';
            $telephone = $_POST['telephone'] ?? '';
            $address = $_POST['address'] ?? '';
            $postcode = $_POST['postcode'] ?? '';
            $password = $_POST['password'] ?? '';

            if (!empty($email) && !empty($firstName) && !empty($lastName) && !empty($telephone) && !empty($address) && !empty($postcode) && !empty($password)) {
                $registrationResult = registerAccount($conn, $email, $firstName, $lastName, $telephone, $address, $postcode, $password);
                echo $registrationResult;
            } else {
                echo "Registration data missing.";
            }
        } else {
            echo "Invalid request method.";
        }
    } elseif ($action === 'login_account') {
        // Login to an account
        if ($_SERVER['REQUEST_METHOD'] === 'POST') {
            $email = $_POST['email'] ?? '';
            $password = $_POST['password'] ?? '';

            if (!empty($email) && !empty($password)) {
                $loginResult = loginAccount($conn, $email, $password);
                echo $loginResult;
            } else {
                echo "Login data missing.";
            }
        } else {
            echo "Invalid request method.";
        }
    } elseif ($action === 'get_user_data') {
		// Get user data
		if (isset($_GET['email'])) {
			$email = $_GET['email'];
			$userData = getUserData($conn, $email);
			if ($userData !== null) {
				echo json_encode($userData);
			} else {
				echo "User not found.";
			}
		} else {
			echo "Email parameter missing.";
		}
	} elseif ($action === 'get_news') {
        $news = getNews($conn);
        if ($news !== null) {
			echo json_encode($news);
		} else {
			echo "No news found.";
		}
    } else {
		echo "Invalid action.";
	}
} else {
    echo "Action parameter missing.";
}

// Close the database connection
$conn->close();
?>