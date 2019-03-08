$(function () {

    // Randomly pick an animated class for the Happy Birthday heading 1 text
    var randNbr = Math.floor(Math.random() * 10); // pick random nbr from 0 - 9
    var arr = ['bounce', 'flash', 'pulse', 'rubberBand', 'shake', 'swing', 'tada', 'wobble', 'jello', 'heartBeat'];
    var appliedClass = "animated " + arr[randNbr]; //construct the class
    $('#heading1').removeClass().addClass(appliedClass); // apply the class to the element

    $('#birthday').pickadate({ format: 'mmmm, d' });

    // uncheck all checkboxes (FireFox)
    $('.form-check-input').each(function () {
        $(this).prop('checked', false);
    });

    // event listener for check/uncheck
    $('.form-check-input').on('change', function () {
        // make the image visible
        $('#' + this.id + 'Img').css('visibility', 'visible') //if red checkbox click, it's the id of the red checkbox, etc.
        // animate balloon in/out based on checkbox
        $(this).is(':checked') ?
            $('#' + this.id + 'Img').removeClass().addClass('animated bounceInDown') :
            $('#' + this.id + 'Img').addClass('animated bounceOutUp');
    });

    // display toast if no selection is made and you hit the Submit button
    $('#submit').on('click', function () {
        var anyBoxesChecked = false;
        $('input[type="checkbox"]').each(function () {  //cycle through each checkbox
            if ($(this).is(":checked")) { //if the checkbox is checked
                anyBoxesChecked = true;
            }
        });

        if (anyBoxesChecked == false) {
            $('.toast').toast({ autohide: false }).toast('show');
            event.preventDefault();
        }

    });

    // select all or none checkboxes
    $('#allNone').on('click', function () {
        if ($(".form-check-input").prop("checked")) {
            $('input[type="checkbox"]').each(function () {
                if ($(this).is(":checked")) {
                    $('#' + this.id + 'Img').removeClass().addClass('animated bounceOutUp');
                }
            })
            $(".form-check-input").prop('checked', false);
            
        }
        else {
           
            $(".form-check-input").prop('checked', true);
            $('input[type="checkbox"]').each(function () {
                if ($(this).is(":checked")) {
                    $('#' + this.id + 'Img').removeClass().addClass('animated bounceInDown');
                }
            })
            
        }
    });


    // Change color of Happy Birthday heading based on mouse hovering over favorite balloon selections
    $('#label-blue').on('mouseover', function () {
        $('#heading1').css('color', 'blue');
    });

    $('#label-blue').on('mouseout', function () {
        $('#heading1').css('color', 'black');
    });

    $('#label-red').on('mouseover', function () {
        $('#heading1').css('color', 'red');
    });

    $('#label-red').on('mouseout', function () {
        $('#heading1').css('color', 'black');
    });

    $('#label-green').on('mouseover', function () {
        $('#heading1').css('color', 'green');
    });

    $('#label-green').on('mouseout', function () {
        $('#heading1').css('color', 'black');
    });




    // Close the toast with escape key
    $('body').keydown(function (event) {

        if (event.which == 27) {
            $('.toast').toast('hide');
            //alert("escape key pressed");
        }

    });

});