//API recognises in 2D array elements as 1 is normal place; element 0 is an obstacle; element -1 is meta; element 2 is start
const url = "https://gameoflifeproject.azurewebsites.net";
let buttonList = [];


function validateInput(xSize, ySize) {
    if (xSize <= 200 && xSize > 2 && ySize <= 200 && ySize > 2) return true;
    else return false;
}

//Create Table based on html input
function generateTable() {

    //reset table
    if (document.getElementById("map").hasChildNodes()) {
        const prevTables = document.getElementById("map").childNodes;
        document.getElementById("map").removeChild(prevTables[0]);
        buttonList = []
    }

    //get data from the form
    const xSize = document.getElementById("X").value;
    const ySize = document.getElementById("Y").value;

    if (!validateInput(xSize, ySize)) {
        userInfo("Map size must be grater than 2 and smaler than 200")
        return;
    }

    // creates a <table> element and a <tbody> element
    const tbl = document.createElement("table");
    const tblBody = document.createElement("tbody");

    // creating all cells
    for (let i = 0; i < xSize; i++) {
        // creates a table row
        const row = document.createElement("tr");
        let buttonListRow = [];
        for (let j = 0; j < ySize; j++) {
            // Create a <td> element and a text node, make the text
            // node the contents of the <td>, and put the <td> at
            // the end of the table row
            const cell = document.createElement("td");
            const button = document.createElement("button");
            button.classList.add("table-button");
            button.style.background = "purple";
            button.onclick = function (button) { OnClickAction(button) };
            button.val = false;
            buttonListRow.push(button);
            cell.appendChild(button);
            row.appendChild(cell);
        }

        buttonList.push(buttonListRow);
        // add the row to the end of the table body
        tblBody.appendChild(row);
    }

    // put the <tbody> in the <table>
    tbl.appendChild(tblBody);
    tbl.setAttribute('id', "table-map");
    // appends <table> into <body>
    document.getElementById("map").appendChild(tbl);
    userInfo("Click Generate Next Iteration")

}

// define behaovr of a button in table
function OnClickAction(button) {

    const thisButton = button.path[0];

    thisButton.val = !thisButton.val;
    // change color based on value
    thisButton.style.backgroundColor = thisButton.val ? "grey" : "purple";
}

// take value from button list
function GenerateUploadData(tableOfButtons) {

    let uploadData = [];

    tableOfButtons.forEach(row => {
        let element = row.map((element) => element.val);
        uploadData.push(element);
    });
    return uploadData;
}

// send json data from table
function send() {

    let uploadDataJSON = JSON.stringify({
        data: GenerateUploadData(buttonList)
    })

    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: uploadDataJSON,
    })
        .then(response => response.json())
        .then(value => changevalues(value))
        .catch((err) => {
            userInfo(err)
        });

    userInfo("Click Next Step To Generate Next Step");

}


function changevalues(value) {
    for (let i = 0; i < value.length; i++) {
        for (let j = 0; j < value[0].length; j++) {
            buttonList[i][j].val = value[i][j] == 0 ? false : true;
            buttonList[i][j].style.backgroundColor = buttonList[i][j].val ? "grey" : "purple";
        }
    }
}

function userInfo(text) {
    document.getElementById("info").innerHTML = "Game of Life: " + text;
}