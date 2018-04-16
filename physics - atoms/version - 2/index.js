var coulombConstant = 8990000000;

var chargeString = {0: "negative", 1: "positive"};
var chargeValue = {0: "-", 1: "+"};

var prefixString = {0: "", 1: "d", 2: "c", 3: "in", 4: "u", 5: "n", 6: "p", 7: "da", 8: "H", 9: "K", 10: "M", 11: "G", 12: "T"};
var prefixValue = {0: 1, 1: 0.1, 2: 0.01, 3: 0.001, 4: 0.000001, 5: 0.000000001, 6: 0.000000000001, 7: 10, 8: 100, 9: 1000, 10: 1000000, 11: 1000000000, 12: 1000000000000};

var unitsString = {0: "m", 1: "yd", 2: "ft", 3: "in", 4: "cm", 5: "mm", 6: "um", 7: "nm", 8: "km", 9: "mi", 10: "M"};
var unitsValue = { 0: 1, 1: 1.09361296, 2: 3.2808388799999997, 3: 39.370066559999997935, 4: 100, 5: 1000, 6: 1000000, 7: 1000000000, 8: 0.001, 9: 0.000621371, 10: 0.00053995663640604750835};

var distanceToPx = 3.3; // How much one distance unit is worth in pixels; distance to pixel ration. Exp. 3.3d = 1px

var maxDistance = 1000; // Maximum distance for charge objects from center of canvas.

var chargeObjectSize = 24; // Size of charge objects on canvas.
var textObjectSize = 30; // Size of text objects on canvas.
var lineObjectSize = 10; // Size of line objects on canvas.

var groupsCount = 0; // Count of how many groups have been made.
var groups = {}; // Dictionary of groups. Exp. {"0": {"group": farbric.Group, "charge": X, "magnitude": XXXX,"distance": XXXX, "angle": XXX}, ...}

window.onload = function (){
    
//    canvas.on('object:moving', function(e) {
//    var p = e.target;
//    p.line1 && p.line1.set({ 'x2': p.left, 'y2': p.top });
//    p.line2 && p.line2.set({ 'x1': p.left, 'y1': p.top });
//    p.line3 && p.line3.set({ 'x1': p.left, 'y1': p.top });
//    p.line4 && p.line4.set({ 'x1': p.left, 'y1': p.top });
//    canvas.renderAll();
//    });
    
    // Setup event handlers
    var moveHandler = function(evt){
        
        var movingObject = evt.target;
        
        groups[movingObject["groupId"]]["distance"] = giveDistanceBetweenGroups();
        groups[movingObject["groupId"]]["angle"] = giveAngleBetweenGroups(movingObject["groupId"], 0 , 1);
        
        var data = groups[movingObject["groupId"]];
        
        console.log("Line: " + groups[movingObject["groupId"]]["line"].toString() + "\nX1:" + groups[movingObject["groupId"]]["line"].x1.toString() + " | Y1:" + groups[movingObject["groupId"]]["line"].y1.toString() + "\nX2:" + groups[movingObject["groupId"]]["line"].x2.toString() + " | Y2:" + groups[movingObject["groupId"]]["line"].y2.toString());
        
        // So that objects can't exceed maximum distance
        if (giveDistanceBetweenGroups() > maxDistance){
            
            evt.target["left"] = giveX2FromCenter(maxDistance, groups[movingObject["groupId"]]["angle"]);
            evt.target["top"] = giveY2FromCenter(maxDistance, groups[movingObject["groupId"]]["angle"]);
            
        }
        
        // Update line of group
        groups[movingObject["groupId"]]["line"].set({"x2": data["group"]["left"], "y2": data["group"]["top"]});
        
        canvas.renderAll();
        
        console.log("Moving Object: " + movingObject.toString() + "\nGroup ID: " + movingObject["groupId"].toString() + "\n********");
        
        updateSettings();
        
    }
    
    var selectHandler = function(evt){
    
        var selectedObject = evt.target;
        
        document.forms["options"].elements["chargeList"].value = selectedObject["groupId"];
        
        updateSettings();
        
        console.log("Selected Object: " + selectedObject.toString() + "\nGroup ID: " + selectedObject["groupId"].toString() + "\n********");
    
    };
    
    canvas.on({
        
        'object:moving' : moveHandler,
        'object:selected' : selectHandler

    });
    
    //Create the first group of objects
    addObjectGroup((canvas.width / 2) , (canvas.height / 2), 0, 0, (canvas.width / 2), (canvas.height / 2));
    
    // Additional group set-ups
    groups[0]["group"]["lockMovementX"] = true;
    groups[0]["group"]["lockMovementY"] = true;
    
    groups[0]["distance"] = 0;
    groups[0]["angle"] = 0;
    groups[0]["line"].set({"fill": "yellow", "stroke": "yellow"});
    groups[0]["line"].moveTo(1000);
    
    updateSettings();
    
}

function giveRandomDistance(){
    
    return Math.round(Math.floor((Math.random() * maxDistance) + 1));
    
}

function giveRandomAngle(){
    
    return Math.floor((Math.random() * 360) + 1);
    
}

function giveRandomAngleUsage(){
    
    return Math.floor(Math.random() * 3);
    
}

function giveRandomColor(){
    
    var colors = [
        
        "aquamarine",
        "darkorchid",
        "chartreuse",
        "deeppink",
        "gold",
        "greenyellow",
        "orangered",
        "yellowgreen"
        
    ];
    
    return colors[Math.floor(Math.random() * colors.length)];
    
}

function giveRandomCharge(){
    
    return Object.keys(chargeString)[Math.floor(Math.random() * (Object.keys(chargeString).length))]
    
}

function giveRandomMagnitude(){
    
    return Math.round((Math.random() * maxDistance) + 1);
    
}

function giveRandomPrefix(){
    
    return Object.keys(prefixString)[Math.floor(Math.random() * (Object.keys(prefixString).length - 1))];
    
}

function giveRandomUnit(){
    
    return Object.keys(unitsString)[Math.floor(Math.random() * (Object.keys(unitsString).length - 1))];
    
}

function giveTitle(charge, magnitude, prefix){
    
    return chargeValue[charge] + "\n" + magnitude.toString() + prefixString[prefix] + " C";
    
}

function giveGroupTitle(index){
    
    // Choose selected group if none is given
    index = index || document.forms["options"].elements["chargeList"].value;
    
    return chargeValue[groups[index]["charge"]] + "\n" + groups[index]["magnitude"].toString() + prefixString[groups[index]["magnitudePrefix"]] + " C";
    
}

function addObjectGroup(x , y, distance, angle, oriX, oriY){
    
    // Get the first groups x cords if none is given
    oriX = oriX || groups[0]["group"]["left"];
    
    // Get the first groups y cords if none is given
    oriY = oriY || groups[0]["group"]["top"];
    
    // Main variables
    distance = distance || giveRandomDistance();
    angle = angle || giveRandomAngle();
    
    // Get cords for main objects
    x = x || giveX2FromX1(oriX, distance, angle);
    y = y || giveY2FromY1(oriY, distance, angle);
    
    // Main objects
    var circle = new fabric.Circle(
    
        {
            
            // General settings
            originX: "center",
            originY: "center",
            left: x,
            top: y,
            //Custom settings
            radius: chargeObjectSize,
            fill: giveRandomColor()
            
        }
    
    );
    
    var text = new fabric.Text(
    
        giveTitle(0, 0, 0),
        {
            
            // General settings
            originX: "center",
            originY: "center",
            left: x,
            top: y,
            // Custom settings
            fill: "red",
            stroke: 'yellow',
            strokeWidth: 1,
            fontSize: textObjectSize,
            textAlign: "center",
            fontStyle: "bold"
            
        }
    
    );
    
    // Side objects
    var line = new fabric.Line(
    
        [
            
            oriX,
            oriY,
            x,
            y
//            200,
//            300,
//            600,
//            300
            
        ],
        {
        
            // Custom settings
            fill: 'red',
            stroke: 'red',
            strokeWidth: 10,
            selectable: false
        
        }
    
    );
    
    canvas.add(line);
    line.moveTo(0);
    
    var group = new fabric.Group(
    
        [
            
            circle, // 0
            text   // 1
            
        ],
        {
            
            // General settings
            originX: "center",
            originY: "center",
            // Custom settings
            left: x,
            top: y,
            hasControls: false,
//            hasBorders: false,
            // Custom settings
            groupId: groupsCount
            
        }
        
    );
    
    groups[groupsCount] = {
        
        // Fabric settings
        group: group,
        line: line,
        charge: 0,
        magnitude: 0,
        magnitudePrefix: 0,
        distance: distance,
        distancePrefix: 0,
        distanceUnit: 0,
        angleUsage: 0,
        angle: angle,
        // Custom settings
        groupId: groupsCount
        
        
    };
    
    groupsCount += 1;
    
    canvas.add(group);
    canvas.renderAll();
    
    updateList();
    
    // Set to the charge
    document.forms["options"].elements["chargeList"].value = groupsCount - 1;
    
    updateSettings();
    
    updateGroupLines();
    
    console.log("Added object group:\nGroup ID: " + (groupsCount - 1).toString() + "\nX: " + x.toString() + " | Y: " + y.toString() + "\nOrigin X: " + oriX.toString() + " | Origin Y: " + oriY.toString() + "\n********");
    
}

function removeObjectGroup(index){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    canvas.remove(groups[index]["group"]);
    canvas.remove(groups[index]["line"]);
    delete groups[index];
    
    document.forms["options"].elements["chargeList"].value = 0;
    
    updateList();
    updateSettings();
    
    console.log("Removed Group:\nGroup Id: " + index.toString() + "\n********")
    
}

function updateGroupObjects(index){
    
    var form = document.forms["options"];
    index = index || form.elements["chargeList"].value;
    
    var x = giveX2FromCenter(groups[index]["distance"], groups[index]["angle"])
    var y = giveY2FromCenter(groups[index]["distance"], groups[index]["angle"])
    
//    var x = giveX2FromCenter(form.elements["distanceScroll"].value, form.elements["angleScroll"].value);
//    var y = giveY2FromCenter(form.elements["distanceScroll"].value, form.elements["angleScroll"].value);
    
    groups[index]["group"].set({"left": x, "top": y}).setCoords();
    
//    groups[index]["group"]["left"] = x;
//    groups[index]["group"]["top"] = y;
    
    groups[index]["line"].set({"x2": x, "y2": y});
    
    canvas.renderAll();
    
}

function updateGroupTitle(index){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    groups[index]["group"].item(1).setText(giveGroupTitle(index))._initDimensions();
    
    groups[index]["group"].addWithUpdate();
    
    canvas.renderAll();
    
}

function updateGroupLines(index){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    if (index == 0){
        
        console.log("It's the main object!")
        
        for (var key in Object.keys(groups)){
            
            if (key == 0){
                
                console.log("pass!")
                
                continue
                
            }
            else{
                
                console.log("looking at " + key.toString() + "!")
                
                var c1 = groups[key]["charge"];
                var c2 = groups[0]["charge"];
                
                var line = groups[key]["line"];
                
                console.log("Line: " + line.toString())

                if (c1 == 0){

                    if (c1 == c2){

                        groups[key]["line"].setGradient(
                            "stroke", 
                            {

                                type: 'linear',
                                x1: 0,
                                y1: -line.width / 2,
                                x2: 0,
                                y2: line.width / 2,
                                colorStops:
                                {

//                                    0: "blue",
//                                    1: "blue"
                                    0: "rgb(0,0,255)", // blue
                                    1: "rgb(0,0,255)" // blue

                                }

                            }
                        );
                        
                        console.log("blue blue");

                    }
                    else{

                        groups[key]["line"].setGradient(
                            "stroke", 
                            {

                                type: 'linear',
                                x1: 0,
                                y1: -line.width / 2,
                                x2: 0,
                                y2: line.width / 2,
                                colorStops:
                                {

//                                    0: "blue",
//                                    1: "red"
                                    0: "rgb(0,0,255)", // blue
                                    1: "rgb(255,0,0)" // red

                                }

                            }
                        );
                        
                        console.log("blue red");

                    }

                }
                else{

                    if (c1 == c2){

                        groups[key]["line"].setGradient(
                            "stroke", 
                            {

                                type: 'linear',
                                x1: 0,
                                y1: -line.width / 2,
                                x2: 0,
                                y2: line.width / 2,
                                colorStops:
                                {

//                                    0: "red",
//                                    1: "red"
                                    0: "rgb(255,0,0)", // red
                                    1: "rgb(255,0,0)" // red

                                }

                            }
                        );
                        
                        console.log("red red");

                    }
                    else{

                        groups[key]["line"].setGradient(
                            "stroke", 
                            {

                                type: 'linear',
                                x1: 0,
                                y1: -line.width / 2,
                                x2: 0,
                                y2: line.width / 2,
                                colorStops:
                                {

//                                    0: "red",
//                                    1: "blue"
                                    0: "rgb(255,0,0)", // red
                                    1: "rgb(0,0,255)" // blue

                                }

                            }
                        );
                        
                        console.log("red blue");

                    }
                
                }

            }
        
        }
    }
    else{
        
        var c1 = groups[0]["charge"];
        var c2 = groups[index]["charge"];
                
        var line = groups[index]["line"];

        console.log("Line: " + line.toString())
        
        if (c1 == 0){
            
            if (c1 == c2){
                
                groups[index]["line"].setGradient(
                    "stroke", 
                    {
                        
                        type: 'linear',
                        x1: 0,
                        y1: -line.width / 2,
                        x2: 0,
                        y2: line.width / 2,
                        colorStops:
                        {
                            
//                            0: "blue",
//                            1: "blue"
                            0: "rgb(0,0,255)", // blue
                            1: "rgb(0,0,255)" // blue
                            
                        }
                        
                    }
                );
                
                console.log("blue blue");
                
            }
            else{
                
                groups[index]["line"].setGradient(
                    "stroke", 
                    {
                        
                        type: 'linear',
                        x1: 0,
                        y1: -line.width / 2,
                        x2: 0,
                        y2: line.width / 2,
                        colorStops:
                        {
                            
//                            0: "blue",
//                            1: "red"
                            0: "rgb(0,0,255)", // blue
                            1: "rgb(255,0,0)" // red
                            
                        }
                        
                    }
                );
                
                console.log("blue red");
                
            }
            
        }
        else{
            
            if (c1 == c2){
                
                groups[index]["line"].setGradient(
                    "stroke", 
                    {
                        
                        type: 'linear',
                        x1: 0,
                        y1: -line.width / 2,
                        x2: 0,
                        y2: line.width / 2,
                        colorStops:
                        {
                            
//                            0: "red",
//                            1: "red"
                            0: "rgb(255,0,0)", // red
                            1: "rgb(255,0,0)" // red
                            
                        }
                        
                    }
                );
                
                console.log("red red");
                
            }
            else{
                
                groups[index]["line"].setGradient(
                    "stroke", 
                    {
                        
                        type: 'linear',
                        x1: 0,
                        y1: -line.width / 2,
                        x2: 0,
                        y2: line.width / 2,
                        colorStops:
                        {
                            
//                            0: "red",
//                            1: "blue"
                            0: "rgb(255,0,0)", // red
                            1: "rgb(0,0,255)" // blue
                            
                        }
                        
                    }
                );
                
                console.log("red blue");
                
            }
            
        }
        
    }
    
    canvas.renderAll();
    
    console.log("Updated lines!")
    
}

function updateList(){
    
    var form = document.forms["options"];
    
    var msg = "";
    var num = 1;
    for (var c in groups){
        
        msg += " <option value=" + c + ">" + num.toString() + " Charge</option>";
        num += 1;
        
    }
    
    form.elements["chargeList"].innerHTML = msg;
    
    form.elements["chargeList"].value = 0;
    
    canvas.deactivateAll().renderAll();
    
}

function updateSelectedCanvas(index){
    
    // If index isn't specified assume they ask for the selected charge in chargeList
    index = index || document.forms["options"].elements["chargeList"].value;
    
    canvas.setActiveObject(groups[index]["group"]);
    
    canvas.renderAll();
    
}

function updateSettings(index){
    
    // If index isn't specified assume they ask for the selected charge in chargeList
    index = index || document.forms["options"].elements["chargeList"].value;
    
    var form = document.forms["options"];
    var data = groups[index];
    
    if (index == 0){
        
        form.elements["removeCharge"].disabled = true;
        form.elements["distanceScroll"].disabled = true;
        form.elements["distanceField"].disabled = true;
        form.elements["distancePrefix"].disabled = true;
        form.elements["distanceUnit"].disabled = true;
        form.elements["angleUsage"].disabled = true;
        form.elements["angleScroll"].disabled = true;
        form.elements["angleField"].disabled = true;
        
    }
    else{
        
        form.elements["removeCharge"].disabled = false;
        form.elements["distanceScroll"].disabled = false;
        form.elements["distanceField"].disabled = false;
        form.elements["distancePrefix"].disabled = false;
        form.elements["distanceUnit"].disabled = false;
        form.elements["angleUsage"].disabled = false;
        form.elements["angleScroll"].disabled = false;
        form.elements["angleField"].disabled = false;
        
    }
    if (Object.keys(groups).length == 1){
        
        form.elements["random"].disabled = true;
        
    }
    else{
        
        form.elements["random"].disabled = false;
        
    }
    
    form.elements["chargeSelect"].value = data["charge"];
    form.elements["magnitudeScroll"].value = data["magnitude"];
    form.elements["magnitudeField"].value = data["magnitude"];
    form.elements["magnitudePrefix"].value = data["magnitudePrefix"];
    form.elements["distanceScroll"].value = data["distance"];
    form.elements["distanceField"].value = data["distance"];
    form.elements["distancePrefix"].value = data["distancePrefix"];
    form.elements["distanceUnit"].value = data["distanceUnit"];
    form.elements["angleUsage"].value = data["angleUsage"];
    form.elements["angleScroll"].value = data["angle"];
    form.elements["angleField"].value = data["angle"];
    
}

function updateSection(section, part){
    
    var form = document.forms["options"];
    var index = form.elements["chargeList"].value;
    
    if (section == "charge"){
        
        if (part == 0){
            
            groups[index]["charge"] = form.elements["chargeSelect"].value;
            
            // Update charges text object(title)
            updateGroupTitle(index);
            
        }
        
    }
    else if (section == "magnitude"){
        
        if (part == 0){
            
            form.elements["magnitudeField"].value = form.elements["magnitudeScroll"].value;
            
            groups[index]["magnitude"] = form.elements["magnitudeScroll"].value;
            
            // Update charges text object(title)
            updateGroupTitle(index);
            
        }
        else{
            
            if (isNaN(form.elements["magnitudeField"].value) == true){
                
                form.elements["magnitudeField"].value = form.elements["magnitudeScroll"].value;
            
                groups[index]["magnitude"] = form.elements["magnitudeScroll"].value;
            
                // Update charges text object(title)
                updateGroupTitle(index);
                
            }
            else if (form.elements["magnitudeField"].value > 1000){
                
                form.elements["magnitudeField"].value = 1000;
                form.elements["magnitudeScroll"].value = 1000;
            
                groups[index]["magnitude"] = 1000;
            
                // Update charges text object(title)
                updateGroupTitle(index);
                
                
            }
            else if (form.elements["magnitudeField"].value < 0.001){
                
                form.elements["magnitudeField"].value = 0.001;
                form.elements["magnitudeScroll"].value = 0.001;
            
                groups[index]["magnitude"] = 0.001;
            
                // Update charges text object(title)
                updateGroupTitle(index);
                
            }
            else{
                
                form.elements["magnitudeScroll"].value = form.elements["magnitudeField"].value;
            
                groups[index]["magnitude"] = form.elements["magnitudeField"].value;
            
                // Update charges text object(title)
                updateGroupTitle(index);
                
            }
            
        }
        
    }
    else if (section == "magnitudePrefix"){
        
        if (part == 0){
            
            groups[index]["magnitudePrefix"] = form.elements["magnitudePrefix"].value;
            
        }
        
    }
    else if (section == "distance"){
        
        if (part == 0){
            
            form.elements["distanceField"].value = form.elements["distanceScroll"].value;
            
            groups[index]["distance"] = form.elements["distanceScroll"].value;
            
        }
        else{
            
            if (isNaN(form.elements["distanceField"].value) == true){
                
                form.elements["distanceField"].value = form.elements["distanceScroll"].value;
            
                groups[index]["distance"] = form.elements["distanceScroll"].value;
                
            }
            else if (form.elements["distanceField"].value > 1000){
                
                form.elements["distanceField"].value = 1000;
                form.elements["distanceScroll"].value = 1000;
            
                groups[index]["distance"] = 1000;
                
                
            }
            else if (form.elements["distanceField"].value < 0.001){
                
                form.elements["distanceField"].value = 0.001;
                form.elements["distanceScroll"].value = 0.001;
            
                groups[index]["distance"] = 0.001;
                
            }
            else{
                
                form.elements["distanceScroll"].value = form.elements["distanceField"].value;
            
                groups[index]["distance"] = form.elements["distanceField"].value;
                
            }
            
        }
        
        
    }
    else if (section == "distancePrefix"){
        
        if (part == 0){
            
            groups[index]["distancePrefix"] = form.elements["distancePrefix"].value;
            
        }
        
    }
    else if (section == "distanceUnit"){
        
        if (part == 0){
            
            groups[index]["distanceUnit"] = form.elements["distanceUnit"].value;
            
        }
        
    }
    else if (section == "angle"){
        
        if (part == -1){
            
            groups[index]["angleUsage"] = form.elements["angleUsage"].value;
            
        }
        else if (part == 0){
            
            form.elements["angleField"].value = form.elements["angleScroll"].value;
            
            groups[index]["angle"] = form.elements["angleScroll"].value;
            
        }
        else{
            
            if (isNaN(form.elements["angleField"].value) == true){
                
                form.elements["angleField"].value = form.elements["angleScroll"].value;
            
                groups[index]["angle"] = form.elements["angleScroll"].value;
                
            }
            else if (form.elements["angleField"].value > 360){
                
                form.elements["angleField"].value = 360;
                form.elements["angleScroll"].value = 360;
            
                groups[index]["angle"] = 360;
                
                
            }
            else if (form.elements["angleField"].value < 0){
                
                form.elements["angleField"].value = 0;
                form.elements["angleScroll"].value = 0;
            
                groups[index]["angle"] = 0;
                
            }
            else{
                
                form.elements["angleScroll"].value = form.elements["angleField"].value;
            
                groups[index]["angle"] = form.elements["angleField"].value;
                
            }
            
        }
        
    }
    
}

function updateSections(){
    
    var form = document.forms["options"];
    var index = form.elements["chargeList"].value;
    
    form.elements["chargeSelect"].value = 0;
    form.elements["magnitudeScroll"].value = 0;
    form.elements["magnitudeField"].value = 1;
    form.elements["magnitudePrefix"].value = 1;
    form.elements["distanceScroll"].value = 1;
    form.elements["distanceField"].value = 1;
    form.elements["distancePrefix"].value = 0;
    form.elements["distanceUnit"].value = 0;
    form.elements["angleUsage"].value = 0;
    form.elements["angleScroll"].value = 0;
    form.elements["angleField"].value = 0;
    
    groups[index]["charge"] = form.elements["chargeSelect"].value;
    groups[index]["magnitude"] = form.elements["magnitudeScroll"].value;
    groups[index]["magnitudePrefix"] = form.elements["magnitudePrefix"].value;
    groups[index]["distance"] = form.elements["distanceScroll"].value;
    groups[index]["distancePrefix"] = form.elements["distancePrefix"].value;
    groups[index]["distanceUnit"] = form.elements["distanceUnit"].value;
    groups[index]["angleUsage"] = form.elements["angleUsage"].value;
    groups[index]["angle"] = form.elements["angleScroll"].value;
    
    updateGroupObjects(index);
    updateGroupTitle(index);
    
}

function randomProblem(){
    
    console.log("Creating a random problem...")
    
    for (var group in groups){
        
        randomizeGroup(group);
        
        console.log("Randomized group:\nGroup ID: " + group.toString() + "\n********");
        
    }
    
    document.forms["options"].elements["chargeList"].value = 0;
    updateSettings();
    
    console.log("Random problem created!");
    
}

function randomizeGroup(index){
    
    // If index is not given assume that it's refering to selectedObject
    index = index || document.forms["options"].elements["chargeList"].value;
    
    if (index == 0){
        
        groups[index]["charge"] = giveRandomCharge();
        groups[index]["magnitude"] = giveRandomMagnitude();
        groups[index]["magnitudePrefix"] = giveRandomPrefix();
        
        updateGroupObjects(index);
        updateGroupTitle(index);
        
    }
    else{
        
        groups[index]["charge"] = giveRandomCharge();
        groups[index]["magnitude"] = giveRandomMagnitude();
        groups[index]["magnitudePrefix"] = giveRandomPrefix();
        groups[index]["distance"] = giveRandomDistance();
        groups[index]["distancePrefix"] = giveRandomPrefix();
        groups[index]["distanceUnit"] = giveRandomUnit();
        groups[index]["angleUsage"] = giveRandomAngleUsage();
        groups[index]["angle"] = giveRandomAngle();
        
        updateGroupObjects(index);
        updateGroupTitle(index);
        updateGroupLines(index);
        
    }
    
}

function giveX2FromCenter(distance, angle){
    
    distance = distance / distanceToPx;
    
    angle = angle / 180 * Math.PI;
    
    return Math.floor(distance * Math.cos(angle) + Math.floor(canvas.width / 2));
    
}

function giveY2FromCenter(distance, angle){
    
    distance = distance / distanceToPx;
    
    angle = angle / 180 * Math.PI;
    
    return Math.floor(distance * Math.sin(angle) + Math.floor(canvas.height / 2));
    
}

function giveX2FromX1(x1, distance, angle){
    
    distance = distance / distanceToPx;
    
    angle = angle / 180 * Math.PI;
    
    return Math.floor(distance * Math.cos(angle) + x1);
    
}

function giveY2FromY1(y1, distance, angle){
    
    distance = distance / distanceToPx;
    
    angle = angle / 180 * Math.PI;
    
    return Math.floor(distance * Math.sin(angle) + y1);
    
}

function giveGroupDistanceFromCenter(group, type){
    
    // Group one is selected group if none is given
    group = group || document.forms["options"].elements["chargeList"].value;
    
    type = type || 0;
    
    var xt = groups[groupTwo]["group"].get("left") - (canvas.width / 2);
    var yt = groups[groupTwo]["group"].get("top") - (canvas.height / 2);
    
    if (type == 0){
        
        // Give as distance
        return Math.floor(Math.sqrt(xt*xt + yt*yt)) * distanceToPx;
        
    }
    else{
        
        // Give as pixels
        return Math.floor(Math.sqrt(xt*xt + yt*yt));
        
    }
    
}

function giveGroupAngleFromCenter(group, type){
    
    // Group one is selected group if none is given
    group = group || document.forms["options"].elements["chargeList"].value;
    
    type = type || 0;
    
    var xt = groups[groupTwo]["group"].get("left") - (canvas.width / 2);
    var yt = groups[groupTwo]["group"].get("top") - (canvas.height / 2);
    
    if (type == 0){
        
        // Give in radians (?0.000 to 3.60?)
        return Math.floor(Math.atan2(yt, xt) + 180);
        
    }
    else{
        
        // Give in degrees (0 to 360)
        return Math.floor((Math.atan2(yt, xt) * 180 / Math.PI) + 180);
        
    }
    
}

function giveDistanceBetweenGroups(groupOne, groupTwo, type){
    
    // Group one is selected group if none is given
    groupOne = groupOne || document.forms["options"].elements["chargeList"].value;
    
    // Group two is first group if none is given
    groupTwo = groupTwo || 0;
    
    type = type || 0;
    
    var xt = groups[groupTwo]["group"].get("left") - groups[groupOne]["group"].get("left");
    var yt = groups[groupTwo]["group"].get("top") - groups[groupOne]["group"].get("top");
    
    if (type == 0){
        
        // Give as distance
        return Math.floor(Math.sqrt(xt*xt + yt*yt)) * distanceToPx;
        
    }
    else{
        
        // Give as pixels
        return Math.floor(Math.sqrt(xt*xt + yt*yt));
        
    }
    
}

function giveAngleBetweenGroups(groupOne, groupTwo, type){
    
    // Group one is selected group if none is given
    groupOne = groupOne || document.forms["options"].elements["chargeList"].value;
    
    // Group two is first group if none is given
    groupTwo = groupTwo || 0;
    
    type = type || 0;
    
    var xt = groups[groupTwo]["group"].get("left") - groups[groupOne]["group"].get("left");
    var yt = groups[groupTwo]["group"].get("top") - groups[groupOne]["group"].get("top");
    
    if (type == 0){
        
        // Give in radians (?0.000 to 3.60?)
        return Math.floor(Math.atan2(yt, xt) + 180);
        
    }
    else{
        
        // Give in degrees (0 to 360)
        return Math.floor((Math.atan2(yt, xt) * 180 / Math.PI) + 180);
        
    }
    
}

function submitProblem(){
    
    console.log("Solving problem...")
    
    if (Object.keys(groups).length == 1){
        
        msg = "<h3>Can't explain anything without two or more charges!</h3>"
        
    }
    else{
        
        // Start of explanation
        var msg = "";
        var sectionOne = "<h3><u>List Data</u></h3><ul><b>";
        var sectionTwo = "<h3><u>Simplify Data</u></h3><i><ul>";
        var sectionThree = "<h3><u>Solve</u></h3>To solve for what we want we'll use a maximum of five things: <ol><li>Coulomb's Number (<b>k</b>): " + coulombConstant.toString() + "</li><li>Force Formula (<b>f</b>): (k x q# x q#) / r^2</li><li>Resulting Foce Formula (<b>fr</b>): f# + f#</li><li>Angle Formula (<b>0</b>): tan^-1(fy / fx)</li><li>Resulting Force With Angle (<b>fr</b>): root(f#^2 + f#^2)</li></ol><ul>";
        var sectionFour = "<h3><u>Get Resulting Forces</u></h3><ul>";

        var num = 1;
        for (var group in Object.keys(groups)){
            
            sectionOne += explainData(group, num);
            sectionTwo += explainmagnitude(group, num) + explainDistance(group, num);
            if (group != 0){
                
                sectionThree += explainSolve(group, num);
                sectionFour += explainResultingForce(group, num);
                
            }

            num += 1;

            console.log("ID: " + group + " Read: true");

        }
        
        sectionOne += "</b></ul>";
        sectionTwo += "</i></ul>";
        sectionThree += "</ul>";
        sectionFour += "</ul><h3>Add all of the <b>fx</b> and <b>fy</b> and use the following formula:<br>sqr(fxr^2 + fyr^2)</h3>";
        
        msg += sectionOne;
        
        if (sectionTwo != "<h3><u>Simplify Data</u></h3><i><ul></i></ul>"){
            
            msg += sectionTwo;
            
        }
        
        msg += sectionThree;
        msg += sectionFour;
        msg += "<h1>Resulting Force:</h1><h2>" + giveResultingForceOfGroups() + "</h2><h1>Resulting Directions:</h1><h2><ul>"
        
        var resultingDirections = giveResultingDirectionOfGroups();
        
        for (var index in resultingDirections){
            
            msg += "<li>Direction: " + index.toString() + " | Force: " + resultingDirections[index].toString() + "</li>"
            
        }
        
        msg += "</ul></h2><h2>Resulting Angle: " + Math.round(giveResultingAngleOfGroups()) + " degrees</h2>";
        
        
    }
    
    document.getElementById("explanation").innerHTML = msg;
    
    var force = giveResultingForceOfGroups();
    var angle = giveResultingAngleOfGroups();
    
    if (force > (canvas.width * distanceToPx)){
        
        force = (canvas.width * distanceToPx) + (200 * distanceToPx);
        
    }
    
    groups[0]["line"].set({"x2": giveX2FromCenter(force,  angle), "y2": giveY2FromCenter(force, angle)});
    groups[0]["line"].moveTo(1000);
    
    canvas.renderAll();
    
    console.log("Problem solved!")
    
}

function explainData(index, num){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    num = num || 1;
    
    var msg = "<li>Charge " + num.toString() + ": " + chargeString[groups[index]["charge"] ] + "</li><li>q" + num.toString() +": " + (groups[index]["magnitude"]) + "</li><li>r" + num.toString() +": " + (groups[index]["distance"]) + "</li>";
    
    if (groups[index]["angleUsage"] == 0){
        
        msg += "<li>Angle " + num.toString() + ": " + (groups[index]["angle"]) + "</li>";
        
    }
    else if (groups[index]["angleUsage"] == 1){
        
        msg += "<li>Angle " + num.toString() + ": ?</li>";
        
    }
    
    msg += "<br>";
    
    return msg;
    
}

function explainmagnitude(index, num){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    num = num || 1;
    
    var msg = "";
    
    if (groups[index]["magnitudePrefix"] != 0){
        
        msg += "<li>We have <b>" + (groups[index]["magnitude"]).toString() + prefixString[groups[index]["magnitudePrefix"]] + " C</b>. Which is equal to:<br><b>q" + num.toString() + " = " + (groups[index]["magnitude"]).toString() + " x " + prefixValue[groups[index]["magnitudePrefix"]].toString() + " = " + (giveFullMagnitude(groups[index]["magnitude"], groups[index]["magnitudePrefix"])).toString() + " C</b></li><br>"
        
    }
    
    return msg;
    
}

function explainDistance(index, num){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    num = num || 1;
    
    var msg = "";
    
    if (groups[index]["distancePrefix"] != 0){
        
        msg += "<li>We have <b>" + (groups[index]["distance"]).toString() + prefixString[groups[index]["distancePrefix"]] + " " + unitsString[groups[index]["distanceUnit"]] + "</b>. Which is equal to:<br><b>r" + num.toString() + " = " + (groups[index]["distance"]).toString() + " x " + prefixValue[groups[index]["distancePrefix"]].toString() + " = " + (giveFullDistance(groups[index]["distance"], groups[index]["distancePrefix"])).toString() + " " + unitsString[groups[index]["distanceUnit"]].toString() + "</b></li>"
        
    }
    if (groups[index]["distanceUnit"] != 0){
        
        msg += "<br><li>We have <b>" + (giveFullDistance(groups[index]["distance"], groups[index]["distancePrefix"])).toString() + " " + unitsString[groups[index]["distanceUnit"]] + "</b>. One <b>" + unitsString[groups[index]["distanceUnit"]] + "</b> is equal to <b>" + unitsValue[groups[index]["distanceUnit"]].toString() + " m</b> which means:<br><b>r" + num.toString() + " = " + (giveFullDistance(groups[index]["distance"], groups[index]["distancePrefix"])).toString() + " x " + unitsValue[groups[index]["distanceUnit"]] + " = " + (giveBasicDistance(groups[index]["distance"], groups[index]["distancePrefix"], groups[index]["distanceUnit"])).toString() + " m</b></li>"
        
    }
    
    if (msg != ""){
        
        msg += "<br>";
        
    }
    
    return msg;
    
}

function explainAngle(index){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    return ;
    
}

function explainSolve(index, numOne, originIndex, numTwo){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    numOne = numOne || 2;
    
    originIndex = originIndex || 0;
    
    numTwo = numTwo || 1;
    
    var k = coulombConstant;
    
    var c1 = groups[index]["charge"];
    var q1 = giveFullMagnitude(groups[index]["magnitude"], groups[index]["magnitudePrefix"]);
    
    var c2 = groups[originIndex]["charge"];
    var q2 = giveFullMagnitude(groups[originIndex]["magnitude"], groups[originIndex]["magnitudePrefix"]);
    
    var r = giveBasicDistance(groups[index]["distance"], groups[index]["distancePrefix"], groups[index]["distanceUnit"]);
    
    var au = groups[index]["angleUsage"]
    var a = groups[index]["angle"];
    
    var f = giveForce(q1, q2, r);
    var fwc = giveForceWithChargeOfGroups(index, originIndex);
    
    groups[index]["force"] = fwc;
    
    var msg = "";
    
    if (au == 0){
        
        msg += "<li>We know the <b>angle between charge one and charge two is " + a.toString() + "</b>; and that the <b>distance between them is " + r.toString() + "</b>. So using the <b>force formula: (k * q" + numOne.toString() + " * q" + numTwo.toString() + ") / r" + numOne.toString() + numTwo.toString() + "^2</b>. So we do the following:<br><b> f" + numTwo.toString() + numOne.toString() + " = (" + coulombConstant.toString() + " * " + q1.toString() + " * " + q2.toString() + ") / (" + r.toString() + ")^2 = " + f.toString() + "</b>";
        
        if (f > fwc){
            
            msg += "<br>But since <b>charge " + numOne.toString() + " and charge " + numTwo.toString() + " have the same charge of " + chargeString[c1] + "</b> they repel each other so we make the force negative:<br><b>f" + numTwo.toString() + numOne.toString() + " = abs(" + f.toString() + ") * -1 = " + fwc.toString() + "</b>";
            
        }
        else{
            
            msg += "<br>But since <b>charge " + numOne.toString() + " and charge " + numTwo.toString() + " have the same charge of " + chargeString[c1] + "</b> they attract each other so we make the force positive:<br><b>f" + numTwo.toString() + numOne.toString() + " = abs(" + f.toString() + ") = " + fwc.toString() + "</b>";
            
        }
        
        msg += "</li>"
        
        groups[index]["force"] = giveForce(q1, q2, r);
        
    }
    else if (au == 1){
        
        msg += "<li>We need to know the <b>angle between charge one and charge two</b>. We know that the <b>distance between them is " + r.toString() + "</b>. We use the <b>force formula: (k * q" + numOne.toString() + " * q" + numTwo.toString() + ") / r" + numOne.toString() + numTwo.toString() + "^2</b>. So we do the following:<br><b> f" + numTwo.toString() + "" + numOne.toString() + " = (" + coulombConstant.toString() + " * " + q1.toString() + " * " + q2.toString() + ") / (" + r.toString() + ")^2 = " + f.toString() + "</b>";
        
        if (f > fwc){
            
            msg += "<br>But since <b>charge " + numOne.toString() + " and charge " + numTwo.toString() + " have the same charge of " + chargeString[c1] + "</b> they repel each other so we make the force negative:<br><b>f" + numTwo.toString() + numOne.toString() + " = abs(" + f.toString() + ") * -1 = " + fwc.toString() + "</b>";
            
        }
        else{
            
            msg += "<br>But since <b>charge " + numOne.toString() + " and charge " + numTwo.toString() + " have the same charge of " + chargeString[c1] + "</b> they attract each other so we make the force positive:<br><b>f" + numTwo.toString() + numOne.toString() + " = abs(" + f.toString() + ") = " + fwc.toString() + "</b>";
            
        }
        
        msg += "</li>"
        
    }
    else{
        
        msg += "<li>We know that the <b>distance between them is " + r.toString() + "</b>. So using the <b>force formula: (k * q" + numOne.toString() + " * q" + numTwo.toString() + ") / r" + numOne.toString() + numTwo.toString() + "^2</b>. So we do the following:<br><b> f" + numTwo.toString() + numOne.toString() + " = (" + coulombConstant.toString() + " * " + q1.toString() + " * " + q2.toString() + ") / (" + r.toString() + ")^2 = " + f.toString() + "</b>";
        
        if (f > fwc){
            
            msg += "<br>But since <b>charge " + numOne.toString() + " and charge " + numTwo.toString() + " have the same charge of " + chargeString[c1] + "</b> they repel each other so we make the force negative:<br><b>f" + numTwo.toString() + numOne.toString() + " = abs(" + f.toString() + ") * -1 = " + fwc.toString() + "</b>";
            
        }
        else{
            
            msg += "<br>But since <b>charge " + numOne.toString() + " and charge " + numTwo.toString() + " have the same charge of " + chargeString[c1] + "</b> they attract each other so we make the force positive:<br><b>f" + numTwo.toString() + numOne.toString() + " = abs(" + f.toString() + ") = " + fwc.toString() + "</b>";
            
        }
        
        msg += "</li>";
        
    }
    
    msg += "<br>";
    
    return msg;
    
}

function explainResultingForce(index, num){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    num = num || 1;
    
    var f = groups[index]["force"];
    var a = groups[index]["angle"];
    var fx = getFxOfGroups(index);
    var fy = getFyOfGroups(index);
    
    groups[index]["fx"] = fx;
    groups[index]["fy"] = fy;
    
    var msg = "<li>To get <b>fx</b> and <b>fy</b> of <b>charge " + num.toString() + "</b> we use <b>fx = force * sin(angle)</b> and <b>fy = force * cos(angle)</b>; as follows:<br><b>fx = " + f.toString() + " * sin(" + a.toString() + ") = " + fx.toString() + "</b>&nbsp;|&nbsp;<b>fy = " + f.toString() + " * cos(" + a.toString() + ") = " + fy.toString() + "</b></li><br>";
    
    return msg;
    
}

function giveForce(q1, q2, r){
    
    var force = (coulombConstant * q2 * q1) / Math.pow(r, 2);
    
    console.log("Force: " + force)
    
    return force;
    
}

function giveForceWithCharge(c1, q1, c2, q2, r){
    
    var force = (coulombConstant * q2 * q1) / Math.pow(r, 2);
    
    if (c1 == c2){
        
        force = Math.abs(force) * -1;
        
    }
    else{
        
        force = Math.abs(force);
        
    }
    
    console.log("Charge Force: " + force)
    
    return force;
    
}

function giveForceWithChargeOfGroups(index, originIndex){
    
    var q1 = giveFullMagnitude(groups[index]["magnitude"], groups[index]["magnitudePrefix"]);
    
    var q2 = giveFullMagnitude(groups[originIndex]["magnitude"], groups[originIndex]["magnitudePrefix"]);
    
    var r = giveBasicDistance(groups[index]["distance"], groups[index]["distancePrefix"], groups[index]["distanceUnit"]);
    
    var force = (coulombConstant * q2 * q1) / Math.pow(r, 2);
    
    if (isForceBetweenGroupsNegative(index, originIndex) == true){
        
        force = Math.abs(force) * -1;
        
    }
    else{
        
        force = Math.abs(force);
        
    }
    
    return force;
    
}

function getFxOfGroups(index){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    var force = groups[index]["force"];
    var angle = groups[index]["angle"];
    
    var fx = force * Math.cos(angle);
    
    if (isFxBetweenGroupsNegative(index) == true){
        
        fx = Math.abs(fx) * -1;
        
    }
    else{
        
        fx = Math.abs(fx);
        
    }
    
    return fx;
    
}

function getFyOfGroups(index){
    
    index = index || document.forms["options"].elements["chargeList"].value;
    
    var force = groups[index]["force"];
    var angle = groups[index]["angle"];
    
    var fy = force * Math.sin(angle);
    
    if (isFyBetweenGroupsNegative(index) == true){
        
        fy = Math.abs(fy) * -1;
        
    }
    else{
        
        fy = Math.abs(fy);
        
    }
    
    return fy;
}

function isFxBetweenGroupsNegative(groupOne, groupTwo){
    
    groupOne = groupOne || document.forms["options"].elements["chargeList"].value;
    
    groupTwo = groupTwo || 0;
    
    var c1 = groups[groupOne]["charge"];
    var x1 = groups[groupOne]["group"].left;
    
    var c2 = groups[groupTwo]["charge"];
    var x2 = groups[groupTwo]["group"].left;
    
    if (c1 == c2){
        
        if (x1 > x2){

            return true;

        }
        else{

            return false;

        }
        
    }
    else{
        
        if (x1 > x2){

            return false;

        }
        else{

            return true;

        }
        
    }
    
    
    
}

function isFyBetweenGroupsNegative(groupOne, groupTwo){
    
    groupOne = groupOne || document.forms["options"].elements["chargeList"].value;
    
    groupTwo = groupTwo || 0;
    
    var c1 = groups[groupOne]["charge"];
    var y1 = groups[groupOne]["group"].top;
    
    var c2 = groups[groupTwo]["charge"];
    var y2 = groups[groupTwo]["group"].top;
    
    if (c1 == c2){
        
        if (y1 > y2){

            return true;

        }
        else{

            return false;

        }
        
    }
    else{
        
        if (y1 > y2){

            return false;

        }
        else{

            return true;

        }
        
    }
    
}

function isForceBetweenGroupsNegative(groupOne, groupTwo){
    
    groupOne = groupOne || document.forms["options"].elements["chargeList"].value;
    
    groupTwo = groupTwo || 0;
    
    var c1 = groups[groupOne]["charge"];
    var x1 = groups[groupOne]["group"].left;
    var y1 = groups[groupOne]["group"].top;
    
    var c2 = groups[groupTwo]["charge"];
    var x2 = groups[groupTwo]["group"].left;
    var y2 = groups[groupTwo]["group"].top;
    
    if (c1 == c2){
        
        if (x1 == x2 && y1 == y2){
            
            return true;
            
        }
        else if (x1 > x2 && y1 == y2){
            
            return true;
            
        }
        else if (x1 == x2 && y1 > y2){
            
            return true;
            
        }
        else if (x1 > x2 && y1 > y2){
            
            return true;
            
        }
        else if (x1 > x2 && y1 < y2){
            
            return false; // ?
            
        }
        else if (x1 < x2 && y1 == y2){
            
            return false;
            
        }
        else if (x1 == x2 && y1 < y2){
            
            return false;
            
        }
        else if (x1 < x2 && y1 < y2){
            
            return true;
            
        }
        else if (x1 < x2 && y1 > y2){
            
            return false; // ?
            
        }
        
    }
    else{
        
        if (x1 == x2 && y1 == y2){
            
            return true;
            
        }
        else if (x1 > x2 && y1 == y2){
            
            return true;
            
        }
        else if (x1 == x2 && y1 > y2){
            
            return true;
            
        }
        else if (x1 > x2 && y1 > y2){
            
            return true;
            
        }
        else if (x1 > x2 && y1 < y2){
            
            return false; // ?
            
        }
        else if (x1 < x2 && y1 == y2){
            
            return false;
            
        }
        else if (x1 == x2 && y1 < y2){
            
            return false;
            
        }
        else if (x1 < x2 && y1 < y2){
            
            return true;
            
        }
        else if (x1 < x2 && y1 > y2){
            
            return false; // ?
            
        }
        
    }
    
}

function giveResultingForceOfGroups(fixedGroup){
    
    fixedGroup = fixedGroup || 0;
    
    var rx = 0.0;
    var ry = 0.0;
    
    for (var group in Object.keys(groups)){
        
        if (group != fixedGroup){
            
            rx += groups[group]["fx"];
            ry += groups[group]["fy"];
            
        }
        
    }
    
    return resultingForce(rx, ry);
    
}

function resultingForce(fx, fy){
    
    return Math.sqrt(Math.pow(fx, 2) + Math.pow(fy, 2));
    
}

function giveFullMagnitude(magnitude, prefix){
    
    return (magnitude * prefixValue[prefix]);
    
}

function giveBasicDistance(distance, prefix, unit){
    
    return ((distance * prefixValue[prefix]) * unitsValue[unit]);
    
}

function giveFullDistance(distance, prefix){
    
    return (distance * prefixValue[prefix]);
    
}

function giveResultingDirectionOfGroups(fixedGroup){
    
    fixedGroup = fixedGroup || 0;
    
    var directions = {"rightup": 0.0, "leftup": 0.0, "equalup": 0.0, "rightdown": 0.0, "leftdown": 0.0, "equaldown": 0.0, "equalequal": 0.0};
    
    for (var group in Object.keys(groups)){
        
        
        if (group != fixedGroup){
            
            var direction = directionOfPushFromGroupToGroup(group);
        
            for (var dire in direction){

                try{

                    directions[dire] += direction[dire];

                }
                catch(err){

                    alert(err);

                }

            }
            
        }
        
    }
    
    return directions;
    
}

function directionOfPushFromGroupToGroup(groupOne, groupTwo){
    
    groupOne = groupOne || document.forms["options"].elements["chargeList"].value;
    
    groupTwo = groupTwo || 0;
    
    var c1 = groups[groupOne]["charge"];
    var x1 = groups[groupOne]["group"].left;
    var y1 = groups[groupOne]["group"].top;

    var f = groups[groupOne]["force"];
    
    console.log("Group One Force: " + groups[groupOne]["force"])
    
    var c2 = groups[groupTwo]["charge"];
    var x2 = groups[groupTwo]["group"].left;
    var y2 = groups[groupTwo]["group"].top;
    
    var direction = "";
    var result = {};
    
    if (c1 == c2){
        
        if (x1 > x2){

            direction += "left";

        }
        else if (x1 < x2){

            direction += "right";

        }
        else{

            direction += "equal";

        }
        
    }
    else{
        
        if (x1 > x2){

            direction += "right";

        }
        else if (x1 < x2){

            direction += "left";

        }
        else{

            direction = "equal";

        }
        
    }
    
    if (c1 == c2){
        
        if (y1 > y2){

            direction += "down";

        }
        else if (y1 < y2){

            direction += "up";

        }
        else{

            direction += "equal";

        }
        
    }
    else{
        
        if (y1 > y2){

            direction += "up";

        }
        else if (y1 < y2){

            direction += "down";

        }
        else{

            direction += "equal";

        }
        
    }
    
    result[direction] = f;
    
    console.log("Result:" + result.toString() + " | Direction: " + direction.toString() + " | Resultin Direction: " + result[direction])
    
    return result;
    
}

function giveResultingAngleOfGroups(fixedGroup){
    
    fixedGroup = fixedGroup || 0;
    
    var rx = 0.0;
    var ry = 0.0;
    
    for (var group in Object.keys(groups)){
        
        if (group != fixedGroup){

            rx += groups[group]["fx"];
            ry += groups[group]["fy"];   
            
        }
        
    }
    
    return resultingAngle(rx, ry);
    
}

function resultingAngle(fx, fy){
    
    var degree = (Math.atan(fy / fx) * 180 / Math.PI) + 180;
    
    if (degree == NaN || degree == undefined){
        
        // Just in case of huge error
        degree == 0;
        
    }
    
    return degree;
    
}
