$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
});

function bindEvents() {
    $(".employeeDetails").on("click", function (event) {

        var employeeId = event.currentTarget.getAttribute("data-id");
        $.ajax({
            url: 'https://localhost:6001/api/internal/employees/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);
                showEmployeeDetailCard();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        var result = confirm("Are you sure you want to delete the data from database");
        if (result) {
            alert("Successfully deleted the data");
            $.ajax({
                url: 'https://localhost:6001/api/internal/employees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",

                success: function (result) {
                    location.reload();

                    $("#DeleteCard").html(newDeleteCard);
                    showDeleteCard();
                },
                error: function () {
                    console.log();
                }
            });
        }
        else {
            alert("Deletion Canceled");
        }
    });


    $("#CreateEmployee").on("click", function (event) {
        debugger;
        var Id = event.currentTarget.getAttribute("data-id");
        var name = $("#name").val();
        var employee = {}
        employee["Id"] = 0;
        employee["Name"] = name;
        employee["Department"] = name;
        employee["Age"] = 0;
        employee["Address"] = name
        //$.ajax({
        //    type: 'POST',
        //    url: "https://localhost:6001/api/employee",
        //    data: myKeyVals,
        //    dataType: "text",
        //    success: function (resultData) { alert("Save Complete") }
        //});


        $.ajax({
            url: 'https://localhost:6001/api/employee/test',
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: employee,
            success: function (result) {

                //$("#UpdateCard").html(newEmployeeCard);
                //showEmployeeUpdateCard();
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

