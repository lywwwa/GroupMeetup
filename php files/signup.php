<?php
$date = new DateTime('now');
$date->setTimezone(new DateTimeZone('UTC'));
$fulldate = $date->format('YmdHis');

$con=mysqli_connect("localhost","id6879497_group5app","password12345","id6879497_meetup_app");
if(isset($_GET['username'])){
    $username = $_GET['username'];
    $password = $_GET['password'];
    $firstname = $_GET['firstname'];
    $lastname = $_GET['lastname'];
    if (mysqli_connect_errno($con))
    {
       echo "Failed to connect to MySQL: " . mysqli_connect_error();
    }
    else
    {
       	$result = mysqli_query($con,"SELECT * FROM tblUser where username='$username'");
       	$row = mysqli_fetch_array($result);
       	if(is_null($row)){
            $sql =  "INSERT INTO tblUser (username, password, firstName, lastName, created_at, updated_at) VALUES ('$username', '$password', '$firstname', '$lastname', $fulldate, $fulldate)";
            if (mysqli_query($con,$sql)){
                echo "success";
            }
        }
        else{
          echo "Username already exists.";
        }
       	/*$hash = $row[2];
        if (password_verify($password, $hash)) {
            http_response_code(200);
        } else {
            echo 'Invalid password.';
        }*/
    }
}
mysqli_close($con);
?>