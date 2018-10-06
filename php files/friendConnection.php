<?php
$con=mysqli_connect("localhost","id6879497_group5app","password12345","id6879497_meetup_app");
if(isset($_GET['user1'])){
    $u1 = $_GET['user1'];
    $u2 = $_GET['user2'];
    if (mysqli_connect_errno($con))
    {
       echo "Failed to connect to MySQL: " . mysqli_connect_error();
    }
    else
    {
       	$result = mysqli_query($con,"SELECT * FROM tblConnections WHERE user1_id=$u1 AND user2_id=$u2");
       	$row = mysqli_fetch_array($result);
       	if($row != NULL) echo "$row[2]";
       	else{
       	    $result = mysqli_query($con,"SELECT * FROM tblConnections WHERE user1_id=$u2 AND user2_id=$u1");
           	$row = mysqli_fetch_array($result);
           	if($row != NULL) echo "$row[2]";
           	else echo "0";
       	}
    }
}
else{
    echo "No user1.";
}
mysqli_close($con);
?>