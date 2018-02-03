jQuery(document).ready(function() {

    //$(".asd").css('background-color', 'silver');
    // $(".qwe").css('background-color', 'gold');
    $(".test1:even").css('background-color', 'silver'); //4etnie

    //var array = $("tr");
    //for (var i = 0; i < array.length; i++) {
    //    if (i%2!==1) {
    //        $(array[i]).css('background-color', 'silver');
    //        console.log(i.toString() + ". " + array[i].innerHTML);
    //    }
    //}

    // $('.asd').attr('testAttr', "123");;

    //$('a').each(function (index, elem) {
    //    $('a').first().prop('href', '33.html'); //1ssilka 33
    //    console.log($(elem).prop('href'));
    //});
    //$("tr:even").each(function (index, elem) {

    //    console.log(index + ". " + elem.innerHTML);

    //    $(elem).css('background-color', 'red'); 

    //});
    $('.asd').html("<div class='test2'>rrrr<div>")

});

function onCkickTest() {
    $('#btnTest').trigger('click');
}

$('#btnTest').on('click',
    function() {
        alert('BLA');
    });



function pis() {
    $('.asd').trigger('click');
}

$('.asd').on('click',
    function () {
        $('.asd').css('background-color', 'green');
    });





$("#inputTest").on("keyup",
    function(value) {
        var res = $(this).val();
        console.log('event:', res);
    });


setTimeout(() => {

    onAjax();

    },
    5000);

function onAjax() {
    $.ajax({
        type: 'GET',
        url: '/Home/AjaxTest',
        success: function(data) {
            alert(data);
        },
        error: function(err) {
            console.log('err', err);
        }
    });
}

