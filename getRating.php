<?php

	include_once 'C:\xampp\htdocs\playerStats\dbconn.php';

	$playerUser = $_POST["username"];

	$sql = "SELECT ranking FROM gamestats WHERE username = '" .$playerUser. "' GROUP BY ranking ORDER BY COUNT(*) DESC LIMIT 1 ;";

	$result = $conn->query($sql);

	$row = $result->fetch_array(MYSQLI_NUM);
	printf ("%s\n", $row[0]);
?>