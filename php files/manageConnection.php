<?php
$date = new DateTime('now');
$date->setTimezone(new DateTimeZone('UTC'));
$fulldate = $date->format('YmdHis');

$con=mysqli_connect("localhost","id6879497_group5app","password12345","id6879497_meetup_app");
if(isset($_GET['user1'])){
    $u1 = $_GET['user1'];
    $u2 = $_GET['user2'];
    $actionType = $_GET['action'];
    if (mysqli_connect_errno($con))
    {
       echo "Failed to connect to MySQL: " . mysqli_connect_error();
    }
    else
    {
        if($actionType == "add"){
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u1");
            $row = mysqli_fetch_array($result);
            $notif_sender = $row[3];
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u2");
            $row = mysqli_fetch_array($result);
            $notif_receiver = $row[3];
            $result = mysqli_query($con,"SELECT * FROM tblConnections WHERE user1_id=$u1 AND user2_id=$u2");
           	$row = mysqli_fetch_array($result);
           	if(is_null($row)){
                $result = mysqli_query($con,"SELECT * FROM tblConnections WHERE user1_id=$u2 AND user2_id=$u1");
           	    $row = mysqli_fetch_array($result);
           	    if(is_null($row)){
           	        $sql = "INSERT INTO tblConnections (user1_id, user2_id, connectionStatus, latestAction, created_at, updated_at) VALUES ($u1, $u2, 0, 1, $fulldate, $fulldate)";
           	        if (mysqli_query($con,$sql)){
                        $sql =  "INSERT INTO tblNotifications (user_id, notification_content, notification_type, read_check, created_at, updated_at) VALUES ($u2, '$notif_sender added you as a friend.', 'request', 1, $fulldate, $fulldate)";
                        if (mysqli_query($con,$sql)){
                            echo "success";
                        }
           	        }
                }
                else{
                    $sql = "UPDATE tblConnections SET user1_id=$u1, user2_id=$u2, latestAction=1, updated_at=$fulldate WHERE user1_id=$u2 AND user2_id=$u1";
                    if (mysqli_query($con,$sql)){
                        $sql =  "UPDATE tblNotifications SET user_id=$u2, notification_content='$notif_sender added you as a friend.', updated_at=$fulldate, isDeleted=0 WHERE notification_content LIKE '$notif_receiver added%'" ;
                        if (mysqli_query($con,$sql)){
                            echo "success";
                        }
                    }
                }
            }
            else{
                $sql = "UPDATE tblConnections SET latestAction=1, updated_at=$fulldate WHERE user1_id=$u1 AND user2_id=$u2";
                if (mysqli_query($con,$sql)){
                    $sql =  "UPDATE tblNotifications SET updated_at=$fulldate, isDeleted=0 WHERE notification_content LIKE '$notif_sender added%' AND user_id=$u2";
                        if (mysqli_query($con,$sql)){
                            echo "success";
                        }
                }
            }
        }
        
        else if($actionType == "cancel"){
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u1");
            $row = mysqli_fetch_array($result);
            $notif_sender = $row[3];
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u2");
            $row = mysqli_fetch_array($result);
            $notif_receiver = $row[3];
            $sql = "UPDATE tblConnections SET latestAction=0, updated_at=$fulldate WHERE user1_id=$u1 AND user2_id=$u2";
            if (mysqli_query($con,$sql)){
                $sql = "UPDATE tblNotifications SET isDeleted=1, updated_at=$fulldate WHERE user_id=$u2 AND notification_content LIKE '$notif_sender added%'";
                if (mysqli_query($con,$sql)){
                    echo "success";
                }
            }
        }
        
        else if($actionType == "accept"){
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u1");
            $row = mysqli_fetch_array($result);
            $notif_sender = $row[3];
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u2");
            $row = mysqli_fetch_array($result);
            $notif_receiver = $row[3];
            $sql = "UPDATE tblConnections SET connectionStatus=1, updated_at=$fulldate WHERE user1_id=$u2 AND user2_id=$u1";
            if (mysqli_query($con,$sql)){
                $sql = "UPDATE tblNotifications SET isDeleted=1, updated_at=$fulldate WHERE user_id=$u2 AND notification_content LIKE '$notif_sender added%'";
                if (mysqli_query($con,$sql)){
                    echo "success";
                }
            }
        }
        
        else if($actionType == "reject"){
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u2");
            $row = mysqli_fetch_array($result);
            $notif_sender = $row[3];
            $result = mysqli_query($con,"SELECT * FROM tblUser WHERE id=$u1");
            $row = mysqli_fetch_array($result);
            $notif_receiver = $row[3];
            
            $result = mysqli_query($con,"SELECT * FROM tblConnections WHERE user1_id=$u1 AND user2_id=$u2");
            $row = mysqli_fetch_array($result);
            if(is_null($row)){
                $sql = "UPDATE tblConnections SET connectionStatus=0, latestAction=2, updated_at=$fulldate WHERE user1_id=$u2 AND user2_id=$u1";
                if (mysqli_query($con,$sql)){
                    $sql = "UPDATE tblNotifications SET isDeleted=1, updated_at=$fulldate WHERE user_id=$u1 AND notification_content LIKE '$notif_sender added%'";
                    if (mysqli_query($con,$sql)){
                        echo "success";
                    }
                }
            }
            else{
                $sql = "UPDATE tblConnections SET connectionStatus=0, latestAction=2, updated_at=$fulldate WHERE user1_id=$u1 AND user2_id=$u2";
                if (mysqli_query($con,$sql)){
                    $sql = "UPDATE tblNotifications SET isDeleted=1, updated_at=$fulldate WHERE user_id=$u1 AND notification_content LIKE '$notif_sender added%'";
                    if (mysqli_query($con,$sql)){
                        echo "success";
                    }
                }
            }
        }
        
        else if($actionType == "remove"){
            $result = mysqli_query($con,"SELECT * FROM tblConnections WHERE user1_id=$u1 AND user2_id=$u2");
           	$row = mysqli_fetch_array($result);
           	if(is_null($row)){
                $sql =  "UPDATE tblConnections SET connectionStatus=0, latestAction=3, updated_at=$fulldate WHERE user1_id=$u2 AND user2_id=$u1";
                if (mysqli_query($con,$sql)){
                    echo "success";
                }
            }
            else{
                $sql =  "UPDATE tblConnections SET connectionStatus=0, latestAction=3, updated_at=$fulldate WHERE user1_id=$u1 AND user2_id=$u2";
                if (mysqli_query($con,$sql)){
                    echo "success";
                }
            }
        }
    }
}
else{
    echo "No user1.";
}
mysqli_close($con);
?>