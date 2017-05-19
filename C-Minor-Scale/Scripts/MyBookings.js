
$(document).ready(function () {
    updateBookingsTable();
})

function updateBookingsTable() {
    var from = moment().subtract(3, 'hours').valueOf();
    var until = moment().add(1, 'month').valueOf();
    var img = '<img src="/Images/garbage.png" height="25" width="25"/>';
    
    

    //$(table).find(tbody).append("<td>" + img + "</td>");

    $.ajax({
        url: "https://stage-booking.intelligentdesk.com/booking?username=" + localStorage.getItem("user") + "&from=" + from + "&until=" + until,
        type: "GET",
        headers: getHeaders(),
        contentType: 'application/vnd.idesk-v5+json',
        success: function (bookings) {
            bookings.sort(SortByStart)
            for (var i = 0; i < bookings.length; i++) {
                tdid = "booking" + i;
                $("<tr><td id=\"" + tdid + "\"></td><td>" + bookings[i].Subject + "</td><td>" + moment.utc(bookings[i].From).tz("Europe/Stockholm").format("YYYY-MM-DD HH:mm") + "</td><td>" + moment.utc(bookings[i].Until).tz("Europe/Stockholm").format("YYYY-MM-DD HH:mm") + "</td>"+"<td>"+img+"</td>").appendTo("#bookingsTbody");
                updateBookingsTableEntry(tdid, bookings[i].Zid);

            }
            $('#bookingsTable').DataTable();
        },
        error: function (data) {
            alert(JSON.parse(data.responseText).Message);
        }
    });
}

function SortByStart(a, b) {
    var aFrom = a.From;
    var bFrom = b.From;
    return ((aFrom < bFrom) ? -1 : ((aFrom > bFrom) ? 1 : 0));
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