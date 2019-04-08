<?php
	include_once 'C:\xampp\htdocs\playerStats\dbconn.php';

	$playerUser = $_POST["username"];
	$playerpPass = $_POST["password"];
	
	//Check if user exists
	$LoginCheckQuery = "SELECT username, salt, hash FROM users WHERE username='" .$playerUser. "';";

	$checkName = mysqli_query($conn, $LoginCheckQuery) or die ("Name Query Failed");

	if (mysqli_num_rows($checkName) == 0) 
	{
		echo "5: No username exists";
		exit();
	}
	else if (mysqli_num_rows($checkName) > 1)
	{
		echo "6: More than 1 user acoounts exists";
		exit();
	}

	//Get login info
	$loginInfo = mysqli_fetch_assoc($checkName);
	$salt = $loginInfo["salt"];
	$hash = $loginInfo["hash"];

	$loginHash = crypt($playerpPass, $salt);
	if ($hash != $loginHash) 
	{
		echo "7: Incorrect Password";
		exit();
	}

	echo "0\t";
?>