<?php

	// Common Credentials
	$server_name = 'localhost';

	// Local Credentials
	$user_name = 'turain25';
  $password = 'rfantomg8!';
  $old_db = 'turain25_forms';
	$old_table = 'ap_form_13251';
	$new_db = 'turain25_maquilaDash';
	$new_table = 'jobs';

	$GLOBALS['conn'] = new mysqli($server_name, $user_name, $password, $old_db);

	// Check connection
  if ($GLOBALS['conn']->connect_error) {
		die("Connection to SQL server couldn't be initialize<br />ERROR:<br />{$conn->connect_error}");
	}

	echo 'START:<br />---<br />';

	$valids = array(
		'date_created' => 'created',
		'status' => 'active',
		'element_2' => 'title',
		'element_3' => 'description'
	);
	$skip_elements = TRUE;
	$skips = array(
		'element_1'
	);
	$results_data = array();

	$results = $GLOBALS['conn']->query("SELECT * FROM `{$old_table}` WHERE 1 ORDER BY `date_created` ASC");

	echo "RESULTS: " . $results->num_rows . "<br />------<br />";

	if ($results->num_rows > 0) {
		while($result = $results->fetch_assoc()) {
			$index = array_push($results_data, array()) - 1;

			echo "TRUE ID: {$index}<br /><br />";

			// Setup data
			$num = 0;
			foreach($result as $title => $data) {
				$data = trim(preg_replace('/\s\s+/', ' ', str_replace("'", "''", utf8_encode($data))));

				if (in_array($title, $skips)) {
					continue;
				} else if (array_key_exists($title, $valids) || (!$skip_elements && substr($title , 0, strlen('element_')) === 'element_')) {
					echo "{$title} : TRUE<br />";

					if (array_key_exists($title, $valids)) {
						$results_data[$index][$valids[$title]] = $data;
					}

					if (!$skip_elements && substr($title , 0, strlen('element_')) === 'element_') {
						$results_data[$index]['data']["id_{$num}"] = $data;
						$num += 1;
					}
				} else {
					echo "{$title} : FALSE<br />";
				}
			}

			echo '<br />DATA:<br />';
			print_r($results_data[$index]);
			echo '<br />------<br />';
		}
	}

	echo '<br /><br />START TRANSFERRING!!!<br />---<br /><br />';

	if ($GLOBALS['conn']->select_db($new_db) === FALSE) {
		die("Couldn't connect to the {$new_db} database!");
	} else {
		foreach($results_data as $result) {
			echo '<br />DATA:<br /> ';
			print_r($result);
			echo '<br /><br />';
			echo "SQL : INSERT INTO `{$new_table}` (`id`, `created`, `title`, `description`, `active`) VALUES (NULL, '{$result["created"]}', '{$result["title"]}', '{$result["description"]}', '{$result["active"]}')<br />";

			if ($GLOBALS['conn']->query("INSERT INTO `{$new_table}` (`id`, `created`, `title`, `description`, `active`) VALUES (NULL, '{$result["created"]}', '{$result["title"]}', '{$result["description"]}', '{$result["active"]}')") === TRUE) {
				echo "{$GLOBALS['conn']->insert_id} SUCCESS!!!";
			} else {
				echo 'FAILER!!!';
			}
			echo '<br />------<br />';
		}
	}

?>
