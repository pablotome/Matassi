$(document).ready(function () {

	//Solo permitimos números, sin signos ni la e
	/*document.querySelector(".soloNumeros").addEventListener("keypress", function (evt) {
		if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) {
			evt.preventDefault();
		}
	});*/
	$(".soloNumeros").keypress(function (evt) {
		if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) {
			evt.preventDefault();
		}
	});

	$("#btnBuscarLicitaciones").click(function () {
		var grupo = $("#txtGrupo").val();
		$.getJSON("/API/Matassi/Ofertas/" + grupo, function (result) {

			$("#divOfertas").empty();

			for (var i = 0; i < result.length; i++) {
				var tblOferta = $('<table id="tblGanadores"><tr class="Encabezado"><th colspan="5">' + String(result[i].Fecha) + '</th></tr><tr class="Encabezado"><th>Fecha</th><th>Grupo</th><th>Modelo</th><th>Total Ajustado</th><th>Total Licitado</th></tr></table>');

				for (var o = 0; o < result[i].Ofertas.length; o++) {
					var row = $("<tr />")
					row.append($("<td>" + String(result[i].Ofertas[o].Fecha) + "</td>"));
					row.append($("<td>" + result[i].Ofertas[o].Grupo + "</td>"));
					row.append($("<td>" + result[i].Ofertas[o].Modelo + "</td>"));
					row.append($("<td>" + result[i].Ofertas[o].TotalAjustado + "</td>"));
					row.append($("<td>$ " + result[i].Ofertas[o].TotalLicitado + "</td>"));
					$(tblOferta).append(row);
				}

				$("#divOfertas").append(tblOferta);
				$("#divOfertas").append('<br/>');

			}


			


			/*for (var i = 0; i < result.length; i++) {
				var row = $("<tr />")
				row.append($("<td>" + result[i].Grupo + " -" + result[i].Orden + "</td>"));
				row.append($("<td>" + result[i].Nombre + "</td>"));
				row.append($("<td>$ " + result[i].Tipo + "</td>"));
				row.append($("<td>$ " + result[i].Monto + "</td>"));
				$("#tblGanadores").append(row); //this will append tr element to table... keep its reference for a while since we will add cels into it
			}*/
		});
	});

	$("#btnBuscarEmisiones").click(function () {
		var grupo = $("#txtGrupo").val();
		var orden = $("#txtOrden").val();
		$.getJSON("/API/Matassi/Emisiones/" + grupo + "?ord=" + orden, function (result) {

			$("#divEmisiones").empty();

			for (var i = 0; i < result.length; i++) {

				var tblOferta = $('<table id="tblEmision"><tbody><tr class="Encabezado"><th	colspan="4">APELLIDO Y NOMBRE DEL CLIENTE: ' + result[i].Nombre + '</th></tr><tr><td><b>VENCIMIENTO:</b></td><td><b>' + result[i].Vencimiento + '</b></td><td colspan="2"></td></tr><tr><td>GRUPO:</td><td>' + result[i].Grupo + '</td><td>ALICUOTA:</td><td>$ ' + result[i].Alicuota + '</td></tr><tr><td>ORDEN:</td><td>' + result[i].Orden + '</td><td>CARGOS ADM.:</td><td>$ ' + result[i].CargosAdm + '</td></tr><tr><td>DESVIO:</td><td>' + result[i].Desvio + '</td><td>ACT.ALICUOTA:</td><td>$ ' + result[i].ActAlicuota + '</td></tr><tr><td>CUOTA NRO:</td><td>' + result[i].NroCuota + '</td><td>CARGOS ACT.ALIC.:</td><td>$ ' + result[i].CargosActAlic + '</td></tr><tr><td>SEG.BIEN:</td><td>$ ' + result[i].SegBien + '</td><td>SEG.VIDA:</td><td>$ ' + result[i].SegVida + '</td></tr><tr><td>CUOTAS PLAN:</td><td>' + result[i].CuotasPlan + '</td><td>MORA:</td><td>$ ' + result[i].Mora + '</td></tr><tr><td>MODELO PLAN:</td><td>' + result[i].Modelo + '</td><td>DEB/CRED:</td><td>$ ' + result[i].DebCred + '</td></tr><tr><td>BANCO:</td><td>' + result[i].Banco + '</td><td>INT.LIQUIDACION:</td><td>$ ' + result[i].IntLiq + '</td></tr><tr><td>SUCURSAL:</td><td>' + result[i].Sucursal + '</td><td>OTROS:</td><td>$ ' + result[i].Otros + '</td></tr><tr><td>CUENTA:</td><td>' + result[i].Cuenta + '</td><td><b>***** TOTAL:</b></td><td><b>$ ' + result[i].Total + '</b></td></tr></tbody></table>');
				$("#divEmisiones").append(tblOferta);
				$("#divEmisiones").append('<br/>');

			}
		});
	});
});

function CargarDatosGanadores(){
	$("#divScreen").show();

	$.getJSON("/API/Matassi/MesAnioGanadores", function (result) {
		/*$.each(result, function(i, field){
			$("#spnMes").append(field + " ");
		});*/
		$("#spnMes").append(result.Mes)
		$("#spnAnio").append(result.Anio)
	});

	$.getJSON("/API/Matassi/Ganadores", function (result) {

		$("#tblGanadores").find("tr:gt(0)").remove();
		for (var i = 0; i < result.length; i++) {
			var row = $("<tr />")
			row.append($("<td>" + result[i].Grupo + " -" + result[i].Orden + "</td>"));
			row.append($("<td>" + result[i].Nombre + "</td>"));
			row.append($("<td>" + result[i].Tipo + "</td>"));
			row.append($("<td>$ " + result[i].Monto + "</td>"));
			$("#tblGanadores").append(row); //this will append tr element to table... keep its reference for a while since we will add cels into it
		}

		$("#divScreen").hide();
	});
}

function CargarPlanes() {
	
	$("#divScreen").show();

	$("#divPlanesAutoahorro").empty();

	$.getJSON("/API/Matassi/Planes", function (result) {
		for (var i = 0; i < result.length; i++) {

			var tblPlan = $('<li class="divPlanAutoAhorro"><div class="divTituloPlanAutoAhorro">' + result[i].Titulo + '</div><div class="divImagenPlanAutoAhorro"><img src="/Autoahorro/VerImagenPlanAutoahorro?codPlanAutoahorro=' + result[i].CodPlanAutoahorro + '"></div><div class="divSubTituloPlanAutoAhorro">' + result[i].Subtitulo + '</div><div class="divCaracteristicasPlanAutoAhorro"><ul></ul></div></li>');

			for (var j = 0; j < result[i].Cuotas.length; j++) {

				var liCuota = $('<li>' + result[i].Cuotas[j].RangoCuota + ': ' + result[i].Cuotas[j].Valor + '</li>');

				$(tblPlan).find('ul').append(liCuota);
			}

			$("#divPlanesAutoahorro").append(tblPlan);
		}

		$("#divScreen").hide();
	});
}