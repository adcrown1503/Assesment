$(document).ready(function () {

    $("#divSaveAsNew").css('display', 'none');
    $("#btnDelete").css('display', 'none');
   $("#btnCancel").css('display', 'none');
    $("#txtAppName").focus();

    $("#btnSave").click(function () {
        
        $("#lblError").text("");
        var length = $("#txtAppName").val().length;

        if (length > 25 || length == 0) {
            $("#lblError").css("color", "Red");
            $("#lblError").text("maximum length 25 charecters");
            $("#txtAppName").focus();
            return false;
        }
       

        var saveasnew = null;

        if ($("#saveAsNew").prop("checked") == true) {
            saveasnew = "ON";
            
        } else {
            saveasnew = "OFF";
        }

        
        // alert(saveasnew);

        var application = new Object();

        if ($("#txtAppId").val().length > 0) {
            application.appId = $("#txtAppId").val();
        } else {
            application.appId = 0;
        }

        application.appName = $("#txtAppName").val();
        
        if ($("#txtDeleteId").val() == "Delete") {
            saveasnew = "Delete";
        }

        var model = { app: application, saveasnew: saveasnew };
       

        $.ajax({
            type: "POST",
            url: "/Application/Add",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "HTML",
            success: function (response) {
                $("#txtAppId").val("");
                $("#txtAppName").val("");

                if ($("#txtDeleteId").val() == "Delete") {
                    $("#lblError").text("Record Deleted");
                    $("#txtDeleteId").val("")
                } else {
                    $("#lblError").text("Record saved");
                }
                
                $('#divShowApps').html(response);
            },
            failure: function (response) {

                $("#lblError").text("Failure");
            },
            error: function (response) {                
                
                $("#lblError").text("Record not saved");

            }

        }) // ajax

        $("#saveAsNew").prop("checked", false);
        $("#divSaveAsNew").css('display', 'none');
        $("#btnDelete").css('display', 'none');
        $("#btnCancel").css('display', 'none');

        $("#lblError").css("color", "Red");
        $("#txtAppName").focus()
    }); 

    $("#btnCancel").click(function () {
        $("#txtAppId").val("");
        $("#txtAppName").val("");
        $("#saveAsNew").prop("checked", false);
        $("#divSaveAsNew").css('display', 'none');
        $("#btnDelete").css('display', 'none');
        $("#btnCancel").css('display', 'none');

        
    }); 

    $("#btnDelete").click(function () {

        $("#txtDeleteId").val("Delete");
        $("#btnSave").click();
    });

 
});
