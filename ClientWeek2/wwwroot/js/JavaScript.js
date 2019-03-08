$(document).ready(function () {
    $("#seasoningDiscount").on('click', {
        product: 'Chef Anton\'s Italian Seasoning',
        discount: '2111'
    }, runToast);

    $("#goGreenDiscount").on('click', {
        product: 'Genen Shouyu',
        discount: '2222'
    }, runToast);

    $("#halfPriceDiscount").on('click', {
        product: 'Schoggo Schokolade',
        discount: '2333'
    }, runToast);

    function runToast(event) {
        var toast = new Audio('media/toast.wav');
        //$(".result").html("Hello <b>world</b>!");
        //alert("Product: " + event.data.product + "\nDiscount: " + event.data.discount);
        $('#code').text(event.data.discount);
        $('#product').text(event.data.product);
        $('.toast').toast({ autohide: false }).toast('show');
        event.preventDefault();
        toast.pause();
        // reset the audio
        toast.currentTime = 0;
        // play audio
        toast.play();
    }

    // Close the toast with escape key
    $('body').keydown (function (event) {
        
        if (event.which == 27) {
            $('.toast').toast('hide');
            //alert("escape key pressed");
        }
           
    });
        
 

    /*$(function () {
        var toast = new Audio('media/toast.wav');
        $('.code').on('click', function (e) {
            e.preventDefault();
            // first pause the audio (in case it is still playing)
            toast.pause();
            // reset the audio
            toast.currentTime = 0;
            // play audio
            toast.play();
            $('#toast').toast({ autohide: false }).toast('show');
        });

    });*/
});