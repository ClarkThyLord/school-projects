<?php

	// Load configs of Landmarks on load of this file
	// **********
	$landmarksConfig = loadConfig();
	$landmarksAtlas = loadAtlas();

	/**
	* Loads config of landmarks.
	* @return {dictionary} Returns a dictionary.
	*/
	function loadConfig() {
	
		// Load from config.json file; place the @ to skip errors
		$config = @json_decode(file_get_contents("./landmarks/config/config.json"), true);
			
		if ($config === null){ // If load wasn't possible assume we have to create a new config
			$config = array(
		
				"keys_count" => 0
		
			);
		
			$fp = fopen("./landmarks/config/config.json", 'w');
			fwrite($fp, json_encode($config));
			fclose($fp);
		}
		
		$GLOBALS["response"]["extra"]["landmarksConfig"] = $config;
		
		return $config;
	
	}

	/**
	* Save config of landmarks.
	* @return {boolean} True, save was possible, False, save wasn't possible.
	*/
	function saveConfig() {
    
		try{ // Save config.json
			$fp = fopen("./landmarks/config/config.json", 'w');
			fwrite($fp, json_encode($GLOBALS["landmarksConfig"]));
			fclose($fp);
			
			return true;
		}
		catch(Exception $error){ // Save wasn't possible send back false
			return false;
		}
		
		$GLOBALS["response"]["extra"]["landmarksConfig"] = $GLOBALS["landmarksConfig"];
	
	}

	/**
	* Loads atlas of landmarks.
	* @return {array} Returns an array.
	*/
	function loadAtlas() {
	
		// Load from atlas.json file; place the @ to skip errors
		$atlas = @json_decode(file_get_contents("./landmarks/config/atlas.json"), true);
			
		if ($atlas === null){ // If load wasn't possible assume we have to create a new atlas
			$atlas = array();
		
			$fp = fopen("./landmarks/config/atlas.json", 'w');
			fwrite($fp, json_encode($atlas));
			fclose($fp);
		}
		
		$GLOBALS["response"]["extra"]["landmarksAtlas"] = $atlas;
		
		return $atlas;
	
	}

	/**
	* Save atlas of landmarks.
	* @return {Boolean} True, save was possible, False, save wasn't possible.
	*/
	function saveAtlas() {
    
		try{ // Save atlas.json
			$fp = fopen("./landmarks/config/atlas.json", 'w');
			fwrite($fp, json_encode($GLOBALS["landmarksAtlas"]));
			fclose($fp);
			
			return true;
		}
		catch(Exception $error){ // Save wasn't possible send back false
			return false;
		}
		
		$GLOBALS["response"]["extra"]["landmarksAtlas"] = $GLOBALS["landmarksAtlas"];
	
	}

	/**
	* Get a valid unused key.
	* @return {integer} Returns a valid unused key.
	*/
	function keyGet() {
	
		$result = $GLOBALS["landmarksConfig"]["keys_count"];
	
		$GLOBALS["response"]["extra"]["result"] = $result;
	
	}

	/**
	* Load a JSON and all it's assets.
	* @return {dictionary/false} Returns a dictionary.
	*/
	function landmarksGet() {
	
		// Load from config.json file; place the @ to skip errors
		$result = @json_decode(file_get_contents("./landmarks/jsons/" . $_GET["key"] . ".json"), true);
			
		if ($result === null){ // If load wasn't possible assume data isn't there
			$GLOBALS["response"]["extra"]["result"] = false;
			return;
		}
		
		$GLOBALS["response"]["extra"]["result"] = $result;
	
	}

	/**
	* Save JSON and all it's assets.
	* @return {integer/boolean} Integer, JSON and assets saved successfully to this integer ID; False JSON or assets weren't saved successfully.
	*/
	function landmarksPost() {
	
		try{ // Save JSON and files
            
            $id = $GLOBALS["landmarksConfig"]["keys_count"]; // ID we're using
            $GLOBALS["landmarksConfig"]["keys_count"]++; // Increment the value to prevent usage
            saveConfig();
            array_push($GLOBALS["landmarksAtlas"], array(
            "id" => $id,
            "name" => $_GET["name"]
            )); // Add new data to atlas
            saveAtlas();
            
            // FOR DEBUGGING
            $GLOBALS["response"]["extra"]["post"] = $_POST;
            $GLOBALS["response"]["extra"]["files"] = $_FILES;
            
            foreach ($_FILES as $file){ // Save files
                $realFile = file_get_contents($file["tmp_name"]);
                file_put_contents("./landmarks/" . $file["name"], $realFile);
            }
            
			$fp = fopen("./landmarks/jsons/$id.json", 'w');
			
			fwrite($fp, json_encode(array( // This is the JSON structure
			
				"version" => "1.0.0",
				"id" => $id,
				"name" => $_GET["name"],
				"classification" => $_GET["classification"],
				"latitude" => $_GET["latitude"],
				"longitude" => $_GET["longitude"],
				"summary" => $_GET["summary"],
				"images" => json_decode($_GET["files"]),
                "videos" => json_decode($_GET["urls"])
			
			)));
			fclose($fp);
            
		}
		catch(Exception $error){ // Save wasn't possible so send back false
			$GLOBALS["response"]["extra"]["result"] = false;
			return;
		}
		
        // Send back the ID to which data was saved to
		$GLOBALS["response"]["extra"]["result"] = $id;
	
	}

	/**
	* Save JSON and all it's assets.
	* @return {integer/boolean} True, data has been modified sucesfully; False, data hasn't been modified sucesfully.
	*/
	function landmarksPatch() {
	
		try{
			
            // Load from config.json file; place the @ to skip errors
            $result = @json_decode(file_get_contents("./landmarks/jsons/" . $_GET["key"] . ".json"), true);

            if ($result === null){ // If load wasn't possible assume data isn't there
                $GLOBALS["response"]["extra"]["result"] = false;
                return;
            }
            
            foreach ($_GET as $patch => $value){ // Update landmark accordingly
                if ($patch == "name"){
					// Update name in data
					$result[$patch] = $value;
					
					// Update landmark name at atlas
					for ($num = 0; $num < (count($GLOBALS["landmarksAtlas"])); $num++){
						if ($GLOBALS["landmarksAtlas"][$num]["id"] == $_GET["key"]){
							$GLOBALS["landmarksAtlas"][$num]["name"] = $value;
							saveAtlas();
						}
					}
				}
				else if ($patch == "urls"){
                    
                    foreach (json_decode($value) as $nUrl){
                        
						// Check if this url isn't already in data
						$save = true;
                        foreach ($result["videos"] as $sUrl){
							if ($sUrl === $nUrl){
								$save = false;
								break;
							}
						}
                        
						if ($save == false){
							continue;
						}
                        
						// Add file to data
						$result["videos"][] = $nUrl;
                    }
                        
                }
				else if ($patch == "files"){
                    // FOR DEBUGGING
                    $GLOBALS["response"]["extra"]["post"] = $_POST;
                    $GLOBALS["response"]["extra"]["files"] = $_FILES;
                    
					// Save files
                    foreach ($_FILES as $nFile){
						// Save the file
                        $rFile = file_get_contents($nFile["tmp_name"]);
                        file_put_contents("./landmarks/" . $nFile["name"], $rFile);
                    }
                    
                    // Add files to data
                    foreach (json_decode($value) as $nFile){
                        
						// Check if this file isn't already in data
						$save = true;
                        foreach ($result["files"] as $sFile){
							if ($sFile[1] === $nFile[1]){
								$save = false;
								break;
							}
						}
                        
						if ($save == false){
							continue;
						}
                        
						// Add file to data
						$result["files"][] = $nFile;
                    }
                    
                }
                else if($patch != "key"){ // Update value to everything but key/id
                    $result[$patch] = $value;
                }
            }
            
            // Save modified landmark
            $fp = fopen("./landmarks/jsons/" . $_GET["key"] . ".json", 'w');
			fwrite($fp, json_encode($result));
			fclose($fp);
            
		}
		catch(Exception $error){ // Save wasn't possible so send back false
			$GLOBALS["response"]["extra"]["result"] = false;
			return;
		}
		
        // Send back the ID to which data was saved to
		$GLOBALS["response"]["extra"]["result"] = $_GET["key"];
        
    }

	/**
	* Save JSON and all it's assets.
	* @return {integer/boolean} True, data has been deleted sucesfully; False, data hasn't been deleted sucesfully.
	*/
	function landmarksDelete() {
	
		try{ // Remove landmakr from everything
            
            // Load the data
            $result = @json_decode(file_get_contents("./landmarks/jsons/" . $_GET["key"] . ".json"), true);

            if ($result === null){ // If load wasn't possible assume data isn't there
                $GLOBALS["response"]["extra"]["result"] = false;
                return;
            }
			
			// If there are assets to remove
			if ($result["files"] != []){
				// Remove each asset of landmark
				foreach ($result["files"] as $file){
					unlink("./landmarks/" . $file[1]);
				}
			}
            
            // Remove landmark file
            $result = @unlink("./landmarks/jsons/" . $_GET["key"] . ".json");
            
			// Remove landmark from atlas
            for ($num = 0; $num < (count($GLOBALS["landmarksAtlas"])); $num++){
                if ($GLOBALS["landmarksAtlas"][$num]["id"] == $_GET["key"]){
                    unset($GLOBALS["landmarksAtlas"][$num]);
                }
            }
            saveAtlas();
            
		}
		catch(Exception $error){ // Save wasn't possible so send back false
			$GLOBALS["response"]["extra"]["result"] = false;
			return;
		}
		
        // Send back the ID to which data was saved to
		$GLOBALS["response"]["extra"]["result"] = $_GET["key"];
        
    }
	
?>