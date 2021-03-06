﻿
$(document).ready(function () {
    updateBookingsTable();
})

function updateBookingsTable() {
    var from = moment().subtract(3, 'hours').valueOf();
    var until = moment().add(1, 'month').valueOf();
    var img = '<img src="/Images/garbage.png" height="25" width="25"/>';

    $.ajax({
        url: "https://stage-booking.intelligentdesk.com/booking?username=" + localStorage.getItem("user") + "&from=" + from + "&until=" + until,
        type: "GET",
        headers: getHeaders(),
        contentType: 'application/vnd.idesk-v5+json',
        success: function (bookings) {
            bookings.sort(SortByStart)
            for (var i = 0; i < bookings.length; i++) {
                tdid = "booking" + i;
                $("<tr><td id=\"" + tdid + "\"></td><td>" + bookings[i].Subject + "</td><td>" + moment.utc(bookings[i].From).tz("Europe/Stockholm").format("YYYY-MM-DD HH:mm") + "</td><td>" + moment.utc(bookings[i].Until).tz("Europe/Stockholm").format("YYYY-MM-DD HH:mm") + "</td>" + "<td id=\"" + bookings[i].Bid + "\">" + img + "</td>").appendTo("#bookingsTbody");
                $("#" + bookings[i].Bid).click(deleteBooking)
                updateBookingsTableEntry(tdid, bookings[i].Zid);
            }
        },
        error: function (data) {
            alert(JSON.parse(data.responseText).Message);
        }
    });
}

function updateBookingsTableEntry(tdid, zid) {
    $.ajax({
        url: "https://stage-core.intelligentdesk.com/zone/" + zid,
        type: "GET",
        headers: getHeaders(),
        contentType: 'application/vnd.idesk-v5+json',
        success: function (zoneInfo) {
            $("#" + tdid).replaceWith("<td>" + zoneInfo.Name + "</td>");
        },
        error: function (data) {
            alert(JSON.parse(data.responseText).Message);
        }
    });
}

function deleteBooking(event) {
    var bid = event.currentTarget.id;
    $.ajax({
        url: "https://stage-booking.intelligentdesk.com/booking/" + bid,
        type: "DELETE",
        headers: getHeaders(),
        contentType: 'application/vnd.idesk-v5+json',
        success: function () {
            event.currentTarget.parentElement.remove();
        },
        error: function (data) {
            alert("Could not delete booking");
        }
    });
}