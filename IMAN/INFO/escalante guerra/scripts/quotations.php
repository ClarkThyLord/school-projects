<?php

	// Common Credentials
	$server_name = 'localhost';

	// Local Credentials
	$user_name = 'turain25';
  $password = 'rfantomg8!';
  $old_db = 'turain25_forms';
	$old_table = 'ap_form_10775';
	$new_db = 'turain25_maquilaDash';
	$new_table = 'quotations';

	$GLOBALS['conn'] = new mysqli($server_name, $user_name, $password, $old_db);

	// Check connection
  if ($GLOBALS['conn']->connect_error) {
		die("Connection to SQL server couldn't be initialize<br />ERROR:<br />{$conn->connect_error}");
	}

	echo 'START:<br />---<br />';

	$valids = array(
		'date_created' => 'created',
		'status' => 'active',
		'element_1' => 'company name',
		'element_2' => 'job'
	);
	$skip_elements = FALSE;
	$skips = array(
	);
	$orders = array(
		'element_1',
		'element_2',
		'',
		'element_3',
		'element_4',
		'element_5',
		'semanal'
	);
	$results_data = array();

	$results = $GLOBALS['conn']->query("SELECT * FROM `{$old_table}` WHERE 1 ORDER BY `date_created` ASC");

	echo "RESULTS: " . $results->num_rows . "<br />------<br />";

	if ($results->num_rows > 0) {
		while($result = $results->fetch_assoc()) {
			$index = array_push($results_data, array()) - 1;

			echo "TRUE ID: {$index}<br /><br />";

			// Setup data
			foreach($result as $title => $data) {
				$data = trim(preg_replace('/\s\s+/', ' ', str_replace("'", "''", utf8_encode($data))));

				if (in_array($title, $skips)) {
					continue;
				} else if (array_key_exists($title, $valids) || (!$skip_elements && substr($title , 0, strlen('element_')) === 'element_')) {
					echo "{$title} : TRUE<br />";

					if (array_key_exists($title, $valids)) {
						$results_data[$index][$valids[$title]] = $data;
					}
				} else {
					echo "{$title} : FALSE<br />";
				}
			}

			$num = 0;
			$results_data[$index]['data'] = array();
			foreach($orders as $order) {
				if (isset($result[$order])) {
					$results_data[$index]['data']["id_{$num}"] = trim(preg_replace('/\s\s+/', ' ', str_replace("'", "''", utf8_encode($result[$order]))));
				} else {
					$results_data[$index]['data']["id_{$num}"] = 'desconocido';
				}

				$num += 1;
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
			echo "SQL : INSERT INTO `{$new_table}` (`id`, `created`, `company name`, `job`, `data`, `active`) VALUES (NULL, '{$result["created"]}', '{$result["company name"]}', '{$result["job"]}', '" . json_encode($result["data"]) . "', '{$result["active"]}')<br />";

			if ($GLOBALS['conn']->query("INSERT INTO `{$new_table}` (`id`, `created`, `company name`, `job`, `data`, `active`) VALUES (NULL, '{$result["created"]}', '{$result["company name"]}', '{$result["job"]}', '" . json_encode($result["data"]) . "', '{$result["active"]}')") === TRUE) {
				echo "{$GLOBALS['conn']->insert_id} SUCCESS!!!";
			} else {
				echo 'FAILER!!!';
			}
			echo '<br />------<br />';
		}
	}

?>
