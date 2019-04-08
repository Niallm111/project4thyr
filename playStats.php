<?php
	$server_name = "localhost";
	$server_user = "root";
	$server_password = "";
	$dbname = "playerstats";
	
	//Connect to server
	$conn = new mysqli($server_name, $server_user, $server_password, $dbname);
	
	//If connection fails, print fail message
	if (!$conn) {
		die("Connection Failed. ". mysqli_connect_error());
	}
	$sql = "SELECT ID, lap_time FROM playStats";
	$result = mysqli_query($conn, $sql);
	
	if (mysqli_num_rows($result) > 0) {
		while($row = mysqli_fetch_assoc($result)){
			echo "ID:".$row['ID'] . "Lap Time:".$row['lap_time']. "<br><br>";
		}
	}
?>