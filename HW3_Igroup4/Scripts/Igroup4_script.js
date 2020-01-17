
var allData = null;
var allAirports = [];
var allCitiesName = [];
//----------------------------------------------------------------------//

$(document).ready(function () {
    bringAllAirports();
    bringAllCitiesName();
    minDate();
    $('#myForm').submit(checkingCode);
    $('#myFlight').click(showFlights);
    $(document).on("click", ".btnAddFlight", addFlight);
    $(document).on("click", ".btnRemoveFlight", removeFlight);
    $('#specificConnection').click(function () {
        $('#connectionTextBox').toggle();
        if ($('#specificConnection').prop('checked')) {
            $('#connectionTextBox').attr('required', '');
        }
        else {
            $('#connectionTextBox').removeAttr('required');
        }
    })
    $('input[type=text]').keypress(function (e) {
        var key = e.keyCode;
        if (key >= 0 && key <= 64 || key >= 91 && key <= 96) {
            e.preventDefault();
        }
    })
    $(document).on('keypress', function (e) {
        if (e.which == 13) {
            $('#searchBtn').click();
        }
    });
})

//---------------------------------------------------------------------//


function bringAllAirports() {
    ajaxCall("GET", "../api/airports", "", bringAllAirportSsuccessCB, bringAllAirportErrorCB);
    loading();
}

function bringAllAirportSsuccessCB(data) {
    for (let i in data) {
        allAirports.push(data[i]);
    }
    $('#from').autocomplete({ source: allAirports });
    $('#to').autocomplete({ source: allAirports });
}

function bringAllAirportErrorCB(err) {
    console.log("error");

}

function bringAllCitiesName() {
    url = "https://api.skypicker.com/locations?type=dump&locale=en-US&location_types=airport&limit=4000&active_only=true&sort=name";
    $.get(url).done(successCB);
    $.get(url).fail(errorCB);
}

function successCB(data) {
    for (var i = 0; i < data.locations.length; i++) {
        if (allCitiesName.includes(data.locations[i].city.name)) {
        }
        else {
            allCitiesName.push(data.locations[i].city.name);
        }

    }
    $('#connectionTextBox').autocomplete({ source: allCitiesName });
}

function errorCB(err) {
    console.log(err);
}

function bringAllCitiesNameSsuccessCB(data) {
    for (let i in data) {
        allCitiesName.push(data[i]);
    }
    $('#connectionTextBox').autocomplete({ source: allCitiesName });

}

function bringAllCitiesNameErrorCB() {
    console.log("Bring all cities name errorCB");

}

function minDate() {
    var today = new Date();
    var tommorow = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    today = yyyy + '-' + mm + '-' + dd;
    tommorow = yyyy + '-' + mm + '-' + String(parseInt(dd) + 1);
    $('#depart').attr("min", today);
    $('#return').attr("min", tommorow);

}

function checkingCode(e) {
    e.preventDefault();
    if ($('#depart').val() > $('#return').val()) {
        alert('Return date is less than departure date');
        return false;
    }
    else {
        search();
    }
}

function search() {
    let origin = "";
    let destenation = "";
    let nameFrom = $('#from').val();
    let nameTo = $('#to').val();

    ajaxCall("GET", "../api/Airports/code/" + nameFrom.trim(), "", nameFromCodeSuccessCB, nameFromCodeErorrCB);

    function nameFromCodeSuccessCB(airportCode) {
        origin = airportCode;
        ajaxCall("GET", "../api/Airports/code/" + nameTo.trim(), "", nameToCodeSuccessCB, nameToCodeErorrCB);
    };
    function nameFromCodeErorrCB(e) { console.log(e); alert('make sure you choose the Origin from the name list') };
    function nameToCodeSuccessCB(airportCode) {
        destenation = airportCode;
        let tmpStartTime = $('#depart').val().split('-');
        let startTime = tmpStartTime[2] + "/" + tmpStartTime[1] + "/" + tmpStartTime[0];
        let tmpEndTime = $('#return').val().split('-');
        let endTime = tmpEndTime[2] + "/" + tmpEndTime[1] + "/" + tmpEndTime[0];
        let url = "https://api.skypicker.com/flights?flyFrom=" + origin + "&to=" + destenation + "&dateFrom=" + startTime + "&dateTo=" + endTime + "&partner=Ori";
        ajaxCall("GET", url, "", getFlightSuccessCB, getFlightErrorCB);

    };
    function nameToCodeErorrCB() { alert('make sure you choose the destantion from the name list') };
}

function getFlightSuccessCB(data) {
    console.log(data);
    $('#flightRes').html("");
    $('#myFlightRes').html("");
    allData = data.data;
    var result = "";
    var counter = 0;
    for (f in allData) {
        let price = allData[f].conversion.EUR;
        let time = new Date(allData[f].dTimeUTC * 1000);
        result += `<br />price: ${price} &#8364<br />Date of departure: ${time.toLocaleDateString()}<h3>Route:</h3> <br />`;
        for (r in allData[f].route) {
            result += `${allData[f].route[r].cityFrom}&nbsp;&#8594;&nbsp; ${allData[f].route[r].cityTo}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; by: ${allData[f].route[r].airline} airline<br /><br />`;
        }
        result += `<button id="flight-${counter}" data-flight-id="${counter}" class="btnAddFlight button1">Add flight</button><hr />`
        counter++;
    }
    $('#flightRes').append(result);
}

function getFlightErrorCB() {
    alert('Error , try againe')
    location.reload();
}

function showFlights() {
    if ($('#connectionTextBox').val() == "") {
        ajaxCall("GET", "../api/Flight", "", getShowFlightSuccessCB, getShowFlightErrorCB);
    }
    else {
        let stop = $('#connectionTextBox').val();
        ajaxCall("GET", "../api/Flight/Stop/" + stop, "", getShowFlightSuccessCB, getShowFlightErrorCB);
    }
}

function getShowFlightSuccessCB(data) {
    $('#flightRes').html("");
    $('#myFlightRes').html("");
    myListOfFlights = data;
    if (myListOfFlights.length == 0) { swal.fire("you dont have flight"); }
    var res = "";
    for (index in myListOfFlights) {
        flight = myListOfFlights[index];
        let Cities = flight.Road.split(',');
        let CityFrom = [];
        let CityTo = [];
        let Airlines = flight.Airlines.split(',');
        for (var j = 0; j < Cities.length; j += 2) {
            CityFrom.push(Cities[j]);
            CityTo.push(Cities[j + 1]);
        }
        let price = flight.Price;
        let tmp_date = flight.Date.split('-');
        let date = tmp_date[2] + "/" + tmp_date[1] + "/" + tmp_date[0];
        res += `<br />price: ${price} &#8364 <br />Date of departure: ${date}<h3>Route:</h3> <br />`;
        for (var i = 0; i < CityFrom.length; i++) {
            res += `${CityFrom[i]}&#8594; ${CityTo[i]}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; by: ${Airlines[i]} airline<br /><br />`;
        }
        res += `<input type="button" data-remove-id=${myListOfFlights[index].Id}  class="btnRemoveFlight" value="remove"/>`
        res += `<hr />`
    }

    $('#myFlightRes').append(res);
}

function getShowFlightErrorCB() {
    alert('Error , try againe')
}

function addFlight() {
    $(this).hide();
    let selectedItem = $(this).data('flight-id');
    let flight = allData[selectedItem];
    let id = flight.id;
    let origin = flight.flyFrom;
    let destination = flight.flyTo;
    let price = flight.conversion.EUR;
    let dateTime = new Date(flight.dTimeUTC * 1000);
    let cityFrom = [];
    let cityTo = [];
    let airlines = [];
    let road = "";
    for (r in flight.route) {
        cityFrom.push(flight.route[r].cityFrom);
        cityTo.push(flight.route[r].cityTo);
        airlines.push(flight.route[r].airline);
    }
    for (k in flight.route) {
        if (k < flight.route.length - 1) {
            road += flight.route[k].cityFrom + ',' + flight.route[k].cityTo + ',';
        }
        else {
            road += flight.route[k].cityFrom + ',' + flight.route[k].cityTo;
        }
    }

    road = road.toString();
    airlines = airlines.toString();
    let dd = dateTime.getDate();
    if (dd < 10) {
        dd = '0' + dd;
    }
    let mm = dateTime.getMonth() + 1
    if (mm < 10) {
        mm = '0' + mm;
    }
    let yyyy = dateTime.getFullYear()
    let date = `${yyyy}-${mm}-${dd}`;
    FlightReady = {
        "Id": id,
        "Origin": origin,
        "Destination": destination,
        "Price": price,
        "Date": date,
        "Road": road,
        "Airlines": airlines
    };
    ajaxCall("POST", "../api/Flight", JSON.stringify(FlightReady), postFlightSuccessCB, postFlightErrorCB)
}

function removeFlight() {
    $(this).hide();
    let selectedItem = $(this).data('remove-id').toString();
    ajaxCall("DELETE", "../api/Flight", JSON.stringify(selectedItem), FlightRemovedSuccessCB, FlightRemovedErorrCB)
}

function FlightRemovedSuccessCB() {
    $('#myFlight').click();
    swal.fire("Flight removed")
}

function FlightRemovedErorrCB() {
    swal.fire("The flight was not removed")
}

function postFlightSuccessCB(numEffected) {
    if (numEffected > 0) {
        console.log("post success")
        Swal.fire('post sucess');
    }
    else {
        postFlightErrorCB();
    }
}

function postFlightErrorCB() {
    console.log("error");
    /*Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Something went wrong!',
    })*/
}

