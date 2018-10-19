<?php
$con=mysqli_connect("localhost","id6879497_group5app","password12345","id6879497_meetup_app");
if(isset($_GET['username'])){
    $username = $_GET['username'];
    if (mysqli_connect_errno($con))
    {
       echo "Failed to connect to MySQL: " . mysqli_connect_error();
    }
    else
    {
       	$result = mysqli_query($con,"SELECT * FROM tblUser where username='$username'");
       	$row = mysqli_fetch_array($result);
       	echo $row[0] . "/" . $row[1] . "/" . $row[2] . "/" . $row[3];
    }
}
mysqli_close($con);
?>