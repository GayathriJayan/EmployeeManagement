$(document).ready(function () {
    bindEvents();
    initialize();
});
var url = 'https://localhost:6001/api/internal/employees'
function bindEvents() {
    $("#CreateEmployee").on("click", function (event) {
       
        $("#hdnType").val("insert");   
            create();          
    });
    $(".employeeDetails").on("click", function (event) {

        var employeeId = event.currentTarget.getAttribute("data-id");
        $("#employeeId").val(employeeId);
        var result = getEmployeeDetails(employeeId);
        var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

        $("#EmployeeCard").html(newEmployeeCard);
        showEmployeeDetailCard();

    });
    $(".employeeEdit").on("click", function (event) {
        console.log("update");
        var employeeId = event.currentTarget.getAttribute("data-id");
        $("#hdnType").val("update");
        $("#hdnEmployeeId").val(employeeId);
        var result = getEmployeeDetails(employeeId);
        if (result != undefined) {
            create();
            $("#name").val(result.name);
            $("#Department").val(result.department);
            $("#age").val(result.age);
            $("#address").val(result.address);
        }
    });

    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        var urls = url + '/' + employeeId;
        var result = confirm("Are you sure you want to delete the data from database");
        if (result) {
            $.ajax({
                url: urls,
                type: 'DELETE',
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    alert("Successfully deleted the data");
                    location.reload();
                },
                error: function () {
                    console.log();
                }
            });
        }
        else {
        }
    });


    // $("#submit").on("click", function (event) {
    $("#insertUpdate").submit(function (event) {
          var employee;
          var urls;
        if ($("#hdnType").val() == "insert")
        {
          
          if (name != null | Department != null | age != null | address != null ){
                console.log("gvkulox");
                employee = JSON.stringify(getEmployee(2));
                urls = url + '/employee';
          }
          else if (name == "" | Department == "" | age == "" | address == "") {
                console.log("invalid");

            }
                
         }
         else {
            employee = JSON.stringify(getEmployee(1));
            urls = url + '/employee-update';
          }
        
        $.ajax({
            url: urls,
            type: 'POST',         
            contentType: "application/json; charset=utf8",
            data: employee,
            async: false,
            success: function (result) {
                console.log("insert");
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
}

function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}

function showDeleteCard() {
    $("#DeleteCard").show();
}

function showEmployeeUpdateCard() {
    $("#UpdateCard").show();
}
function getEmployee(type, employeeId) {
    var employee = {}
    if (type == 1)
        employee["Id"] = parseInt($("#hdnEmployeeId").val());
    employee["Name"] = $("#name").val();
    employee["Department"] = $("#Department").val();
    employee["Age"] = parseInt($("#age").val());
    employee["Address"] = $("#address").val();
    return employee;
}
function create() {
    $("#employeeList").hide();
    $("#EmployeeCard").hide();
    $("#insertUpdate").show()
}
function initialize() {
    $("#employeeList").show();
    $("#EmployeeCard").hide();
    $("#insertUpdate").hide()
}
function getEmployeeDetails(employeeId) {
    var data;
    var urls = url + "/" + employeeId;
    $.ajax({
        url: urls,
        type: 'GET',
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            data = result;
        },
        error: function (error) {
            console.log(error);
        }
    });
    return data;
}
function clear() {
    $("#name").val("");
    $("#Department").val("");
    $("#age").val("");
    $("#address").val("");
    $("#hdnEmployeeId").val("");
}
