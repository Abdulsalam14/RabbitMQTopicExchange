"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/messageHub").build();

connection.start().then(function () {
    console.log("Started first")
}).catch(function (err) {
    return console.error(err.toString());
})


connection.on("Connect", function (info) {
    console.log("Connect Work first");
})
connection.on("Disconnect", function (info) {
    console.log("DisConnect Work first");
})


var checkboxes = document.querySelectorAll('input[type="checkbox"][name="color"]');
checkboxes.forEach((checkbox) => {
    checkbox.addEventListener("change", function () {
        if (checkbox.checked) {
            checkbox.parentElement.style.backgroundColor = "orange";
        } else {
            checkbox.parentElement.style.backgroundColor = ""; // Arka plan rengini kaldırmak için boş string kullanabilirsiniz.
        }
    });
});

document.getElementById("selectForm1").addEventListener("submit", function (event) {
    event.preventDefault();
    //console.log("Selected colors:", selectedColors);
});



$('#selectForm1').submit(function () {
    var selecteds = [];

    $('input[type="checkbox"][name="color"]:checked').each(function () {
        selecteds.push($(this).val()); // Değeri diziye ekle
    });

    if (selecteds.length == 0) {
        alert("No Selected")
        return;
    }

    $.ajax({
        url: '/Home/SaveSelects',
        method: 'POST',
        data: { selects: selecteds },
        success: function (response) {
            console.log('Colors saved successfully');
        },
        error: function (err) {
            console.error('Error saving colors',err);
        }
    });
});



