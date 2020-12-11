$(document).ready(function () {

    $("#btnNew").click(function () {

        //var r = $('<input/>').attr({ type: "button", id: "field" });onclick='deleteRow(this);'>Delete</button></td>";


        var r = "<td><button class='delete' onclick='deleteRow(this);'>Delete</button></td>";
        markup = "<tr> <td>one</td> <td>two</td> <td>three</td> <td>four</td>";
        markup= markup +  r;
        markup = markup + "</tr > ";

        $("#tblGrid").append(markup);
    
        
    
       
    });
 
   
    $("#btnAdd").click(function () {

        //var totalDays = getTotalDays();

        //if (totalDays == 0) {
        //    alert("Please enter days");
        //    return;
        //} else {
        //    $("#totaldays").text(totalDays);
        //}

        //AssembleProjectGateVersion();

        //AssembleEstimate();

        PostValues();
    });

    function AssembleEstimate() {

        var TotalPhase = $('.xclass').length;
        var TotalRole = $('.myClassx').length;

        var CounterPhase = 0;
        var CounterRole = 0;

        var pfobj = "";
        $('.myClass').each(function (i, e) {

            if ($.isNumeric($(this).val())) {

                if ($(this).val() > 0) {

                    // alert("this is trim" + $.trim($("#txtObject").val()));

                    if (pfobj.length == 0) {
                        pfobj = addQuote("ProjFuncId", 1) + "0,";
                        pfobj = pfobj + addQuote("GateversionId", 1) + $("#gate_gateId").val() + ",";
                        pfobj = pfobj + addQuote("FuncId", 1) + $("#functionality_FuncId").val() + ",";
                        pfobj = pfobj + addQuote("AppId", 1) + $("#application_appid").val() + ",";
                        pfobj = pfobj + addQuote("Rel_id", 1) + $("#release_releaseId").val() + ",";
                        pfobj = pfobj + addQuote("estimate", 1) + "[";

                    } else {

                        pfobj = pfobj + ",";
                    }

                   // $("#txtObject").val();

                    
                    //if ($.trim($("#txtObject").val()).length == 0) {

                    //} else {
                    //    $("#txtObject").val($("#txtObject").val() + ",");
                    //}



                   // textBoxValue = textBoxValue + parseFloat($(this).val());

                    //  alert("Phase Id =" + $(".xclass").eq(CounterPhase).text());
                    //  alert("Role Id =" + $(".myClassx").eq(CounterRole).val());
                    //  alert("Days = " + $(this).val())

                    var days = $(this).val();
                    var phaseId = $(".xclass").eq(CounterPhase).text();
                    var roleId = $(".myClassx").eq(CounterRole).val()

                    var estobj= CreateEstimateJsonObject(phaseId, roleId, days);

                    pfobj = pfobj + estobj;
                }

            } //else {
            //   $(this).val(0);
            // }


          //  $(e).css('backgroundColor', 'red');

            CounterPhase++;


            if (CounterPhase >= TotalPhase) {
                CounterPhase = 0;

                CounterRole++;
            }
            if (CounterRole >= TotalRole) {
                CounterRole = 0;
            }
        });

        pfobj = pfobj + "]}";

      
        $("#txtObject").val($("#txtObject").val() + pfobj)
       
    }

    $("#validateDays").click(function () {
       
        

       

        $('.myClassx').each(function () {
            //alert($(this).text());
            TotalRole++;
        });
        
        $('.xclass').each(function (i, e) {
            //alert($(this).text());
            TotalPhase++;
        });
      //  alert($("#functionality_FuncId").val());
       // alert($("#application_appid").val());
      //  alert($("#release_releaseId").val());
      
     
        
    })

    function getTotalDays() {
        
        var day = 0;
        $('.myClass').each(function () {

            if ($.isNumeric($(this).val())) {

                if ($(this).val() > 0) {
                    day = day + parseFloat($(this).val());
                }
            }
       
        });

        return day;
    }
    function AssembleProjectGateVersion() {

        var projectStr = "{\"project\":{";

        projectStr = projectStr + addQuote("id", 1) + $("#txtProjectId").val() + ",";
        projectStr = projectStr + addQuote("name", 1) + addQuote($("#txtProjectName").val(), 0) + ",";
        projectStr = projectStr + addQuote("assesmentDate", 1) + addQuote($("#txtAssesmentDate").val(), 0) + ",";
        projectStr = projectStr + addQuote("desiredReleaseDate", 1) + addQuote($("#txtDesiredDate").val(), 0) + ",";
        projectStr = projectStr + addQuote("impactType", 1) + addQuote($("#txtImpactType").val(), 0) + ",";
        projectStr = projectStr + addQuote("assumption", 1) + addQuote($("#txtAssumption").val(), 0) + ",";
        projectStr = projectStr + addQuote("comments", 1) + addQuote($("#txtComments").val(), 0) + ",";
        projectStr = projectStr + addQuote("statusId", 1) + $("#projectStatus_statusId").val();
        projectStr = projectStr + "},";

        projectStr = projectStr + addQuote("gateVersion", 1) + "{";
        projectStr = projectStr + addQuote("gvId", 1) + $("#txtGateRecNo").val() + ",";
        projectStr = projectStr + addQuote("gvProjectId", 1) + "0,";
        projectStr = projectStr + addQuote("gvGateId", 1) + $("#gate_gateId").val() + ",";
        projectStr = projectStr + addQuote("gvComments", 1) + addQuote($("#txtgvComments").val(), 0) + ",";
        projectStr = projectStr + addQuote("gvAssumption", 1) + addQuote($("#txtgvAssumption").val(), 0) + ",";
        projectStr = projectStr + addQuote("gvStatus", 1) + addQuote($("#txtGateStatus").val(), 0) + "},";

        $("#txtObject").val(projectStr);
    }

    function CreateEstimateJsonObject(phaseId, roleId, days) {

       var mdate = new Date().getMonth() + 1 + "-" + new Date().getDay() + "-" + new Date().getFullYear();

      

        var str = "{";
        str = str + addQuote("estId", 1) + "0,";
        str = str + addQuote("projectFunctioId", 1) + "0,";
        str = str + addQuote("estDays", 1) + days +",";
        str = str + addQuote("phaseCode", 1) + phaseId+",";
        str = str + addQuote("roleId", 1) + roleId +",";
        str = str + addQuote("estimatedOn", 1) + addQuote(mdate,0)+",";
        str = str + addQuote("userEstmate", 1) + addQuote("adeel",0)+"}";
    
        return str;
       // $("#txtObject").val($("#txtObject").val()+str);
    }

    function PostValues() {

        var model = JSON.parse($("#txtObject").val());

        $.ajax
            (
                {
                    type: "POST",
                    url: "/Project/Create",
                    data: JSON.stringify(model),
                    //data:model,
                    contentType: "application/json; charset=utf-8",
                    dataType: "HTML",
                    success: function (response) {
                        alert("success");
                        alert(response);
                    },
                    failure: function (response) {
                        alert("failure");
                        alert(response);
                    },
                    error: function (error) {
                        alert("error");
                        alert(error);
                    }
                }
            ); // ajax


    }
    $("#btnSave").click(function () {
        var projectStr = "{\"project\":{";
       // projectStr = projectStr + "\"id\":" + $("#txtProjectId").val() + ",";
        projectStr = projectStr + addQuote("id",1) + $("#txtProjectId").val() + ",";
        projectStr = projectStr + addQuote("name",1) + addQuote($("#txtProjectName").val(),0)+ ",";
        projectStr = projectStr + addQuote("assesmentDate",1) + addQuote($("#txtAssesmentDate").val(),0) + ",";
        projectStr = projectStr + addQuote("desiredReleaseDate",1) + addQuote($("#txtDesiredDate").val(),0) + ",";
        projectStr = projectStr + addQuote("impactType",1) + addQuote($("#txtImpactType").val(),0)+ ",";
        projectStr = projectStr + addQuote("assumption",1) + addQuote($("#txtAssumption").val(),0) + ",";
        projectStr = projectStr + addQuote("comments",1) + addQuote($("#txtComments").val(),0) + ",";
        projectStr = projectStr + addQuote("statusId",1) + $("#projectStatus_statusId").val();
        projectStr = projectStr + "},";

        projectStr = projectStr + addQuote("gateVersion", 1) + "{";
        projectStr = projectStr + addQuote("gvId", 1) + $("#txtGateRecNo").val()+",";
        projectStr = projectStr + addQuote("gvProjectId", 1) + "0,";
        projectStr = projectStr + addQuote("gvGateId", 1) + $("#gate_gateId").val()+ ",";
        projectStr = projectStr + addQuote("gvComments", 1) + addQuote($("#txtgvComments").val(), 0)+",";
        projectStr = projectStr + addQuote("gvAssumption", 1) + addQuote($("#txtgvAssumption").val(), 0)+",";
        projectStr = projectStr + addQuote("gvStatus", 1) + addQuote($("#txtGateStatus").val(), 0)+"}}";
       
        $("#txtObject").val(projectStr);

        var model = JSON.parse($("#txtObject").val());

        $.ajax
            (
                {
                    type: "POST",
                    url: "/Project/Create",
                    data: JSON.stringify(model),
                    //data:model,
                    contentType: "application/json; charset=utf-8",
                    dataType: "HTML",
                    success: function (response) {
                        alert("success");
                        alert(response);
                    },
                    failure: function (response) {
                        alert("failure");
                        alert(response);
                    },
                    error: function (error) {
                        alert("error");
                        alert(error);
                    }
                }
            ); // ajax
      
    })
    function addQuote(fieldname,indicator)
    {
        if (indicator == 1) {
            return "\"" + fieldname + "\":";
        } else {
            return "\"" + fieldname + "\"";
        }
       
    }
    $("#btnSave2").click(function () {

     


       // 
        //var project = new Object();


        //project.id = 0;
        //project.name = "Swift";
        //project.assesmentDate = "12/31/2020";
        //project.desiredReleaseDate = "01/01/2021";
        //project.impactType = "small";
        //project.assumption = "This is assumption";
        //project.comments = "This is comments";
        //project.statusId = 1;

        //var gateversion = new Object();

        //gateversion.gvId = 0;
        //gateversion.gvProjectId = 0;
        //gateversion.gvGateId = 1;
        //gateversion.gvComments = "this is gate comments";
        //gateversion.gvAssumption = "this is gate assumption";
        //gateversion.gvStatus = "Final";
        //gateversion.gvLine = 1

        //var Function = new Object();
        //Function = { FuncId: 0, FuncName: "change database schema" }

        //var ProjFunc = new Object();
        //ProjFunc = { ProjFuncId: 0, GateversionId: 2, FuncId: 0, AppId: 43, Rel_id: 2 }

        //var daysEstimate = new Object();
        //daysEstimate = [
        //    { estId: 0, projectFunctioId: 0, estDays: 1, phaseCode: 1, roleId: 1, estimatedOn: new Date(), userEstmate: 'Adeel' },
        //    { estId: 0, projectFunctioId: 0, estDays: 2, phaseCode: 1, roleId: 2, estimatedOn: new Date(), userEstmate: 'Adeel' },
        //    { estId: 0, projectFunctioId: 0, estDays: 3, phaseCode: 1, roleId: 3, estimatedOn: new Date(), userEstmate: 'Adeel' }
        //]


        //var Wrapper01 = new Object();

        //Wrapper01.functionality = Function;
        //Wrapper01.projectFunction = ProjFunc;
        //Wrapper01.estimate = daysEstimate;

        //threeObjects.one = Function;
        //threeObjects.two = ProjFunc;
        //threeObjects.three = daysEstimate;

      //  var Wrapper02 = new Object();

       // Wrapper02.FunctionList = [{ Wrapper01: Wrapper01 }]



     //   var Wrapper03 = new Object();

     //   Wrapper03.project = project;
     //   Wrapper03.gateVersion = gateversion;
     //   Wrapper03.ListFunctionalities = Wrapper02.FunctionList;
       // AllObjects.fun = fourthObject;
       
        //var model = {'Contact': { 'Address':'Cougar', 'City':'Toronto', 'State':'Ontario'},
        //    'FirstName':'adeel',
        //    'LastName':'khan',
        //    'Profession':'87'};

      //  var model = {
      //      'project': {
      //          'id': 0,
      //          'name': 'Swift',
      //          'assesmentDate': '12/31/2020',
      //          'desiredReleaseDate': '01/01/2021',
      //          'impactType': 'small',
      //          'assumption': 'This is assumption',
      //          'comments': 'This is comments',
      //          'statusId': 1
      //      },
      //      'gateVersion': {
      //          'gvId': 0,
      //          'gvProjectId': 0,
      //          'gvGateId': 1,
      //          'gvComments': 'this is gate comments',
      //          'gvAssumption': 'this is gate assumption',
      //          'gvStatus': 'Final',
      //          'gvLine': 1
      //      },
      //      'functionality': [
      //                  { 'FuncId': 0, 'FuncName': 'change database schema1' },
						//{ 'FuncId': 0, 'FuncName': 'change database schema2' },
						//{ 'FuncId': 0, 'FuncName': 'change database schema3' },
						//{ 'FuncId': 0, 'FuncName': 'change database schema4' }
      //      ]
      //  }
        var model = JSON.parse($("#txtObject").val());
        //var Contact = new Object();
        //Contact.FirsName = 'Adeel';
        //Contact.LastName = 'Khan';
        //Contact.Profession = "Programmer";
        //var Address = new Object();
        //Address.Adddress = "Cougar";
        //Address.City = "Toronto";
        //Address.State = "Ontario";


       // var model = { Wrapper03: Wrapper03 };
        // var model = { project: project, gateversion: gateversion};
        debugger
        $.ajax
            (
                {
                    type: "POST",
                    url: "/Project/Create",
                   data: JSON.stringify(model),
                    //data:model,
                   contentType: "application/json; charset=utf-8",
                   dataType: "HTML",
                    success: function (response) {
                        alert("success");
                        alert(response);
                    },
                    failure: function (response) {
                        alert("failure");
                        alert(response);
                    },
                    error: function (error) {
                        alert("error");
                        alert(error);
                    }
                }
            ); // ajax
    }); //save button

});