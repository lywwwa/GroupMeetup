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
       	$result = mysqli_query($con,"SELECT * FROM tblConnections where user1_id=$u1 and user2_id=$u2");
       	$row = mysqli_fetch_array($result);
       	if($row != NULL) echo "$row[3]";
       	else echo "null";
    }
}
else{
    echo "No user1.";
}
mysqli_close($con);
?>