/**
 * @license
 * Copyright 2019 Google LLC. All Rights Reserved.
 * SPDX-License-Identifier: Apache-2.0
 */
let map;
const hanoi = { lat: 21.0285, lng: 105.8542 };

/**
 * Creates a control that recenters the map on Hanoi.
 */
function createCenterControl(map) {
    const controlButton = document.createElement("button");

    // Set CSS for the control.
    controlButton.classList.add('buttonStyle');

    controlButton.textContent = "Center Map";
    controlButton.title = "Click to recenter the map";
    controlButton.type = "button";
    // Setup the click event listeners: simply set the map to Hanoi.
    controlButton.addEventListener("click", () => {
        map.setCenter(hanoi);
    });
    return controlButton;
}

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10, // Có thể điều chỉnh độ phóng to cho phù hợp
        center: hanoi, // Đặt trung tâm là Hà Nội
    });

    var georssLayer = new google.maps.KmlLayer({
        url: "http://api.flickr.com/services/feeds/geo/?g=322338@N20&lang=en-us&format=feed-georss",
    });
    georssLayer.setMap(map);

    // Create the DIV to hold the control.
    const centerControlDiv = document.createElement("div");
    // Create the control.
    const centerControl = createCenterControl(map);

    // Append the control to the DIV.
    centerControlDiv.appendChild(centerControl);
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(
        centerControlDiv
    );
}

window.initMap = initMap;
