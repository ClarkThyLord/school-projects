var coulombConstant = 8990000000;

var chargeString = {0: "negative", 1: "positive"};
var chargeValue = {0: "-", 1: "+"};

var prefixString = {0: "", 1: "d", 2: "c", 3: "in", 4: "u", 5: "n", 6: "p", 7: "da", 8: "H", 9: "K", 10: "M", 11: "G", 12: "T"};
var prefixValue = {0: 1, 1: 0.1, 2: 0.01, 3: 0.001, 4: 0.000001, 5: 0.000000001, 6: 0.000000000001, 7: 10, 8: 100, 9: 1000, 10: 1000000, 11: 1000000000, 12: 1000000000000};

var unitsString = {0: "m", 1: "yd", 2: "ft", 3: "in", 4: "cm", 5: "mm", 6: "um", 7: "nm", 8: "km", 9: "mi", 10: "M"};
var unitsValue = { 0: 1, 1: 1.09361296, 2: 3.2808388799999997, 3: 39.370066559999997935, 4: 100, 5: 1000, 6: 1000000, 7: 1000000000, 8: 0.001, 9: 0.000621371, 10: 0.00053995663640604750835};


function updateRepresentation(){
    
    var formObject = document.forms["info"];
    
    document.getElementById("representation").innerHTML = "<h3>(" + chargeValue[formObject.elements["c1"].value] + " | " + formObject.elements["q1"].value.toString() + prefixString[formObject.elements["p1a"].value] + " C) <-----|" + formObject.elements["r1"].value.toString() + prefixString[formObject.elements["p1b"].value] + " " + unitsString[formObject.elements["u1"].value] + "|-----> (" + chargeValue[formObject.elements["c2"].value] + " | " + formObject.elements["q2"].value.toString() + prefixString[formObject.elements["p2a"].value] + "C) <-----|" + formObject.elements["r2"].value.toString() + prefixString[formObject.elements["p3b"].value] + " " + unitsString[formObject.elements["u2"].value] + "|-----> (" + chargeValue[formObject.elements["c3"].value] + " | " + formObject.elements["q3"].value.toString() + prefixString[formObject.elements["p3a"].value] + "C)</h3>";
    
}

function randomValues(){
    
    var formObject = document.forms["info"];
    
    // Atom One
    formObject.elements["c1"].value = (Math.floor(Math.random() * 2));
    formObject.elements["q1"].value = (Math.floor(Math.random() * 101) + 1);
    formObject.elements["p1a"].value = (Math.floor(Math.random() * 13));
    formObject.elements["r1"].value = (Math.floor(Math.random() * 101) + 1);
    formObject.elements["p1b"].value = (Math.floor(Math.random() * 13));
    formObject.elements["u1"].value = (Math.floor(Math.random() * 11));
    
    // Atom Two
    formObject.elements["c2"].value = (Math.floor(Math.random() * 2));
    formObject.elements["q2"].value = (Math.floor(Math.random() * 101) + 1);
    formObject.elements["p2a"].value = (Math.floor(Math.random() * 13));
    
    // Atom Three
    formObject.elements["c3"].value = (Math.floor(Math.random() * 2));
    formObject.elements["q3"].value = (Math.floor(Math.random() * 101) + 1);
    formObject.elements["p3a"].value = (Math.floor(Math.random() * 13));
    formObject.elements["r2"].value = (Math.floor(Math.random() * 101) + 1);
    formObject.elements["p3b"].value = (Math.floor(Math.random() * 13));
    formObject.elements["u2"].value = (Math.floor(Math.random() * 11));
    
    // document.getElementById("explain").innerHTML = (Math.floor(Math.random() * 2))
    
}

function processProblem(){
    
    var msg = ""
    var formObject = document.forms["info"];
    
    // Atom One
    var charge1 = chargeString[formObject.elements["c1"].value];
    
//    msg += "<br>Charge: " + charge1.toString()
    
    var quantity1 = formObject.elements["q1"].value;
    var prefix1A = prefixValue[formObject.elements["p1a"].value];
    
//    msg += "<br>Quantity: " + quantity1.toString()
//    msg += "<br>Prefix: " + prefix1A.toString()
    
    var quantity1 = quantity1 * prefix1A;
    
//    msg += "<br>Quantity Result: " + quantity1.toString()
    
    var distance1 = formObject.elements["r1"].value;
    var prefix1B = prefixValue[formObject.elements["p1b"].value];
    var unit1 = unitsValue[formObject.elements["u1"].value];
    
//    msg += "<br>Distance: " + distance1.toString()
//    msg += "<br>Prefix: " + prefix1B.toString()
//    msg += "<br>Unit: " + unit1.toString()
    
    var distance1 = (distance1 * prefix1B) / unit1;
    
//    msg += "<br>Distance Result: " + distance1.toString()
    
    // Atom Two
    var charge2 = chargeString[formObject.elements["c2"].value];
    
//    msg += "<br><br><br>Charge: " + charge2.toString()
    
    var quantity2 = formObject.elements["q2"].value;
    var prefix2A = prefixValue[formObject.elements["p2a"].value];
    
//    msg += "<br>Quantity: " + quantity2.toString()
//    msg += "<br>Prefix: " + prefix2A.toString()
    
    var quantity2 = quantity2 * prefix2A;
    
//    msg += "<br>Quantity: " + quantity2.toString()
    
    // Atom Three
    var charge3 = chargeString[formObject.elements["c3"].value];
    
//    msg += "<br><br><br>Charge:" + charge3.toString()
    
    var quantity3 = formObject.elements["q3"].value;
    var prefix3A = prefixValue[formObject.elements["p3a"].value];
    
//    msg += "<br>Quantity: " + quantity3.toString()
//    msg += "<br>Prefix: " + prefix3A.toString()
    
    var quantity3 = quantity3 * prefix3A;
    
//    msg += "<br>Quantity Result: " + quantity3.toString()
    
    var distance2 = formObject.elements["r2"].value;
    var prefix3B = prefixValue[formObject.elements["p3b"].value];
    var unit2 = unitsValue[formObject.elements["u2"].value];
    
//    msg += "<br>Distance: " + distance2.toString()
//    msg += "<br>Prefix: " + prefix3B.toString()
//    msg += "<brUnit: >" + unit2.toString()
    
    var distance2 = (distance2 * prefix3B) / unit2;
    
//    msg += "<br>Distance Result: " + distance2.toString()
    
    
    // Result One
    var result1 = (coulombConstant * quantity1 * quantity2) / (distance1 * distance1);
    
//    msg += "<br><br>Result: " + result1.toString()
    
    if (charge1 != charge2){
        
        var result1 = Math.abs(result1) * -1;
    
    }
    
//    msg += "<br><br>Result: " + result1.toString()
    
    
    // Result Two
    var result2 = (coulombConstant * quantity2 * quantity3) / (distance2 * distance2);
    
//    msg += "<br><br>Result: " + result2.toString()
    
    if (charge2 == charge3){
        
        var result2 = Math.abs(result2) * -1;
        
    }
    
//    msg += "<br><br>Result: " + result2.toString()
    
    var result = result1 + result2;
    if (result == 0){
        
        var moving = "not moving";
    
    }
    else if (result > 0){
     
        var moving = "right";
        
    }
    else{
        
        var moving = "left";
    
    }
    
//    msg += "<br><br>Result: " + result.toString()
    
    msg += "<b> - List All The Data That's Given</b><br><ul><li>q1=" + formObject.elements["q1"].value.toString() + prefixString[formObject.elements["p1a"].value] + " C</li><li>q2=" + formObject.elements["q2"].value.toString() + prefixString[formObject.elements["p2a"].value] + " C</li><li>q3=" + formObject.elements["q3"].value.toString() + prefixString[formObject.elements["p3a"].value] + " C</li><li>r1=" + formObject.elements["r1"].value.toString() + prefixString[formObject.elements["p1b"].value] + " " + unitsString[formObject.elements["u1"].value] + "</li><li>r2=" + formObject.elements["r2"].value.toString() + prefixString[formObject.elements["p3b"].value] + " " + unitsString[formObject.elements["u2"].value] + "</li></ul>"
    
    if (formObject.elements["p1a"].value != 0 || formObject.elements["p1b"].value != 0 || formObject.elements["p2a"].value != 0 || formObject.elements["p3a"].value != 0 || formObject.elements["p3b"].value != 0 || formObject.elements["u1"].value != 0 || formObject.elements["u2"].value != 0){
        
        distance1 = formObject.elements["r1"].value.toString() * prefixValue[formObject.elements["p1b"].value];
        distance2 = formObject.elements["r2"].value.toString() * prefixValue[formObject.elements["p3b"].value];
        
        msg += "<br><b> - Convert Unwanted Data Into What We Need </b><br>";
        if (formObject.elements["p1a"].value != 0){
            
            var value = formObject.elements["q1"].value.toString() * prefixValue[formObject.elements["p1a"].value];
                
            msg += "We want to convert <b>" + formObject.elements["q1"].value.toString() + prefixString[formObject.elements["p1a"].value] + " C</b> to a number we multiply.<br><b>" + prefixString[formObject.elements["p1a"].value] + "</b> is equal to <b>x" + prefixValue[formObject.elements["p1a"].value].toString() + "</b> so we do the following:<br><b>" + formObject.elements["q1"].value.toString() + " x " + prefixValue[formObject.elements["p1a"].value].toString() + " = " + value.toString() +"</b><br><b><i>" + value.toString() + "</i></b> is what we want for <b><i>q1</i></b>.<br><br>";
            
        }
        if (formObject.elements["p1b"].value != 0){
            
            var value = formObject.elements["r1"].value.toString() * prefixValue[formObject.elements["p1b"].value];
                
            msg += "We want to convert <b>" + formObject.elements["r1"].value.toString() + prefixString[formObject.elements["p1b"].value] + " C</b> to a number we multiply.<br><b>" + prefixString[formObject.elements["p1b"].value] + "</b> is equal to <b>x" + prefixValue[formObject.elements["p1b"].value].toString() + "</b> so we do the following:<br><b>" + formObject.elements["r1"].value.toString() + " x " + prefixValue[formObject.elements["p1b"].value].toString() + " = " + value.toString() +"</b><br><b><i>" + value.toString() + "</i></b> is what we want for <b><i>r1</i></b>.<br><br>";
            
        }
        if (formObject.elements["p2a"].value != 0){
            
            var value = formObject.elements["q2"].value.toString() * prefixValue[formObject.elements["p2a"].value];
                
            msg += "We want to convert <b>" + formObject.elements["q2"].value.toString() + prefixString[formObject.elements["p2a"].value] + " C</b> to a number we multiply.<br><b>" + prefixString[formObject.elements["p2a"].value] + "</b> is equal to <b>x" + prefixValue[formObject.elements["p2a"].value].toString() + "</b> so we do the following:<br><b>" + formObject.elements["q2"].value.toString() + " x " + prefixValue[formObject.elements["p2a"].value].toString() + " = " + value.toString() +"</b><br><b><i>" + value.toString() + "</i></b> is what we want for <b><i>q2</i></b>.<br><br>";
            
        }
        if (formObject.elements["p3a"].value != 0){
            
            var value = formObject.elements["q3"].value.toString() * prefixValue[formObject.elements["p3a"].value];
                
            msg += "We want to convert <b>" + formObject.elements["q3"].value.toString() + prefixString[formObject.elements["p3a"].value] + " C</b> to a number we multiply.<br><b>" + prefixString[formObject.elements["p3a"].value] + "</b> is equal to <b>x" + prefixValue[formObject.elements["p3a"].value].toString() + "</b> so we do the following:<br><b>" + formObject.elements["q3"].value.toString() + " x " + prefixValue[formObject.elements["p3a"].value].toString() + " = " + value.toString() +"</b><br><b><i>" + value.toString() + "</i></b> is what we want for <b><i>q3</i></b>.<br><br>";
            
        }
        if (formObject.elements["p3b"].value != 0){
            
            var value = formObject.elements["r2"].value.toString() * prefixValue[formObject.elements["p3b"].value];
                
            msg += "We want to convert <b>" + formObject.elements["r2"].value.toString() + prefixString[formObject.elements["p3b"].value] + " C</b> to a number we multiply.<br><b>" + prefixString[formObject.elements["p3b"].value] + "</b> is equal to <b>x" + prefixValue[formObject.elements["p3b"].value].toString() + "</b> so we do the following:<br><b>" + formObject.elements["r2"].value.toString() + " x " + prefixValue[formObject.elements["p3b"].value].toString() + " = " + value.toString() +"</b><br><b><i>" + value.toString() + "</i></b> is what we want for <b><i>r2</i></b>.<br><br>";
            
        }
        if (formObject.elements["u1"].value != 0){
        
            var value = distance1 / unitsValue[formObject.elements["u1"].value];
            
            msg += "We have our distance <b>" + distance1.toString() + unitsString[formObject.elements["u1"].value] + "</b> we want to convert that to <b>m</b>.<br><b>" + unitsString[formObject.elements["u1"].value] + "</b> is equal to <b>" + unitsValue[formObject.elements["u1"].value] + " m</b> so we do the following:<br><b>" + distance1.toString() + " / " + unitsValue[formObject.elements["u1"].value].toString() + " = " + value.toString() + "</b><br><b><i>" + value.toString() + "</i></b> is what we want for <b><i>r1</i></b>.<br><br>";
            
        }
        if (formObject.elements["u2"].value != 0){
        
            var value = distance2 / unitsValue[formObject.elements["u2"].value];
            
            msg += "We have our distance <b>" + distance2.toString() + unitsString[formObject.elements["u2"].value] + "</b> we want to convert that to <b>m</b>.<br><b>" + unitsString[formObject.elements["u2"].value] + "</b> is equal to <b>" + unitsValue[formObject.elements["u2"].value] + " m</b> so we do the following:<br><b>" + distance2.toString() + " / " + unitsValue[formObject.elements["u2"].value].toString() + " = " + value.toString() + "</b><br><b><i>" + value.toString() + "</i></b> is what we want for <b><i>r2</i></b>.<br><br>";
            
        }
    }
    
    msg += "<b> - Solve</b><br>Using logic, <b>Coulombs Constant " + coulombConstant.toString() + "</b> and two formuals we can the distance moved:<br><b>f = (k)(q1)(q2) / r^2</b> and <b>result = (f1) + (f2)</b><br>";
    
    // Solve F1
    var value = (coulombConstant * quantity1 * quantity2) / (distance1 * distance1);
    
    msg += "<h5>First Solve For f1:</h5><b>f1 = (" + coulombConstant.toString() + " * " + quantity1.toString() + " * " + quantity2.toString() + ") / (" + distance1.toString() + ")^2 = " + value.toString() + "</b><br>";
    
    if (charge1 == charge2){
        
        msg += "Since <b>charge one</b> and <b>charge two</b> have the same charge you make the resulting force positive.";
        msg += "<b>abs(" + value.toString() + ") = " + result1.toString() + "</b>";
        
    }
    
    
    // Solve F2
    var value = (coulombConstant * quantity2 * quantity3) / (distance2 * distance2);
    
    msg += "<h5>First Solve For f2:</h5><b>f2 = (" + coulombConstant.toString() + " * " + quantity2.toString() + " * " + quantity3.toString() + ") / (" + distance2.toString() + ")^2 = " + value.toString() + "</b><br>";
    
    if (charge2 == charge3){
        
        msg += "Since <b>charge two</b> and <b>charge three</b> have the same charge you make the resulting force negative.";
        msg += "<b>abs(" + value.toString() + ") * -1 = " + result2.toString() + "</b>";
        
    }
    
    // Solve All
    msg += "<h5>Get Addition Of Both Forces</h5><b>result = (" + result1.toString() + ") + (" + result2.toString() + ") = " + result.toString() + "</b><br>"
    
    msg += "<h2>Result: " + result.toString() + "<br>Moving: " + moving + "</h2>"
    
    document.getElementById("explain").innerHTML = msg;
    
}
