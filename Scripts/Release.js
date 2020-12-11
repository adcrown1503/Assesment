$(document).ready(function () {

    $("#btnCancel").click(function () {
        debugger
        $("#releaseId").val("0");
        $("#releaseName").val("");
        $('#releaseDate').val("");
        $("#RecType").val("New")
        $("#saveAsNew").prop("checked", false);
        $("#divSaveAsNew").hide();
        $("#btnDelete").hide();
        $("#btnCancel").hide();
        $('#releaseName').focus();

    }); 

    $('#saveAsNew').click(function () {
        if (!$(this).is(':checked')) {
            $("#RecType").val("Edit")
        } else {
            
            $("#RecType").val("New")
        }
    });

    $("#btnDelete").click(function () {
        $("#RecType").val("Delete")
    
        $("#btnSave").click();
    }); 
});

