var listOfBookings = [];
var listOfZid = [];
var timeBooked = [];
var ownerOfTimeBooked = [];
var lastModified = 0;
var BookingStartTime = 0
var BookingEndTime = 0

var teacher = 6570433172733952
var LookFromTime = 0
var LookTooTime = 0

$(document).ready(function () {
    document.getElementById("date").valueAsDate = new Date()
    LookFromTime = new Date(document.getElementById('date').value)
    LookTooTime = new Date(document.getElementById('date').value)
    LookFromTime.setHours('08')
    LookFromTime.setMinutes('00')
    LookFromTime.setSeconds('00')
    LookFromTime.setMilliseconds('00')
    LookTooTime.setHours('18')
    LookTooTime.setMinutes('00')
    LookTooTime.setSeconds('00')
    LookTooTime.setMilliseconds('00')
    LookFromTime = Math.round(LookFromTime)
    LookTooTime = Math.round(LookTooTime)
    console.log(LookFromTime, LookTooTime)

    StatusOfDesks(LookFromTime, LookTooTime)


    $("#dateButton").click(function () {
        var newTimeFrom = new Date(document.getElementById('date').value)
        var newTimeToo = new Date(document.getElementById('date').value)
        newTimeFrom.setHours('08')
        newTimeFrom.setMinutes('00')
        newTimeFrom.setSeconds('00')
        newTimeFrom.setMilliseconds('00')
        newTimeToo.setHours('18')
        newTimeToo.setMinutes('00')
        newTimeToo.setSeconds('00')
        newTimeToo.setMilliseconds('00')
        newTimeFrom = Math.round(newTimeFrom)
        newTimeToo = Math.round(newTimeToo)

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
            BookingEndTime = (parseFloat(this.value.split('-')[2])) - 1
            lastModified = (parseFloat(this.value.split('-')[3]))


        }
    });
}

function StatusOfDesks(LookFromTime, LookTooTime) {
    $.ajax({
        url: "https://stage-booking.intelligentdesk.com/booking?from=" + LookFromTime + "&until=" + LookTooTime + "&parent=5115225993379840",
        type: "GET",
        headers: getHeaders(),
        contentType: 'application/vnd.idesk-v5+json',
        success: function (data) {
            alert('Yaaay!');
            console.log(data)
            listOfBookings = data
            desks()
        },

        error: function (data) {
            alert(JSON.parse(data.responseText).Message);
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
            alert('Success!');

            var parentdivRoom1 = document.getElementById('2440')
            $(parentdivRoom1).empty()

            var parentdivRoom2 = document.getElementById('1330')
            $(parentdivRoom2).empty()

            for (var i = 0; i < data.length; i++) {
                var timeButtons = 5
                if (data[i].Type == 'DESK') {

                    var Desk = document.createElement("h3")
                    var DeskTitle = document.createTextNode(data[i].Name)
                    Desk.appendChild(DeskTitle)


                    if (data[i].Name.split(" ")[1] < 5) {

                        parentdivRoom1.appendChild(Desk)


                        for (var j = 0; j < timeButtons; j++) {
                            timeBooked.length = 0
                            ownerOfTimeBooked.length = 0
                            var span = document.createElement('span')
                            span.setAttribute("class", "button-checkbox")

                            var checkBox = document.createElement('input')
                            checkBox.setAttribute("class", "hidden")
                            checkBox.type = "checkbox"


                            var button = document.createElement('button')
                            button.setAttribute("class", "btn btn-primary btn-lg")
                            for (var k = 0; k < listOfBookings.length; k++) {
                                if (data[i].Zid == listOfBookings[k].Zid) {
                                    timeBooked.push(listOfBookings[k].From)
                                    ownerOfTimeBooked.push(listOfBookings[k].Owner)
                                }
                            }

                            button.type = "button"

                            if (j == 0) {
                                button.innerHTML = "08-10"

                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)

                                buttonTimeStart.setHours('08')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)



                                buttonTimeEnd.setHours('10')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)

                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else if (j == 1) {
                                button.innerHTML = ("10-12")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('10')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('12')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else if (j == 2) {
                                button.innerHTML = ("12-14")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('12')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('14')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else if (j == 3) {
                                button.innerHTML = ("14-16")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('14')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('16')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else {
                                button.innerHTML = ("16-18")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('16')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('18')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }

                            span.appendChild(checkBox)

                            parentdivRoom1.appendChild(span)

                        }

                    }
                    else {


                        parentdivRoom2.appendChild(Desk)

                        for (var j = 0; j < timeButtons; j++) {
                            timeBooked.length = 0
                            ownerOfTimeBooked.length = 0
                            var span = document.createElement('span')
                            span.setAttribute("class", "button-checkbox")


                            var checkBox = document.createElement('input')
                            checkBox.setAttribute("class", "hidden")
                            checkBox.type = "checkbox"

                            var button = document.createElement('button')
                            button.setAttribute("class", "btn btn-primary btn-lg")
                            for (var k = 0; k < listOfBookings.length; k++) {
                                if (data[i].Zid == listOfBookings[k].Zid) {
                                    timeBooked.push(listOfBookings[k].From)
                                    ownerOfTimeBooked.push(listOfBookings[k].Owner)
                                }
                            }
                            button.type = "button"

                            if (j == 0) {
                                button.innerHTML = "08-10"

                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)

                                buttonTimeStart.setHours('08')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)



                                buttonTimeEnd.setHours('10')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)

                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else if (j == 1) {
                                button.innerHTML = ("10-12")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('10')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('12')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else if (j == 2) {
                                button.innerHTML = ("12-14")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('12')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('14')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else if (j == 3) {
                                button.innerHTML = ("14-16")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('14')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('16')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }
                            else {
                                button.innerHTML = ("16-18")
                                var buttonTimeStart = new Date(document.getElementById('date').value)
                                var buttonTimeEnd = new Date(document.getElementById('date').value)
                                buttonTimeStart.setHours('16')
                                buttonTimeStart.setMinutes('00')
                                buttonTimeStart.setSeconds('00')
                                buttonTimeStart.setMilliseconds('00')
                                buttonTimeStart = Math.round(buttonTimeStart)

                                buttonTimeEnd.setHours('18')
                                buttonTimeEnd.setMinutes('00')
                                buttonTimeEnd.setSeconds('00')
                                buttonTimeEnd.setMilliseconds('00')
                                buttonTimeEnd = Math.round(buttonTimeEnd)


                                button.style.backgroundColor = getColorForButton(buttonTimeStart)

                                checkBox.setAttribute("value", data[i].Zid + "-" + buttonTimeStart + "-" + buttonTimeEnd + "-" + data[i].LastModified)
                                span.appendChild(button)
                            }

                            span.appendChild(checkBox)
                            parentdivRoom2.appendChild(span)
                        }
                    }

                }
            }
            checkboxsButton()
        },

        error: function (data) {
            alert(JSON.parse(data.responseText).Message);
        }
    });

}

function bookings() {
    console.log(JSON.stringify({
        "Owner": localStorage.getItem("user"),
        "Lastmodified": lastModified,
        "From": BookingStartTime,
        "Until": BookingEndTime,
        "Zids": listOfZid,
        "Subject": "bookingtest",
        "Private": "false"
    }))

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
            alert("Error");
            console.log(data.responseText);
        }
    });
}

function book() {
    $.ajax({
        url: "http://localhost:60156/api/bookingapi",
        type: "POST",
        headers: getHeaders,
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
            alert("Error");
            console.log(data.responseText);
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

function getColorForButton(startTime) {
    var indexOfBooking = timeBooked.indexOf(startTime);
    if (indexOfBooking >= 0) {
        if (ownerOfTimeBooked[indexOfBooking].toLowerCase() === localStorage.user.toLowerCase()) {
            return "blue"
        }
        else {
            return "red"
        }
    }
    else {
        return "green"
    }
}