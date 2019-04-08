<?php
	include_once 'C:\xampp\htdocs\playerStats\dbconn.php';

	//Connect to server
	$conn = mysqli_connect($server_name, $server_user, $server_password, $dbname);

	if (mysqli_connect_errno())
	{
		echo("1: DB Connection Failed"); // Error Code 1; No DB Connection
		exit();
	}

	$player_user = $_POST["usernamePOST"];
	$player_email = $_POST["emailPOST"];
	$player_pass = $_POST["paswordPOST"];

	//Check if User or Email exists
	$checkNameQuery = "SELECT username FROM users WHERE username ='".$player_user."';";

	$checkName = mysqli_query($conn, $checkNameQuery) or die ("2: Name Check Query Failed");

	if (mysqli_num_rows($checkName) >0) 
	{
		echo "3: Username in use, cannot register";
		exit();
	}

	//Add users to table with security functionality
	$salt = "\$5\$rounds=5000\$" . "pauliepocket" . $player_user . "\$";
	$hash = crypt($player_pass, $salt);
 	$sql = "INSERT INTO users (username, hash, salt, email) VALUES ('".$player_user."','".$hash."','".$salt."','".$player_email."')";
	$result = mysqli_query($conn, $sql) or die ("4: Add user failed");

	echo("0");
?>