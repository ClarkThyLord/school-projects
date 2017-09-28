// Variables
var debugging = true,
key, // Unique key
url = (window.location).protocol + "//" + (window.location).host + "/" + (window.location).pathname.split('/')[1],
server = "server.php",
fileObject;

window.onload = function () { 

	// Disable page until key is given
	pageDisable(true);
	
	if (generateKey() === false){
		return;
	}
	
	pageDisable(false);

}

/**
 * Check if the id given in DOM Form object is valid.
 * @return {boolean} Returns true if valid; else false.
 */
function checkId(){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["id"];
	
	if (isNaN(element.value) == true || element.value == ""){ // If the name isn't just a integer/float
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "ID elegido no es válido";
		
		return false;
	}
	else{
		// Mark input with green and display valid msg
		element.style.borderColor = "green";
		element.title = "válido";
		
		return true;
	}

}

/**
 * Check if the name given in DOM Form object is valid.
 * @return {boolean} Returns true if valid; else false.
 */
function checkName(){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["name"];
	
	if (isNaN(element.value) == false){ // If the name is just a integer/float
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el nombre no puede ser un número";
		
		return false;
	}
	else if(element.value.length < 3){ // If the name is less then 3 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el nombre debe tener menos de 3 caracteres";
		
		return false;
	}
	else if(element.value.length > 64){ // If the name is more then 64 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el nombre debe tener mas de 64 caracteres";
		
		return false;
	}
	else{
		// Mark input with green and display valid msg
		element.style.borderColor = "green";
		element.title = "válido";
		
		return true;
	}

}

/**
 * Check if the latitude given in DOM Form object is valid.
 * @return {boolean} Returns true if valid; else false.
 */
function checkLatitude(){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["latitude"];
	
	if (isNaN(element.value) == true){ // If the latitude isn't a integer/float
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "la latitude no puede ser un texto";
		
		return false;
	}
	else if(element.value.length < 1){ // If the latitude is less then 3 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el latitude debe tener menos de 1 caracteres";
		
		return false;
	}
	else if(element.value.length > 12){ // If the latitude is more then 64 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el latitude no debe tener mas de 12 caracteres";
		
		return false;
	}
	else{
		// Mark input with green and display valid msg
		element.style.borderColor = "green";
		element.title = "válido";
		
		return true;
	}

}

/**
 * Check if the longitude given in DOM Form object is valid.
 * @return {boolean} Returns true if valid; else false.
 */
function checkLongitude(){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["longitude"];
	
	if (isNaN(element.value) == true){ // If the namelongitude isn't a integer/float
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "la longitude no puede ser un texto";
		
		return false;
	}
	else if(element.value.length < 1){ // If the longitude is less then 3 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el longitude debe tener menos de 1 caracteres";
		
		return false;
	}
	else if(element.value.length > 12){ // If the longitude is more then 64 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el longitude no debe tener mas de 12 caracteres";
		
		return false;
	}
	else{
		// Mark input with green and display valid msg
		element.style.borderColor = "green";
		element.title = "válido";
		
		return true;
	}

}

/**
 * Check if the summary given in DOM Form object is valid.
 * @return {boolean} Returns true if valid; else false.
 */
function checkSummary(){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["summary"];
	
	if (isNaN(element.value) == false){ // If the summary is just a integer/float
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el extracto no puede ser un número";
		
		return false;
	}
	else if(element.value.length < 12){ // If the summary is less then 12 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el extracto debe tener mas de 12 caracteres";
		
		return false;
	}
	else if(element.value.length > 1024){ // If the summary is more then 1024 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el extracto debe tener menos de 1024 caracteres";
		
		return false;
	}
	else{
		// Mark input with green and display valid msg
		element.style.borderColor = "green";
		element.title = "válido";
		
		return true;
	}

}

/**
 * Check if the urls given in DOM Form object is valid.
 * @return {boolean} Returns true if valid; else false.
 */
function checkUrls(){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["urls"];
	
	if(element.value.length > 2048){ // If the summary is more then 1024 characters
		// Mark input as red and display error msg
		element.style.borderColor = "red";
		element.title = "el extracto debe tener menos de 2048 caracteres";
		
		return false;
	}
	else{
		// Mark input with green and display valid msg
		element.style.borderColor = "green";
		element.title = "válido";
		
		return true;
	}

}

/**
 * Update file area.
 * @return {undefined} Returns nothing.
 */
function updateFileArea(){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["fileInput"];
	// What to update with
    var update = "";
    
	// If there are "files" is present do the following
	if ("files" in element){
        if (element.files.length == 0){ // If there are no files selected
            telementt = "<i>no se seleccionaron archivos</i>";
        } else { // There are files
            for (var num = 0; num < element.files.length; num++) { // List each file
                update += "<br><strong>" + (num + 1) + ". Archivo:</strong><br>";
                var file = element.files[num]; // Reference to file
				update += "Nombre: " + file.name;
				update += "<br>";
				// update += '<input type="button" value="x" onclick="removeFile(' + file.name + ');> <br>'; // To remove file no support for this >_>
				update += "Tamaño: " + Math.round((file.size / 1000000) * 100) / 100 + " MB";
            }
        }
    } 
    else {
        if (element.value == "") {
            update += "<i>no se seleccionaron archivos</i>";
        } else {
            update += "<b>la carga de archivos no es compatible con su navegador!</b>";
        }
    }
	
    document.getElementById("fileArea").innerHTML = update;

}

/**
 * Remove a file by it's name. !!!PROBABLY WON'T WORK!!!
 * @param {string} The name of the file.
 * @return {undefined} Returns nothing.
 */
function removeFile(name){

	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["fileInput"];

	if (name == undefined){
		return;
	}
	
	if ("files" in element){
        if (element.files.length == 0){ // If there are no files selected
            telementt = "<i>no se seleccionaron archivos</i>";
        } else { // There are files
            for (var num = 0; num < element.files.length; num++) { // List each file
                var file = element.files[num]; // Reference to file
				if (file.name == name){ // If element has the name
					delete element.files[num]; 
				}
            }
        }
    } 
    else { // No support for files
        return;
    }

}

/**
 * Check and POST landmark object to server.
 * @return {undefined} Returns nothing.
 */
function formSubmit(){
	
	// Check that all things are valid
	if (checkId() == false|| checkName() == false || checkLatitude() == false || checkLongitude() == false || checkSummary() == false || checkUrls() == false){
	
		// Double check to warn user
        checkId();
		checkName();
		checkLatitude();
		checkLongitude();
		checkSummary();
        checkUrls();
		
        // If we're debugging don't return
		if (debugging == false){
			return;
		}
	
	}

	// Disable the page
	pageDisable(true);

    // Get URLS
    var urlNames = (document.getElementById("landmarkForm").elements["urls"].value).split(",");
    
    var realUrlNames = [];
    for (var num = 0; (urlNames.length) > num; num++){
        if (urlNames[num] === ""){
            continue;
        }
        else{
            realUrlNames.push(urlNames[num].replace(" ", ""));
        }
    }
    
	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["fileInput"];
    var fileNames = [];
    var fileObjects = new FormData();

	if ("files" in element){
        for (var num = 0; num < element.files.length; num++){
            var file = element.files[num]; // Reference to file
            
            // Add file url and name
            fileNames.push(
			url + "/landmarks/" + file.name, // File URL
            );
			// Add file object
            fileObjects.append(file.name, file);
        }
    }
	
    if (fileNames == []){ // Set-up with a empty list if no files
        fileNames = "&files=[]";
    }
    
    try{
	
		var xmlHttp = new XMLHttpRequest();
		xmlHttp.open("POST", server + "/landmarks?" +
		"key=" + document.getElementById("landmarkForm").elements["id"].value +
        "&name=" + document.getElementById("landmarkForm").elements["name"].value +
		"&classification=" + document.getElementById("landmarkForm").elements["classification"].value +
		"&latitude=" + document.getElementById("landmarkForm").elements["latitude"].value +
		"&longitude=" + document.getElementById("landmarkForm").elements["longitude"].value +
		"&summary=" + document.getElementById("landmarkForm").elements["summary"].value +
		"&files=" + JSON.stringify(fileNames) + 
        "&urls=" + JSON.stringify(realUrlNames)
		, false);
		
		// When the state of the response changes do the following
		xmlHttp.onreadystatechange = function () {
			if(xmlHttp.readyState === XMLHttpRequest.DONE && xmlHttp.status === 200) { // When response is nice and done do the following
				var json = JSON.parse(xmlHttp.responseText);
                
                // Update notice to user
                if (json.extra.result === false){
					updateNotice("<h2>Error Al Enviar Archivo!</h2>");
                }
                else{
                    updateNotice("<h2>Archivo Enviado y Guardado ID: " + json.extra.result + "!</h2>");
                }
				
				// Clean up form
				formReset();
				if (generateKey() === false){
					return;
				}
				
				pageDisable(false);
			}
		};
		
        // Send the file objects with POST
		xmlHttp.send(fileObjects);
		
	}
	catch(error){
		updateNotice("<h2>Error Al Enviar Archivo!</h2>");
		pageDisable(false);
	}
    

}

/**
 * Update a landmark object with PATCH request to server.
 * @return {undefined} Returns nothing.
 */
function formUpdate(){
    
    var updates = "";
    
    // Check that a valid id is given
    if (checkId() == false){
        return;    
    }
    
    // Check what's valid to update with
    if (checkName() == true){
        updates += "&name=" + document.getElementById("landmarkForm").elements["name"].value;
    }
    if (checkLatitude() == true){
        updates += "&latitude=" + document.getElementById("landmarkForm").elements["latitude"].value;
    }
    if (checkLongitude() == true){
        updates += "&longitude=" + document.getElementById("landmarkForm").elements["longitude"].value;
    }
    if (checkSummary() == true){
        updates += "&summary=" + document.getElementById("landmarkForm").elements["summary"].value;
    }
    if (checkUrls() == true){
        // Get URLS
        var urlNames = (document.getElementById("landmarkForm").elements["urls"].value).split(",");

        var realUrlNames = [];
        for (var num = 0; (urlNames.length) > num; num++){
            if (urlNames[num] === ""){
                continue;
            }
            else{
                realUrlNames.push(urlNames[num].replace(" ", ""));
            }
        }
        updates += "&urls=" + JSON.stringify(realUrlNames);
    }

	// Disable the page
	pageDisable(true);
	// Make a reference to the DOM element
	var element = document.getElementById("landmarkForm").elements["fileInput"];
	var fileObjects = new FormData();
    
    if (element.files.length > 0){ // If files are given to update with
		var fileNames = [];
		for (var num = 0; num < element.files.length; num++){
			var file = element.files[num]; // Reference to file
			fileNames.push(
				url + "/landmarks/" + file.name, // File URL
                );
			fileObjects.append(file.name, file);
		}
		// Add to updates
		updates += "&files=" + JSON.stringify(fileNames);
    }
    
    if (updates == ""){ // If nothing is valid to update
        pageDisable(false);
        return;
    }
    
    try{
	
		var xmlHttp = new XMLHttpRequest();
		xmlHttp.open("POST", server + "/landmarks/update?" +
		"key=" + document.getElementById("landmarkForm").elements["id"].value +
		"&classification=" + document.getElementById("landmarkForm").elements["classification"].value +
        updates
		, false);
		
		// When the state of the response changes do the following
		xmlHttp.onreadystatechange = function () {
			if(xmlHttp.readyState === XMLHttpRequest.DONE && xmlHttp.status === 200) { // When response is nice and done do the following
				var json = JSON.parse(xmlHttp.responseText);
                
                // Update notice to user
                if (json.extra.result === false){
					updateNotice("<h2>Error Al Intentar Actualizar Objeto!</h2>");
                }
                else{
                    updateNotice("<h2>Objeto Actualizado ID: " + json.extra.result + "!</h2>");
                }
				
				// Clean up form
				formReset();
				if (generateKey() === false){
					return;
				}
				
				pageDisable(false);
			}
		};
		
		xmlHttp.send(fileObjects);
		
	}
	catch(error){
		updateNotice("<h2>Error Al Intentar Actualizar Objeto!</h2>");
		pageDisable(false);
	}
    

}

/**
 * Delete a landmark object from server with DELETE request.
 * @return {undefined} Returns nothing.
 */
function formDelete(){
	
	// Check if ID is invalid and that we aren't in debugging mode
	if (checkId() == false){
        return;
	}

	// Disable the page
	pageDisable(true);
    
    try{
	
		var xmlHttp = new XMLHttpRequest();
		xmlHttp.open("DELETE", server + "/landmarks?key=" + document.getElementById("landmarkForm").elements["id"].value, false);
		
		// When the state of the response changes do the following
		xmlHttp.onreadystatechange = function () {
			if(xmlHttp.readyState === XMLHttpRequest.DONE && xmlHttp.status === 200) { // When response is nice and done do the following
				var json = JSON.parse(xmlHttp.responseText);
                
                // Update notice to user
                if (json.extra.result === false){
					updateNotice("<h2>Error Al Intentar Eliminar El Objeto!</h2>");
                }
                else{
                    updateNotice("<h2>Objeto Eliminado ID: " + json.extra.result + "!</h2>");
                }
				
				// Clean up form
				formReset();
				if (generateKey() === false){
					return;
				}
				
				pageDisable(false);
			}
		};
		
        // Send the file objects with POST
		xmlHttp.send();
		
	}
	catch(error){
		updateNotice("<h2>Error Al Intentar Eliminar El Objeto!</h2>");
		pageDisable(false);
	}
    

}

/**
 * Reset all form elements.
 * @return {undefined} Returns nothing.
 */
function formReset(){

	document.getElementById("landmarkForm").reset();
	document.getElementById("fileArea").innerHTML = "";
	
	// Mark things accordingly
    checkId();
	checkName();
	checkLatitude();
	checkLongitude();
	checkSummary();

}

/**
 * Disable all page elements.
 * @param {value} Value to update to; boolean.
 * @return {undefined} Returns nothing.
 */
function pageDisable(value){

	if (value in [false, true]){
		document.getElementById("landmarkForm").elements["page"].disabled = value;
	}
	else{
		document.getElementById("landmarkForm").elements["page"].disabled = !document.getElementById("landmarkForm").elements["page"].disabled;
	}

}

/**
 * Retrieves a valid unused key and updates DOM object representing key.
 * @return {integer/boolean} Returns key; false if nothing was retrieved.
 */
function generateKey(){
	
	try{
	
		// Disable page until key is obtained
		pageDisable(true);
	
		var xmlHttp = new XMLHttpRequest();
		xmlHttp.open("GET", server + "/landmarks/key", false);
		xmlHttp.withCredentials = true;
		
		// When the state of the response changes do the following
		xmlHttp.onreadystatechange = function () {
			if(xmlHttp.readyState === XMLHttpRequest.DONE && xmlHttp.status === 200) { // When response is nice and done do the following
			
				var json = JSON.parse(xmlHttp.responseText);
			
				if (json.extra.result === false){
					updateNotice("<h2>Error Cargando ID Único!</h2>");
					return;
				}
				
				// Set-up the key where it's needed
				document.getElementById("landmarkForm").elements["id"].value = json.extra.result;
				key = json.extra.result;
				
				pageDisable(false);
				
				return json.extra.result;
			
			}
		};
		
		xmlHttp.send();
		
	}
	catch(error){
		// Warn user and disable page
		updateNotice("<h2>Error Cargando ID Único!</h2>");
		pageDisable(true);

		return false;
	}
	
}

/**
 * Updates DOM object notice with given value.
 * @param {anything} Value to update notice with.
 * @return {undefined} Returns nothing.
 */
function updateNotice(value){

	// If value is not given clear notice
	value = value || "";

	document.getElementById("notice").innerHTML = value;

}
