<?php
$con=mysqli_connect("localhost","id6879497_group5app","password12345","id6879497_meetup_app");
if(isset($_GET['id'])){
    $id = $_GET['id'];
    if (mysqli_connect_errno($con))
    {
       echo "Failed to connect to MySQL: " . mysqli_connect_error();
    }
    else
    {
       	$result = mysqli_query($con,"SELECT * FROM tblNotifications WHERE user_id = $id AND isDeleted = 0 ORDER BY id DESC");
       	//$row = mysqli_fetch_array($result);
       	//echo $row[1] . "/" . $row[2] . "/" . $row[3];
       	$searchRes = "";
       	while($row = mysqli_fetch_array($result)){
       	    $searchRes = $searchRes.$row[0] . "/" . $row[1] . "/" . $row[2] . "/" . $row[3] . "/" . $row[4] . "/" . $row[5] . "\n";
       	}
       	echo $searchRes;
    }
}
mysqli_close($con);
?>