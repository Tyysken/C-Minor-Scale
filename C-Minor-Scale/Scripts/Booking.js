var listOfBookings = [];
var listOfZid = [];
var listOfDesks = [];
var timeBooked = [];
var ownerOfTimeBooked = [];
var lastModified = 0;
var BookingStartTime = 0
var BookingEndTime = 0

var teacher = 6570433172733952
var LookFromTime = 0
var LookTooTime = 0
var newTimeFrom = 0
var newTimeToo = 0

$(document).ready(function () {
    document.getElementById('date').valueAsDate = new Date()
    LookFromTime = getButtonTime($('#date')[0].value, 8)
    LookTooTime = getButtonTime($('#date')[0].value, 18)
    StatusOfDesks(LookFromTime, LookTooTime)

    $("#dateButton").click(function () {
        newTimeFrom = getButtonTime($('#date')[0].value, 8)
        newTimeToo = getButtonTime($('#date')[0].value, 18)

        StatusOfDesks(newTimeFrom, newTimeToo)
    });

    $("#bookingButton").click(function () {
        checkAllTimeButtons()
        if (localStorage.getItem('role') == teacher)
            bookings()
        else
            book()
    });

})

function checkAllTimeButtons() {
    listOfZid = []
    $('input[type="checkbox"]').each(function () {
        if ($(this).is(":checked")) {
            listOfZid.push(parseInt(this.value.split('-')[0]))
            BookingStartTime = (parseFloat(this.value.split('-')[1]))
            BookingEndTime = (parseFloat(this.value.split('-')[2]))
            lastModified = (parseFloat(this.value.split('-')[3]))
        }
    });
}

function StatusOfDesks(startTime, endTime) {
    console.log("StartTime: " + startTime);
    console.log("EndTime: " + endTime);
    $.ajax({
        url: "http://localhost:60156/api/bookingapi?from=" + startTime + "&until=" + endTime + "&parent=5115225993379840",
        type: "GET",
        headers: getHeaders(),
        contentType: 'application/vnd.idesk-v5+json',
        success: function (data) {
            listOfBookings = data
            desks()
        },

        error: function (data) {
            $("#ErrorModal .modal-body").empty()
            switch (data.status) {
                case 400:
                    $("#ErrorModal .modal-body").append("<p>Select a valid date</p>")
                    $('#ErrorModal').modal('show')
                    break;
                default:
                    $("#ErrorModal .modal-body").append("<p>Something went wrong, please try again</p>")
                    $('#ErrorModal').modal('show')
                    break;
            }
        }
    });
}

function desks() {
    $.ajax({
        url: "https://stage-core.intelligentdesk.com/zone?realm=true",
        type: "GET",
        headers: getHeaders(),
        contentType: 'application/vnd.idesk-v5+json',
        success: function (data) {
            var desks = filterDesks(data);
            desks.sort(compareDesks);

            var parentdivRoom1 = document.getElementById('2440')
            $(parentdivRoom1).empty()

            var parentdivRoom2 = document.getElementById('1330')
            $(parentdivRoom2).empty()

            var date = $('#date')[0].value;

            $.each(desks, function (index, element) {
                if (element.Name.split(" ")[1] < 5) {
                    parentdivRoom1.appendChild(createButtonRow(element, date, listOfBookings, localStorage.getItem("user")));
                } else {
                    parentdivRoom2.appendChild(createButtonRow(element, date, listOfBookings, localStorage.getItem("user")));
                }
            });
            
            checkboxsButton()
        },

        error: function (data) {
            $("#ErrorModal .modal-body").empty()
            $("#ErrorModal .modal-body").append("<p>" + JSON.parse(data.responseText).Message + "</p>")
            $('#ErrorModal').modal('show')
        }
    });

}

function bookings() {
    $.ajax({
        url: "http://localhost:60156/api/bookingapi/multi/",
        type: "POST",
        headers: getHeaders(),
        data: JSON.stringify({
            "Owner": localStorage.getItem("user"),
            "Lastmodified": lastModified,
            "From": BookingStartTime,
            "Until": BookingEndTime,
            "Zids": listOfZid,
            "Subject": "bookingtest",
            "Private": "false"

        }),
        contentType: 'application/json',
        success: function (data) {
            alert('Success! As teacher');
            StatusOfDesks(LookFromTime, LookTooTime)
        },

        error: function (data) {
            $("#ErrorModal .modal-body").empty()
            switch (JSON.parse(data.responseText).Message) {
                case "E_MULTI_BOOKING_PARTIALLY_FAILED":
                    $("#ErrorModal .modal-body").append("<p>Not all desks were successfully booked</p>")
                    $('#ErrorModal').modal('show')
                    break;
                case "E_MULTI_BOOKING_FAILED":
                    $("#ErrorModal .modal-body").append("<p>Something went wrong with your bookings, maybe the desk is already booked</p>")
                    $('#ErrorModal').modal('show')
                    break;
                default:
                    $("#ErrorModal .modal-body").append("<p>Something went wrong, please try again</p>")
                    $('#ErrorModal').modal('show')
                    break;
            }
        }
    });
}

function book() {
    $.ajax({
        url: "http://localhost:60156/api/bookingapi",
        type: "POST",
        headers: getHeaders(),
        data: JSON.stringify({
            "Owner": localStorage.getItem("user"),
            "Lastmodified": lastModified,
            "From": BookingStartTime,
            "Until": BookingEndTime,
            "Zid": listOfZid[0],
            "Subject": "bookingtest",
            "Private": "false"

        }),
        contentType: 'application/json',
        success: function (data) {
            alert('Success! As student');
            StatusOfDesks(LookFromTime, LookTooTime)

        },

        error: function (data) {
            $("#ErrorModal .modal-body").empty()
            switch (JSON.parse(data.responseText).Message) {
                case "E_MULTI_BOOKING_PARTIALLY_FAILED":
                    $("#ErrorModal .modal-body").append("<p>You can only book one desk as a student</p>")
                    $('#ErrorModal').modal('show')
                    break;
                case "E_MULTI_BOOKING_FAILED":
                    $("#ErrorModal .modal-body").append("<p>Something went wrong with your bookings, maybe the desk is already booked</p>")
                    $('#ErrorModal').modal('show')
                    break;
                default:
                    $("#ErrorModal .modal-body").append("<p>Something went wrong, please try again</p>")
                    $('#ErrorModal').modal('show')
                    break;
            }
        }
    });
}

function checkboxsButton() {
    $('.button-checkbox').each(function () {

        // Settings
        var $widget = $(this),
            $button = $widget.find('button'),
            $checkbox = $widget.find('input:checkbox'),
            color = $button.data('color'),
            settings = {
                on: {
                    icon: 'glyphicon glyphicon-check'
                },
                off: {
                    icon: 'glyphicon glyphicon-unchecked'
                }
            };

        // Event Handlers
        $button.on('click', function () {
            $checkbox.prop('checked', !$checkbox.is(':checked'));
            $checkbox.triggerHandler('change');
            updateDisplay();
        });
        $checkbox.on('change', function () {
            updateDisplay();
        });

        // Actions
        function updateDisplay() {
            var isChecked = $checkbox.is(':checked');

            // Set the button's state
            $button.data('state', (isChecked) ? "on" : "off");

            // Set the button's icon
            $button.find('.state-icon')
                .removeClass()
                .addClass('state-icon ' + settings[$button.data('state')].icon);

            // Update the button's color
            if (isChecked) {
                $button
                    .removeClass('btn-default')
                    .addClass('btn-' + color + ' active');
            }
            else {
                $button
                    .removeClass('btn-' + color + ' active')
                    .addClass('btn-default');
            }
        }

        // Initialization
        function init() {

            updateDisplay();

            // Inject the icon if applicable
            if ($button.find('.state-icon').length == 0) {
                $button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i>');
            }
        }
        init();
    });
};
  