"use strict"

$(document).ready(function () {
    $('.submit-delete-modal').click(function () { 
        $("exampleModal").modal('show');
    });

    $('.modal-footer > button[type="submit"]').click(function () {
        $('#removeFriend').submit();
    });

});