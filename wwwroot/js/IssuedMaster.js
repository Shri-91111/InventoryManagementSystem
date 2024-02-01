function loadNoOfQuantity()
{
    debugger
    // Create a new XMLHttpRequest object
    var xhr = new XMLHttpRequest();
    var selectedValue = document.getElementById("product").value;

    // Configure it: GET-request for the URL /getData
    xhr.open("GET", "/ims/IssuedMasters/GetData?value=" + selectedValue,true);

    // Setup callback function to handle the response
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            // Parse the JSON response
            var responseData = JSON.parse(xhr.responseText);

            // Extract specific data from the JSON (in this case, the doubled value)
            var processedData = responseData.result;
            console.log(processedData);
            // Update the content of the result div with the processed data
            document.getElementById("result").innerHTML = "No Of Quantity: " + processedData.noOfQuantity;
            document.getElementById("result1").innerHTML = "Item Category: " + processedData.itemCategory;
        }
    };

    // Send the request
    xhr.send();
}