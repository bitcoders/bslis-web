var workingId;
var addNewItem = function (reportCode) {
    var param = JSON.stringify({
        'ReportCode': reportCode,
        'DataType': $("#DataType").val(),
        'CellFrom': $("#CellFrom").val(),
        'CellTo': $("#CellTo").val(),
        'Value': $("#Value").val(),
        'Bold': $("#Bold").prop('checked') ? "true" : "false",
        'Italic': $("#Italic").prop('checked') ? "true" : "false",
        'FontColor': null,
        'BackGroundColor': null,
        'NumerFormat': null,
        'HelpText': ''
    });
    $.ajax({
        type: 'POST',
        url: '/api/ExcelReportTemplate/Add',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: param,
        success: (Response) => {
            //console.log(Response);
            $("#DataType").val('');
            $("#CellFrom").val('');
            $("#CellTo").val('');
            $("#Value").val('');
            $("#Bold").prop('checked', false);
            $("#Italic").prop('checked', false);
            showSuccessToastCustomMessage("Data saved successfully.");
            $("#DataType").focus();
        },
        Error: (Error) => {
            showDangerToast("Failed to save details!");
            console.warn(Error);
        }
    });
};

var getDetailsOfSelectedItem = function (id) {
    workingId = id; // change the working id to the recently selected id, so that the update api will update currently selected id not the previous one.
    var param = JSON.stringify({ 'Id': id });
    if ($('#btnUpdate').length > 0) {

        $('#btnUpdate').remove();
    }
    $.ajax({
        type: 'POST',
        url: '/api/ExcelReportTemplate/GetDetailsOfTemplateRowById',
        data: param,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (Response) => {
            //console.log(Response);
            $("#btnSave").hide();
            if (Response.statusCode == 200) {
                workingId = Response.model["Id"];
                $("#DataType").val(Response.model["DataType"]);
                $("#CellFrom").val(Response.model["CellFrom"]);
                $("#CellTo").val(Response.model["CellTo"]);
                $("#Value").val(Response.model["Value"]);

                if (Response.model["Bold"] == true) {
                    $("#Bold").prop('checked', true);
                } else {
                    $("#Bold").prop('checked', false);
                }

                if (Response.model["Italic"] == true) {
                    $("#Italic").prop('checked', true);
                } else {
                    $("#Italic").prop('checked', false);
                }
                console.log(workingId);
               
                if ($('#btnUpdate').length <= 0) {

                    $(".btnContainer").append($('<input/>').attr({ type: 'button', name: 'btnUpdate', id: 'btnUpdate', class: 'btn btn-sm btn-inverse-warning', value: 'Update', onclick: 'editSelectedTemplateItem(' + workingId + ');' }));
                }
                //else {
                //    $(".btnContainer").append($('<input/>').attr({ type: 'button', name: 'btnUpdate', id: 'btnUpdate', class: 'btn btn-sm btn-inverse-warning', value: 'Update', onclick: 'editSelectedTemplateItem(' + workingId + ');' }));
                //}
                
                
            }
            
        },
        Failure: (Error) => {
            
            console.log(Error);
        }
    });
};

var editSelectedTemplateItem = function (id) {
    console.log('working id to update' + id);
    var param = JSON.stringify({
        'Id': id,
        'DataType': $("#DataType").val(),
        'CellFrom': $("#CellFrom").val(),
        'CellTo': $("#CellTo").val(),
        'Value': $("#Value").val(),
        'Bold': $("#Bold").prop('checked') ? "true" : "false",
        'Italic': $("#Italic").prop('checked') ? "true" : "false",
        'FontColor': null,
        'BackGroundColor': null,
        'NumerFormat': null,
        'HelpText': ''
    });

    $.ajax({
        type: 'POST',
        url : '/api/ExcelReportTemplate/Edit',
        data : param,
        dataType: 'json',
        contentType: 'application/json; charset = utf-8',
        success: (Response) => {
            console.log(Response);
            $("#btnUpdate").remove();
            $("#btnSave").show();
            clearForm();
            showSuccessToastCustomMessage("Data updated successfully");
        },
        Error: (Error) => {
            showDangerToast("Failed to Update details. "+Error);
        }
    });
};

var clearForm = function () {
    $("#DataType").val('');
    $("#CellFrom").val('');
    $("#CellTo").val('');
    $("#Value").val('');
    $("#Bold").prop('checked', false);
    $("#Italic").prop('checked', false);
    $("#DataType").focus();
};
(function ($) {
    'use strict';
    var columnList = [];
    
    var datafetched = false;
    var getSchmaColumnList = function () {
        var param = JSON.stringify({
            'ReportSchemaCode': $("#reportSchemaCode").val()
        });
        $.ajax({
            type: 'Post',
            url: '/api/ReportSchmeaColumn/ColumnList',
            data: param,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: (Response) => {
                console.log(Response);
                $.each(Response['Model'], function (key, data) {
                    columnList.push(data['ColumnText']);
                });
            },
            Error: (Error) => {

            }
        });
    }
    getSchmaColumnList();
    $("#DataType").focusout(function () {
        
        if ($("#DataType").val() == 12) {
            // if column name is not fetched run the function other wise dont run the function
            if (datafetched == false) {
                search(columnList);
            }
            
        }
    });

    var search = function (columnList) {
        datafetched = true;
        var substringMatcher = function (strs) {
            return function findMatches(q, cb) {
                var matches, substringRegex;

                // an array that will be populated with substring matches
                matches = [];

                // regex used to determine if a string contains the substring `q`
                var substrRegex = new RegExp(q, 'i');

                // iterate through the pool of strings and for any string that
                // contains the substring `q`, add it to the `matches` array
                for (var i = 0; i < strs.length; i++) {
                    if (substrRegex.test(strs[i])) {
                        matches.push(strs[i]);
                    }
                }

                cb(matches);
            };
        };

        var searchList = columnList;
        console.log('searchlist = ' + searchList);
        $('#the-basics .typeahead').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
        }, {
            name: 'columnList',
            source: substringMatcher(columnList)
        });
        // constructs the suggestion engine
        var columnList = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            // `states` is an array of state names defined in "The Basics"
            local: columnList
        });

        $('#bloodhound .typeahead').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
        }, {
            name: 'columnList',
            source: columnList
        });
    }
})(jQuery);

