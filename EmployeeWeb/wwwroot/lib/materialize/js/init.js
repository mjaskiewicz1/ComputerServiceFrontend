(function ($) {
    $(function () {

        $('#layout-main-side-nav').sidenav();

        $(".collapsible").collapsible({
            inDuration: 400,
            outDuration: 400
        });
        $("#dropdown-user-avatar-trigger").dropdown({
            coverTrigger: false
        });
        $('select').formSelect();
        $('.tabs').tabs();
        $('.tooltipped').tooltip();



    }); // end of document ready
})(jQuery); // end of jQuery name space