const teacherId = 6570433172733952;
const studentId = 5124030458232832;

// Filter out all elements that is not of Type DESK
// Returns an array of desk objects
function filterDesks(data) {
    var desks = [];

    $.each(data, function (index, element) {
        if (element.Type === 'DESK') {
            desks.push(element);
        }
    })

    return desks;
}

// For sorting desks, compares on name
function compareDesks(desk1, desk2) {
    var desk1Number = desk1.Name.split(' ')[1];
    var desk2Number = desk2.Name.split(' ')[1];

    return desk1Number - desk2Number;
}

// Create a row of buttons for a desk
function createButtonRow(desk, date, bookings, username) {
    var deskDiv = document.createElement("div")
    var deskHeader = document.createElement("h3")
    deskHeader.appendChild(document.createTextNode(desk.Name));
    deskDiv.appendChild(deskHeader);

    for (var i = 8; i <= 16; i += 2) {
        var startTime = getButtonTime(date, i)
        var button = createCheckButton(desk, startTime, getColorForButton(desk.Zid, username, startTime, bookings));
        deskDiv.appendChild(button);
    }

    return deskDiv;
}

// Creates a span with a button and a checkbox for use on booking page
// Returns the span
function createCheckButton(desk, startTime, buttonColor) {
    var span = document.createElement('span')
    span.setAttribute("class", "button-checkbox")

    var checkBox = document.createElement('input')
    checkBox.setAttribute("class", "hidden")
    checkBox.type = "checkbox"
    checkBox.setAttribute("value", desk.Zid + "-" + startTime + "-" + (startTime + twoHoursInMS - 1) + "-" + desk.LastModified)

    var button = document.createElement('button')
    button.setAttribute("class", "btn btn-primary btn-lg")
    button.setAttribute("style", "margin-left: 2px")
    button.type = "button"
    button.style.backgroundColor = buttonColor;
    button.style.borderColor = buttonColor;
    var time = moment(startTime);
    button.innerHTML = time.format('HH') + "-" + time.add(2, 'hour').format('HH')
    
    span.appendChild(button)
    span.appendChild(checkBox)

    return span;
}

// Returns a millisecond epoch timestamp for the provided date and hour
function getButtonTime(dateString, hour) {
    return moment(dateString).set({ 'hour': hour, 'minute': 0, 'second': 0, 'millisecond': 0 }).valueOf();
}

function getColorForButton(deskId, userName, startTime, bookings) {
    for (var i = 0; i < bookings.length; ++i) {
        if (bookings[i].Zid === deskId && bookings[i].From == startTime) {
            if (bookings[i].Owner == userName) {
                return "blue"
            } else if (bookings[i].OwnerParent === studentId) {
                return "darkgoldenrod"
            } else if (bookings[i].OwnerParent === teacherId) {
                return "violet"
            } else {
                return "red"
            }
        }
    }

    return "green";
}