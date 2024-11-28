function Open_Modal(Vista, tittle, Large, btn_save, save_text, cancel_text) {
    //debugger;
    Large || false;
    btn_save || true;
    save_text || "Save";
    cancel_text || "Cancel";

    $('#NewModal').removeData("#ModalBody")

    $("#ModalBody").html(Vista);
    $("#NewModal").modal("show");
    if (Large == true)
        $(".modal-dialog").addClass("modal-lg").removeClass("modal-md");
    else
        $(".modal-dialog").removeClass("modal-lg").addClass("modal-md");

    $(".modal-title").html(tittle);
    if (btn_save == true)
        $("#btnsave").show();
    else
        $("#btnsave").hide();

    $("#btnCancel").show();
    $("#btnsave").html(save_text);
    $("#btnCancel").html(cancel_text);

}
function Open_Modal_(Vista, tittle, Large, btn_save, save_text, cancel_text, add_class, btn_other,other_text,other_class) {
    debugger;
    var all_class = "btn btn-primary";
  
    Large || false;
    btn_save || true;
    save_text || "Save";
    cancel_text || "Cancel";
    add_class || " ";
    other_class = all_class + " " + other_class;
    all_class = all_class + " " + add_class;
    btn_other || false;
    other_text || "";
     if (btn_other) {
        $("#btnOther").show();
        $("#btnOther").text(other_text);
        $("#btnOther").removeClass().addClass(other_class);

    } else {
        $("#btnOther").hide();

    }

     $('#GeneralModal').removeData("#ModalBody")

    $("#ModalBody").html(Vista);
    $("#GeneralModal").modal("show");
    if (Large == true)
        $(".modal-dialog").addClass("modal-lg").removeClass("modal-md");
    else
        $(".modal-dialog").removeClass("modal-lg").addClass("modal-md");

    $(".modal-title").html(tittle);
    if (btn_save == true) {
        $("#btnsave").show();
        $("#btnsave").removeClass().addClass(all_class);

    }
    else
        $("#btnsave").hide();

    $("#btnCancel").show();
    $("#btnsave").html(save_text);
    $("#btnCancel").html(cancel_text);

}
function Open_Front_Modal(Vista, tittle, Large, btn_save, save_text, cancel_text) {
    //debugger;
    Large || false;
    btn_save || true;
    save_text || "Save";
    cancel_text || "Cancel";

    $('#FrontModal').removeData("#FModalBody")

    $("#FModalBody").html("");
    $("#FModalBody").html(Vista);
    $("#FrontModal").modal("show");
    if (Large == true)
        $(".F_modal-dialog").addClass("modal-lg").removeClass("modal-md");
    else
        $(".F_modal-dialog").removeClass("modal-lg").addClass("modal-md");

    $(".F_modal-title").html(tittle);
    if (btn_save == true)
        $("#F_btnsave").show();
    else
        $("#F_btnsave").hide();


    $("#F_btnsave").html(save_text);
    $("#F_btnCancel").show();
    $("#F_btnCancel").html(cancel_text);

}

function Open_Front_Modal_(Vista, tittle, Large, btn_save, save_text, cancel_text,add_class) {
    //debugger;
    $("#F_btnsave").prop("disabled", false);

    var all_class = "btn btn-primary";
    Large || false;
    btn_save || true;
    save_text || "Save";
    cancel_text || "Cancel";
    add_class || " ";
    all_class = all_class + " "+  add_class;
    $('#FrontModal').removeData("#FModalBody")

    $("#FModalBody").html("");
    $("#FModalBody").html(Vista);
    $("#FrontModal").modal("show");
    if (Large == true)
        $(".F_modal-dialog").addClass("modal-lg").removeClass("modal-md");
    else
        $(".F_modal-dialog").removeClass("modal-lg").addClass("modal-md");

    $(".F_modal-title").html(tittle);
    if (btn_save == true) {
        $("#F_btnsave").show();
        $("#F_btnsave").removeClass().addClass(all_class);

    }
    else
        $("#F_btnsave").hide();


    $("#F_btnsave").html(save_text);
    $("#F_btnCancel").show();
    $("#F_btnCancel").html(cancel_text);

}
function show_Status_Modal(title, content, isLoading) {
    console.log(title, content, isLoading);
    if (isLoading) {
        $("#status_Modal_Document").css("display", "none");
        $("#status_Modal_Loading").css("display", "");
    } else {
        $("#status_Modal_Document").css("display", "");
        $("#status_Modal_Loading").css("display", "none");
        $("#status_Modal_Title").html("Prueba");
        $("#status_Modal_Body").html("Body");
    }
    $('#status_Modal').modal('show');
}
function show_Status_Modal_close() {
    $('#status_Modal').modal('hide');
}


function show_Status_Modal_(id_disabled) {
    console.log(id_disabled);

    if (id_disabled != null)
    {
        $("#" + id_disabled).prop("disabled", true);
    }
    $("#status_Modal_Document").css("display", "none");
    $("#status_Modal_Loading").css("display", "");
    $("#status_Modal_Body").html("Body")
    $('#status_Modal').modal('show');
}

function show_Status_Modal_close_(id_disabled) {
    if (id_disabled != null) {
        $("#" + id_disabled).prop("disabled", false);
    }
    $('#status_Modal').modal('hide');

}

function Close_Modal_() {
    var all_class = "btn btn-primary";
    $("#btnsave").removeClass().addClass(all_class);
    $("#NewModal").modal("hide");
    $("#btnsave").prop("disabled", false);


}

function Close_Front_Modal_() {
    var all_class = "btn btn-primary";
    $("#F_btnsave").removeClass().addClass(all_class);
    $("#FrontModal").modal("hide");
    $("#F_btnsave").prop("disabled", false);


}

function objectifyForm(formArray) {//serialize data function

    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}


