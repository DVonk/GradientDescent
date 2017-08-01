<?php
$servername = "localhost";
$username = "";
$password = "";
$dbname = "";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$c1 = ($_GET["c1"]);
$c2 = ($_GET["c2"]);
$c3 = ($_GET["c3"]);
$c4 = ($_GET["c4"]);
$c5 = ($_GET["c5"]);

$sql = "UPDATE ldjam39 SET hitcount = hitcount+1, c1 = c1+".$c1.", c2 = c2+".$c2.", c3 = c3+".$c3.", c4 = c4+".$c4.", c5 = c5+".$c5;
$conn->query($sql);

$sql = "SELECT * FROM ldjam39";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo $row["hitcount"]." ".$row["c1"]." ".$row["c2"]." ".$row["c3"]." ".$row["c4"]." ".$row["c5"];
}
}

?> 