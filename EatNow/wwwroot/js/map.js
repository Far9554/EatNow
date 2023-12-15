﻿let GridSizeX = 15;
let GridSizeY = 15;

let PincelBorrar = document.getElementById('Borrar');
let PincelSuelo = document.getElementById('Suelo');
let PincelMesa = document.getElementById('Mesa');

let container = document.getElementById('MapContainer');

class CasillaJS {
    constructor(x, y, numeroMesa, esAire, esMesa, estaOcupada, idRestaurante) {
        this.x = x;
        this.y = y;
        this.numeroMesa = numeroMesa;
        this.esAire = esAire;
        this.esMesa = esMesa;
        this.estaOcupada = estaOcupada;
        this.idRestaurante = idRestaurante;
    }
}

const casillas = [];

window.onload = CreateNewTable();
window.onload = GetCasillas();

function CreateNewTable() {
    const body = document.body;

    if (document.getElementById('mapa')) {
        document.getElementById('mapa').remove();
    }

    const tbl = document.createElement('table');
    tbl.id = "mapa";

    for (let i = 0; i < GridSizeX; i++) {
        const tr = tbl.insertRow();
        for (let j = 0; j < GridSizeY; j++) {
            const td = tr.insertCell();
            td.id = i + "-" + j;
            td.width = 630 / GridSizeY;
            td.height = 630 / GridSizeX;

            td.addEventListener('click', (event) => {
                if (event.button == 0)
                    ChangeState(td.id);
            });
            td.addEventListener('contextmenu', (ev) => {
                ev.preventDefault();
                ChangeZoneState(td.id);
            });

            let casilla = new CasillaJS(j, i, 0, false, false, 0);
            casillas.push(casilla);
        }
    }
    container.appendChild(tbl);
}

function SetTable(casillasRestaurante) {
    const body = document.body;

    document.getElementById('mapa').remove();

    const tbl = document.getElementById('mapa');

    casillasRestaurante.forEach(function (cR) {
        let cords = cR.Y + '-' + cR.X;
        console.log(cords);
        //const casilla = document.getElementById(cords);

        if (cR.EsMesa == false)
            SetState(1);
        else
            SetState(2);

        ChangeState(cords);
    });
}

let PencilDraw = 0;

function SetState(type) {
    PencilDraw = type;

    switch (type) {
        case 0:
            PincelBorrar.classList.add("Selected");
            PincelSuelo.classList.remove("Selected");
            PincelMesa.classList.remove("Selected");
            break;
        case 1:
            PincelBorrar.classList.remove("Selected");
            PincelSuelo.classList.add("Selected");
            PincelMesa.classList.remove("Selected");
            break;
        case 2:
            PincelBorrar.classList.remove("Selected");
            PincelSuelo.classList.remove("Selected");
            PincelMesa.classList.add("Selected");
            break;
    }
}
SetState(0);

function ChangeState(IdCasilla) {
    let Casilla = document.getElementById(IdCasilla);
    let cords = Casilla.id.split("-");
    let c = casillas.find(x => x.x == cords[1], y => y.y == cords[0]);

    if (PrimerCasilla != null) {
        PrimerCasilla.classList.remove("Selec");
        PrimerCasilla = null;
    }

    if (PencilDraw == 0) {
        Casilla.classList.remove("Aire");
        Casilla.classList.remove("Mesa");
        c.esAire = false;
        c.esMesa = false;
    }
    else if (PencilDraw == 1) {
        Casilla.classList.add("Aire");
        Casilla.classList.remove("Mesa");
        c.esAire = true;
        c.esMesa = false;
    }
    else if (PencilDraw == 2) {
        Casilla.classList.remove("Aire");
        Casilla.classList.add("Mesa");
        c.esAire = true;
        c.esMesa = true;
    }
}

let PrimerCasilla = null;
let SegundaCasilla = null;

function ChangeZoneState(IdCasilla) {
    if (PrimerCasilla == null) {
        IdCasillaA = IdCasilla;
        PrimerCasilla = document.getElementById(IdCasilla);
        PrimerCasilla.classList.add("Selec");
    }
    else {
        PrimerCasilla.classList.remove("Selec");
        SegundaCasilla = document.getElementById(IdCasilla);

        let cordsA = PrimerCasilla.id.split("-");
        let cordsB = SegundaCasilla.id.split("-");

        //Rellenar Casillas entre A y B
        let ABiggerX = parseInt(cordsA[0]) <= parseInt(cordsB[0]);
        let ABiggerY = parseInt(cordsA[1]) <= parseInt(cordsB[1]);

        for (let x = cordsA[0];
            ABiggerX ? x <= parseInt(cordsB[0]) : x >= parseInt(cordsB[0]);
            ABiggerX ? x++ : x--) {
            for (let y = cordsA[1];
                ABiggerY ? y <= parseInt(cordsB[1]) : y >= parseInt(cordsB[1]);
                ABiggerY ? y++ : y--) {
                let idSel = x + "-" + y;
                CasillaRellenar = document.getElementById(idSel);
                ChangeState(idSel);
            }
        }

        PrimerCasilla = null;
        SegundaCasilla = null;
    }
}

function GetCasillas() {
    $.ajax({
        type: "GET",
        url: "@Url.Action("GetCasillas", "Empleado")",
        dataType: "json",
        success: function (casillasJson) {
            // Convertir el JSON a una array de objetos CasillasJS
            var casillasArray = JSON.parse(casillasJson);

            console.log(casillasArray);

            SetTable(casillasArray);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function PushCasillas() {
    // Convertir el array a una cadena JSON
    let casillasG = casillas.filter(c => c.esAire == true);
    var jsonMiArray = JSON.stringify(casillasG);

    console.log(jsonMiArray);

    $.ajax({
        type: "POST",
        url: "@Url.Action("SaveCasillas", "Empleado")",
        data: { casillasJson: jsonMiArray },
        dataType: "json",
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(error);
        }
    });
}