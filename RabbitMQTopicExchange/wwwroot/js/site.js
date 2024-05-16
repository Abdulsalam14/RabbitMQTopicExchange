
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