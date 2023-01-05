// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function clearScreen() {
    document.getElementById("result").value = "";
    document.getElementById("Query").value = "";
}

// This function display values
function display(value) {
    document.getElementById("Query").value += value;
    var Query = document.getElementById("Query").value;
    var Occurance = Query.indexOf('+' || '-' || '*' || '/') > -1
        console.log(str.indexOf(char1) > -1);
}

// This function evaluates the expression and returns result
function calculate() {
    var Query = document.getElementById("Query").value;
    var Result = eval(Query);
    document.getElementById("result").value = Result;
    var props =
    {
        Input: document.getElementById("Query").value,
        FinalResult: document.getElementById("result").value
    };
    //alert(Input);
    //alert(FinalResult);
    $.ajax({
        url: '/Home/SaveCurrentCalculation',
        type: 'POST',
        data: JSON.stringify(props),
        contentType: 'application/json',
        dataType: 'json',
        success: function (userlogin) {
            if (userlogin == true) {
                $.ajax({
                    url: '/Home/GetLastFiveCalculations',
                    type: 'Get',
                    data: null,
                    contentType: 'application/json',
                    dataType: 'json',
                    success: function (calculationsList) {
                        if (calculationsList.count > 0) {
                            window.location.href = '/Home/Index';
                        }
                    }
                });
            }
        },
        error: function () {
            alert('Some error Occured')
        }
    });
}

function DeleteCalculation(Id) {
    alert(Id);
    $.ajax({
        url: '/Home/DeleteCalculation',
        type: 'Delete',
        data: JSON.stringify(Id),
        contentType: 'application/json',
        dataType: 'json',
        success: function (userlogin) {
            if (userlogin == true) {
                alert('Calculation Deleted Successfully!!');
                window.location.href = '/Home/Index';
            }
        },
        error: function () {
            alert('Some error Occured')
        }
    });
}

function DeleteAllCalculation() {
    $.ajax({
        url: '/Home/DeleteLastFiveCalculations',
        type: 'Delete',
        data: null,
        contentType: 'application/json',
        dataType: 'json',
        success: function (userlogin) {
            if (userlogin == true) {
                alert('Calculation Deleted Successfully!!');
                window.location.href = '/Home/Index';
            }
        },
        error: function () {
            alert('Some error Occured')
        }
    });
}