<?php

	include_once 'C:\xampp\htdocs\playerStats\dbconn.php';

	if (mysqli_connect_errno())
	{
		echo("1: DB Connection Failed"); // Error Code 1; No DB Connection
		exit();
	}

	$player_user = $_POST["usernamePOST"];
	$player_lap_time = $_POST["lapTimePOST"];
	$ranking = $_POST["rankingPOST"];

	//Check if User or Email exists
	$insertStatsQuery = "INSERT INTO gamestats (username, lap_time, ranking) VALUES ('".$player_user."', '".$player_lap_time."' , '".$ranking."')";
	$result = mysqli_query($conn, $insertStatsQuery) or die ("8: User data failed to send");
?>